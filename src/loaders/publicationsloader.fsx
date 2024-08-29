#r "nuget: FSharpAux"
#r "../_lib/Fornax.Core.dll"

open FSharpAux
open System.Text.RegularExpressions
open System.IO

open Html

// adapted via https://github.com/Genometric/BibitemParser because their implementation does not let us access any untyped fields (we need "notes")
let parseBibtexStringDirty (bibtex:string) =
    let stopChars = [ '\r'; '\n'; '\t' ]

    let removeStopChars (s: string) =
        s
        |> Seq.filter (fun c -> not (stopChars |> Seq.contains c))
        |> Array.ofSeq
        |> String.fromCharArray


    let BibitemSplitRegex = @".*@(?<type>[^{]+){(?<id>[^,]*),(?<body>.+)}"
    let BibitemBodyAttributesRegex = @"(?<attribute>[^{}]*)\s*=\s*\{(?<value>(?:[^{}]|(?<open>\{)|(?<-open>\}))*(?(open)(?!)))\}(,|$)"

    let bibitem = removeStopChars(bibtex)
    let groups = Regex(BibitemSplitRegex).Match(bibitem).Groups

    let pubType = groups.Item("type").Value.Trim()

    Regex(BibitemBodyAttributesRegex).Matches(groups.Item("body").Value)
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Groups["attribute"].Value.Trim(), m.Groups["value"].Value.Trim())
    |> Seq.append [|"type", pubType|]
    |> Map.ofSeq


type Publication = {
    Title     : string
    Year      : int
    Authors   : string
    Featured  : bool
    OpenAccess: bool
    DOI       : string option
    URL       : string option
    PubType   : string option
    Publisher : string option
} with
    static member createElement (pub:Publication) =

        let link =
            match pub.URL, pub.DOI with
            | Some url, None -> Some url
            | _, Some doi -> 
                let link = 
                    match doi.Split("/", System.StringSplitOptions.RemoveEmptyEntries) with
                    | [|"https:"; "doi.org"; prefix; suffix|] -> $"https://doi.org/{prefix}/{suffix}"
                    | [|"doi.org"; prefix; suffix|] -> $"https://doi.org/{prefix}/{suffix}"
                    | [|"https:"; "doi.org"; unprefixed|] -> $"https://doi.org/{unprefixed}"
                    | [|"doi.org"; unprefixed|] -> $"https://doi.org/{unprefixed}"
                    | [|prefix; suffix|] -> $"https://doi.org/{prefix}/{suffix}"
                    | [|unprefixed|] -> $"https://doi.org/{unprefixed}"
                    | _ -> $"https://doi.org/{doi}"
                Some link
            | _ -> None

        div [Class "Container box p-4 is-rounded"] [
            h4 [Class "title is-4"] [ 
                if link.IsSome then a [Href link.Value] [!!pub.Title]
                else !!pub.Title
            ]
            h6 [Class "subtitle is-6"] [!!pub.Authors]
            div [Class "tags are-medium"] [
                if pub.OpenAccess then span [Class "tag is-success"] [
                    span [Class "icon-text"] [
                        span [Class "icon"] [
                            i [Class "fas fa-unlock"] []
                        ]
                        span [] [!!"Open Access"]
                    ]
                ]
                if pub.Publisher.IsSome then span [Class "tag"] [!!pub.Publisher.Value]
                if pub.PubType.IsSome then span [Class "tag"] [!!pub.PubType.Value]
                
            ]
        ]
    static member tryParseFromFile (featured: bool) (path: string) =
        let bibtex = File.ReadAllText path
        let parsed = parseBibtexStringDirty bibtex
        let pubType = Map.tryFind "type" parsed
        let title = Map.tryFind "title" parsed
        let year = Map.tryFind "year" parsed
        let authors = Map.tryFind "author" parsed
        let publisher = Map.tryFind "journal" parsed
        let doi = Map.tryFind "doi" parsed
        let url = Map.tryFind "url" parsed
        let note = Map.tryFind "note" parsed

        let printNone (o: Option<'T>) prop path = 
            match o with
            | None -> printfn $"[Publications-Loader]: {prop} is None for {path}"
            | Some _ -> ()

        match title, year, authors, doi, url with
        | _, _, _, None, None -> 
            printfn $"[Publications-Loader]: Both URL and DOI are None for {path}"
            None
        | Some title, Some year, Some authors, doi, url ->
            Some {
                PubType = if Option.contains "" pubType then None else pubType
                Publisher = if Option.contains "" publisher then None else publisher
                Title = title
                Year = int year
                Authors = 
                    authors.Split(" and ", System.StringSplitOptions.RemoveEmptyEntries) 
                    |> Array.map (fun a ->
                        if a.Contains(",") then 
                            match a.Split(",", System.StringSplitOptions.TrimEntries) with
                            | [|l; s; f|] -> 
                                let l, star = l.TrimEnd('*'), l.EndsWith("*")
                                $"""{l}{" " + s} {f.[0]}{if star then "*" else""}"""
                            | [|l; f|] ->
                                let l, star = l.TrimEnd('*'), l.EndsWith("*")
                                $"""{l} {f.[0]}{if star then "*" else""}"""
                            | _ -> a
                        else 
                            let splt = a.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)
                            match splt with
                            | [|f; l|] -> 
                                let l, star = l.TrimEnd('*'), l.EndsWith("*")
                                $"""{l} {f.[0]}{if star then "*" else""}"""
                            | _ -> a
                    )
                    |> String.concat ", "
                DOI = doi
                URL = url
                Featured = featured
                OpenAccess = 
                    match note with
                    | Some note -> note = "open access"
                    | _ -> false
            }
        | _ ->
            printNone title "title" path
            printNone year "year" path
            printNone authors "authors" path
            printNone doi "doi" path
            None
let contentDir = "content/publications/"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let postsPath = Path.Combine(projectRoot, contentDir)
    let options = EnumerationOptions(RecurseSubdirectories = false)
    let files = Directory.GetFiles(postsPath, "*", options)

    let mutable failed_pubs = 0
    let mutable failed_featured_pubs = 0

    files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.iter (fun path ->
        let p = Publication.tryParseFromFile false path
        match p with
        | Some p -> siteContent.Add(p)
        | None -> 
            printfn $"[Publications-Loader]: Failed to parse publication at {path}"
            failed_pubs <- failed_pubs + 1
    )
    
    let featured_files = Directory.GetFiles(postsPath+"/featured/", "*", options)
    featured_files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.iter (fun path ->
        let p = Publication.tryParseFromFile true path
        match p with
        | Some p -> siteContent.Add(p)
        | None -> 
            printfn $"[Publications-Loader]: Failed to parse publication at {path}"
            failed_featured_pubs <- failed_featured_pubs + 1
    )
    printfn $"[Publications-Loader]: {files.Length - failed_pubs}/{files.Length} publications loaded"
    printfn $"[Publications-Loader]: {featured_files.Length - failed_featured_pubs}/{featured_files.Length} featured publications loaded"
    printfn "[Publications-Loader]: Done loading publications items"
    siteContent