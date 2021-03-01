#r "_lib/Fornax.Core.dll"

open Config
open System.IO

let staticPredicate (projectRoot: string, page: string) =
    let ext = Path.GetExtension page
    if page.Contains "_public" ||
       page.Contains "_bin" ||
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

let handleSass path = 
    printfn "%s" path
    path
        .Replace("_src", "")
        .Replace("scss", "css")

let config = {
    Generators = [
        {Script = "sass.fsx"; Trigger = OnFileExt ".scss"; OutputFile = GeneratorOutput.Custom handleSass }
        {Script = "staticfile.fsx"; Trigger = OnFilePredicate staticPredicate; OutputFile = SameFileName }
        {Script = "index.fsx"; Trigger = Once; OutputFile = NewFileName "index.html" }
    ]
}
