namespace Day05;

public class Solver
{
    private List<string[]> _rules;
    private List<string[]> _updates;
    private List<string> _uniqueValues;

    public Solver()
    {
        GetInputs();
    }
    
    public int Solve1()
    {
        var validUpdates = new List<string[]>();
        foreach (var update in _updates)
        {
            // sub-select the rules to only the ones involved in the update
            var subrules = _rules.Where(rule =>
                update.Contains(rule[0]) && update.Contains(rule[1])).ToList();

            var (isValid, idealOrder) = IsValidUpdate(update, subrules);

            if (isValid)
            {
                validUpdates.Add(update);
            }
        }
        
        return validUpdates.Sum(x => int.Parse(GetMiddleElement(x)));
    }
    
    public int Solve2()
    {
        var fixedUpdates = new List<string[]>();
        
        foreach (var update in _updates)
        {
            // sub-select the rules to only the ones involved in the update
            var subrules = _rules.Where(rule =>
                update.Contains(rule[0]) && update.Contains(rule[1])).ToList();

            var (isValid, idealOrder) = IsValidUpdate(update, subrules);

            if (!isValid)
            {
                fixedUpdates.Add(idealOrder);
            }
        }
        
        return fixedUpdates.Sum(x => int.Parse(GetMiddleElement(x)));
    }

    (bool, string[]) IsValidUpdate(string[] update, List<string[]> rules)
    {
        var sortedPages = new string[_uniqueValues.Count];

        for (var i = 0; i < _uniqueValues.Count; i++)
        {
            var start = FindLeftmostNumber(rules);
            sortedPages[i] = start;
            
            var subsetRules = rules.Where(rule => rule[0] == start).ToList();
            
            rules = rules.Except(subsetRules).ToList();

            // Maybe some of this isn't necessary
            if (rules.Count == 0 || subsetRules.Count == 1)
            {
                sortedPages[i + 1] = subsetRules[0][1];
                break;
            }
        }
        
        var sortedUpdates = update.OrderBy(x => Array.IndexOf(sortedPages, x)).ToArray();
        return (update.SequenceEqual(sortedUpdates), sortedUpdates); 
    }
    
    string FindLeftmostNumber(List<string[]> rules)
    {
        var lefts = rules.Select(rule => rule[0]).Distinct();
        var rights = rules.Select(rule => rule[1]).Distinct();
        
        return lefts.Except(rights).First();
    }

    string GetMiddleElement(string[] update)
    {
        var middleIndex = ((update.Length + 1) / 2) - 1;
        return update[middleIndex];
    }

    void GetInputs()
    {
        var rawText = File.ReadAllText("input.txt");
        var sections = rawText.Split("\r\n\r\n");
        
        _rules = sections[0].Split("\r\n")
            .Select(line => line.Split('|')).ToList();
        
        _updates = sections[1].Split("\r\n")
            .Select(x => x.Split(",")).ToList();

        _uniqueValues = _rules.SelectMany(x => x).Distinct().ToList();
    }
}