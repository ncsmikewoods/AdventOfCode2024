using System.Text.RegularExpressions;

namespace Day03;

public class Solver
{
    public double Solve1()
    {
        var matches = GetInputs("input1.txt", @"mul\(\d{1,3},\d{1,3}\)");
        return matches
            .Sum(m => m.MultiplyInstruction());
    }

    public double Solve2()
    {
        var matches = GetInputs("input2.txt", @"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don\'t\(\))");
        
        var isEnabled = true;
        var sum = 0d;
        foreach (Match match in matches)
        {
            if (IsInstruction(match) && isEnabled)
            {
                sum += match.MultiplyInstruction();
                continue;
            }

            isEnabled = IsToggleOn(match);
        }
        
        return sum;
    }

    MatchCollection GetInputs(string fileName, string pattern)
    {
        var input = File.ReadAllText(fileName);
        var regex = new Regex(pattern);
        
        return regex.Matches(input);
    }

    bool IsInstruction(Match match)
    {
        return match.Value.StartsWith("mul");
    }

    bool IsToggleOn(Match match)
    {
        return match.Value == "do()";
    }
}

public static class MatchExtension
{
    public static double MultiplyInstruction(this Match match)
    {
        var operation = match.Value;
        var tokens = operation.Split(["mul", ",", "(", ")"], StringSplitOptions.RemoveEmptyEntries);

        var left = int.Parse(tokens[0]);
        var right = int.Parse(tokens[1]);
        return left * right;
    }
}