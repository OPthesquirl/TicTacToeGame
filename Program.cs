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

        DisplayHistoriesLoop(historyService);

        ConsoleOutputs.GameStartExplanation();
        string[] playerNames = ConsoleUserInterface.TakeUserNames();

        PlayGame(xSquares, oSquares);

        if (Tools.IsDraw(xSquares, oSquares))
        {
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
            ConsoleOutputs.GameDrawLine(playerNames[0], playerNames[1]);
        }

        Tools.StoreHistoryFile(playerNames[0], playerNames[1], xSquares, oSquares);

        Tools.ExitLoop();

    }

    static void DisplayHistoriesLoop(IHistoryService historyService)
    {
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Backspace))
        {
            ConsoleOutputs.DisplayLine(Constants.historySearchByNameLine);
            var input = ConsoleInputs.GetConsoleStringInput();
            historyService.DisplayGamesByPlayerName(input);
        }
    }

    static void PlayGame(byte[] xSquares, byte[] oSquares)
    {
        while (!Tools.IsWinCondition(xSquares, oSquares) && xSquares.Last() == 0)
        {
            PlayTurn(xSquares, oSquares);
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
        }
        if (Tools.IsWinCondition(xSquares, oSquares))
        {
            ConsoleOutputs.DeclareWinnerLine(xSquares, oSquares);
        }
    }

    static void PlayTurn(byte[] xSquares, byte[] oSquares)
    {
        int xIndex = Tools.FirstEmptyIndex(xSquares);
        int oIndex = Tools.FirstEmptyIndex(oSquares);

        if (Tools.CurrentTurn(xSquares, oSquares) % 2 != 0) { ConsoleOutputs.DisplayLine(Constants.oTurnLine); }
        else { ConsoleOutputs.DisplayLine(Constants.xTurnLine); }

        byte input = ConsoleUserInterface.TakeUserInput(xSquares, oSquares);
        if (Tools.CurrentTurn(xSquares, oSquares) % 2 == 0)
        {
            xSquares[xSquares.ToList().IndexOf(0)] = input;
            return;
        }
        else
        {
            oSquares[oSquares.ToList().IndexOf(0)] = input;
            return;
        }
    }

}
