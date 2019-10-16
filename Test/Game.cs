using System;

public class Game
{
	public Game(int row, int column)
	{
        if (!(row * column % 2 == 0))
        {
            Console.WriteLine("nee");
        }
        else
        {
            _row = row;
            _column = column;
        }
	}
    public string PrintGame()
    {
        string printGame = "";
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                printGame += "X ";
            }
            printGame += "\n";
        }
        return printGame;
    }
}
