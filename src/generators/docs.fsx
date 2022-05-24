#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Globals
open Docsloader
open Html

let docsLayout (docs: Docsloader.Docs) =
    custom "nfdi-body" [Class "content"; if Array.isEmpty docs.sidebar |> not then HtmlProperties.Custom("hasSidebar", "true")] [
        if Array.isEmpty docs.sidebar |> not then 
            for sidebarEle in docs.sidebar do
                custom "nfdi-sidebar-element" [HtmlProperties.Custom ("slot", "sidebar"); HtmlProperties.Custom ("isActive","true") ] [
                    div [HtmlProperties.Custom ("slot", "title")] [!! sidebarEle.Title]
                    !! sidebarEle.Content
                ]
        custom "nfdi-h1" [] [!! docs.title]
        if docs.add_toc then custom "nfdi-toc" [] []
        !! docs.content
    ]

let generate' (ctx : SiteContents) (page: string) =
    let docsPages = ctx.TryGetValues<Docsloader.Docs>() |> Option.defaultValue Seq.empty
    printfn "[Docs-Generator] mapping docs page for %s" page
    
    let currentDocsPage =
        docsPages
        |> Seq.tryFind (fun lmp ->
            lmp.file = page
        )

    match currentDocsPage with
    | Some page ->
         Layout.layout ctx page.title [
            docsLayout page
        ]
    | None -> 
        printfn "[Docs-Generator] No docs page found for %s" page
        div [] []


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
    generate' ctx page
    |> Layout.render ctx