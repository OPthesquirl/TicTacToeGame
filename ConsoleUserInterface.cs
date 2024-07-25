using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

internal class ConsoleUserInterface
{
    public static string[] TakeUserNames()
    {
        ConsoleOutputs.PlayerNameInputLine("X");
        string playerXName = ConsoleInputs.GetConsoleStringInput();

        ConsoleOutputs.PlayerNameInputLine("O");
        string playerOName = ConsoleInputs.GetConsoleStringInput();

        return [playerXName, playerOName];
    }

    public static byte TakeUserInput(byte[] xSquares, byte[] oSquares)
    {
        byte input = ConsoleInputs.GetConsoleByteInput();
        while (Tools.IsInputError(input, xSquares, oSquares))
        {
            ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
            input = ConsoleInputs.GetConsoleByteInput();
        }
        return input;
    }

    
}
