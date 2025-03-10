module Homework2.CalculationAST

type AST =
    | Number of int
    | Add of AST * AST
    | Subtract of AST * AST
    | Multiply of AST * AST
    | Divide of AST * AST

let rec eval expr =
    match expr with
    | Number x -> x
    | Add (left, right) -> eval left + eval right
    | Subtract (left, right) -> eval left - eval right
    | Multiply (left, right) -> eval left * eval right
    | Divide (left, right) -> eval left / eval right