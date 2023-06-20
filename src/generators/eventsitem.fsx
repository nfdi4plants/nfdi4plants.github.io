#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Html

let generate' (ctx : SiteContents) (page: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let events = ctx.TryGetValues<Eventsloader.EventsItem>() |> Option.defaultValue Seq.empty

    printfn "[EventsItem-Generator] mapping events site for %s" page

    let currentEventsItem = events |> Seq.tryFind (fun n -> n.File = page.Replace("_public",""))

    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    Layout.layout ctx "Events" [
        match currentEventsItem with
        | Some currentEventsItem -> 
            section [Class "hero is-medium is-dark has-bg-lightblue"] [
              div [Class "hero-body"] [
                    div [Class "container has-text-centered"] [
                        h1 [Class "title"] [!!desc]
                    ]
                ]
            ]
            section [Class "section"] [
                div [Class "container p-4"] [
                    h1 [Class "title is-1"] [!!currentEventsItem.Title]
                    h2 [Class "subtitle is-2"] [!! (sprintf "%i %s %i" currentEventsItem.Date.Day (currentEventsItem.Date.Month |> Events.getStringForMonth) currentEventsItem.Date.Year)]
                    div [Class "content"] [!!currentEventsItem.Body]
                ]
            ]
        | _ -> 
            printfn "[EventsItem-Generator] cannot find item %A in %A" page (events |> Seq.map (fun n -> n.File) |> String.concat "\r\n")
            div [] []
    ]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx

  