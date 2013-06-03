#I "packages/FSharp.Formatting.1.0.15/lib/net40"
#load "packages/FSharp.Formatting.1.0.15/literate/literate.fsx"

open System.IO
open FSharp.Literate

let generateLatexDoc() =
  let source = __SOURCE_DIRECTORY__
  let template = Path.Combine(source, "./templates/template-cheatsheet.tex")
  let sources = Path.Combine(source, ".")
  let output = Path.Combine(source, ".")

  Literate.ProcessMarkdown
      ( Path.Combine(source, "fsharp-cheatsheet.md"), template,
        Path.Combine(output, "fsharp-cheatsheet.tex"), OutputKind.Latex,
        includeSource = true)

#time "on";;

generateLatexDoc()
