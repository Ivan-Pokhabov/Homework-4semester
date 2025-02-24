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
        if n < 0 then
            None
        else
            let rec recFibonacci n cur next =
                match n with
                | 0 -> Some(cur)
                | _ -> recFibonacci (n - 1) next (cur + next)
        
            recFibonacci n 0 1


/// Function to reverse a list
let rec reverse list =
    match list with
    | [] -> []
    | head :: tail -> (reverse tail) @ [head] 


/// Function to find the first occurrence of a given element in a list
let findFirst list x =
    let rec recFindFirst list idx =
        match list with
        | [] -> None
        | h :: t ->
        if h = x then Some(idx)
        else recFindFirst t (idx + 1)

    recFindFirst list 0


/// Function to find list of degrees from 2^n to 2^(n+m)
let powerList n m =
    List.scan (fun acc _ -> acc * 2) (pown 2 n) [1..m]


