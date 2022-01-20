#r "../_lib/Fornax.Core.dll"

type Page = {
    title: string
    link: string
}

let loader (projectRoot: string) (siteContent: SiteContents) =
    siteContent.Add({title = "Home"; link = "/"})
    siteContent.Add({title = "News"; link = "/news.html"})
    siteContent.Add({title = "Service"; link = "/content/service.html"})
    siteContent.Add({title = "About"; link = "/content/about.html"})
    siteContent.Add({title = "Jobs"; link = "/content/jobs.html"})
    siteContent.Add({title = "DataHUB"; link = "https://gitlab.nfdi4plants.de/"})
    siteContent
