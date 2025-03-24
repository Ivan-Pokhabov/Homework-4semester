namespace Homework5.Tests

open NUnit.Framework
open Moq
open System.Collections.Generic
 open Homework5

module LocalNetworkTests =
    [<Test>]
    let ``Step should infect adjacent computers`` () =
        let mockOs = Mock<IOperatingSystem>()
        mockOs.Setup(fun os -> os.InjectionChance).Returns 100 |> ignore

        let computer1 = Computer(1, mockOs.Object)
        let computer2 = Computer(2, mockOs.Object)
        let network = Dictionary<Computer, Computer list>()
        network.Add(computer1, [computer2])
        network.Add(computer2, [])

        let localNetwork = LocalNetwork network
        computer1.IsInjected <- true

        let result = localNetwork.Step()

        Assert.That(result, Is.True)
        Assert.That(computer2.IsInjected, Is.True)

    [<Test>]
    let ``Step should return false if no new injections`` () =
        let mockOs = Mock<IOperatingSystem>()
        mockOs.Setup(fun os -> os.InjectionChance).Returns 0 |> ignore

        let computer1 = Computer(1, mockOs.Object)
        let computer2 = Computer(2, mockOs.Object)
        let network = Dictionary<Computer, Computer list>()
        network.Add(computer1, [computer2])
        network.Add(computer2, [])

        let localNetwork = LocalNetwork network
        computer1.IsInjected <- true

        let result = localNetwork.Step()

        Assert.That(result, Is.True)
        Assert.That(computer2.IsInjected, Is.False)

    [<Test>]
    let ``GetStatus should return correct injection status`` () =
        let mockOs = Mock<IOperatingSystem>()
        mockOs.Setup(fun os -> os.InjectionChance).Returns 100 |> ignore

        let computer1 = Computer(1, mockOs.Object)
        let computer2 = Computer(2, mockOs.Object)
        let network = Dictionary<Computer, Computer list>()
        network.Add(computer1, [computer2])
        network.Add(computer2, [])

        let localNetwork = LocalNetwork network
        computer1.IsInjected <- true

        let statuses = localNetwork.GetStatus()

        Assert.That(statuses, Is.EqualTo [true; false])

    [<Test>]
    let ``Step should not infect when chance is 0`` () =
        let os = Windows 0 :> IOperatingSystem
        let computer1 = Computer(1, os)
        let computer2 = Computer(2, os)
        let network = Dictionary<Computer, Computer list>()
        network.Add(computer1, [computer2])
        network.Add(computer2, [])

        let localNetwork = LocalNetwork network
        computer1.IsInjected <- true

        let result = localNetwork.Step()

        Assert.That(result, Is.True)
        Assert.That(computer2.IsInjected, Is.False)

    [<Test>]
    let ``All computers should eventually get infected`` () =
        let os = LolOS 100 :> IOperatingSystem
        let computer1 = Computer(1, os)
        let computer2 = Computer(2, os)
        let computer3 = Computer(3, os)
        let network = Dictionary<Computer, Computer list>()
        network.Add(computer1, [computer2; computer3])
        network.Add(computer2, [computer3])
        network.Add(computer3, [])

        let localNetwork = LocalNetwork network
        computer1.IsInjected <- true
        localNetwork.StartNetwork()

        let statuses = localNetwork.GetStatus()
        Assert.That(statuses, Is.EqualTo [true; true; true])

