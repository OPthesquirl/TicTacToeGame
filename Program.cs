// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Text.Json;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;
internal class Program
{
    public static byte[] xSquares = new byte[5];
    public static byte[] oSquares = new byte[4];

    static void Main(string[] args)
    {
        IHistoryService historyService = new JsonHistoryService();

        string input = "";
        while (input != "0")
        {
            Console.WriteLine("Type name to search by playername, type 0 to continue to the game");
            input = ConsoleInputs.GetConsoleStringInput();
            historyService.DisplayGamesByPlayerName(input);
        }

        ConsoleOutputs.GameStartExplanation();
        string[] playerNames = ConsoleUserInterface.TakeUserNames();

        PlayGame(xSquares, oSquares);

        var history = new History
        {
            date = DateTime.Now,
            OMoveHistory = Tools.ByteArrayToInt(oSquares),
            XMoveHistory = Tools.ByteArrayToInt(xSquares),
            playerOName = playerNames[1],
            playerXName = playerNames[0]
        };
        historyService.WriteHistoryFile(history);

        ExitLoop();

    }

    public static void GetHistory(IHistoryService historyService)
    {
        historyService.ReadHistoryFile(Constants.fileName);
    }

    static void PlayGame(byte[] xSquares, byte[] oSquares)
    {
        while (!IsWinCondition(xSquares, oSquares) && xSquares.Last() == 0)
        {
            PlayTurn(xSquares, oSquares);
            ConsoleUserInterface.ViewTicTacToeBoard(xSquares, oSquares);
        }
        if (IsWinCondition(xSquares, oSquares))
        {
            ConsoleOutputs.DeclareWinner(xSquares, oSquares);
        }
        CheckForDraw(xSquares, oSquares);
    }

    static void CheckForDraw(byte[] xSquares, byte[] oSquares)
    {
        if (!IsWinCondition(xSquares, oSquares) && xSquares.Last() != 0)
        {
            ConsoleUserInterface.ViewTicTacToeBoard(xSquares, oSquares);
            ConsoleOutputs.DisplayDraw();
        }
    }

    static void PlayTurn(byte[] xSquares, byte[] oSquares)
    {
        int xIndex = Tools.FirstEmptyIndex(xSquares);

        int oIndex = Tools.FirstEmptyIndex(oSquares);

        if (xIndex == oIndex) { ConsoleOutputs.DisplayXTurn(); }
        else { ConsoleOutputs.DisplayOTurn(); }

        byte input = ConsoleUserInterface.TakeUserInput(xSquares, oSquares);

        if (xIndex == oIndex)
        {
            xSquares[xIndex] = input;
            return;
        }
        else
        {
            oSquares[oIndex] = input;
            return;
        }
    }

    static void ExitLoop()
    {
        while (true)
        {
            ConsoleOutputs.DisplayExit();
            if (ConsoleInputs.GetConsoleByteInput() != 0)
            {
                break;
            }
        }
    }



    public static bool IsWinCondition(byte[] xSquares, byte[] oSquares)
    {
        foreach (var element in Constants.winningCombinationsOfCoördinates)
        {
            if (Tools.IsSubsetOf(element, xSquares))
            {
                return true;
            }
            else if (Tools.IsSubsetOf(element, oSquares))
            {
                return true;
            }
        }

        return false;
    }

}
