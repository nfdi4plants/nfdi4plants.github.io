#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Html

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
        div [Class "container"] (
            news
            |> List.ofSeq
            |> List.map (fun newsItem ->
                div [Class "container"] [
                    div [Class "columns"] [
                        div [Class "column is-2"] [
                            h2 [Class "Title"] [!!(newsItem.Date.Day.ToString()) ]
                            h4 [Class "Title"] [!!(newsItem.Date.Month.ToString() )]
                        ]
                        div [Class "column is-10"] [
                            newsItem.Body
                            // .Split(" ")
                            // |> Array.take (min (newsItem.Body.Length - 1) 50)
                            // |> String.concat " "
                            // |> sprintf "%s ..."
                            |> (!!)
                        ]
                    ]
                ]
            )
        )
    ]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx