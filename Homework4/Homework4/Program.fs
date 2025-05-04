module Program

open System

open Homework4.Phonebook

let displayMenu() =
    printfn "Phone Book Menu"
    printfn "1. Add Contact"
    printfn "2. Search Contact"
    printfn "3. Delete Contact"
    printfn "4. Display All Contacts"
    printfn "5. Help"
    printfn "6. Read from file"
    printfn "7. Write to file"
    printfn "8. Exit"

let rec main phonebook =
    displayMenu()
    printf "Enter your choice: "
    let command = Console.ReadLine()

    match command with
    | "1" ->
        printf "Enter the contact name: "
        let name = Console.ReadLine()
        printf "Enter the contact phone: "
        let phone = Console.ReadLine()
        let newContact = Contact(name, phone)
        printfn "Contact added successfully.\n"
        main (addContact (newContact, phonebook))

    | "2" ->
        printf "Enter the contact name to search: "
        let name = Console.ReadLine()
        match findByName(name, phonebook) with
        | Some phone -> printfn "Contact found: %s - %s\n" name phone
        | None -> printfn "Contact not found.\n"
        main phonebook

    | "3" ->
        printf "Enter the contact phone to search: "
        let phone = Console.ReadLine()
        match findByPhone(phone, phonebook) with
        | Some name -> printfn "Contact found: %s - %s\n" name phone
        | None -> printfn "Contact not found.\n"
        main phonebook

    | "4" ->
        printfn "All contacts:"
        getPhonebookData phonebook
        |> List.iter (fun (name, phone) -> printfn "%s: %s" name phone)
        printfn ""
        main phonebook

    | "5" ->
        displayMenu()
        main phonebook

    | "6" ->
        printf "Enter the file path: "
        let path = Console.ReadLine()
        match readFromFile path with
        | Some newPhonebook ->
            printfn "Contacts read from file.\n"
            main newPhonebook
        | None ->
            printfn "Failed to read contacts from file.\n"
            main phonebook

    | "7" ->
        printf "Enter the file path: "
        let path = Console.ReadLine()
        storeToFile(path, phonebook)
        printfn "Contacts written to file.\n"
        main phonebook

    | "8" ->
        printfn "Exiting..."

    | _ ->
        printfn "Invalid choice. Please try again.\n"
        main phonebook

displayMenu()

main (Phonebook [])