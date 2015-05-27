#load "packages/FSharp.Formatting.2.9.6/FSharp.Formatting.fsx"

#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open System.IO
open Fake.FileHelper
open FSharp.Literate
open System.Diagnostics

let source = __SOURCE_DIRECTORY__
let sources = source @@ "../docs"
let output = source @@ "../docs/output"
let formatting = source @@ "packages/FSharp.Formatting.2.9.6"

let copyFiles() =
  ensureDirectory (output @@ "content")
  CopyRecursive (formatting @@ "styles") (output @@ "content") true 
    |> Log "Copying styles and scripts: "

let generateHtmlDoc() =
  let template = source @@ "../templates/template-project.html"

  // Additional strings to be replaced in the HTML template
  let projInfo =
    [ "page-description", "A typesetted F# Cheatsheet \
                           in PDF and HTML formats using F# literate tools."
      "page-title", "fsharp-cheatsheet"
      "page-author", "Anh-Dung Phan"
      "github-link", "https://github.com/dungpa/fsharp-cheatsheet"
      "project-name", "F# Cheatsheet" ]
      
  printfn "Generate index.html"
  Literate.ProcessMarkdown
      (sources @@ "fsharp-cheatsheet.md", template,
       output @@ "index.html", OutputKind.Html,
       includeSource = true, lineNumbers = false, replacements = projInfo)

let createPDF fileName =
    use p = new Process()
    // Assume that pdflatex is in the path
    p.StartInfo.FileName <- "pdflatex.exe"
    p.StartInfo.Arguments <- sprintf "-output-directory=%s %s" (Path.GetDirectoryName(fileName)) fileName
    p.StartInfo.UseShellExecute <- false
    p.StartInfo.RedirectStandardOutput <- false
    p.Start() |> ignore
    p.WaitForExit()
    for ext in ["aux"; "out"; "log"] do
        let auxFile = Path.ChangeExtension(fileName, ext)
        printfn "Delete auxiliary file: %s" auxFile
        File.Delete(auxFile)

let generatePDFDoc() =
  let template = source @@ "../templates/template-cheatsheet.tex"

  // These strings have to be well-formed in LaTeX 
  let projInfo = [ "project-name", "F\# Cheatsheet" ]

  printfn "Generate fsharp-cheatsheet.tex"
  Literate.ProcessMarkdown
      (sources @@ "fsharp-cheatsheet.md", template,
       output @@ "fsharp-cheatsheet.tex", OutputKind.Latex,
       includeSource = true, lineNumbers = false, replacements = projInfo)
  createPDF (output @@ "fsharp-cheatsheet.tex")


#time "on";;

copyFiles();;
generateHtmlDoc();;
generatePDFDoc();;
