module BracketBalance.tests

open NUnit.Framework
open FsUnit
open Homework4.BracketBalance

let TestCases () = 
    [   
        "", true
        "[{()}]", true
        "[{()}", false
        "[{(", false
        "})]", false
        "({[)})", false
        "({[]){[()]})", false
        "({[]})", true
        "({[}])", false
        "{[(])}", false
        "", true
        "()[]{}", true
    ]
    |> List.map (fun (input, expected) -> TestCaseData(input, expected))

[<TestCaseSource("TestCases")>]
let ``String balance test`` (input, expected) =
    input |> isBalanced |> should equal expected