module Homework2.EvenNumbers

let countEvenNumbersByMap list =
    list |> List.map (fun number -> if number % 2 = 0 then 1 else 0) |> List.sum

let countEvenNumbersByFilter list =
    list |> List.filter (fun number -> if number % 2 = 0 then true else false) |> List.length

let countEvenNumbersByFold list =
    list |> List.fold (fun acc number -> if number % 2 = 0 then acc + 1 else acc) 0