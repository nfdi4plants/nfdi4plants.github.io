#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Aboutloader
open Html

let createHero (hero:Aboutloader.AboutHero) =

    let heroBG = Colors.hasBgColor hero.BgColor
    let emphasisColor = Colors.isColor hero.EmphasisColor

    let createImageComponent ratio =
        figure [Class (sprintf "image is-%s" ratio)] [
            img [Src hero.Image]
        ]

    let headingComponent = 
        h1 [Class (sprintf "title %s is-uppercase has-text-weight-light" emphasisColor)] [!!hero.Heading]

    let titleComponent = 
        h1 [Class "title"] [!!hero.Title]

    let bodyComponent =
        div [Class "content"] [!!hero.Body]

    section [Class (sprintf "hero is-medium %s" heroBG)] [
        div [Class "hero-body"] [
            match hero.Layout with
            | TextOnly              -> 
                div [Class "container"] [
                    headingComponent
                    titleComponent
                    bodyComponent
                ]
            | ImageOnly             -> 
                div [Class "container"] [
                    createImageComponent "5by3"
                ]
            | TextTopImageBottom    -> 
                div [Class "container"] [
                    headingComponent
                    titleComponent
                    bodyComponent
                    createImageComponent "3by1"
                ]
            | TextBottomImageTop    -> 
                div [Class "container"] [
                    createImageComponent "3by1"
                    headingComponent
                    titleComponent
                    bodyComponent
                ]
            | TextLeftImageRight    -> 
                div [Class "container"] [
                    div [Class "columns"] [
                        div [Class "column"] [
                            headingComponent
                            titleComponent
                            bodyComponent
                        ]
                        div [Class "column"] [
                            createImageComponent "4by3"
                        ]
                    ]
                ]
            | TextRightImageLeft    -> 
                div [Class "container"] [
                    div [Class "columns"] [
                        div [Class "column"] [
                            createImageComponent "4by3"
                        ]
                        div [Class "column"] [
                            headingComponent
                            titleComponent
                            bodyComponent
                        ]
                    ]
                ]
        ]
    ]

let createCard (card:Aboutloader.AboutCard) =

    let cardBG = Colors.hasBgColor card.BgColor
    let cardBorder = Colors.hasBorderColor card.BorderColor
    let emphasisColor = Colors.isColor card.EmphasisColor

    let createImageComponent ratio =
        figure [Class (sprintf "image is-%s" ratio)] [
            img [Src card.Image]
        ]

    let titleComponent = 
        h1 [Class (sprintf "title %s" emphasisColor)] [!!card.Title]

    let bodyComponent =
        div [Class "content"] [!!card.Body]

    section [Class (sprintf "section")] [
        div [Class (sprintf "container box %s %s" cardBG cardBorder)] [
            match card.Layout with
            | TextOnly              -> 
                div [Class "container"] [
                    titleComponent
                    bodyComponent
                ]
            | ImageOnly             -> 
                div [Class "container"] [
                    createImageComponent "5by3"
                ]
            | TextTopImageBottom    -> 
                div [Class "container"] [
                    titleComponent
                    bodyComponent
                    createImageComponent "3by1"
                ]
            | TextBottomImageTop    -> 
                div [Class "container"] [
                    createImageComponent "3by1"
                    titleComponent
                    bodyComponent
                ]
            | TextLeftImageRight    -> 
                div [Class "container"] [
                    div [Class "columns"] [
                        div [Class "column"] [
                            titleComponent
                            bodyComponent
                        ]
                        div [Class "column"] [
                            createImageComponent "4by3"
                        ]
                    ]
                ]
            | TextRightImageLeft    -> 
                div [Class "container"] [
                    div [Class "columns"] [
                        div [Class "column"] [
                            createImageComponent "4by3"
                        ]
                        div [Class "column"] [
                            titleComponent
                            bodyComponent
                        ]
                    ]
                ]
        ]
    ]

let generate' (ctx : SiteContents) (page: string) =
    let AboutPages = ctx.TryGetValues<Aboutloader.AboutPage>() |> Option.defaultValue Seq.empty

    printfn "[About-Generator] mapping about page for %s" (page.Replace("/hero.md",""))
    
    let currentAboutPage =
        AboutPages
        |> Seq.tryFind (fun lmp ->
            lmp.OutputName = System.IO.DirectoryInfo(page.Replace("/hero.md","")).Name
        )

    match currentAboutPage with
    | Some page ->
        Layout.layout ctx "About" [
            createHero page.Hero
            yield! 
                page.Cards
                |> Array.sortBy (fun c -> c.Index)
                |> Array.map createCard
        ]
    | None -> 
        printfn "[About-Generator] No about page found for %s" page
        div [] []
     



let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx