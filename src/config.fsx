#load "globals.fsx"

open Globals
open Config
open System.IO

let config = {
    Generators = [
        {Script = "sass.fsx"; Trigger = OnFileExt ".scss"; OutputFile = GeneratorOutput.Custom Sass.handleSassPath }
        {Script = "staticfile.fsx"; Trigger = OnFilePredicate Predicates.staticPredicate; OutputFile = SameFileName }
        {Script = "index.fsx"; Trigger = Once; OutputFile = NewFileName "index.html" }
    ]
}
