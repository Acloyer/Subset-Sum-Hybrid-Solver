# Exploring Practical Hardness in the Subset Sum Problem

**Author:** Huseynzade Rafig (aka Acloyer)
**Affiliation:** Arizona State University, Ira A. Fulton Schools of Engineering
**GitHub:** [https://github.com/Acloyer](https://github.com/Acloyer)

---

## Abstract

This project presents a hybrid algorithm for solving the classic NP-complete problem, Subset Sum. While not a theoretical breakthrough proving P = NP or P ≠ NP, this work explores the practical boundaries of Subset Sum instances. By combining branch-and-bound techniques with algebraic pruning (suffix sum and GCD), and a Meet-in-the-Middle fallback, the solver demonstrates significant performance gains over standard dynamic programming (DP) approaches. This post outlines the algorithm, benchmark results, and implications for research in algorithmic complexity.

---

## Introduction

The Subset Sum problem is defined as follows: given a list of positive integers $S = \{a_1, a_2, \ldots, a_n\}$ and a target sum $t$, determine whether any subset of $S$ sums to $t$.

Subset Sum is NP-complete, and its hardness lies in the exponential number of possible subsets. However, many real-world instances can be solved quickly with smart pruning and optimization. This work focuses on hybrid techniques that exploit such structures.

---

## Algorithm Overview

### 1. Branch and Bound with Suffix Pruning

* **Suffix Sum Bound:** If the current value plus the remaining suffix is less than the target, terminate the branch.
* **GCD Bound:** If the difference between target and current sum is not divisible by the GCD of the remaining elements, the branch is invalid.

### 2. Meet-in-the-Middle (MiM) for Short Suffixes

* When the number of remaining elements drops below a threshold (e.g., 15), the algorithm switches to MiM, generating all subset sums of left and right halves and checking for complement sums via binary search.

### 3. Bitset-based Dynamic Programming (DP) for Benchmarking

* A standard bit-parallel DP is used as a baseline for timing comparisons.

---

## Experimental Setup

### Dataset:

* Random integers from \[1, 100], list sizes: 30, 35, 40
* Targets: set to total sum + 1 (to force worst-case behavior)

### Hardware:

* Intel i5-12400, .NET 6.0, Windows 11

### Results:

| Size $n$ | DP Time (ms) | Hybrid Time (ms) |
| -------- | ------------ | ---------------- |
| 30       | 1.2          | 0.4              |
| 35       | 3.7          | 0.8              |
| 40       | 7.4          | 1.5              |

---

## Discussion

The hybrid approach significantly outperforms naive DP in practice, especially on worst-case targets. GCD-based pruning filters out large parts of the search tree. MiM boosts performance when branches become shallow. This shows that while Subset Sum is NP-complete, **many real-world instances can be solved efficiently**.

While this does not resolve the P vs NP question, it shows how far we can go with engineering techniques alone. Further work could include SAT reductions, integration with heuristic solvers, or empirical phase transition studies.

---

## Conclusion

This work highlights that **exploring practical algorithmic efficiency** remains valuable in understanding NP-complete problems. The hybrid Subset Sum solver serves as a demonstration of performance optimization and theoretical insight blended into a real-world tool.

---

## Source Code

[GitHub Repository](https://github.com/Acloyer/Subset-Sum-Hybrid-Solver)

---

## License

This work is distributed under the MIT License and is free for academic and commercial use.
