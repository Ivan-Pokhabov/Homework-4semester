module Homework2.PrimeNumbers.Tests

open NUnit.Framework
open FsUnit
open Homework2.PrimeNumbers

[<Test>]
let ``isPrime should return true for prime numbers`` () =
    let primeNumbers = [2; 3; 5; 7; 11; 13; 17; 19; 23]
    primeNumbers |> List.iter (fun n -> isPrime n |> should equal true)

[<Test>]
let ``isPrime should return false for non-prime numbers`` () =
    let nonPrimeNumbers = [4; 6; 8; 9; 10; 12; 14; 15; 16]
    nonPrimeNumbers |> List.iter (fun n -> isPrime n |> should equal false)

[<Test>]
let ``isPrime should return false for numbers less than 2`` () =
    let nonPrimeNumbers = [0; 1; -1; -5]
    nonPrimeNumbers |> List.iter (fun n -> isPrime n |> should equal false)

[<Test>]
let ``getPrime should produce first 20 primes correctly`` () =
    let expectedPrimes =
        [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71 ]

    let primes = getPrime () |> Seq.take 20 |> Seq.toList
    primes |> should equal expectedPrimes

