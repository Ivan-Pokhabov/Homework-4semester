module Homework2.MapTree.Tests

open NUnit.Framework
open FsUnit
open Homework2.MapTree

[<Test>]
let ``mapTree should correctly map Node value`` () =
    let tree = Node(5, Leaf, Leaf)
    let result = mapTree (fun x -> x * 2) tree
    result |> should equal (Node(10, Leaf, Leaf))

[<Test>]
let ``mapTree should correctly map Node value in larger tree`` () =
    let tree = Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf))
    let result = mapTree (fun x -> x * 2) tree
    result |> should equal (Node(2, Node(4, Leaf, Leaf), Node(6, Leaf, Leaf)))

[<Test>]
let ``mapTree should apply function to all nodes in the tree`` () =
    let tree = Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf))
    let result = mapTree (fun x -> x + 1) tree
    result |> should equal (Node(2, Node(3, Leaf, Leaf), Node(4, Leaf, Leaf)))
