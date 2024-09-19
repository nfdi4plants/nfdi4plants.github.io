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


type LearnMoreHero = {
    Heading:        string
    Title:          string
    BgColor:        string
    EmphasisColor:  string
    Image:          string
    Layout:         Layout
    Body:           string
} with

    static member create heading title bgColor emphasisColor image layout body = 
        {
            Heading         = heading      
            Title           = title        
            BgColor         = bgColor      
            EmphasisColor   = emphasisColor
            Image           = image        
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
        let layout          = config |> Map.find "layout" |> MarkdownProcessing.trimString |> Layout.ofString
        let body            = content 

        LearnMoreHero.create heading title bgColor emphasisColor image layout body 

type LearnMoreCard = {
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

        LearnMoreCard.create title bgColor borderColor emphasisColor image layout index body 

type LearnMorePage = {
    Path        : string
    OutputName  : string
    Hero        : LearnMoreHero
    Cards       : LearnMoreCard []
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
            |> Option.map LearnMoreHero.fromFile

        let cards = 
            content 
            |> Array.filter (fun c -> 
              Path.GetFileNameWithoutExtension c <> "hero"
              && ((Path.GetFileNameWithoutExtension c).StartsWith("_") |> not)
            )
            |> Array.map LearnMoreCard.fromFile

        match hero with
        | Some h -> 
            printfn "[LearnMore-Loader]: Found hero at %s" folderPath
            LearnMorePage.create relPath outName h cards
        | None -> failwithf "[LearnMore-Loader] directory %s does not contain a hero.md file." folderPath


let contentDir = "content/pages"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let learnMorePath = System.IO.Path.Combine(projectRoot, contentDir)

    let contentDirs = Directory.GetDirectories learnMorePath

    contentDirs
    |> Array.iter (fun cDir ->
        printfn "[LearnMore-Loader]: processing folder %s" cDir
        try 
            siteContent.Add (LearnMorePage.fromFolder projectRoot cDir)
        with e as exn -> 
            siteContent.AddError {Path = cDir; Message = (sprintf "Unable to load mainpagecard %s. \n%s" cDir e.Message); Phase = Loading}

    )
    printfn "[LearnMore-Loader]: Done loading learn-more pages"
    siteContent