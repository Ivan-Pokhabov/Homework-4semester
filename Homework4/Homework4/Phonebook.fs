module Homework4.Phonebook

type Contact = Contact of string * string

type Phonebook = Phonebook of Contact list

let addContact (contact, Phonebook phonebook) =
    Phonebook (contact::phonebook)

let findByPhone(phone, Phonebook phonebook) =
    phonebook
    |> List.tryFind (fun (Contact (_, contactPhone)) -> contactPhone = phone)
    |> Option.map (fun (Contact (name, _)) -> name)

let findByName(name, Phonebook phonebook) =
    phonebook
    |> List.tryFind (fun (Contact (contactName, _)) -> contactName = name)
    |> Option.map (fun (Contact (_, phone)) -> phone)

let getPhonebookData(Phonebook phonebook) =
    phonebook
    |> List.map (fun (Contact (name, phone)) -> name, phone)

let readFromFile path =
    match System.IO.File.Exists path with
    | false -> None
    | true ->
        System.IO.File.ReadAllLines path
        |> Array.toList
        |> List.map (fun line -> 
            let parts = line.Split(':')
            Contact(parts.[0], parts.[1])
            )
        |> Phonebook
        |> Some

let storeToFile(path, phonebook) =
    let lines = List.map (fun (name, phone) -> sprintf "%s:%s" name phone) (getPhonebookData phonebook)
    System.IO.File.WriteAllLines(path, List.toArray lines)