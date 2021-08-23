module Scalar

open System


let private auxHadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    // We know that there will be fewer a Key/Value pairs than b
    let outKeys = Array.zeroCreate aKeys.Length
    let outValues = Array.zeroCreate aKeys.Length

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

let hadamardProduct (aKeys: Span<int>, aValues: Span<float>, bKeys: Span<int>, bValues: Span<float>) =
    if aKeys.Length < bKeys.Length then
        auxHadamardProduct (aKeys, aValues, bKeys, bValues)
    else
        auxHadamardProduct (bKeys, bValues, aKeys, aValues)
        