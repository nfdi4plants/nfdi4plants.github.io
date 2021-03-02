#r "../_lib/Fornax.Core.dll"

type SiteInfo = {
    title: string
    description: string
}

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add({title = "nfdi4plants static site template"; description = "static webpages with nfdi4plants branding created with fornax and bulma sass"})

    siteContent
