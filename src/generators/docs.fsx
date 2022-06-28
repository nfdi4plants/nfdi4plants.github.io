#load "layout.fsx"
#load "../../.paket/load/main.group.fsx"

open Html
open Fornax.Nfdi4Plants
open Layout

let generate' (ctx : SiteContents) (page: string) =
    let docs = 
        ctx.TryGetValues<DocsData>() 
        |> Option.defaultValue Seq.empty

    // printfn "Number of generated docs: (Generator) %i" <| Seq.length docs

    let relevantDoc =
        docs
        |> Seq.tryFind (fun lmp ->
            lmp.file = page
        ) 

    match relevantDoc with
    | Some page ->
         Layout.layout ctx page.title [
            Components.docsLayout baseUrl page
        ]
    | None -> 
        printfn "[Docs-Generator] No docs page found for %s" page
        div [] []


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
    generate' ctx page
    |> Layout.render ctx