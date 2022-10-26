#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Jobsloader
open Html

let createHero (hero:Jobsloader.JobsHero) =

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

    
    let noJobOffersMsg = 
        [
            p [Class "subtitle"] [!!"Currently no open positions are available!"]
            p [] [
                !!"Please do not hesitate to "
                // http://www.email-obfuscator.com
                a [Href "javascript:location='mailto:\u0069\u006e\u0066\u006f\u0040\u006e\u0066\u0064\u0069\u0034\u0070\u006c\u0061\u006e\u0074\u0073\u002e\u006f\u0072\u0067';void 0"] [!!"contact us"]
                !!" if you are interested in joining the DataPLANT team or in cooperating with us!"
            ] 
        ]

    let bodyComponent =
        match hero.OpenJobOffers with
        | true ->
            div [Class "content"] [!!hero.Body]
        | false ->
            div [Class "content"] noJobOffersMsg

    section [Class (sprintf "hero is-medium %s" heroBG)] [
        div [Class "hero-body"] [
            match hero.Layout with
            | TextOnly              -> 
                div [Class "container"] [
                    headingComponent
                    div [Class "content"] [ 
                        titleComponent
                        // this part breaks with previous maintanence standards by not using "bodyComponent". but for "TextOnly" it is 
                        // better to have title and body in Class "content".
                        match hero.OpenJobOffers with
                        | true ->
                            !!hero.Body
                        | false ->
                            yield! noJobOffersMsg
                    ]
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

let createCard (card:Jobsloader.JobsCard) =

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
    let JobsPages = ctx.TryGetValues<Jobsloader.JobsPage>() |> Option.defaultValue Seq.empty
    
    printfn "[Jobs-Generator] mapping jobs page for %s" (page.Replace("/hero.md",""))
    
    let currentJobsPage =
        JobsPages
        |> Seq.tryFind (fun lmp ->
            lmp.OutputName = System.IO.DirectoryInfo(page.Replace("/hero.md","")).Name
        )

    match currentJobsPage with
    | Some page ->
        Layout.layout ctx "" [
            createHero page.Hero
            yield! 
                page.Cards
                |> Array.sortBy (fun c -> c.Index)
                |> Array.map createCard
        ]
    | None -> 
        printfn "[Jobs-Generator] No jobs page found for %s" page
        div [] []
     
let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx