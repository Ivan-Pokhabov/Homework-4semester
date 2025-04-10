module Test

let alternatingSeq = Seq.initInfinite (fun i -> pown -1 i)

let targetSequence() = Seq.map2 (*) (Seq.initInfinite ((+) 1)) alternatingSeq



type BinaryTree<'a> =
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Leaf

let filterTree predicate tree =
    let rec filterNode tree acc =
        match tree with
        | Leaf -> acc
        | Node(value, left, right) ->
            let newAcc = if predicate value then value::acc else acc
            filterNode left newAcc |> filterNode right
    filterNode tree []



type PriorityQueue<'T, 'P when 'P : comparison>() =
    let mutable elements: List<'P * 'T> = []

    member _.Enqueue(prior: 'P, value: 'T) =
        let rec insert item lst =
            match lst with
            | [] -> [item]
            | (p, _)::_ when prior < p -> item :: lst
            | head::tail -> head :: insert item tail
        elements <- insert (prior, value) elements

    member _.Dequeue() =
        match elements with
        | [] -> raise <| System.InvalidOperationException "Empty queue"
        | (_, v)::tail ->
            elements <- tail
            v