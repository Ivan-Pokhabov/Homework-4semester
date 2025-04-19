module Lazy
open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a

type LazySimple<'a>(supplier: unit -> 'a) =
        let mutable result = None
        interface ILazy<'a> with
            member _.Get() =
                match result with
                | Some value -> value
                | None ->
                    let computed = supplier()
                    result <- Some computed
                    computed

type LazyConcurrent<'a>(supplier: unit -> 'a) =
    let mutable result = None
    let lockObj = obj()
    interface ILazy<'a> with
        member _.Get() =
            match result with
            | Some value -> value
            | None ->
                lock lockObj (fun () ->
                    match result with
                    | Some value -> value
                    | None ->
                        let computed = supplier()
                        result <- Some computed
                        computed
                )

type LazyLockFree<'a>(supplier: unit -> 'a) =
    let mutable result = null
    interface ILazy<'a> with
        member _.Get() =
            let current = result
            if current <> null then
                unbox current
            else
                let computed = supplier()
                let boxed = box computed
                let original = Interlocked.CompareExchange(&result, boxed, null)
                if original = null then
                    computed
                else
                    unbox original