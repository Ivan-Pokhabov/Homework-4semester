module Homework2CalculationAstTests

open NUnit.Framework
open FsUnit
open FsCheck
open Homework2CalculationAst

let testCases () =
    [ Number 5, 5
      Add (Number 2, Number 3), 5
      Subtract (Number 10, Number 3), 7
      Multiply (Number 4, Number 3), 12
      Divide (Number 8, Number 2), 4
      Add (Multiply (Number 2, Number 3), Number 4), 10
      Divide (Number 7, Number 3), 2
      Divide (Number 10, Number 1), 10
      Multiply (Number 30000, Number 30000), 900000000 ]

    |> List.map TestCaseData

[<TestCaseSource(nameof testCases)>]
let ``eval should return correct results`` expr expected =
    eval expr |> should equal expected

[<Test>]
let ``Addition should be commutative`` () =
    Check.QuickThrowOnFailure (fun (a:int) (b:int) ->
        eval (Add (Number a, Number b)) = eval (Add (Number b, Number a)))

[<Test>]
let ``Multiplication should be commutative`` () =
    Check.QuickThrowOnFailure (fun (a:int) (b:int) ->
        eval (Multiply (Number a, Number b)) = eval (Multiply (Number b, Number a)))

[<Test>]
let ``Multiplication should distribute over addition`` () =
    Check.QuickThrowOnFailure (fun (a:int) (b:int) (c:int) ->
        let left = eval (Multiply (Number a, Add (Number b, Number c)))
        let right = eval (Add (Multiply (Number a, Number b), Multiply (Number a, Number c)))
        left = right)
