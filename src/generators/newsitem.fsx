#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Html

let generate' (ctx : SiteContents) (page: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let news = ctx.TryGetValues<Newsloader.NewsItem>() |> Option.defaultValue Seq.empty

    printfn "[NewsItem-Generator] mapping news site for %s" page

    let currentNewsItem = news |> Seq.tryFind (fun n -> n.File = page.Replace("_public",""))

    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    Layout.layout ctx "Home" [
        match currentNewsItem with
        | Some currentNewsItem -> 
            section [Class "hero is-medium is-dark has-bg-lightblue"] [
              div [Class "hero-body"] [
                    div [Class "container has-text-centered"] [
                        h1 [Class "title"] [!!desc]
                    ]
                ]
            ]
            section [Class "section"] [
                div [Class "container p-4"] [
                    h1 [Class "title is-1"] [!!currentNewsItem.Title]
                    h2 [Class "subtitle is-2"] [!! (sprintf "%i %s %i" currentNewsItem.Date.Day (currentNewsItem.Date.Month |> News.getStringForMonth) currentNewsItem.Date.Year)]
                    div [Class "content"] [!!currentNewsItem.Body]
                ]
            ]
        | _ -> 
            printfn "[NewsItem-Generator] cannot find item %A in %A" page (news |> Seq.map (fun n -> n.File) |> String.concat "\r\n")
            div [] []
    ]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx