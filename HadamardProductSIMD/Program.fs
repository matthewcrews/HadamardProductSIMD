open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let rng = Random 123

let aIndexCount = 1_000
let aMaxIndex = 10_000
let bIndexCount = 1_000
let bMaxIndex = 10_000

let aKeys =
    let x = 
        [|for _ in 1 .. aIndexCount -> rng.Next (0, aMaxIndex)|]
        |> Array.distinct
    Array.sortInPlace x
    x

let bKeys =
    let x = 
        [|for _ in 1 .. bIndexCount -> rng.Next (0, bMaxIndex)|]
        |> Array.distinct
    Array.sortInPlace x
    x

let aValues = [| for _ in 1 .. aKeys.Length -> rng.NextDouble () |]
let bValues = [| for _ in 1 .. bKeys.Length -> rng.NextDouble () |]


type Benchmarks () =

    [<Benchmark>]
    member _.Test () =
        HadamardProduct.hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())

[<EntryPoint>]
let main argv =
    //let summary = BenchmarkRunner.Run<Benchmarks>()

    let aKeys = [|1 .. 2 .. 9|]
    let bKeys = [|1..9|]
    let aValues = [|1.0 .. 2.0 .. 9.0|]
    let bValues = [|1.0..9.0|]

    let r = HadamardProduct.hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())


    0 // return an integer exit code