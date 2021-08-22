open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let rng = Random 123

let aIndexCount = 1_000
let aMaxIndex = 10_000
let bIndexCount = 1_000
let bMaxIndex = 10_000

let aIndices =
    let x = 
        [|for _ in 1 .. aIndexCount -> rng.Next (0, aMaxIndex)|]
        |> Array.distinct
    Array.sortInPlace x
    x

let bIndices =
    let x = 
        [|for _ in 1 .. bIndexCount -> rng.Next (0, bMaxIndex)|]
        |> Array.distinct
    Array.sortInPlace x
    x

let aValues = [| for _ in 1 .. aIndices.Length -> rng.NextDouble () |]
let bValues = [| for _ in 1 .. bIndices.Length -> rng.NextDouble () |]

let hadamardProduct (aIndices: Span<int>, aValues: Span<float>, bIndices: Span<int>, bValues: Span<float>) =
    let maxN = Math.Max (aIndices.Length, bIndices.Length)
    let outIndices = Array.zeroCreate maxN
    let outValues = Array.zeroCreate maxN

    let mutable aIdx = 0
    let mutable bIdx = 0
    let mutable outIdx = 0

    while aIdx < aIndices.Length && bIdx < bIndices.Length do
        
        if aIndices.[aIdx] = bIndices.[bIdx] then
            outIndices.[outIdx] <- aIndices.[aIdx]
            outValues.[outIdx] <- aValues.[aIdx] * bValues.[bIdx]
            outIdx <- outIdx + 1
            aIdx <- aIdx + 1
            bIdx <- bIdx + 1
        elif aIndices.[aIdx] < bIndices.[bIdx] then
            aIdx <- aIdx + 1
        else
            bIdx <- bIdx + 1

    let resultIndices = Memory (outIndices, 0, outIdx)
    let resultValues = Memory (outValues, 0, outIdx)

    resultIndices, resultValues


type Benchmarks () =

    [<Benchmark>]
    member _.Test () =
        hadamardProduct (aIndices.AsSpan(), aValues.AsSpan(), bIndices.AsSpan(), bValues.AsSpan())

[<EntryPoint>]
let main argv =
    let summary = BenchmarkRunner.Run<Benchmarks>()
    0 // return an integer exit code