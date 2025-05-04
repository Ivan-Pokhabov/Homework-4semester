module Homework2CalculationAst

// Struct of abstraction syntax tree
type Ast =
    | Number of int
    | Add of Ast * Ast
    | Subtract of Ast * Ast
    | Multiply of Ast * Ast
    | Divide of Ast * Ast

// Function of evaluating syntax tree expression
let rec eval expr =
    match expr with
    | Number x -> x
    | Add (left, right) -> eval left + eval right
    | Subtract (left, right) -> eval left - eval right
    | Multiply (left, right) -> eval left * eval right
    | Divide (left, right) -> eval left / eval right