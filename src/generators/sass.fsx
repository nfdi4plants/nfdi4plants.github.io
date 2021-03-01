#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open System.IO
open System.Diagnostics


let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
    
    let tempOutput = (Path.GetTempPath())
    let sassFile = Path.Combine(projectRoot, page)
    let sassFileName = Path.GetFileName(sassFile)
    let tempOutputFile = Path.Combine(tempOutput,(sassFileName.Replace(".scss",".css")))
    let cmd = sprintf "webcompiler %s -o %s --minify disable --zip disable" sassFile tempOutput

    printfn "%s" cmd

    let psi = ProcessStartInfo()
    psi.FileName <- "dotnet"
    psi.Arguments <- cmd
    psi.CreateNoWindow <- true
    psi.WindowStyle <- ProcessWindowStyle.Hidden
    psi.UseShellExecute <- true
    try
        let proc = Process.Start psi
        proc.WaitForExit()
        let output = File.ReadAllText tempOutputFile
        File.Delete tempOutputFile
        output
    with
    | ex ->
        printfn "EX: %s" ex.Message
        printfn "Please check you have installed the Sass compiler if you are going to be using files with extension .scss. https://sass-lang.com/install"
        ""