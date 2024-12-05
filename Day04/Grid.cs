namespace Day04;

public class Grid
{
    public Grid(char[][] inputRaw)
    {
        InputRaw = inputRaw;
        
        Nodes = inputRaw
            .Select((r, i) => 
                r.Select((c, j) => 
                    new Node(c, i, j)).ToArray()).ToArray();
        
        BuildNeighbors();
    }
    
    public char[][] InputRaw { get; set; }
    
    public Node[][] Nodes { get; set; }
    

    public void BuildNeighbors()
    {
        foreach (var row in Nodes)
        {
            foreach (var node in row)
            {
                node.SetNeighbors(InputRaw.Length, InputRaw[0].Length, (x, y) => Nodes[x][y]);
            }
        }
    }

    public int FindWords()
    {
        var sum = 0;
        
        foreach (var row in Nodes)
        {
            foreach (var node in row)
            {
                if (node.Letter != 'X') continue;

                foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
                {
                    sum += node.FindWord("MAS", direction) ? 1 : 0;
                }
            }
        }

        return sum;
    }

    public int FindXs()
    {
        var sum = 0;

        foreach (var row in Nodes)
        {
            foreach (var node in row)
            {
                if (node.Letter != 'A') continue;

                sum += node.IsXCenter() ? 1 : 0;
            }
        }

        return sum;
    }
}