#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Globals.HTMLComponents
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

let createMainPageCard (mainPageCard: Mainpagecardloader.MainPageCard) = 

    let containerBG = Colors.hasBgColor mainPageCard.ContainerBg
    let cardBG = Colors.hasBgColor mainPageCard.CardBg
    let emphasisColor = Colors.isColor mainPageCard.EmphasisColor

    section [Class "section"] [
        div [Class (sprintf "Container box p-4 %s" containerBG)] [ 
            div [Class "columns"] ([
                div [Class "column pb-10"] [
                    div [Class (sprintf "container box %s m-4" cardBG)] [
                        h1 [Class (sprintf "title %s is-uppercase has-text-weight-light" emphasisColor)] [!!mainPageCard.Heading]
                        h1 [Class "title"] [!!mainPageCard.Title]
                        div [Class "content"] [!!mainPageCard.Body]
                        a [Class (sprintf "button is-rounded %s" containerBG); Href mainPageCard.LearnMoreLink] [!!"Learn more"]
                    ]
                ]
                div [Class "column pt-10"] [
                    div [Class "container m-4"] [
                        figure [Class "image is-4by3"] [
                            img [Src mainPageCard.Image]
                        ]
                    ]
                ]
            ]
            |> fun cols ->
                if mainPageCard.TextPosition = Mainpagecardloader.TextPosition.Right then
                    cols |> List.rev
                else cols
            )
        ]
    ]
    

let generate' (ctx : SiteContents) (_: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let news = ctx.TryGetValues<Newsloader.NewsItem>() |> Option.defaultValue Seq.empty
    let mainPageCards = ctx.TryGetValues<Mainpagecardloader.MainPageCard>() |> Option.defaultValue Seq.empty
    let desc =
        siteInfo
        |> Option.map (fun si -> si.description)
        |> Option.defaultValue ""

    Layout.layout ctx "Home" [
        section [Class "hero is-medium"] [
          div [Class "hero-body"] [
                div [Class "container has-text-centered"] [
                    figure [Class "image"] [
                        img [Src "images/nfdi-hero.svg"]
                    ]
                    p [Class "title"] [!!"Democratization of plant research"]
                    p [Class "title"] [!!"Made easy."]
                    a [Class "button is-rounded has-bg-lightblue"] [iconTextLeft "fas fa-caret-right" "Become a member"]
                ]
            ]
        ]

        yield! mainPageCards |> Seq.sortBy (fun mpc -> mpc.Index) |> Seq.map createMainPageCard


        section [Class "section"] [
            div [Class "Container box p-4 has-bg-yellow is-rounded"] [ 
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