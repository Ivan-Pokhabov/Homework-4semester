namespace Homework5

// Some implemetations of operating system types
type Linux(?injectionChance: int) =
    interface IOperatingSystem with
        member val Name = "Linux" with get
        member val InjectionChance = defaultArg injectionChance 30 with get


type MacOS(?injectionChance: int) =
    interface IOperatingSystem with
        member val Name = "MacOS" with get
        member val InjectionChance = defaultArg injectionChance 10 with get


type Windows(?injectionChance: int) =
    interface IOperatingSystem with
        member val Name = "Windows" with get
        member val InjectionChance = defaultArg injectionChance 70 with get


type LolOS(?injectionChance: int) = 
    interface IOperatingSystem with
        member val Name = "LolOS" with get
        member val InjectionChance = defaultArg injectionChance 100 with get 