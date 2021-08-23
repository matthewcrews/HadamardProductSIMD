module HadamardProduct

#nowarn "9" "51" "20" // Don't want warnings about pointers

open System
open FSharp.NativeInterop
open System.Numerics
open System.Runtime.Intrinsics.X86
open System.Runtime.Intrinsics

let hadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    let maxN = Math.Min (aKeys.Length, bKeys.Length)
    let outKeys = Array.zeroCreate maxN
    let outValues = Array.zeroCreate maxN

    let mutable aIdx = 0
    let mutable bIdx = 0
    let mutable outIdx = 0
    let lastBlockIdx = bKeys.Length - (bKeys.Length % 4)
    let bPointer = && (bKeys.GetPinnableReference ())

    while aIdx < aKeys.Length && bIdx < lastBlockIdx do
        let aVector = Vector128.Create aKeys.[aIdx]
        let bVector = Sse2.LoadVector128 (NativePtr.add bPointer bIdx)
        let comparison = Sse2.CompareEqual (aVector, bVector)
        let matches = Sse2.MoveMask (comparison.AsByte ())

        if matches > 0 then
            let bIdxOffset = (BitOperations.TrailingZeroCount matches) / 4 // Convert byte offset to index
            outKeys.[outIdx] <- aKeys.[aIdx]
            outValues.[outIdx] <- aValues.[aIdx] * bValues.[bIdx + bIdxOffset]
            aIdx <- aIdx + 1
            outIdx <- outIdx + 1
            // REMEMBER, bIdx is testing 4 values at a time so we don't always want to jump

        elif aKeys.[aIdx] > bKeys.[bIdx + 3] then
            // REMEMBER!! bIdx needs to stride, not increment
            bIdx <- bIdx + 4
        else
            aIdx <- aIdx + 1

    // Final pass for data that didn't fit the Vector128 size
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