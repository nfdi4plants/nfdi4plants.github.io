#load "globals.fsx"

open Globals
open Config
open System.IO

let config = {
    Generators = [
        {Script = "sass.fsx"; Trigger = OnFileExt ".scss"; OutputFile = ChangeExtension ".css"}
        {Script = "staticfile.fsx"; Trigger = OnFilePredicate Predicates.staticPredicate; OutputFile = SameFileName }
        {Script = "index.fsx"; Trigger = Once; OutputFile = NewFileName "index.html" }
        {Script = "news.fsx"; Trigger = Once; OutputFile = NewFileName "news.html" }
        {Script = "newsitem.fsx"; Trigger = OnFilePredicate Predicates.newsPredicate; OutputFile = ChangeExtension ".html"}
        {Script = "learnmore.fsx"; Trigger = OnFilePredicate Predicates.learnMoreHeroPredicate; OutputFile = Custom LearnMore.createLernMorePageName}
        {Script = "details.fsx"; Trigger = Once; OutputFile = NewFileName "content/learn-more/service/arccommander.html"}
        {Script = "about.fsx"; Trigger = OnFilePredicate Predicates.AboutHeroPredicate; OutputFile = Custom About.createAboutPageName}
    ]
}
