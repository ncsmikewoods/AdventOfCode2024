using System.Text.RegularExpressions;

namespace Day03;

public class Solver
{
    private List<Instruction> _instructions;

    public Solver()
    {
        GetInputs();
    }

    public double Solve1()
    {
        return _instructions.Sum(x => x.Multiply());
    }

    public int Solve2()
    {
        return 1;
    }

    
    void GetInputs()
    {
        var input = File.ReadAllText("input.txt");
        var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
        
        var matches = regex.Matches(input).Select(x => x.Value).ToList();
        _instructions = matches.Select(m => new Instruction(m)).ToList();
    }
}

public class Instruction
{
    public Instruction(string operation)
    {
        var tokens = operation.Split(["mul", ",", "(", ")"], StringSplitOptions.RemoveEmptyEntries);

        Operation = operation;
        Left = int.Parse(tokens[0]);
        Right = int.Parse(tokens[1]);
    }
    
    public string Operation { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }

    public double Multiply()
    {
        return Left * Right;
    }
}