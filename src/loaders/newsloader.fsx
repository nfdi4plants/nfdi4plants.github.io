#load "../globals.fsx"
open Globals

type NewsItem = {
    Date: System.DateTime
    Title: string
    Body: string
} with
    static member create date title body =
        {
            Date = date
            Title = title
            Body = body
        }

let contentDir = "../content/news"

let loadFile (newsMarkdownPath:string) =
    let text = System.IO.File.ReadAllText newsMarkdownPath

    let config = MarkdownProcessing.getFrontMatter text
    let content = MarkdownProcessing.getMarkdownContent text

    let title = config |> Map.find "title" |> MarkdownProcessing.trimString
    let date = config |> Map.find "date" |>  MarkdownProcessing.trimString |> System.DateTime.Parse
    let body = content

    NewsItem.create date title body

let loader (projectRoot: string) (siteContent: SiteContents) =
    let newsPath = System.IO.Path.Combine(projectRoot, contentDir)
    printfn "%A" newsPath
    System.IO.Directory.GetFiles newsPath
    |> Array.filter (Predicates.isMarkdownFile)
    |> Array.iter (fun path ->
        try 
            printfn "[News-Loader]: Adding news item at %s" path
            siteContent.Add (loadFile path)
        with e as exn -> 
            siteContent.AddError {Path = path; Message = (sprintf "Uable to load news item %s. \n%s" path e.Message); Phase = Loading}
    )
    printfn "[News-Loader]: Done loading news items"
    siteContent