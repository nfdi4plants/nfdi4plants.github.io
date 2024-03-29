#load "../globals.fsx"

open Globals
open System.IO

type Layout = 
    | TextOnly
    | ImageOnly
    | TextTopImageBottom
    | TextBottomImageTop
    | TextLeftImageRight
    | TextRightImageLeft

    static member ofString (str:string) =
        match str.Trim().ToLowerInvariant() with
        | "text-only"               -> TextOnly
        | "image-only"              -> ImageOnly
        | "text-top-image-bottom"   -> TextTopImageBottom
        | "text-bottom-image-top"   -> TextBottomImageTop
        | "text-left-image-right"   -> TextLeftImageRight
        | "text-right-image-left"   -> TextRightImageLeft
        | _ -> failwithf "Layout type %s does not exist." str


type JobsHero = {
    Heading:        string
    Title:          string
    BgColor:        string
    EmphasisColor:  string
    Image:          string
    OpenJobOffers:  bool
    Layout:         Layout
    Body:           string
} with

    static member create heading title bgColor emphasisColor image openJobOffers layout body = 
        {
            Heading         = heading      
            Title           = title        
            BgColor         = bgColor      
            EmphasisColor   = emphasisColor
            Image           = image        
            OpenJobOffers   = openJobOffers
            Layout          = layout     
            Body            = body
        }

    static member fromFile heroMarkdownPath = 
        
        let text = System.IO.File.ReadAllText heroMarkdownPath

        let config = MarkdownProcessing.getFrontMatter text
        let content = MarkdownProcessing.getMarkdownContent text

        let heading         = config |> Map.find "heading" |> MarkdownProcessing.trimString
        let title           = config |> Map.find "title" |> MarkdownProcessing.trimString
        let bgColor         = config |> Map.find "bg-color" |> MarkdownProcessing.trimString
        let emphasisColor   = config |> Map.find "emphasis-color" |> MarkdownProcessing.trimString
        let image           = config |> Map.find "image" |> MarkdownProcessing.trimString
        let openJobOffers   = config |> Map.find "open-job-offers" |> MarkdownProcessing.trimString |> System.Boolean.Parse
        let layout          = config |> Map.find "layout" |> MarkdownProcessing.trimString |> Layout.ofString
        let body            = content 

        JobsHero.create heading title bgColor emphasisColor image openJobOffers layout body 

type JobsCard = {
    Title           : string
    BgColor         : string
    BorderColor     : string
    EmphasisColor   : string
    Image           : string
    Layout          : Layout
    Index           : int
    Body            : string
} with

    static member create title bgColor borderColor emphasisColor image layout index body = 
        {
            Title           = title        
            BgColor         = bgColor      
            BorderColor     = borderColor
            EmphasisColor   = emphasisColor
            Image           = image        
            Layout          = layout     
            Index           = index
            Body            = body  
        }

    static member fromFile heroMarkdownPath = 
        
        let text = System.IO.File.ReadAllText heroMarkdownPath

        let config = MarkdownProcessing.getFrontMatter text
        let content = MarkdownProcessing.getMarkdownContent text

        let title           = config |> Map.find "title" |> MarkdownProcessing.trimString
        let bgColor         = config |> Map.find "bg-color" |> MarkdownProcessing.trimString
        let borderColor     = config |> Map.find "border-color" |> MarkdownProcessing.trimString
        let emphasisColor   = config |> Map.find "emphasis-color" |> MarkdownProcessing.trimString
        let image           = config |> Map.find "image" |> MarkdownProcessing.trimString
        let layout          = config |> Map.find "layout" |> MarkdownProcessing.trimString |> Layout.ofString
        let index           = config |> Map.find "index" |> MarkdownProcessing.trimString |> int
        let body            = content 

        JobsCard.create title bgColor borderColor emphasisColor image layout index body 

type JobsPage = {
    Path        : string
    OutputName  : string
    Hero        : JobsHero
    Cards       : JobsCard []
} with

    static member create path outName hero cards = {
        Path        = path 
        OutputName  = outName
        Hero        = hero 
        Cards       = cards
    }

    static member fromFolder (rootDir:string) (folderPath:string) =
        
        let relPath = folderPath.Replace(rootDir,"")
        let outName = DirectoryInfo(folderPath).Name

        let content = 
            System.IO.Directory.GetFiles folderPath
            |> Array.filter (Predicates.isMarkdownFile)

        let hero = 
            content 
            |> Array.tryFind (fun c -> Path.GetFileNameWithoutExtension c = "hero")
            |> Option.map JobsHero.fromFile

        let cards = 
            content 
            |> Array.filter (fun c -> Path.GetFileNameWithoutExtension c <> "hero")
            |> Array.map JobsCard.fromFile

        match hero with
        | Some h -> 
            printfn "[Jobs-Loader]: Found hero at %s" folderPath
            JobsPage.create relPath outName h cards
        | None -> failwithf "[Jobs-Loader] directory %s does not contain a hero.md file." folderPath


let contentDir = "content/jobs"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let JobsPath = System.IO.Path.Combine(projectRoot, contentDir)
    try 
        siteContent.Add (JobsPage.fromFolder projectRoot JobsPath)
    with e as exn -> 
        siteContent.AddError {Path = JobsPath; Message = (sprintf "Unable to load mainpagecard %s. \n%s" JobsPath e.Message); Phase = Loading}

    printfn "[Jobs-Loader]: Done loading Jobs pages"
    siteContent