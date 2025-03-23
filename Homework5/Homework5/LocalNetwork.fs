namespace Homework5

open System
open System.Collections.Generic

// Local network modeling class implemetation
type LocalNetwork(computersNetwork: Dictionary<Computer, Computer list>) =
    let mutable network = computersNetwork
    let computers = network.Keys |> Seq.toList

    // Gets adjency list of network
    member _.ComputersNetwork
        with get() = network
        and private set value = network <- value

    member private _.GetNotInjectedComputers() =
        computers
        |> List.filter (fun c -> c.IsInjected && network[c] |> List.exists (fun t -> not t.IsInjected))

    // Modeling next step of injecting
    member this.Step () =
        match this.GetNotInjectedComputers() with
        | [] -> false
        | computers ->
            computers
            |> List.collect (fun c -> network[c])
            |> List.iter (fun c -> c.TryInject Random.Shared)
            true

    // Starts network modeling
    member this.StartNetwork () =
        while this.Step() do
            computers |> List.iter (fun c -> printfn "Computer %d is injected: %b" c.ID c.IsInjected)

    // Gets network computers status
    member _.GetStatus () =
        computers |> List.map (fun c -> c.IsInjected)
