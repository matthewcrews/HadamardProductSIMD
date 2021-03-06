module AVX

#nowarn "9" "51" "20" // Don't want warnings about pointers

open System
open FSharp.NativeInterop
open System.Numerics
open System.Runtime.Intrinsics.X86
open System.Runtime.Intrinsics

let private auxHadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    // We know that there will be fewer a Key/Value pairs than b
    let outKeys = Array.zeroCreate aKeys.Length
    let outValues = Array.zeroCreate aKeys.Length

    let mutable aIdx = 0
    let mutable bIdx = 0
    let mutable outIdx = 0

    if bKeys.Length > 8 then

        let bPointer = && (bKeys.GetPinnableReference ())
        let mutable bVector = Avx2.LoadVector256 (NativePtr.add bPointer bIdx)
        let lastBlockIdx = bKeys.Length - (bKeys.Length % Vector256.Count)

        while aIdx < aKeys.Length && bIdx < lastBlockIdx do
            let aVector = Vector256.Create aKeys.[aIdx]
            let comparison = Avx2.CompareEqual (aVector, bVector)
            let matches = Avx2.MoveMask (comparison.AsByte ())

            if matches > 0 then
                let bIdxOffset = (BitOperations.TrailingZeroCount matches) / 4 // Convert byte offset to index
                outKeys.[outIdx] <- aKeys.[aIdx]
                outValues.[outIdx] <- aValues.[aIdx] * bValues.[bIdx + bIdxOffset]
                aIdx <- aIdx + 1
                outIdx <- outIdx + 1
                // REMEMBER, bIdx is several values at a time so we don't always want to jump

            elif aKeys.[aIdx] > bKeys.[bIdx + Vector256.Count - 1] then
                // REMEMBER!! bIdx needs to stride, not increment
                bIdx <- bIdx + Vector256.Count
                // We only want to load new values when necessary
                if bIdx < lastBlockIdx then
                    bVector <- Avx2.LoadVector256 (NativePtr.add bPointer bIdx)
            else
                aIdx <- aIdx + 1

    // Final pass for data that didn't fit the Vector256 size
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


let hadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    if aKeys.Length < bKeys.Length then
        auxHadamardProduct (aKeys, aValues, bKeys, bValues)
    else
        auxHadamardProduct (bKeys, bValues, aKeys, aValues)