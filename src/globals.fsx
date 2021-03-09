#r "_lib/Fornax.Core.dll"
#r "_lib/Markdig.dll"

open System.IO
open Markdig

module HTMLComponents =

    open Html

    let icon iconClass = 
        span [Class"icon"] [
            i [Class iconClass][]
        ]

    let iconText iconClass text = 
        span [Class "icon-text"][
            icon iconClass
            span [] [!!text]
        ]

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