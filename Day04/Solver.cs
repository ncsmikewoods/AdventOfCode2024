using System.Xml.Linq;

namespace Day04;

public class Solver
{
    private Grid _grid;

    public Solver()
    {
        GetInputs();
    }
    
    public int Solve1()
    {
        var wordCount = _grid.FindWords();
        return wordCount;
    }

    public int Solve2()
    {
        var xCount = _grid.FindXs();
        return xCount;
    }

    void GetInputs()
    {
        var lines = File.ReadAllLines("input.txt");
        var gridRaw = lines.Select(row => row.ToArray()).ToArray();

        _grid = new Grid(gridRaw);
    }
}