#load "../../.paket/load/main.group.fsx"

open System.IO
open Fornax.Nfdi4Plants

let contentDir = "content/docs"

type DocsConfig = {
    disableLiveRefresh: bool
}

let loader (projectRoot: string) (siteContent: SiteContents) =
    printfn "[Docs-Loader]: Start loading Docs pages"
    let docsPath = Path.Combine(projectRoot, contentDir)

    let files = 
        Directory.GetFiles(docsPath, "*")
        |> Array.filter (fun n -> n.EndsWith ".md")
        |> Array.filter (fun n -> n.Contains "README.md" |> not)

    let docs : DocsData [] = files |> Array.map (Docs.loadFile projectRoot contentDir)

    docs |> Array.iter siteContent.Add

    siteContent.Add({disableLiveRefresh = false})
    printfn "[Docs-Loader]: Done loading Docs pages"
    siteContent
