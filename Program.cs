// Subset Sum Hybrid Solver with Async + MemoryPool + Parallelism
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class SubsetSumAsyncSolver
{
    public static async Task<bool> SolveAsync(int[] nums, int target, CancellationToken token)
    {
        if (nums == null || nums.Length == 0) return target == 0;
        if (target < 0) return false;

        Array.Sort(nums);
        Array.Reverse(nums);
        
        int totalSum = nums.Sum();
        if (target > totalSum) return false;
        if (target == totalSum) return true;
        if (nums.Contains(target)) return true;

        var suffixSum = new int[nums.Length + 1];
        for (int i = nums.Length - 1; i >= 0; i--)
            suffixSum[i] = suffixSum[i + 1] + nums[i];

        return await Task.Run(() => DfsAsync(0, 0), token);

        bool DfsAsync(int i, int curr)
        {
            if (token.IsCancellationRequested) return false;
            if (curr == target) return true;
            if (i >= nums.Length || curr + suffixSum[i] < target) return false;

            // Parallel search in base cases
            if (nums.Length - i <= 16)
            {
                var results = new ConcurrentBag<bool>();
                Parallel.ForEach(
                    Partitions(nums, i, nums.Length - 1),
                    (range, _, __) =>
                    {
                        var sum = range.Sum();
                        if (sum == target - curr)
                            results.Add(true);
                    });
                return results.Contains(true);
            }

            return DfsAsync(i + 1, curr + nums[i]) || DfsAsync(i + 1, curr);
        }
    }

    static IEnumerable<int[]> Partitions(int[] arr, int start, int end)
    {
        int len = end - start + 1;
        int count = 1 << len;
        var pool = ArrayPool<int>.Shared;

        for (int mask = 0; mask < count; mask++)
        {
            var subset = pool.Rent(len);
            int index = 0;
            for (int j = 0; j < len; j++)
                if ((mask & (1 << j)) != 0)
                    subset[index++] = arr[start + j];

            yield return subset.Take(index).ToArray();
            pool.Return(subset);
        }
    }

    public static async Task<int[]> ReadArrayFromPipeAsync(PipeReader reader)
    {
        var list = new List<int>();
        while (true)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;
            if (buffer.IsEmpty && result.IsCompleted) break;

            var span = buffer.ToArray();
            for (int i = 0; i + 4 <= span.Length; i += 4)
                list.Add(BinaryPrimitives.ReadInt32LittleEndian(span.AsSpan(i, 4)));

            reader.AdvanceTo(buffer.End);
        }
        return list.ToArray();
    }
}

// Example usage with cancellation token
class Program
{
    static async Task Main()
    {
        var nums = Enumerable.Repeat(1, 22).ToArray();
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        bool result = await SubsetSumAsyncSolver.SolveAsync(nums, 20, cts.Token);
        Console.WriteLine($"Result: {result}");
    }
}
