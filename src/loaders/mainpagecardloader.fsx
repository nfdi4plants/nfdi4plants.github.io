#load "../globals.fsx"

open Globals
open System.IO

type TextPosition =
    | Left
    | Right

    static member ofString (s:string) = 
        match s.ToLowerInvariant() with
        | "right"   -> Right
        | _         -> Left

type MainPageCard = {
    Heading         : string
    Title           : string
    Index           : int
    CardBg          : string
    ContainerBg     : string
    EmphasisColor   : string
    Image           : string
    LearnMoreLink   : string
    TextPosition    : TextPosition
    Body            : string
} with
    static member create heading title index cardBg containerBg emphasisColor image learnMoreLink tp body =
        {
            Heading       = heading      
            Title         = title        
            Index         = index        
            CardBg        = cardBg       
            ContainerBg   = containerBg  
            EmphasisColor = emphasisColor
            Image         = image        
            LearnMoreLink = learnMoreLink
            TextPosition  = tp
            Body          = body
        }

    static member fromFile (rootDir:string) (newsMarkdownPath:string) =
        
        let text = System.IO.File.ReadAllText newsMarkdownPath

        let config = MarkdownProcessing.getFrontMatter text
        let content = MarkdownProcessing.getMarkdownContent text

        let heading         = config |> Map.find "heading" |> MarkdownProcessing.trimString
        let title           = config |> Map.find "title" |> MarkdownProcessing.trimString
        let index           = config |> Map.find "index" |> MarkdownProcessing.trimString |> int
        let cardBg          = config |> Map.find "card-bg" |> MarkdownProcessing.trimString
        let containerBg     = config |> Map.find "container-bg" |> MarkdownProcessing.trimString
        let emphasisColor   = config |> Map.find "emphasis-color" |> MarkdownProcessing.trimString
        let image           = config |> Map.find "image" |> MarkdownProcessing.trimString
        let learnMoreLink   = config |> Map.find "learn-more-link" |> MarkdownProcessing.trimString
        let tp              = config |> Map.find "text-position" |> MarkdownProcessing.trimString |> TextPosition.ofString
        let body            = content

        MainPageCard.create heading title index cardBg containerBg emphasisColor image learnMoreLink tp body


let contentDir = "content/mainpagecards"

let loader (projectRoot: string) (siteContent: SiteContents) =
    let newsPath = System.IO.Path.Combine(projectRoot, contentDir)
    System.IO.Directory.GetFiles newsPath
    |> Array.filter (Predicates.isMarkdownFile)
    |> Array.iter (fun path ->
        try 
            printfn "[MainPageCard-Loader]: Adding mainpagecard at %s" path
            siteContent.Add (MainPageCard.fromFile projectRoot path)
        with e as exn -> 
            siteContent.AddError {Path = path; Message = (sprintf "Unable to load mainpagecard %s. \n%s" path e.Message); Phase = Loading}
    )
    printfn "[MainPageCard-Loader]: Done loading mainpagecards"
    siteContent