module Homework2.EvenNumbers

let CountEvenNumbersByMap list =
    list |> List.map (fun number -> if number % 2 = 0 then 1 else 0) |> List.sum

let CountEvenNumbersByFilter list =
    list |> List.filter (fun number -> if number % 2 = 0 then true else false) |> List.length

let CountEvenNumbersByFold list =
    list |> List.fold (fun acc number -> if number % 2 = 0 then acc + 1 else acc)