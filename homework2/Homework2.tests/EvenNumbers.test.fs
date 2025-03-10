module Homework2.Tests

open NUnit.Framework
open FsUnit
open FsCheck
open Homework2.EvenNumbers

let testCases () =
    [ [1; 2; 3; 4; 5], 2
      [], 0
      [0; -2; -4; -6], 4
      [2; 4; 6; 8; 10], 5
      [1; 3; 5; 7; 9], 0
      [2147483646; 2147483647], 1 ]
    |> List.map TestCaseData

[<TestCaseSource(nameof testCases)>]
let ``countEvenNumbersByMap should return correct count`` list expected =
    list |> countEvenNumbersByMap |> should equal expected

[<TestCaseSource(nameof testCases)>]
let ``countEvenNumbersByFilter should return correct count`` list expected =
    list |> countEvenNumbersByFilter |> should equal expected

[<TestCaseSource(nameof testCases)>]
let ``countEvenNumbersByFold should return correct count`` list expected =
    list |> countEvenNumbersByFold |> should equal expected

[<Test>]
let ``All implementations should return the same result`` () =
    let prop (list: int list) =
        let mapResult = countEvenNumbersByMap list
        let filterResult = countEvenNumbersByFilter list
        let foldResult = countEvenNumbersByFold list
        mapResult = filterResult && filterResult = foldResult
    Check.QuickThrowOnFailure prop
