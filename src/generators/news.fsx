
#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Newsloader
open Html

let generate' (ctx : SiteContents) (page: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let news = 
        ctx.TryGetValues<Newsloader.NewsItem>() |> Option.defaultValue Seq.empty
        |> Seq.sortByDescending (fun n -> n.Date)

    Layout.layout ctx "News" [
        section [Class "hero is-medium has-bg-yellow"] [
          div [Class "hero-body"] [
                div [Class "container has-text-centered"] [
                    p [Class "title"] [!!"DataPLANT News"]
                ]
            ]
        ]
        section [Class "section"] (
            news

            |> Seq.map (fun newsItem ->
                div [Class "Container box p-4 is-rounded has-bg-yellow-lighter-90"] [NewsItem.createPreviewElement 75 newsItem]
            )
            |> List.ofSeq
        )
    ]


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx