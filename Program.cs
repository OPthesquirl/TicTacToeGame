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

        ConsoleOutputs.DisplayLine(Constants.exitKeyEnterAnyKeyContinueLine);
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Enter))
        {
            ConsoleOutputs.DisplayLine("Access: PlayerGame(input: P), computergame(input: C), gameHistory(input: H)");
            string input = ConsoleInputs.GetConsoleStringInput();
            if (input == "P" || input == "p") 
            {
                PvPGame(historyService);
            }
            else if (input == "C" || input == "c") 
            {
                ComputerGame(historyService);
            }
            else if (input == "H" || input == "h")
            {
                DisplayHistoriesLoop(historyService);
            }
            ConsoleOutputs.DisplayLine(Constants.exitKeyEnterAnyKeyContinueLine);
        }
    }

    private static void PvPGame(IHistoryService historyService)
    {
        ConsoleOutputs.GameStartExplanation();
        string[] playerNames = ConsoleUserInterface.TakeUserNames();

        PlayGame(xSquares, oSquares);
        ConsoleOutputs.DisplayWinOrDraw(xSquares, oSquares, playerNames[0], playerNames[1]);

        History history = historyService.CreateHistoryFile(playerNames[0], playerNames[1], xSquares, oSquares);
        historyService.WriteHistoryFile(history);
    }

    private static void ComputerGame(IHistoryService historyService)
    {
        ConsoleOutputs.DisplayLine(Constants.inputPlayerName);
        string playerName = ConsoleInputs.GetConsoleStringInput();
        string[] playerNames = ["X", "O"];
        string playerColor;

        while (true)
        {
            ConsoleOutputs.DisplayLine("Play as X(input: X), Play as O(input: O)");
            playerColor = ConsoleInputs.GetConsoleStringInput();
            if (playerColor == "X" || playerColor == "x")
            {
                playerNames = [playerName, Constants.computerName];
                break;
            }
            else if (playerColor == "O" || playerColor == "o")
            {
                playerNames = [Constants.computerName, playerName];
                break;
            }
            else { ConsoleOutputs.DisplayLine("IsInputError"); }
        }

        byte[][] gameHistory = ComputerOpponent.ComputerGame(playerColor, playerNames);

        History history = historyService.CreateHistoryFile(playerNames[0], playerNames[1], gameHistory[0], gameHistory[1]);
        historyService.WriteHistoryFile(history);
    }

    static void DisplayHistoriesLoop(IHistoryService historyService)
    {
        ConsoleOutputs.DisplayLine(Constants.historySearchByNameLine);
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Backspace))
        {
            var input = Console.ReadLine();
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
        if (Tools.IsXTurn(xSquares, oSquares)) { ConsoleOutputs.DisplayLine(Constants.xTurnLine); }
        else { ConsoleOutputs.DisplayLine(Constants.oTurnLine); }

        byte input = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);
        if (Tools.IsXTurn(xSquares, oSquares))
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
