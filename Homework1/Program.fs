module Homework1

/// Function to compute factorial of the given number
let factorial n =
    if n < 0 then None else

    let rec recFactorial n acc =
        match n with
        | 0 -> Some(acc)
        | _ -> recFactorial (n - 1) (n * acc)

    recFactorial n 1


/// Function to compute fibonacci number by its position
let fibonacci n =
    if n < 0 then None else
        
    let rec recFibonacci n cur next =
        match n with
        | 0 -> Some(cur)
        | _ -> recFibonacci (n - 1) next (cur + next)
        
    recFibonacci n 0 1


/// Function to reverse a list
let reverse list =
    let rec reverseAcc acc lst =
        match lst with
        | [] -> acc
        | head :: tail -> reverseAcc (head :: acc) tail
    reverseAcc [] list


/// Function to find the first occurrence of a given element in a list
let findFirst list x =
    let rec recFindFirst list idx =
        match list with
        | [] -> None
        | h :: t ->
        if h = x then Some(idx)
        else recFindFirst t (idx + 1)

    recFindFirst list 0

/// Function for binary exponentiation
let rec binpow num exp =
    match exp with
    | 0 -> 1
    | _ when exp % 2 = 0 -> let half = binpow num (exp / 2) in half * half
    | _ -> num * binpow num (exp - 1)


/// Function to find list of degrees from 2^n to 2^(n+m)
let powerList n m =
    let rec loop acc count currentValue =
        if count > m then
            acc
        else
            loop (currentValue :: acc) (count + 1) (currentValue * 2)
    let initialValue = binpow 2 n
    loop [] 1 initialValue |> reverse


