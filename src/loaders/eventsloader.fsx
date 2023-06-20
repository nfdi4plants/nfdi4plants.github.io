#load "../globals.fsx"

open Globals
open Html
open System.IO

type EventsItem = {
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
    static member fromFile (rootDir:string) (eventsMarkdownPath:string) =
        
        let text = System.IO.File.ReadAllText eventsMarkdownPath

        let config = MarkdownProcessing.getFrontMatter text
        let content = MarkdownProcessing.getMarkdownContent text

        let chopLength =
            if rootDir.EndsWith("\\") then rootDir.Length
            else rootDir.Length + 1

        let dirPart =
            eventsMarkdownPath
            |> Path.GetDirectoryName
            |> fun x -> x.[rootDir.Length .. ]
            
        let preview = config |> Map.find "preview-text" |> MarkdownProcessing.trimString

        let file = Path.Combine(dirPart, (eventsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".md").Replace("\\", "/")

        let link = "/" + Path.Combine(dirPart, (eventsMarkdownPath |> Path.GetFileNameWithoutExtension) + ".html").Replace("\\", "/")
            
        let title = config |> Map.find "title" |> MarkdownProcessing.trimString
        let date = config |> Map.find "date" |>  MarkdownProcessing.trimString |> fun s -> System.DateTime.ParseExact(s,"yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)
        let body = content

        EventsItem.create date title body preview file link

    static member createPreviewElement (showYear:bool) (eventsItem:EventsItem) =
        div [Class "columns"] [
            div [Class "column is-2"] [
                h4 [Class "subtitle is-4"] [
                    if showYear then
                        !! (sprintf "%i %s %i" eventsItem.Date.Day (eventsItem.Date.Month |> Events.getStringForMonth) eventsItem.Date.Year)
                    else
                        !! (sprintf "%i %s" eventsItem.Date.Day (eventsItem.Date.Month |> Events.getStringForMonth))
                ]
            ]
            div [Class "column is-10"] [
                h4 [Class "title is-4"] [!!eventsItem.Title]
                !! eventsItem.PreviewText
                div [Class "control mt-4"] [
                    a [Class "button is-small has-bg-darkblue-lighter-20"; Href eventsItem.Link] [!!"Read more"]
                ]
            ]
        ]


let contentDir = "content/events"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let eventsPath = System.IO.Path.Combine(projectRoot, contentDir)
    System.IO.Directory.GetFiles eventsPath
    |> Array.filter (Predicates.isMarkdownFile)
    |> Array.iter (fun path ->
        try 
            printfn "[Events-Loader]: Adding events item at %s" path
            siteContent.Add (EventsItem.fromFile projectRoot path)
        with e as exn -> 
            siteContent.AddError {Path = path; Message = (sprintf "Unable to load events item %s. \n%s" path e.Message); Phase = Loading}
    )
    printfn "[Events-Loader]: Done loading events items"
    siteContent