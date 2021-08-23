open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let rng = Random 123

let aIndexCount = 10_000
let aMaxIndex = 100_000
let bIndexCount = 10_000
let bMaxIndex = 100_000

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
    member _.Scalar () =
        Scalar.hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())

    [<Benchmark>]
    member _.SSE () =
        SSE.hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())

    [<Benchmark>]
    member _.AVX () =
        AVX.hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())



[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    0 // return an integer exit code