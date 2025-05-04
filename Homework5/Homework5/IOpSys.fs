namespace Homework5

// Operating system interafce
type public IOperatingSystem =
    // Gets Name of OS
    abstract member Name: string with get
    // Gets probability of injection
    abstract member InjectionChance: int with get