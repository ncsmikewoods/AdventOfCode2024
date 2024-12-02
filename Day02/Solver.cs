namespace Day02;

public class Solver
{
    private List<List<int>> _reports;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var fails = _reports
            .Where(report => !IsSafe(report))
            .ToList();

        // foreach (var fail in fails)
        // {
        //     Console.WriteLine($"Fail: {string.Join(" ", fail)}");
        // }
        
        return _reports.Count - fails.Count;
    }

    public int Solve2()
    {
        return 1;
    }

    bool IsSafe(List<int> report)
    {
        var isAscending = true;

        for (var i = 1; i < report.Count; i++)
        {
            var prev = report[i - 1];
            var curr = report[i];
            
            var didIncrease = curr > prev;
            var diff = Math.Abs(prev - curr);
            
            // Rule 1
            if (i == 1)
            {
                isAscending = didIncrease;
            }
            else
            {
                if (didIncrease != isAscending)
                {
                    return false;
                }
            }
            
            // Rule 2
            if (diff is < 1 or > 3)
            {
                return false;
            }
        }

        return true;
    }
    
    void GetInputs()
    {
        _reports = 
            File.ReadAllLines("input.txt")
                .Select(line => 
                    line.Split(' ')
                        .Select(int.Parse)
                        .ToList())
                .ToList();
    }
}