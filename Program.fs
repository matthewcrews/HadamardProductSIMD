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

let hadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    let maxN = Math.Min (aKeys.Length, bKeys.Length)
    let outKeys = Array.zeroCreate maxN
    let outValues = Array.zeroCreate maxN

    let mutable aIdx = 0
    let mutable bIdx = 0
    let mutable outIdx = 0

    while aIdx < aKeys.Length && bIdx < bKeys.Length do
        
        if aKeys.[aIdx] = bKeys.[bIdx] then
            outKeys.[outIdx] <- aKeys.[aIdx]
            outValues.[outIdx] <- aValues.[aIdx] * bValues.[bIdx]
            outIdx <- outIdx + 1
            aIdx <- aIdx + 1
            bIdx <- bIdx + 1
        elif aKeys.[aIdx] < bKeys.[bIdx] then
            aIdx <- aIdx + 1
        else
            bIdx <- bIdx + 1

    let resultKeys = Memory (outKeys, 0, outIdx)
    let resultValues = Memory (outValues, 0, outIdx)

    resultKeys, resultValues


type Benchmarks () =

    [<Benchmark>]
    member _.Test () =
        hadamardProduct (aKeys.AsSpan(), aValues.AsSpan(), bKeys.AsSpan(), bValues.AsSpan())

[<EntryPoint>]
let main argv =
    let summary = BenchmarkRunner.Run<Benchmarks>()
    0 // return an integer exit code