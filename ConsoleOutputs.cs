using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;

public class ConsoleOutputs
{
    public static void DisplayWinOrDraw(byte[] xMoveHistory, byte[] oMoveHistory, string playerXName, string playerOName)
    {
        if (Tools.IsWinCondition(xMoveHistory, oMoveHistory))
        {
            string[] winnerLoser = Tools.OrderNamesToWinnerLoser(xMoveHistory, oMoveHistory, playerXName, playerOName);
            Console.WriteLine(winnerLoser[0] + Constants.wonVsLine + winnerLoser[1]);
        }
        else
        {
            Console.WriteLine(Constants.gameDrawLine + playerXName + Constants.andLine + playerOName);
        }
    }

    public static void ViewTicTacToeBoard(byte[] xSquares, byte[] oSquares)
    {
        char[] gameState = new char[9];
        for (byte i = 1; i < 10; i++)
        {
            if (Tools.IsElementOf(xSquares, i))
            {
                gameState[i - 1] = Constants.xChar;
            }
            else if (Tools.IsElementOf(oSquares, i))
            {
                gameState[i - 1] = Constants.oChar;
            }
            else
            {
                gameState[i - 1] = Constants.emptyChar;
            }
        }
        DisplayGameState(gameState);
    }

    public static void DisplayLine(string line)
    {
        Console.WriteLine(line);
    }

    public static void GameHistoryNumberDateLine(DateTime date, int gameNumber)
    {
        Console.WriteLine(Constants.gameNumberLine + gameNumber);
        Console.WriteLine(date);
    }

    public static void ScrollGameExplanationLines()
    {
        DisplayLine(Constants.exitExplanationLine);
        DisplayLine(Constants.historyScrollExplanationline);
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
        Console.Clear();
        Console.WriteLine(" Input number according to your chosen square:");
        Console.WriteLine("1 | 2 | 3 ");
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine("4 | 5 | 6 ");
        Console.WriteLine(Constants.horizontalDisplayLine);
        Console.WriteLine("7 | 8 | 9 ");
    }

}
