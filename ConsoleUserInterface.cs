using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;

internal class ConsoleUserInterface
{
    public static byte MoveInputWithErrorCheck(byte[] xSquares, byte[] oSquares)
    {
        byte playerMove = ConsoleInputs.GetConsoleByteInput();
        if (Tools.IsInputError(playerMove, xSquares, oSquares))
        {
            ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
            playerMove = ConsoleInputs.GetConsoleByteInput();
        }
        return playerMove;
    }

    public static string[] TakeUserNames()
    {
        ConsoleOutputs.DisplayLine("Player X name:");
        string playerXName = ConsoleInputs.GetConsoleStringInput();

        ConsoleOutputs.DisplayLine("Player O name:");
        string playerOName = ConsoleInputs.GetConsoleStringInput();

        return [playerXName, playerOName];
    }

    public static int ChooseAndDisplayGameHistoryFile(List<History> historyList)
    {
        ConsoleOutputs.DisplayLine(Constants.chooseAGameLine);
        int chosenGame = ConsoleInputs.GetConsoleByteInput() - 1;


        byte[] xMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].XMoveHistory, historyList[chosenGame].XMoveHistory.ToString().Length);
        byte[] oMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].OMoveHistory, historyList[chosenGame].OMoveHistory.ToString().Length);

        Console.Clear();
        ConsoleOutputs.ScrollHistoryGameDisplay(xMoveHistory, oMoveHistory);

        return chosenGame;
    }

    public static void ExitLoop()
    {
        ConsoleOutputs.DisplayLine(Constants.exitExplanationLine);
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Enter)) { }
    }

}
