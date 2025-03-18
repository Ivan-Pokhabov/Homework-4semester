module Homework4.tests

open NUnit.Framework
open FsCheck
open Homework4.PointFree

[<Test>]
let ``All implementations should return the same result`` () =
    let isCorrect x ls = func'1 x ls = func'2 x ls
    let isCorrect2 x ls = func'2 x ls = func'3 x ls
    let isCorrect3 x ls = func'3 x ls = func'4 x ls
    let isCorrect4 x ls = func'1 x ls = func'4 x ls

    Check.QuickThrowOnFailure isCorrect
    Check.QuickThrowOnFailure isCorrect2
    Check.QuickThrowOnFailure isCorrect3
    Check.QuickThrowOnFailure isCorrect4