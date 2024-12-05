namespace Day04;

class Program
{
    static void Main(string[] args)
    {
        var solver = new Solver();

        RunIt("Part 1", () => solver.Solve1().ToString());
        RunIt("Part 2", () => solver.Solve2().ToString());
    }
    
    static void RunIt(string name, Func<string> solveFunc)
    {
        Console.WriteLine($"Solving {name}...");
        var start = DateTime.Now;
        
        var result = solveFunc();
        var duration = DateTime.Now - start;

        Console.WriteLine($"Solution: {result}");
        Console.WriteLine($"Duration: {Math.Round(duration.TotalMilliseconds)}ms");
        Console.WriteLine("");
    }
}