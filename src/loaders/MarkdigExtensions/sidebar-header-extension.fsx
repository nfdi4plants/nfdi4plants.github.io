#r "nuget: Expecto, 9.0.4"
#r "../../_lib/Markdig.dll"

// https://odetocode.com/blogs/scott/archive/2020/01/23/a-custom-renderer-extension-for-markdig.aspx
// https://github.com/arthurrump/MarkdigExtensions/blob/master/src/MarkdigExtensions.ImageAsFigure/ImageAsFigure.fs

open Markdig.Renderers
open Markdig
open Markdig.Renderers.Html
open Markdig.Syntax

open System

let private splitKey (line: string) =
    let seperatorIndex = line.IndexOf(':')
    if seperatorIndex > 0 then
        let key = line.[.. seperatorIndex - 1].Trim()
        let value = line.[seperatorIndex + 1 ..].Trim()
        key, Some value
    else
        line, None

type SidebarHeaderRenderer() =
    inherit HeadingRenderer()

    override __.Write(renderer : HtmlRenderer, hb : HeadingBlock ) =

        let headingTexts = [|
            "h1";
            "h2";
            "h3";
            // "nfdi-h4";
            // "nfdi-h5";
            // "nfdi-h6";
        |]
        let index = hb.Level - 1
        let headingText =
            if index < headingTexts.Length then
                headingTexts[index]
            else
                headingTexts.[headingTexts.Length-1]
        let innerText, href = 
            let s = String.concat "" [for i in hb.Inline do yield i.ToString()]
            splitKey s

        let attr = hb.GetAttributes()
        attr.AddProperty("slot", "inner")
        if href.IsSome then attr.AddProperty("href", href.Value)

        if (renderer.EnableHtmlForBlock) then
            renderer.Write('<') |> ignore
            renderer.Write(headingText) |> ignore
            renderer.WriteAttributes(hb) |> ignore
            renderer.Write('>') |> ignore
        
        renderer.Write(innerText) |> ignore

        if (renderer.EnableHtmlForBlock) then
            renderer.Write("</") |> ignore
            renderer.Write(headingText) |> ignore
            renderer.WriteLine(">") |> ignore
 
        renderer.EnsureLine() |> ignore

/// An extension for Markdig that highlights syntax in fenced code blocks
type SidebarHeaderExtension() =

    interface IMarkdownExtension with

        member __.Setup(_) = ()

        member __.Setup(_, renderer) = 
            renderer.ObjectRenderers.ReplaceOrAdd<HeadingRenderer>(new SidebarHeaderRenderer()) |> ignore

open System.Runtime.CompilerServices

[<Extension>]
type MarkdownPipelineBuilderExtensions() =
    [<Extension>]
    // <summary>Highlight code in fenced code blocks</summary>
    // <param name="pipeline">The Markdig <see cref="MarkdownPipelineBuilder"/> to add the extension to</param>
    static member UseSidebarHeader(pipeline : MarkdownPipelineBuilder) =
        pipeline.Extensions.Add(SidebarHeaderExtension())
        pipeline

// let pipeline = 
//     let builder = new MarkdownPipelineBuilder()
//     builder
//         .UseSidebarHeader()
//         .Build()

// let markdown = """# Start testing!:#start-testing"""
// let result = Markdown.ToHtml(markdown, pipeline)
// printfn "Result! %A" result


module Testing =
    open Expecto

    let pipeline = 
        let builder = new MarkdownPipelineBuilder()
        builder
            .UseSidebarHeader()
            .Build()

    [<Tests>]
    let tests = 
        testList "UseSidebarHeader" [
            test "basic case" {
                let markdown = """# Start testing!:#start-testing"""
                let result = Markdown.ToHtml(markdown, pipeline)
                Expect.equal result $"""<h1 slot="inner" href="#start-testing">Start testing!</h1>{'\010'}""" ""
            }
            test "base case without link" {
                let markdown = """# Start testing!"""
                let result = Markdown.ToHtml(markdown, pipeline)
                Expect.equal result $"""<h1 slot="inner">Start testing!</h1>{'\010'}""" ""
            }
            test "6 depth case" {
                let markdown = """###### Start testing!"""
                let result = Markdown.ToHtml(markdown, pipeline)
                Expect.equal result $"""<h3 slot="inner">Start testing!</h3>{'\010'}""" ""
            }
        ]