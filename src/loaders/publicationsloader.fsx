#r "nuget: Genometric.BibitemParser, 2.5.0"
#r "../_lib/Fornax.Core.dll"

open System.IO
open Genometric
open Html

type Publication = {
    Publisher : string
    Title     : string
    Year      : int
    Authors   : string
    DOI       : string
    Featured  : bool
} with
    static member createElement (pub:Publication) =
        div [Class "Container box p-4 is-rounded"] [
            h3 [Class "title is-3"] [!! pub.Title]
            h5 [Class "subtitle is-6"] [!!pub.Authors]
            div [Class "tags are-medium"] [
                span [Class "tag"] [!!pub.Publisher]
                span [Class "tag"] [!!pub.DOI]
            ]
        ]

let contentDir = "content/publications/"

let tryParse (parser:BibitemParser.Parser) (bibFileNamne:string) =
    let text = File.ReadAllText bibFileNamne
    let tryParseBibTex (item:string) =
        match (parser.TryParse(item)) with
        | true,result -> Some result
        | false,_ -> None 

    tryParseBibTex text


let toPublication (featured:bool) (item:BibitemParser.Model.Publication) =
    let formatAuthor (firstName:string) (lastName:string) =
        let fnLetter = if firstName.Length > 0 then firstName.Substring(0,1) else ""
        let star,lastName' = 
            if lastName.EndsWith("*") then
                "*", lastName.Substring(0,lastName.Length-1)
            else
                "",lastName 
        sprintf "%s %s%s" lastName' fnLetter star

    let year = if item.Year.HasValue then item.Year.Value else 3000
    let tmp = 
        item.Authors
        |> Seq.map (fun a -> formatAuthor a.FirstName a.LastName)
        |> String.concat ", "

    {
        Publisher = item.Journal
        Title     = item.Title
        Year      = year
        Authors   = tmp
        Featured  = featured
        DOI       = item.DOI
    }


let loadFile (featured:bool) (bibFile: string) =    
    let parser = new BibitemParser.Parser()
    let tmp = tryParse parser bibFile
    toPublication featured tmp.Value

let loader (projectRoot: string) (siteContent: SiteContents) =
    let postsPath = Path.Combine(projectRoot, contentDir)
    let options = EnumerationOptions(RecurseSubdirectories = false)
    let files = Directory.GetFiles(postsPath, "*", options)
    files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.map (loadFile false)
    |> Array.iter siteContent.Add
    
    let files = Directory.GetFiles(postsPath+"/featured/", "*", options)
    files
    |> Array.filter (fun n -> n.EndsWith ".bib")
    |> Array.map (loadFile true)
    |> Array.iter siteContent.Add

    siteContent
