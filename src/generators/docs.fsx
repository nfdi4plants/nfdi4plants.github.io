#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Globals
open Docsloader
open Html

let docsLayout (docs: Docsloader.Docs) =
    let publishedDate = docs.published.Value.ToString("yyyy-MM-dd")
    custom "nfdi-body" [Class "content"; if Array.isEmpty docs.sidebar |> not then HtmlProperties.Custom("hasSidebar", "true")] [
        if Array.isEmpty docs.sidebar |> not then 
            for sidebarEle in docs.sidebar do
                custom "nfdi-sidebar-element" [HtmlProperties.Custom ("slot", "sidebar"); HtmlProperties.Custom ("isActive","true") ] [
                    div [HtmlProperties.Custom ("slot", "title")] [!! sidebarEle.Title]
                    !! sidebarEle.Content
                ]

        h1 [Class "front-header"] [!! docs.title]
        i [Class "help" ] [!! $"last updated at {publishedDate}" ]
        
        if docs.add_toc then custom "nfdi-toc" [] []
        !! docs.content

        // support contact
        h3 [] [!! "Dataplant Support"]
        div [] [
            !! "Besides these technical solutions, DataPLANT supports you with community-engaged data stewardship. For further assistance, feel free to reach out via our "
            a [Href "https://support.nfdi4plants.org"] [!! "helpdesk"]
            !! " or by contacting us " 
            a [Href "javascript:location='mailto:\u0069\u006e\u0066\u006f\u0040\u006e\u0066\u0064\u0069\u0034\u0070\u006c\u0061\u006e\u0074\u0073\u002e\u006f\u0072\u0067';void 0"] [!! "directly"]
            !! "."
        ]
        
        // Edit this page link
        div [] [
            a [
                Href $"https://github.com/nfdi4plants/nfdi4plants.github.io/tree/main/src/{docs.file}"; 
                HtmlProperties.Style [MarginLeft "auto"; Display "block"; CSSProperties.Width "130px"]
            ] [!! "✏️ Edit this page"]
        ]
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