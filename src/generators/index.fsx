#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Globals.HTMLComponents
open Html

open Newsloader

let createLatestNewsPreview (newsItems: NewsItem []) =
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
                    n1 |> NewsItem.createPreviewElement false
                ]
                div [Class "column"] [
                    n2 |> NewsItem.createPreviewElement false
                ]
            ]
            div [Class "columns"] [
                div [Class "column"] [
                    n3 |> NewsItem.createPreviewElement false
                ]
                div [Class "column"] [
                    n4 |> NewsItem.createPreviewElement false
                ]
            ]
        ]

let createMainPageCard (mainPageCard: Mainpagecardloader.MainPageCard) = 

    let containerBG = Colors.hasBgSplitColor 20 mainPageCard.ContainerBg
    let bgColor = Colors.hasBgColor  mainPageCard.ContainerBg
    let cardBG = Colors.hasBgColor mainPageCard.CardBg
    let emphasisColor = Colors.isColor mainPageCard.EmphasisColor

    section [Class "section"] [
        div [Class (sprintf "Container p-4 has-rounded-border %s" containerBG)] [ 
            div [Class "columns"] ([
                div [Class "column align-items-top pb-10"] [
                    div [Class (sprintf "container box %s m-4" cardBG)] [
                        h1 [Class (sprintf "title %s is-uppercase has-text-weight-light" emphasisColor)] [!!mainPageCard.Heading]
                        h1 [Class "title"] [!!mainPageCard.Title]
                        div [Class "content"] [!!mainPageCard.Body]
                        a [Class (sprintf "button is-rounded %s" bgColor); Href mainPageCard.LearnMoreLink] [!!"Learn more"]
                    ]
                ]
                div [Class "column align-items-bottom pt-10"] [
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
                    // a [Class "button is-rounded has-bg-lightblue"] [iconTextLeft "fas fa-caret-right" "Become a member"]
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