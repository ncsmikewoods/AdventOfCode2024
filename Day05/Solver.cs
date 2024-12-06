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
        // var rules = _rules.ToList();
        // var sortedPages = new string[_uniqueValues.Count];
        //
        // // extract this.  filter it to only apply to rules involved in an update
        // for (var i = 0; i < _uniqueValues.Count; i++)
        // {
        //     var start = FindLeftmostNumber(rules);
        //     sortedPages[i] = start;
        //     Console.WriteLine("Next leftmost number: " + start);
        //     
        //     var subsetRules = rules.Where(rule => rule[0] == start).ToList();
        //     
        //     rules = rules.Except(subsetRules).ToList();
        //
        //     // Maybe some of this isn't necessary
        //     if (rules.Count == 0 && subsetRules.Count == 1)
        //     {
        //         sortedPages[i + 1] = subsetRules[0][1];
        //         break;
        //     }
        // }
        //
        // Console.WriteLine(string.Join(",", sortedPages));
        // var validUpdates = _updates.Where(update => IsUpdateValid(update, sortedPages));

        var updateResults = new List<string[]>();
        foreach (var update in _updates)
        {
            Console.WriteLine("Checking update: " + string.Join(",", update));
            // sub-select the rules to only hte ones involved in the update
            var subrules = _rules.Where(rule =>
                update.Contains(rule[0]) && update.Contains(rule[1])).ToList();

            var result = IsValidUpdate(update, subrules);

            if (result)
            {
                Console.WriteLine("Valid");
                updateResults.Add(update);
            }
        }
        
        return updateResults.Sum(x => int.Parse(GetMiddleElement(x)));
    }

    bool IsValidUpdate(string[] update, List<string[]> rules)
    {
        var sortedPages = new string[_uniqueValues.Count];

        for (var i = 0; i < _uniqueValues.Count; i++)
        {
            var start = FindLeftmostNumber(rules);
            sortedPages[i] = start;
            // Console.WriteLine("Next leftmost number: " + start);
            
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
        return update.SequenceEqual(sortedUpdates); 
    }
    
    public int Solve2()
    {
        return 1;
    }

    // bool IsUpdateValid(string[] update, string[] sortedPages)
    // {
    //     var sortedUpdates = update.OrderBy(x => Array.IndexOf(sortedPages, x)).ToArray();
    //     return update.SequenceEqual(sortedUpdates); 
    // }
    
    
    string FindLeftmostNumber(List<string[]> rules)
    {
        // var uniques = rules.SelectMany(x => x).Distinct().ToList();
        // var rights = rules.Select(rule => rule[1]).Distinct().Order().ToList();
        // var lefts = rules.Select(rule => rule[0]).Distinct().Order().ToList();
        //
        // var starter = uniques.Except(rights);
        // return starter.First();

        // // find a rule where the left side is not on the right side of any other rule
        // foreach (var rule in rules)
        // {
        //     var left = rule[0];
        //     if (rules.All(r => r[1] != left))
        //     {
        //         return left;
        //     }
        // }
        //
        // return "0"; // shouldn't happen

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