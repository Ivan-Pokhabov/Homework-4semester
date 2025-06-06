module Homework2EvenNumbers

//Function for counting even numbers in list by map
let countEvenNumbersByMap list =
    list |> List.map (fun number -> if number % 2 = 0 then 1 else 0) |> List.sum

//Function for counting even numbers in list by filter
let countEvenNumbersByFilter list =
    list |> List.filter (fun number -> number % 2 = 0) |> List.length

//Function for counting even numbers in list by fold
let countEvenNumbersByFold list =
    list |> List.fold (fun acc number -> if number % 2 = 0 then acc + 1 else acc) 0