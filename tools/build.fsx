// --------------------------------------------------------------------------------------
// FAKE build script 
// --------------------------------------------------------------------------------------

#r @"packages/FAKE/tools/FakeLib.dll"
open Fake 
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open System

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted 
let gitHome = "https://github.com/dungpa"
// The name of the project on GitHub
let gitName = "fsharp-cheatsheet"

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

// --------------------------------------------------------------------------------------
// Clean build results & restore NuGet packages

Target "Clean" (fun _ ->
    CleanDirs ["temp"]
)

Target "RestorePackages" (fun _ ->
    !! "./**/packages.config"
    |> Seq.iter (RestorePackage (fun p -> { p with ToolPath = "./.nuget/NuGet.exe" }))
)

// --------------------------------------------------------------------------------------
// Generate the documentation

Target "GenerateDocs" (fun _ ->
    executeFSIWithArgs __SOURCE_DIRECTORY__ "generate.fsx" ["--define:RELEASE"] [] |> ignore
)

// --------------------------------------------------------------------------------------
// Release Scripts

Target "ReleaseDocs" (fun _ ->
    let ghPages      = "gh-pages"
    let ghPagesLocal = "temp/gh-pages"
    let output = "../docs/output/"
    ensureDirectory "temp"
    Repository.clone "temp" (gitHome + "/" + gitName + ".git") ghPages
    Branches.checkoutBranch ghPagesLocal ghPages
    CopyRecursive output ghPagesLocal true |> printfn "%A"
    CommandHelper.runSimpleGitCommand ghPagesLocal "add ." |> printfn "%s"
    let cmd = """commit -a -m "Update generated documentation from script" """
    CommandHelper.runSimpleGitCommand ghPagesLocal cmd |> printfn "%s"
    Branches.push ghPagesLocal
)

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target "All" DoNothing

"Clean"
  ==> "RestorePackages"
  ==> "All"

"All" 
  ==> "GenerateDocs"
  ==> "ReleaseDocs"

RunTargetOrDefault "GenerateDocs"