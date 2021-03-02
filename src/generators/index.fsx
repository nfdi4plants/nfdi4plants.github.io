#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Html

let createNewsItemPreview (newsItem:Newsloader.NewsItem) =
    div [Class "columns"] [
        div [Class "column is-2"] [
            h4 [Class "subtitle is-4"] [!! (sprintf "%i %s" newsItem.Date.Day (newsItem.Date.Month |> News.getStringForMonth))]
        ]
        div [Class "column is-10"] [
            h4 [Class "title is-4"] [!!newsItem.Title]
            newsItem.Body.Split(" ")
            |> fun n ->
                if n.Length > 50 then 
                    n |> Array.take 50
                    |> String.concat " "
                    |> sprintf "%s ..."
                else 
                    n
                    |> String.concat " "
            |> (!!)
            div [Class "control mt-4"] [
                a [Class "button is-small has-bg-darkblue-lighter-20"; Href newsItem.Link] [!!"Read more"]
            ]
        ]
    ]

let createLatestNewsPreview (newsItems: Newsloader.NewsItem []) =
    match newsItems.Length with
    | 0 -> []
    | 1 -> []
    | 2 -> []
    | _ ->
        let n1,n2,n3,n4 =
            newsItems
            |> Array.sortByDescending (fun n -> n.Date)
            |> fun n -> n.[0],n.[1],n.[2],n.[3]
        [
            div [Class "columns"] [
                div [Class "column"] [
                    n1 |> createNewsItemPreview
                ]
                div [Class "column"] [
                    n2 |> createNewsItemPreview
                ]
            ]
            div [Class "columns"] [
                div [Class "column"] [
                    n3 |> createNewsItemPreview
                ]
                div [Class "column"] [
                    n4 |> createNewsItemPreview
                ]
            ]
        ]
        

let generate' (ctx : SiteContents) (_: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let news = ctx.TryGetValues<Newsloader.NewsItem>() |> Option.defaultValue Seq.empty
    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    Layout.layout ctx "Home" [
        section [Class "hero is-medium is-dark has-bg-lightblue"] [
          div [Class "hero-body"] [
                div [Class "container has-text-centered"] [
                    h1 [Class "title"] [!!desc]
                ]
            ]
        ]
        section [Class "section"] [
            div [Class "Container p-4 has-bg-yellow"] [ 
                h1 [Class "title"] [!!"News"]
                yield! 
                    news
                    |> Array.ofSeq
                    |> createLatestNewsPreview
            ]
        ]
    ]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx