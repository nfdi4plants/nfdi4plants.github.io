#load "../globals.fsx"
open Globals
open System.IO

type NewsItem = {
    Date: System.DateTime
    Title: string
    Body: string
    File: string
    Link: string
} with
    static member create date title body file link =
        {
            Date = date
            Title = title
            Body = body
            File = file
            Link = link
        }

let contentDir = "content/news"

let loadFile (rootDir:string) (newsMarkdownPath:string) =
    let text = System.IO.File.ReadAllText newsMarkdownPath

    let config = MarkdownProcessing.getFrontMatter text
    let content = MarkdownProcessing.getMarkdownContent text

    let chopLength =
        if rootDir.EndsWith("\\") then rootDir.Length
        else rootDir.Length + 1

    let dirPart =
        newsMarkdownPath
        |> Path.GetDirectoryName
        |> fun x -> x.[chopLength .. ]

    let file = Path.Combine(dirPart, (newsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".md").Replace("\\", "/")
    
    printfn "%s" file    
    let link = "/" + Path.Combine(dirPart, (newsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".html").Replace("\\", "/")
        
    let title = config |> Map.find "title" |> MarkdownProcessing.trimString
    let date = config |> Map.find "date" |>  MarkdownProcessing.trimString |> System.DateTime.Parse
    let body = content

    NewsItem.create date title body file link

let loader (projectRoot: string) (siteContent: SiteContents) =
    let newsPath = System.IO.Path.Combine(projectRoot, contentDir)
    System.IO.Directory.GetFiles newsPath
    |> Array.filter (Predicates.isMarkdownFile)
    |> Array.iter (fun path ->
        try 
            printfn "[News-Loader]: Adding news item at %s" path
            siteContent.Add (loadFile projectRoot path)
        with e as exn -> 
            siteContent.AddError {Path = path; Message = (sprintf "Uable to load news item %s. \n%s" path e.Message); Phase = Loading}
    )
    printfn "[News-Loader]: Done loading news items"
    siteContent