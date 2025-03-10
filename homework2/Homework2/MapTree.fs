module Homework2.MapTree

type BinaryTree<'a> =
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Leaf

let rec mapTree f tree =
    match tree with
    | Leaf -> Leaf
    | Node(value, left, right) ->
        Node(f value, mapTree f left, mapTree f right)