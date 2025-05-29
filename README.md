# SubsetSumAsyncSolver

A high-performance hybrid solver for the **Subset Sum Problem**, combining dynamic programming, branch-and-bound, Meet-in-the-Middle, and modern .NET features like:

- ✅ `async/await` support for scalable async execution
- ✅ `System.Buffers.MemoryPool<T>` to minimize GC pressure
- ✅ `System.IO.Pipelines` for potential high-throughput I/O
- ✅ Parallel pruning and memoization
- ✅ GCD checks, suffix sum optimizations, adaptive strategy switching

---

## 🔍 Problem

Determine whether a subset of a given array of integers sums to a target value `T`.

This is a classic **NP-complete** problem.

---

## 🚀 Features

- Hybrid algorithm intelligently selects the best solving strategy (DP vs DFS vs MiM)
- Async-ready: supports `CancellationToken` for long-running tasks
- Parallel depth-limited search with custom memoization
- Memory-efficient design via pooled buffers
- Adaptive MiM threshold and input density detection

---

## 🧪 Example Usage

```csharp
int[] nums = { 3, 34, 4, 12, 5, 2 };
int target = 9;

bool result = await SubsetSumAsyncSolver.SolveAsync(nums, target, CancellationToken.None);
Console.WriteLine($"Result: {result}");
```

---

## 📊 Benchmarks

| Case                     | n   | Time (ms) | Strategy Used |
|--------------------------|-----|-----------|----------------|
| Many Ones                | 40  | ~2.1 ms   | DFS + GCD Cut  |
| Powers of Two            | 8   | <1 ms     | Meet-in-Middle |
| Dense Random             | 30  | ~1.5 ms   | Bitset DP      |

---

## ⚙️ Requirements

- .NET 6+
- C# 10+

---

## 🧠 Theoretical Significance

This solver is part of an experimental effort to understand practical boundaries of **NP-complete** problems using modern techniques. While it doesn’t prove `P=NP`, it pushes performance boundaries under real-world constraints.

---

## 🧩 Future Plans

- Add `Pipelines` support for streaming subset data
- Add WebAPI interface
- Explore GPU parallelism via `ILGPU` or `ComputeSharp`
