#r "nuget: Expecto, 9.0.4"
#load "nfdi-header-extension.fsx"
#load "sidebar-header-extension.fsx"

// put this into separate .fsx file to not run tests everytime file is referenced

open Expecto
open ``Nfdi-header-extension``
open ``Sidebar-header-extension``

let allTests = testList "" [
    ``Nfdi-header-extension``.Testing.tests
    ``Sidebar-header-extension``.Testing.tests
]

runTestsWithCLIArgs [] [||] allTests
