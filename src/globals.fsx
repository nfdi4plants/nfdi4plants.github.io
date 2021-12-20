#r "_lib/Fornax.Core.dll"
#r "_lib/Markdig.dll"

open System.IO
open Markdig

module Colors =

    let validNFDIColorNames = 
        [
            "black"
            "grey"
            "white"
            "mint"                 
            "mint-darker-10"   
            "mint-darker-20"       
            "mint-darker-30"       
            "mint-darker-40"       
            "mint-darker-50"       
            "mint-darker-60"       
            "mint-darker-70"       
            "mint-darker-80"       
            "mint-darker-90"       
            "mint-lighter-10"      
            "mint-lighter-20"      
            "mint-lighter-30"      
            "mint-lighter-40"      
            "mint-lighter-50"      
            "mint-lighter-60"      
            "mint-lighter-70"      
            "mint-lighter-80"      
            "mint-lighter-90"      
            "lightblue"            
            "lightblue-darker-10"  
            "lightblue-darker-20"  
            "lightblue-darker-30"  
            "lightblue-darker-40"  
            "lightblue-darker-50"  
            "lightblue-darker-60"  
            "lightblue-darker-70"  
            "lightblue-darker-80"  
            "lightblue-darker-80"  
            "lightblue-lighter-10" 
            "lightblue-lighter-20" 
            "lightblue-lighter-30" 
            "lightblue-lighter-40" 
            "lightblue-lighter-50" 
            "lightblue-lighter-60" 
            "lightblue-lighter-70" 
            "lightblue-lighter-80" 
            "lightblue-lighter-90" 
            "darkblue"             
            "darkblue-darker-10"   
            "darkblue-darker-20"   
            "darkblue-darker-30"   
            "darkblue-darker-40"   
            "darkblue-darker-50"   
            "darkblue-darker-60"   
            "darkblue-darker-70"   
            "darkblue-darker-80"   
            "darkblue-darker-90"   
            "darkblue-lighter-10"  
            "darkblue-lighter-20"  
            "darkblue-lighter-30"  
            "darkblue-lighter-40"  
            "darkblue-lighter-50"  
            "darkblue-lighter-60"  
            "darkblue-lighter-70"  
            "darkblue-lighter-80"  
            "darkblue-lighter-90"  
            "yellow"               
            "yellow-darker-10"     
            "yellow-darker-20"     
            "yellow-darker-30"     
            "yellow-darker-40"     
            "yellow-darker-50"     
            "yellow-darker-60"     
            "yellow-darker-70"     
            "yellow-darker-80"     
            "yellow-darker-90"     
            "yellow-lighter-10"    
            "yellow-lighter-20"    
            "yellow-lighter-30"    
            "yellow-lighter-40"    
            "yellow-lighter-50"    
            "yellow-lighter-60"    
            "yellow-lighter-70"    
            "yellow-lighter-80"    
            "yellow-lighter-90"    
            "olive"                
            "olive-darker-10"      
            "olive-darker-20"      
            "olive-darker-30"      
            "olive-darker-40"      
            "olive-darker-50"      
            "olive-darker-60"      
            "olive-darker-70"      
            "olive-darker-80"      
            "olive-darker-90"      
            "olive-lighter-10"     
            "olive-lighter-20"     
            "olive-lighter-30"     
            "olive-lighter-40"     
            "olive-lighter-50"     
            "olive-lighter-60"     
            "olive-lighter-70"     
            "olive-lighter-80"     
            "olive-lighter-90"     
            "red"                  
            "red-darker-10"        
            "red-darker-20"        
            "red-darker-30"        
            "red-darker-40"        
            "red-darker-50"        
            "red-darker-60"        
            "red-darker-70"        
            "red-darker-80"        
            "red-darker-90"        
            "red-lighter-10"       
            "red-lighter-20"       
            "red-lighter-30"       
            "red-lighter-40"       
            "red-lighter-50"       
            "red-lighter-60"       
            "red-lighter-70"       
            "red-lighter-80"       
            "red-lighter-90"       
        ] 
        |> Set.ofList

    let isValidNFDIColorName n = validNFDIColorNames.Contains(n)
    
    let hasBgColor cName = 
        if isValidNFDIColorName cName then
            sprintf "has-bg-%s" cName
        else failwithf "%s is not a valid color from the NFDI color palette." cName

    let hasBorderColor cName = 
        if isValidNFDIColorName cName then
            sprintf "has-border-color-%s" cName
        else failwithf "%s is not a valid color from the NFDI color palette." cName

    let isColor cName = 
        if isValidNFDIColorName cName then
            sprintf "is-%s" cName
        else failwithf "%s is not a valid color from the NFDI color palette." cName

    let hasBgSplitColor (percentage:int) cName = 
        if isValidNFDIColorName cName then
            sprintf "has-bg-split-%i-%s" percentage cName
        else failwithf "%s is not a valid color from the NFDI color palette." cName

module HTMLComponents =

    open Html

    let whiteIcon iconClass = 
        span [Class"icon is-white"] [
            i [Class iconClass][]
        ]

    let icon iconClass = 
        span [Class"icon"] [
            i [Class iconClass][]
        ]

    let iconTextRight iconClass text = 
        span [Class "icon-text"][
            icon iconClass
            span [] [!!text]
        ]

    let iconTextLeft iconClass text = 
        span [Class "icon-text"][
            span [] [!!text]
            icon iconClass
        ]

    let block children = 
        div [Class "block"] children

module MarkdownProcessing =

    let markdownPipeline =
        MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseGridTables()
            .UseGenericAttributes()
            .UseEmphasisExtras()
            .UseListExtras()
            .UseCitations()
            .UseCustomContainers()
            .UseFigures()
            .Build()

    let isFrontMatterSeparator (input : string) =
        input.StartsWith "---"

    let trimString (str : string) =
        str.Trim().TrimEnd('"').TrimStart('"')

    ///`fileContent` - content of page to parse. Usually whole content of `.md` file
    ///returns content of config that should be used for the page
    let getFrontMatter (fileContent : string) =
        let fileContent = 
            fileContent.Split '\n'
            |> Array.skip 1 //First line must be ---

        let indexOfSeperator = fileContent |> Array.findIndex isFrontMatterSeparator

        let splitKey (line: string) = 
            let seperatorIndex = line.IndexOf(':')
            if seperatorIndex > 0 then
                let key = line.[.. seperatorIndex - 1].Trim().ToLower()
                let value = line.[seperatorIndex + 1 ..].Trim() 
                Some(key, trimString value)
            else 
                None

        fileContent
        |> Array.splitAt indexOfSeperator
        |> fst
        |> Seq.choose splitKey
        |> Map.ofSeq        

        
    ///`fileContent` - content of page to parse. Usually whole content of `.md` file
    ///returns HTML version of content of the page
    let getMarkdownContent (fileContent : string) =
        let fileContent = fileContent.Split '\n'
        let fileContent = fileContent |> Array.skip 1 //First line must be ---
        let indexOfSeperator = fileContent |> Array.findIndex isFrontMatterSeparator
        let content = 
            fileContent 
            |> Array.splitAt indexOfSeperator
            |> snd
            |> Array.skip 1 
            |> String.concat "\n"

        Markdown.ToHtml(content, markdownPipeline)

module Predicates =
        
    let learnMoreHeroPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("learn-more")
        && fileName = "hero"
        && ext = ".md"
        
    let learnMorePredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("learn-more")
        && ext = ".md"

    let isLearnMoreHero f = learnMoreHeroPredicate("",f)

    let markdownPredicate (projectRoot: string, page: string) =
        let ext = Path.GetExtension page
        let fileName = Path.GetFileNameWithoutExtension page
        fileName.ToUpperInvariant() <> "README"
        && ext = ".md"

    let isMarkdownFile f = markdownPredicate ("",f)

    let newsPredicate (projectRoot: string, page: string) = 
        let ext = Path.GetExtension page
        page.Contains("content")
        && page.Contains("news")
        && ext = ".md"
        && (not (page.Contains "_public"))

    let isNewsFile f = newsPredicate("",f)

    let AboutHeroPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("about")
        && fileName = "hero"
        && ext = ".md"
        
    let AboutPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("about")
        && ext = ".md"

    let isAboutHero f = AboutHeroPredicate("",f)

    let JobsHeroPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("jobs")
        && fileName = "hero"
        && ext = ".md"
        
    let JobsPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("jobs")
        && ext = ".md"

    let isJobsHero f = JobsHeroPredicate("",f)

    let ImprintHeroPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("imprint")
        && fileName = "hero"
        && ext = ".md"
        
    let ImprintPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("imprint")
        && ext = ".md"

    let isImprintHero f = ImprintHeroPredicate("",f)

    let PrivacyHeroPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("privacy")
        && fileName = "hero"
        && ext = ".md"
        
    let PrivacyPredicate (projectRoot: string, page: string) =
        let fileName = Path.GetFileNameWithoutExtension page
        let ext = Path.GetExtension page
        page.Contains("privacy")
        && ext = ".md"

    let isPrivacyHero f = PrivacyHeroPredicate("",f)

    let staticPredicate (projectRoot: string, page: string) =
        let ext = Path.GetExtension page
        if page.Contains "_public" ||
           page.Contains "_bin" ||
           page.Contains "content" ||
           page.Contains "_lib" ||
           page.Contains "_data" ||
           page.Contains "_settings" ||
           page.Contains "_config.yml" ||
           page.Contains ".sass-cache" ||
           page.Contains ".git" ||
           page.Contains ".ionide" ||
           page.Contains "configuration" ||
           page.Contains "bulma-0.9.1" ||
           page.Contains "style_src" ||
           ext = ".fsx" ||
           ext = ".scss"
        then
            false
        else
            true

module News =
        
    let getStringForMonth (m:int) =
        match m with
        | 1  -> "Jan"
        | 2  -> "Feb"
        | 3  -> "Mar"
        | 4  -> "Apr"
        | 5  -> "May"
        | 6  -> "Jun"
        | 7  -> "Jul"
        | 8  -> "Aug"
        | 9  -> "Sep"
        | 10 -> "Oct"
        | 11 -> "Nov"
        | 12 -> "Dec"
        | _ -> failwithf "%i is not a valid month number" m

module LearnMore =

    let createLernMorePageName (heroPath:string) =
        heroPath.Replace("/hero.md",".html")

module About =

    let createAboutPageName (heroPath:string) =
        heroPath.Replace("/hero.md",".html")

module Jobs =

    let createJobsPageName (heroPath:string) =
        heroPath.Replace("/hero.md",".html")

module Imprint =

    let createImprintPageName (heroPath:string) =
        heroPath.Replace("/hero.md",".html")

module Privacy =

    let createPrivacyPageName (heroPath:string) =
        heroPath.Replace("/hero.md",".html")