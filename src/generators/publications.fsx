
#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Publicationsloader
open Html

let generate' (ctx : SiteContents) (page: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let publications = 
        ctx.TryGetValues<Publicationsloader.Publication>() |> Option.defaultValue Seq.empty
        |> Seq.filter (fun p -> not p.Featured)
        |> Seq.sortByDescending (fun n -> n.Year)

    let publications_featured = 
        ctx.TryGetValues<Publicationsloader.Publication>() |> Option.defaultValue Seq.empty
        |> Seq.filter (fun p -> p.Featured)
        |> Seq.sortByDescending (fun n -> n.Year)

    Layout.layout ctx "Publications" [
        section [Class "hero is-medium has-bg-yellow"] [
          div [Class "hero-body"] [
                div [Class "container has-text-centered"] [
                    h1 [Class "title is-1"] [!!"DataPLANT Publications"]
                ]
            ]
        ]
        section [Class "section"] [
            div [Class "Container box p-4 is-rounded"] [
                    h1 [Class "title is-1"] [!!"Featured publications"]
                    yield! publications_featured |> Seq.map (fun publication -> Publication.createElement publication)
                ]
        ]
        section [Class "section"] (
            publications
            |> Seq.groupBy (fun p -> p.Year)
            |> Seq.map (fun (year, publications) ->
                div [Class "Container box p-4 is-rounded"] [
                    h2 [Class "title is-2"] [!!year.ToString()]
                    yield! publications |> Seq.map (fun publication -> Publication.createElement publication)
                ]
            )
            |> List.ofSeq
        )
    ]


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx