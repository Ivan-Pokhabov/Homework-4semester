namespace Homework5

open System

// Class implementation of computer
type public Computer(id: int, os: IOperatingSystem) =
    // Gets computer ID
    member val ID = id with get
    // Gets computer OS
    member val OS = os with get
    // Gets or sets computer injecting status
    member val IsInjected = false with set, get

    // Method that trying inject computer
    member this.TryInject(random: Random) =
        if not this.IsInjected && random.NextDouble() < this.OS.InjectionChance then
            this.IsInjected <- true