module Homework2PrimeNumbers

let isPrime number = 
    if number < 2 then
        false
    else
        let root = float number |> sqrt |> int
        seq { 2 .. root } |> Seq.forall (fun i -> number % i <> 0)

let getPrime() =
    Seq.initInfinite ((+) 2)
    |> Seq.filter isPrime