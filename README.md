# Hadamard Product with SIMD

This is a testbed for trying out the acceleration of taking the Hadamard Product of two data sets using SIMD. The current algorithm is the scalar version. As I learn assembly, SSE, and AVX I will try new approaches.

The StackOverflow question for approaches is [here](https://stackoverflow.com/questions/68884225/simd-instructions-to-accelerate-search-of-int-array).

Current performance results:

| Method |     Mean |    Error |   StdDev |
|------- |---------|---------|---------|
| Scalar | 78.89 us | 1.342 us | 1.436 us |
|    SSE | 32.50 us | 0.391 us | 0.347 us |
|    AVX | 25.22 us | 0.109 us | 0.096 us |
