// https://leetcode.com/problems/two-sum/
using CommandLine;
using Leetcode.TwoSum;

Parser.Default
    .ParseArguments<CmdOptions>(args)
    .WithParsed(Run);

static void Run(CmdOptions opts)
{
    var twoSum = TwoSum(opts.Numbers.ToArray(), opts.Target);
    Console.WriteLine("Result: {0}", string.Join(", ", twoSum));
}

static int[] TwoSum(int[] nums, int target)
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
