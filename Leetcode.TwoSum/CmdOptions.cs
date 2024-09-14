using CommandLine;
using CommandLine.Text;

namespace Leetcode.TwoSum;

public class CmdOptions
{
    [Option('t', "target", Required = true, HelpText = "Sum of two integers from first argument.")]
    public required int Target { get; set; }
    
    [Option('a', "array", Required = true, HelpText = "Array of integers separated by comma.", Separator = ',')]
    public required IEnumerable<int> Numbers { get; set; }

    [Usage(ApplicationAlias = "twosum")]
    public static IEnumerable<Example> Examples =>
        new List<Example>
        {
            new("Get two sum", new CmdOptions { Target = 4, Numbers = [1, 2, 3] }),
        };
}
