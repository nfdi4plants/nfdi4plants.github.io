
#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Eventsloader
open Html

let generate' (ctx : SiteContents) (page: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let events = 
        ctx.TryGetValues<Eventsloader.EventsItem>() |> Option.defaultValue Seq.empty
        |> Seq.sortByDescending (fun n -> n.Date)

    Layout.layout ctx "Events" [
        section [Class "hero is-medium has-bg-yellow"] [
          div [Class "hero-body"] [
                div [Class "container has-text-centered"] [
                    p [Class "title"] [!!"Events"]
                ]
            ]
        ]
        section [Class "section"] (
            events

            |> Seq.map (fun eventsItem ->
                div [Class "Container box p-4 is-rounded"] [EventsItem.createPreviewElement true eventsItem]
            )
            |> List.ofSeq
        )
    ]


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx