#I "packages/FSharp.Formatting.1.0.15/lib/net40"
#load "packages/FSharp.Formatting.1.0.15/literate/literate.fsx"

open System.IO
open FSharp.Literate

let generateHtmlDoc() =
  let source = __SOURCE_DIRECTORY__
  let template = Path.Combine(source, "../templates/template-project.html")
  let sources = Path.Combine(source, "../docs")
  let output = Path.Combine(source, "../docs")

  // Additional strings to be replaced in the HTML template
  let projInfo =
    [ "page-description", "A typesetted F# Cheatsheet \
                           in PDF and HTML formats using F# literate tools."
      "page-author", "Anh-Dung Phan"
      "github-link", "https://github.com/dungpa/fsharp-cheatsheet"
      "project-name", "F# Cheatsheet" ]
      
  printfn "Generate index.html"
  Literate.ProcessMarkdown
      (Path.Combine(sources, "fsharp-cheatsheet.md"), template,
       Path.Combine(output, "index.html"), OutputKind.Html,
       includeSource = true, lineNumbers = false, replacements = projInfo)

let generateLatexDoc() =
  let source = __SOURCE_DIRECTORY__
  let template = Path.Combine(source, "../templates/template-cheatsheet.tex")
  let sources = Path.Combine(source, "../docs")
  let output = Path.Combine(source, "../docs")

  // These strings have to be well-formed in LaTEX 
  let projInfo = [ "project-name", "F\# Cheatsheet" ]

  printfn "Generate fsharp-cheatsheet.tex"
  Literate.ProcessMarkdown
      (Path.Combine(sources, "fsharp-cheatsheet.md"), template,
       Path.Combine(output, "fsharp-cheatsheet.tex"), OutputKind.Latex,
       includeSource = true, lineNumbers = false, replacements = projInfo)

#time "on";;

generateHtmlDoc();;
generateLatexDoc();;
