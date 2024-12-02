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
        
        return _reports.Count - fails.Count;
    }

    public int Solve2()
    {
        var fails = _reports
            .Where(report => !IsSafeish(report))
            .ToList();
        
        return _reports.Count - fails.Count;
    }

    bool IsSafe(List<int> report)
    {
        var failedLevels = GetFailedLevels(report);
        return failedLevels.Count == 0;
    }
    
    bool IsSafeish(List<int> report)
    {
        var failedLevels = GetFailedLevels(report);
        if (failedLevels.Count == 0) return true;
        
        for (var i = 0; i < report.Count; i++)
        {
            var copy = report.ToList();
            copy.RemoveAt(i);

            if (IsSafe(copy)) return true;
        }

        return false;
    }
    
    List<int> GetFailedLevels(List<int> report)
    {
        var isAscending = true;
        var failedIndexes = new List<int>();

        for (var i = 1; i < report.Count; i++)
        {
            var prev = report[i - 1];
            var curr = report[i];
            
            var didIncrease = curr > prev;
            var diff = Math.Abs(prev - curr);
            
            if (i == 1) isAscending = didIncrease;
            
            // Rule 1
            if (didIncrease != isAscending)
            {
                failedIndexes.Add(i);
            }
            
            // Rule 2
            if (diff is < 1 or > 3)
            {
                failedIndexes.Add(i);
            }
        }

        return failedIndexes.Distinct().ToList();
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