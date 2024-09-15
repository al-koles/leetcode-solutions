// https://leetcode.com/problems/two-sum/

using System.Diagnostics;
using CommandLine;
using Leetcode.TwoSum;

Parser.Default
    .ParseArguments<CmdOptions>(args)
    .WithParsed(Run);

static void Run(CmdOptions opts)
{
    var rand = new Random();
    opts.Numbers = Enumerable
        .Repeat(0, 100000)
        .Select(_ => rand.Next(1000))
        .ToHashSet();
    opts.Target = rand.Next(1000);
    Console.WriteLine("Input array count: {0}; target: {1}", opts.Numbers.Count(), opts.Target);
    ExecuteTwoSumFunctions(opts.Numbers.ToArray(), opts.Target);
    Console.WriteLine();
    Console.WriteLine("Finish!");
}

static void ExecuteTwoSumFunctions(int[] nums, int target)
{
    List<TwoSumFunc> funcs =
    [
        new TwoSumFunc(nameof(TwoSumBase), TwoSumBase),
        new TwoSumFunc(nameof(TwoSumDictionary), TwoSumDictionary),
    ];
    foreach (var func in funcs)
    {
        ExecuteTwoSum(() => func.Func(nums, target), func.Name);
    }
}

static void ExecuteTwoSum(Func<int[]> twoSumFunc, string functionName)
{
    Console.WriteLine();
    var baseTime = GetExecutionTime(() =>
        {
            var result = twoSumFunc();
            Console.WriteLine("Result of {0}: {1}", functionName, string.Join(", ", result));
        }
    );
    Console.WriteLine("Time of {0}: {1}", functionName, baseTime.Milliseconds);
}

static TimeSpan GetExecutionTime(Action action)
{
    var stopwatch = Stopwatch.StartNew();
    action.Invoke();
    stopwatch.Stop();

    return stopwatch.Elapsed;
}

static int[] TwoSumBase(int[] nums, int target)
{
    for (var i = 0; i < nums.Length - 1; i++)
    {
        for (var j = i + 1; j < nums.Length; j++)
        {
            if (nums[i] + nums[j] == target)
                return [i, j];
        }
    }

    return [];
}

static int[] TwoSumDictionary(int[] nums, int target)
{
    var dict = nums
        .Select((number, index) => new { index, number })
        .ToDictionary(n => n.number, n => n.index);

    for (var i = 0; i < nums.Length - 1; i++)
    {
        var secondNumber = target - nums[i];
        if (dict.TryGetValue(secondNumber, out var j) && j != i)
            return [i, j];
    }

    return [];
}

internal record TwoSumFunc(string Name, Func<int[], int, int[]> Func);
