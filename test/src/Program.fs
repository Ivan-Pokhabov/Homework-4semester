module Test

// Infinity sequence of numbers 1, -1, 1, ..., (-1)^i
let alternatingSeq = Seq.initInfinite (fun i -> pown -1 i)

// Function that generate infinity sequense of numbers 1, -2, 3, ..., i * (-1)^i
let targetSequence() = Seq.map2 (*) (Seq.initInfinite ((+) 1)) alternatingSeq


// Strcut of binary tree
type BinaryTree<'a> =
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Leaf

// Function that gets binary tree values, that satisfy predicate
let getTreeSpecialValues predicate tree =
    let rec getNodeSpecialValues tree acc =
        match tree with
        | Leaf -> acc
        | Node(value, left, right) ->
            let newAcc = if predicate value then value::acc else acc
            getNodeSpecialValues left newAcc |> getNodeSpecialValues right
    getNodeSpecialValues tree []


// Implementation of priority queue
type PriorityQueue<'T, 'P when 'P : comparison>() =
    let mutable elements: List<'P * 'T> = []

    // Method for adding new value to queue
    member _.Enqueue(prior: 'P, value: 'T) =
        let rec insert item lst =
            match lst with
            | [] -> [item]
            | (p, _)::_ when prior < p -> item :: lst
            | head::tail -> head :: insert item tail
        elements <- insert (prior, value) elements

    // Method for getting value with lowest priority from queue
    member _.Dequeue() =
        match elements with
        | [] -> raise <| System.InvalidOperationException "Empty queue"
        | (_, v)::tail ->
            elements <- tail
            v