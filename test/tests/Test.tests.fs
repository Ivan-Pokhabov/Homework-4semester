module Test.tests

open Test
open System
open NUnit.Framework
open FsUnit
open FsCheck

[<TestFixture>]
type PriorityQueueTests() =

    [<Test>]
    member _.``PriorityQueue returns values in correct order``() =
        let pq = PriorityQueue<string, int>()
        pq.Enqueue(3, "low")
        pq.Enqueue(1, "high")
        pq.Enqueue(2, "mid")

        let first = pq.Dequeue()
        let second = pq.Dequeue()
        let third = pq.Dequeue()

        Assert.AreEqual("high", first)
        Assert.AreEqual("mid", second)
        Assert.AreEqual("low", third)

    [<Test>]
    member _.``PriorityQueue throws when empty``() =
        let pq = PriorityQueue<int, int>()
        let ex = Assert.Throws<InvalidOperationException>(fun () -> pq.Dequeue() |> ignore)
        Assert.That(ex.Message, Is.EqualTo "Empty queue")


[<TestFixture>]
type TargetSequenceTests() =

    [<Test>]
    member _.``targetSequence generates alternating signed integers (FsCheck)``() =
        let property (n: int) =
            let i = abs n % 1000
            let expected = (i + 1) * pown -1 i
            let actual = targetSequence() |> Seq.item i
            expected = actual

        Check.QuickThrowOnFailure property


[<TestFixture>]
type TreeSpecialValuesTests() =

    [<Test>]
    member _.``getTreeSpecialValues returns correct values by predicate``() =
        let tree =
            Node(5,
                Node(2, Leaf, Leaf),
                Node(8,
                    Node(6, Leaf, Leaf),
                    Node(10, Leaf, Leaf)
                )
            )
        let result = getTreeSpecialValues (fun x -> x % 2 = 0) tree
        Assert.That(result, Is.EquivalentTo [2; 6; 8; 10])
