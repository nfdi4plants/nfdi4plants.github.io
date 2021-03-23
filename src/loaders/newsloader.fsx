#load "../globals.fsx"

open Globals
open Html
open System.IO

type NewsItem = {
    Date: System.DateTime
    Title: string
    Body: string
    PreviewText: string
    File: string
    Link: string
} with
    static member create date title body preview file link =
        {
            Date = date
            Title = title
            Body = body
            PreviewText = preview
            File = file
            Link = link
        }
    static member fromFile (rootDir:string) (newsMarkdownPath:string) =
        
        let text = System.IO.File.ReadAllText newsMarkdownPath

        let config = MarkdownProcessing.getFrontMatter text
        let content = MarkdownProcessing.getMarkdownContent text

        let chopLength =
            if rootDir.EndsWith("\\") then rootDir.Length
            else rootDir.Length + 1

        let dirPart =
            newsMarkdownPath
            |> Path.GetDirectoryName
            |> fun x -> x.[rootDir.Length .. ]
            
        let preview = config |> Map.find "preview-text" |> MarkdownProcessing.trimString

        let file = Path.Combine(dirPart, (newsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".md").Replace("\\", "/")

        let link = "/" + Path.Combine(dirPart, (newsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".html").Replace("\\", "/")
            
        let title = config |> Map.find "title" |> MarkdownProcessing.trimString
        let date = config |> Map.find "date" |>  MarkdownProcessing.trimString |> System.DateTime.Parse
        let body = content

        NewsItem.create date title body preview file link

    static member createPreviewElement (showYear:bool) (newsItem:NewsItem) =
        div [Class "columns"] [
            div [Class "column is-2"] [
                h4 [Class "subtitle is-4"] [
                    if showYear then
                        !! (sprintf "%i %s %i" newsItem.Date.Day (newsItem.Date.Month |> News.getStringForMonth) newsItem.Date.Year)
                    else
                        !! (sprintf "%i %s" newsItem.Date.Day (newsItem.Date.Month |> News.getStringForMonth))
                ]
            ]
            div [Class "column is-10"] [
                h4 [Class "title is-4"] [!!newsItem.Title]
                !! newsItem.PreviewText
                div [Class "control mt-4"] [
                    a [Class "button is-small has-bg-darkblue-lighter-20"; Href newsItem.Link] [!!"Read more"]
                ]
            ]
        ]


let contentDir = "content/news"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let newsPath = System.IO.Path.Combine(projectRoot, contentDir)
    System.IO.Directory.GetFiles newsPath
    |> Array.filter (Predicates.isMarkdownFile)
    |> Array.iter (fun path ->
        try 
            printfn "[News-Loader]: Adding news item at %s" path
            siteContent.Add (NewsItem.fromFile projectRoot path)
        with e as exn -> 
            siteContent.AddError {Path = path; Message = (sprintf "Unable to load news item %s. \n%s" path e.Message); Phase = Loading}
    )
    printfn "[News-Loader]: Done loading news items"
    siteContent