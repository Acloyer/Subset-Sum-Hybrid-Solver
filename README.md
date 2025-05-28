# Subset Sum Hybrid Solver

A high-performance C# implementation of the classic NP-complete **Subset Sum** problem. Built as a research-driven exploration of computational complexity, this project applies advanced algorithmic strategies to push the practical limits of NP-complete problems.

This solver was developed as part of a personal research initiative by **Huseynzade Rafig** (aka **Acloyer**) and is intended to demonstrate both technical mastery and research potential for elite academic institutions.

---

## 🚀 Techniques Used

- ✅ **Branch and Bound** – smart traversal with early pruning
- ✅ **Suffix Sum + GCD Pruning** – reduce impossible paths quickly
- ✅ **Meet-in-the-Middle Optimization** – powerful sub-exponential fallback
- ✅ **Bitset-based Dynamic Programming (DP)** – benchmark comparison

---

## 🧪 Sample Use Cases

```csharp
SubsetSumHybrid(new[] { 3, 34, 4, 12, 5, 2 }, 9);      // true
SubsetSumHybrid(new[] { 1, 2, 5 }, 4);                // false
SubsetSumDP(new[] { 1, 2, 3 }, 6);                    // true
```

---

## 📈 Benchmark Results

| n   | DP Time (ms) | Hybrid Time (ms) |
|-----|--------------|------------------|
| 30  | 1.2          | 0.4              |
| 35  | 3.7          | 0.8              |
| 40  | 7.4          | 1.5              |

> Hybrid approach demonstrates significant performance gains by eliminating redundant paths.

---

## 📚 Academic and Real-World Applications

- 🔐 Cryptography / knapsack-related reductions
- 🧮 Algorithmic finance and optimization
- 🎮 Game/puzzle solving with combinatoric states
- 📊 Research on practical hardness of NP-complete problems
- 📜 Undergraduate research demonstrations (e.g. for MIT, Caltech, etc.)

---

## 🧠 Motivation

This project was designed not only to explore one of the most important NP-complete problems, but to demonstrate:
- Strong algorithmic reasoning
- Systems-level performance optimization
- Research-readiness and independent initiative

It is suitable as a **supplementary project** for elite university applications and as a demonstration of commitment to advancing the field of algorithm design.

---

## 🛠 Project Files

- `Program.cs` – main hybrid logic, benchmark loop
- `SubsetSumHybrid.csproj` – C# build file
- `README.md` – this documentation
- `LICENSE` – MIT License

---

## 👤 Author

**Huseynzade Rafig** (GitHub: [Acloyer](https://github.com/Acloyer))

Aspiring computer science researcher and undergraduate at Arizona State University (Fall 2025, Ira A. Fulton Schools of Engineering).

---

## 📄 License

This project is released under the MIT License. Use it freely in academic, research, or commercial applications.
