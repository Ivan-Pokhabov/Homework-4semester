module LazyTests


open Lazy
open NUnit.Framework
open FsUnit
open System.Threading

[<TestFixture>]
type LazyTests() =

    let testSingleThread (getLazy: (unit -> 'a) -> ILazy<'a>) =
        let count = ref 0
        let supplier () = count.Value <- count.Value + 1; "computed"
        let lazyInstance = getLazy supplier


        Assert.AreEqual("computed", lazyInstance.Get())
        Assert.AreEqual("computed", lazyInstance.Get())
        Assert.AreEqual(1, !count)

    let testMultiThreaded () =
        let count = ref 0
        let supplier () =
            Interlocked.Increment count |> ignore
            Thread.Sleep 10
            obj()
        let lazyInst = LazyConcurrent supplier :> ILazy<obj>


        let results =
            Array.init 100 (fun _ -> async { return lazyInst.Get() })
            |> Async.Parallel
            |> Async.RunSynchronously
        let firstResult = results.[0]


        Assert.That(results |> Array.forall (fun r -> obj.ReferenceEquals(r, firstResult)))
        Assert.AreEqual(1, !count)

    [<Test>]
    member _.``LazySimple works in single-threaded mode``() =
        testSingleThread (fun s -> LazySimple s :> ILazy<_>)

    [<Test>]
    member _.``LazyConcurrent works in single-threaded mode``() =
        testSingleThread (fun s -> LazyConcurrent s :> ILazy<_>)

    [<Test>]
    member _.``LazyLockFree works in single-threaded mode``() =
        testSingleThread (fun s -> LazyLockFree s :> ILazy<_>)

    [<Test>]
    member _.``LazyConcurrent should be correct``() =
        testMultiThreaded()

    [<Test>]
    member _.``LazyLockFree should be correct``() =
        testMultiThreaded()

    [<Test>]
    member _.``LazyLockFree should compute multiple times``() =
        let count = ref 0
        let supplier () =
            Interlocked.Increment count |> ignore
            Thread.Sleep 100
            obj()
        let lazyInst = LazyLockFree supplier :> ILazy<obj>
        

        let results =
            Array.init 1000 (fun _ -> 
                async { 
                    return lazyInst.Get() 
                })
            |> Async.Parallel
            |> Async.RunSynchronously
        let firstResult = results.[0]
        

        Assert.That(results |> Array.forall (fun r -> obj.ReferenceEquals(r, firstResult)))
        Assert.That(!count, Is.GreaterThan 1)