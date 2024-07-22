using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

internal class ConsoleUserInterface
{
    public static bool AskAccessHistory()
    {
        ConsoleOutputs.AccessHistoryPrompt();
        string input = ConsoleInputs.GetConsoleStringInput();
        if (input == "y" ||  input == "Y")   {  return true;}
        else { return false;  }
    }

    public static string[] TakeUserNames()
    {
        ConsoleOutputs.DisplayPlayerNameInput("X");
        string playerXName = ConsoleInputs.GetConsoleStringInput();

        ConsoleOutputs.DisplayPlayerNameInput("O");
        string playerOName = ConsoleInputs.GetConsoleStringInput();

        return [playerXName, playerOName];
    }

    public static byte TakeUserInput(byte[] xSquares, byte[] oSquares)
    {
        byte input = ConsoleInputs.GetConsoleByteInput();
        while (Tools.IsInputError(input, xSquares, oSquares))
        {
            ConsoleOutputs.DisplayError();
            input = ConsoleInputs.GetConsoleByteInput();
        }
        return input;
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
        ConsoleOutputs.DisplayGameState(gameState);
    }
}
