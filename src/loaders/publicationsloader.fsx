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
    PubType   : string
    Publisher : string
    Title     : string
    Year      : int
    Authors   : string
    DOI       : string
    Featured  : bool
    OpenAccess: bool
} with
    static member createElement (pub:Publication) =
        div [Class "Container box p-4 is-rounded"] [
            h4 [Class "title is-4"] [!! pub.Title]
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
                span [Class "tag"] [!!pub.Publisher]
                span [Class "tag"] [!!pub.PubType]
                span [Class "tag"] [
                    let doi, link = 
                        match pub.DOI.Split("/", System.StringSplitOptions.RemoveEmptyEntries) with
                        | [|"https:"; "doi.org"; prefix; suffix|] -> $"{prefix}/{suffix}", $"https://doi.org/{prefix}/{suffix}"
                        | [|"doi.org"; prefix; suffix|] -> $"{prefix}/{suffix}", $"https://doi.org/{prefix}/{suffix}"
                        | [|"https:"; "doi.org"; unprefixed|] -> $"{unprefixed}", $"https://doi.org/{unprefixed}"
                        | [|"doi.org"; unprefixed|] -> $"{unprefixed}", $"https://doi.org/{unprefixed}"
                        | [|prefix; suffix|] -> $"{prefix}/{suffix}", $"https://doi.org/{prefix}/{suffix}"
                        | [|unprefixed|] -> $"{unprefixed}", $"https://doi.org/{unprefixed}"
                        | _ -> pub.DOI, $"https://doi.org/{pub.DOI}"
                    a [Href link] [!!doi]
                ]
            ]
        ]
    static member tryParseFromBibTexString (featured: bool) (bibtex:string) =
        let parsed = parseBibtexStringDirty bibtex
        let pubType = Map.tryFind "type" parsed
        let title = Map.tryFind "title" parsed
        let year = Map.tryFind "year" parsed
        let authors = Map.tryFind "author" parsed
        let publisher = Map.tryFind "journal" parsed
        let doi = Map.tryFind "doi" parsed
        let note = Map.tryFind "note" parsed

        match pubType, title, year, authors, publisher, doi with
        | Some pubType, Some title, Some year, Some authors, Some publisher, Some doi ->
            Some {
                PubType = pubType
                Publisher = publisher
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
                Featured = featured
                OpenAccess = 
                    match note with
                    | Some note -> note = "open access"
                    | _ -> false
            }
        | _ -> 
            printfn $"title: {title}, year: {year}, authors: {authors}, publisher: {publisher}, doi: {doi}"
            None
let contentDir = "content/publications/"

let loadFile (featured:bool) (bibFile: string) =    
    bibFile
    |> File.ReadAllText
    |> Publication.tryParseFromBibTexString featured

let loader (projectRoot: string) (siteContent: SiteContents) =
    let postsPath = Path.Combine(projectRoot, contentDir)
    let options = EnumerationOptions(RecurseSubdirectories = false)
    let files = Directory.GetFiles(postsPath, "*", options)
    files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.iter (fun path ->
        printfn "[Publications-Loader]: Adding publication at %s" path
        let p = loadFile false path
        match p with
        | Some p -> siteContent.Add(p)
        | None -> printfn $"[Publications-Loader]: Failed to parse publication at {path}"
    )
    
    let files = Directory.GetFiles(postsPath+"/featured/", "*", options)
    files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.iter (fun path ->
        printfn "[Publications-Loader]: Adding featured publication at %s" path
        let p = loadFile true path
        match p with
        | Some p -> siteContent.Add(p)
        | None -> printfn $"[Publications-Loader]: Failed to parse publication at {path}"
    )
    printfn "[Publications-Loader]: Done loading publications items"
    siteContent
