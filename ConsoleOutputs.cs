using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

public class ConsoleOutputs
{
    public static void AccessHistoryPrompt()
    {
        Console.WriteLine("Input Y to access History");
    }

    public static void DeclareWinner(byte[] xSquares, byte[] oSquares)
    {
        if (xSquares.Length > oSquares.Length)
        {
            Console.WriteLine("X Wins!");
        }
        else
        {
            Console.WriteLine("O wins!");
        }
    }
    public static void DisplayGameState(char[] gameState)
    {
        Console.WriteLine(gameState[0] + Constants.verticalDisplayLine + gameState[1] + Constants.verticalDisplayLine + gameState[2]);
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine(gameState[3] + Constants.verticalDisplayLine + gameState[4] + Constants.verticalDisplayLine + gameState[5]);
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine(gameState[6] + Constants.verticalDisplayLine + gameState[7] + Constants.verticalDisplayLine + gameState[8]);
    }
    public static void GameStartExplanation()
    {
        Console.WriteLine("Input number according to your chosen square:");
        Console.WriteLine("1 | 2 | 3 ");
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine("4 | 5 | 6 ");
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine("7 | 8 | 9 ");
    }
    public static void DisplayPlayerNameInput(string player)
    {
        Console.WriteLine("Player " + player + " name:");
    }

    public static void DisplayDraw()
    {
        Console.WriteLine("Draw!");
    }

    public static void DisplayError()
    {
        Console.WriteLine(Constants.invalidInputLine);
    }

    public static void DisplayXTurn()
    {
        Console.WriteLine(Constants.xTurnLine);
    }

    public static void DisplayOTurn()
    {
        Console.WriteLine(Constants.oTurnLine);
    }

    public static void DisplayExit()
    {
        Console.WriteLine(Constants.exitExplanationLine);
    }
}
