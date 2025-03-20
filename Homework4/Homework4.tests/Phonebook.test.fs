module Homework4.Phonebook.Tests

open NUnit.Framework
open FsUnit
open Homework4.Phonebook

[<Test>]
let ``addContact should add a contact to the phonebook`` () =
    let initialPhonebook = Phonebook []
    let newContact = Contact("Alice", "123-456-789")

    let updatedPhonebook = addContact (newContact, initialPhonebook)

    match updatedPhonebook with
    | Phonebook contacts ->
        contacts |> should haveLength 1
        contacts.Head |> should equal newContact

[<Test>]
let ``findByPhone should return the correct contact name`` () =
    let phonebook = Phonebook [Contact("Alice", "123-456-789"); Contact("Bob", "987-654-321")]

    let result = findByPhone("123-456-789", phonebook)

    result |> should equal (Some "Alice")

[<Test>]
let ``findByPhone should return None if phone is not found`` () =
    let phonebook = Phonebook [Contact("Alice", "123-456-789")]

    let result = findByPhone("000-000-000", phonebook)

    result |> should equal None

[<Test>]
let ``findByName should return the correct phone number`` () =
    let phonebook = Phonebook [Contact("Alice", "123-456-789"); Contact("Bob", "987-654-321")]

    let result = findByName("Alice", phonebook)

    result |> should equal (Some "123-456-789")

[<Test>]
let ``findByName should return None if name is not found`` () =
    let phonebook = Phonebook [Contact("Alice", "123-456-789")]

    let result = findByName("Bob", phonebook)

    result |> should equal None

[<Test>]
let ``getPhonebookData should return a list of name-phone tuples`` () =
    let phonebook = Phonebook [Contact("Alice", "123-456-789"); Contact("Bob", "987-654-321")]

    let result = getPhonebookData phonebook

    result |> should equal ["Alice", "123-456-789"; "Bob", "987-654-321"]

[<Test>]
let ``readFromFile should return None if file does not exist`` () =
    let nonExistentPath = "non_existent_file.txt"

    let result = readFromFile nonExistentPath

    result |> should equal None

[<Test>]
let ``readFromFile should return a Phonebook if file exists and is valid`` () =
    let path = "test_contacts.txt"
    System.IO.File.WriteAllLines(path, [|"Alice:123-456-789"; "Bob:987-654-321"|])

    let result = readFromFile path

    match result with
    | Some (Phonebook contacts) ->
        contacts |> should haveLength 2
        contacts |> should contain (Contact("Alice", "123-456-789"))
        contacts |> should contain (Contact("Bob", "987-654-321"))
    | None -> failwith "Expected Some Phonebook, but got None"

    System.IO.File.Delete path

[<Test>]
let ``storeToFile should write contacts to a file`` () =
    let path = "output_contacts.txt"
    let phonebook = Phonebook [Contact("Alice", "123-456-789"); Contact("Bob", "987-654-321")]

    storeToFile(path, phonebook)

    let lines = System.IO.File.ReadAllLines path
    lines |> should equal [|"Alice:123-456-789"; "Bob:987-654-321"|]

    System.IO.File.Delete path