module Homework4.PointFree


let func'1 x l =
    List.map (fun y -> y * x) l

let func'2 x =
    List.map (fun y -> y * x)

let func'3 x =
    List.map (( * ) x)

let func'4 : int -> int list -> int list =
    List.map << ( * )