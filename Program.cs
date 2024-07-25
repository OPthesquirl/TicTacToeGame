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

        ConsoleOutputs.DisplayWinOrDraw(xSquares, oSquares, playerNames[0], playerNames[1]);

        History history = historyService.CreateHistoryFile(playerNames[0], playerNames[1], xSquares, oSquares);
        historyService.WriteHistoryFile(history);

        ConsoleUserInterface.ExitLoop();

    }

    static void DisplayHistoriesLoop(IHistoryService historyService)
    {
        ConsoleOutputs.DisplayLine(Constants.historySearchByNameLine);
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Backspace))
        {
            var input = ConsoleInputs.GetConsoleStringInput();
            historyService.DisplayGamesByPlayerName(input);
            Console.WriteLine("Press backspace to exit, Input name for another search");
        }
    }

    static void PlayGame(byte[] xSquares, byte[] oSquares)
    {
        while (!Tools.IsWinCondition(xSquares, oSquares) && xSquares.Last() == 0)
        {
            PlayTurn(xSquares, oSquares);
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
        }
    }

    static void PlayTurn(byte[] xSquares, byte[] oSquares)
    {

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
