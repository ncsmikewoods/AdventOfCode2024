namespace Day04;

public class Node
{
    public Node(char c, int row, int col)
    {
        Letter = c;
        Row = row;
        Col = col;
    }
    
    public char Letter { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }

    public Node[] Neighbors { get; set; } = new Node[8];

    public void SetNeighbors(int maxX, int maxY, Func<int, int, Node> getNode)
    {
        var hasRoomLeft = Row > 0;
        var hasRoomRight = Row < maxX - 1;
        var hasRoomUp = Col > 0;
        var hasRoomDown = Col < maxY - 1;
        
        if (hasRoomLeft) 
            Neighbors[(int)Direction.Left] = getNode(Row - 1, Col);
        
        if (hasRoomRight) 
            Neighbors[(int)Direction.Right] = getNode(Row + 1, Col);
        
        if (hasRoomUp) 
            Neighbors[(int)Direction.Up] = getNode(Row, Col - 1);
        
        if (hasRoomDown) 
            Neighbors[(int)Direction.Down] = getNode(Row, Col + 1);
        
        if (hasRoomLeft && hasRoomUp) 
            Neighbors[(int)Direction.LeftUp] = getNode(Row - 1, Col - 1);
        
        if (hasRoomLeft && hasRoomDown) 
            Neighbors[(int)Direction.LeftDown] = getNode(Row - 1, Col + 1);
        
        if (hasRoomRight && hasRoomUp) 
            Neighbors[(int)Direction.RightUp] = getNode(Row + 1, Col - 1);
        
        if (hasRoomRight && hasRoomDown) 
            Neighbors[(int)Direction.RightDown] = getNode(Row + 1, Col + 1);
    }

    public bool FindWord(string word, Direction direction)
    {
        if (word.Length == 0) return true;
        
        var neighbor = Neighbors[(int)direction];
        if (neighbor == null) return false;
        
        if (neighbor.Letter != word[0]) return false;

        return neighbor.FindWord(word[1..], direction);
    }

    public bool IsXCenter()
    {
        // forward slash diagonal
        var rightUp = Neighbors[(int)Direction.RightUp];
        var leftDown = Neighbors[(int)Direction.LeftDown];
        if (rightUp == null || leftDown == null) return false;

        var hasForwardSlash = false;
        var hasBackSlash = false;

        if (rightUp.Letter == 'M' && leftDown.Letter == 'S'
            || rightUp.Letter == 'S' && leftDown.Letter == 'M') 
            hasForwardSlash = true;
        
        // back slash diagonal
        var leftUp = Neighbors[(int)Direction.LeftUp];
        var rightDown = Neighbors[(int)Direction.RightDown];
        if (leftUp == null || rightDown == null) return false;
        
        if (leftUp.Letter == 'M' && rightDown.Letter == 'S' 
            || leftUp.Letter == 'S' && rightDown.Letter == 'M')
            hasBackSlash = true;
        
        return hasForwardSlash && hasBackSlash;
    }
}