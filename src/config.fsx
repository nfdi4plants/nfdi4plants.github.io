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
        {Script = "events.fsx"; Trigger = Once; OutputFile = NewFileName "events.html" }
        {Script = "eventsitem.fsx"; Trigger = OnFilePredicate Predicates.eventsPredicate; OutputFile = ChangeExtension ".html"}
        {Script = "learnmore.fsx"; Trigger = OnFilePredicate Predicates.learnMoreHeroPredicate; OutputFile = Custom LearnMore.createLearnMorePageName}
        {Script = "service.fsx"; Trigger = OnFilePredicate Predicates.ServiceHeroPredicate; OutputFile = Custom Service.createServicePageName}
        {Script = "details.fsx"; Trigger = Once; OutputFile = NewFileName "content/service/arccommander.html"}
        {Script = "jobs.fsx"; Trigger = OnFilePredicate Predicates.JobsHeroPredicate; OutputFile = Custom Jobs.createJobsPageName}
        {Script = "about.fsx"; Trigger = OnFilePredicate Predicates.AboutHeroPredicate; OutputFile = Custom About.createAboutPageName}
        {Script = "imprint.fsx"; Trigger = OnFilePredicate Predicates.ImprintHeroPredicate; OutputFile = Custom Imprint.createImprintPageName}
        {Script = "privacy.fsx"; Trigger = OnFilePredicate Predicates.PrivacyHeroPredicate; OutputFile = Custom Privacy.createPrivacyPageName}
    ]
}
