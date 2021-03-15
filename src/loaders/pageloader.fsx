#r "../_lib/Fornax.Core.dll"

type Page = {
    title: string
    link: string
}

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add({title = "Home"; link = "/"})
    siteContent.Add({title = "News"; link = "/news.html"})
    siteContent.Add({title = "DataHub"; link = "/"})
    siteContent.Add({title = "Resources"; link = "/"})
    siteContent.Add({title = "About"; link = "/"})
    siteContent
