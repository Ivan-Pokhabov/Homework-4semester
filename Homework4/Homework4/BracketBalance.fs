module Homework4.BracketBalance

let private brackets = Map.ofList ['[', ']'; '{', '}'; '(', ')']

let private isMatchingBracket stackChar closeChar =
    match Map.tryFind stackChar brackets with
    | Some expectedClose -> expectedClose = closeChar
    | None -> false

let isBalanced (input: string) =
    let rec check stack bracketString =
        match bracketString with
        | [] -> List.isEmpty stack
        | head::tail when Map.containsKey head brackets -> 
            check (head::stack) tail
        | head::tail ->
            match stack with
            | [] -> false
            | stackHead::stackTail -> 
                if isMatchingBracket stackHead head then
                    check stackTail tail
                else
                    false

    check [] (List.ofSeq input)