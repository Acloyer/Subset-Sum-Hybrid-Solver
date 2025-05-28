using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Unit tests (Hybrid algorithm)
        Test(new[] { 3, 34, 4, 12, 5, 2 }, 9, true);
        Test(new[] { 1, 2, 3 }, 6, true);
        Test(new[] { 1, 2, 5 }, 4, false);
        Test(new[] { 10, 20, 15 }, 25, true);

        // Worst-case scenario: many identical elements
        int[] worst = Enumerable.Repeat(1, 40).ToArray();
        Test(worst, 39, true);

        // Performance comparisons: DP vs Hybrid
        var rnd = new Random();
        foreach (int n in new[] { 30, 35, 40 })
        {
            int[] nums = Enumerable.Range(0, n)
                            .Select(_ => rnd.Next(1, 100))
                            .ToArray();
            int target = nums.Sum() + 1;

            var sw = Stopwatch.StartNew();
            bool dpOk = SubsetSumDP(nums, target);
            sw.Stop();
            double dpMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            bool hyOk = SubsetSumHybrid(nums, target);
            sw.Stop();
            double hyMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"n={n}: DP={dpMs:F3} ms, Hybrid={hyMs:F3} ms");
        }
    }

    static void Test(int[] nums, int target, bool expected)
    {
        bool result = SubsetSumHybrid(nums, target);
        Console.WriteLine($"Hybrid({target}) -> {result} | expected: {expected}");
    }

    // DP: bitset-based, O(n * target / 64)
    public static bool SubsetSumDP(int[] nums, int target)
    {
        int len = target + 1;
        int words = (len + 63) >> 6;
        Span<ulong> bits = stackalloc ulong[words];
        bits[0] = 1UL;

        foreach (int num in nums)
        {
            int wShift = num >> 6;
            int bShift = num & 63;
            for (int w = words - 1; w >= wShift; w--)
            {
                ulong v = bits[w - wShift];
                ulong shifted = bShift == 0
                    ? v
                    : (v << bShift) | (bits[w - wShift - 1] >> (64 - bShift));
                bits[w] |= shifted;
            }
        }

        int wi = target >> 6;
        int bi = target & 63;
        return (bits[wi] & (1UL << bi)) != 0;
    }

    // Hybrid: Branch & Bound + GCD bound + MiM bound
    public static bool SubsetSumHybrid(int[] nums, int target, int mimThreshold = 15)
    {
        Array.Sort(nums);
        Array.Reverse(nums);
        int n = nums.Length;

        int[] suffixSum = new int[n + 1];
        int[] suffixGcd = new int[n + 1];
        for (int i = n - 1; i >= 0; i--)
        {
            suffixSum[i] = suffixSum[i + 1] + nums[i];
            suffixGcd[i] = suffixGcd[i + 1] == 0
                ? nums[i]
                : MathGcd(nums[i], suffixGcd[i + 1]);
        }

        return Dfs(0, 0);

        bool Dfs(int i, int curr)
        {
            if (curr == target)
                return true;
            if (curr + suffixSum[i] < target)
                return false;

            int g = suffixGcd[i];
            if (g > 1 && (target - curr) % g != 0)
                return false;

            int rem = n - i;
            if (rem > 0 && rem <= mimThreshold)
            {
                int[] tail = nums.Skip(i).ToArray();
                return BooleanMeetInMiddle(tail, target - curr);
            }

            if (i >= n)
                return false;

            int v = nums[i];
            if (curr + v <= target && Dfs(i + 1, curr + v))
                return true;

            return Dfs(i + 1, curr);
        }
    }

    static bool BooleanMeetInMiddle(int[] arr, int target)
    {
        int m = arr.Length / 2;
        var left = new List<int>();
        var right = new List<int>();

        for (int mask = 0; mask < (1 << m); mask++)
        {
            int sum = 0;
            for (int j = 0; j < m; j++)
                if ((mask & (1 << j)) != 0)
                    sum += arr[j];
            left.Add(sum);
        }

        int rlen = arr.Length - m;
        for (int mask = 0; mask < (1 << rlen); mask++)
        {
            int sum = 0;
            for (int j = 0; j < rlen; j++)
                if ((mask & (1 << j)) != 0)
                    sum += arr[m + j];
            right.Add(sum);
        }

        right.Sort();
        foreach (int s in left)
        {
            int need = target - s;
            if (right.BinarySearch(need) >= 0)
                return true;
        }
        return false;
    }

    static int MathGcd(int a, int b) => b == 0 ? a : MathGcd(b, a % b);
}
