// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
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
            string input = ConsoleInputs.ReadConsoleStringInput();
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
            else
            {
                ConsoleOutputs.DisplayLine("Mode not found.");
            }
            ConsoleOutputs.DisplayLine(Constants.exitKeyEnterAnyKeyContinueLine);
        }
    }

    private static void PvPGame(IHistoryService historyService)
    {
        string[] playerNames = ConsoleUserInterface.TakeUserNames();

        ConsoleOutputs.GameStartExplanation();

        int turn = 1;
        while (!Tools.IsWinCondition(xSquares, oSquares) && xSquares.Last() == 0)
        {
            ConsoleUserInterface.PlayPvPTurn(xSquares, oSquares, turn);
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
            turn++;
        }
        ConsoleOutputs.GameEndOutputs(xSquares, oSquares, playerNames[0], playerNames[1]);

        var history = History.HistoryConstructor(xSquares, oSquares, playerNames[0], playerNames[1]);
        historyService.WriteHistoryFile(history);
    }

    private static void ComputerGame(IHistoryService historyService)
    {
        string playerName = ConsoleUserInterface.TakeUserName();

        string playerColor = ConsoleUserInterface.UserTeamChoiceWithErrorCheck();

        string[] playerNames = OrderNames(playerName, playerColor);
       
        byte[][] gameHistory = ConsoleUserInterface.PlayComputerGame(playerColor, playerNames);

        var history = History.HistoryConstructor(gameHistory[0], gameHistory[1], playerNames[0], playerNames[1]);
        historyService.WriteHistoryFile(history);
    }

    private static string[] OrderNames(string playerName, string playerColor)
    {
        if (playerColor == "X" || playerColor == "x")
        {
            return [playerName, Constants.computerName];
        }

        return [Constants.computerName, playerName];
    }

    private static void DisplayHistoriesLoop(IHistoryService historyService)
    {
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Backspace))
        {
            ConsoleOutputs.DisplayLine(Constants.historySearchByNameLine);
            string input = ConsoleInputs.ReadConsoleStringInput();
            ConsoleUserInterface.DisplayGamesWithPlayerName(input, historyService);
            
            List<History> historyListWithPlayerName = historyService.FilterHistoriesByPlayerName(historyService.ReadHistoryFile(Constants.fileName), input);

            ConsoleOutputs.DisplayLine(Constants.pressEnterForSpecificGameLine);
            if (ConsoleInputs.IsKeyPressed(ConsoleKey.Enter))
            {
                var chosenGame = ConsoleUserInterface.ChooseGameHistoryFile(historyListWithPlayerName);
                byte[] xMoveHistory = Tools.IntegerToByteArray(historyListWithPlayerName[chosenGame].XMoveHistory, historyListWithPlayerName[chosenGame].XMoveHistory.ToString().Length);
                byte[] oMoveHistory = Tools.IntegerToByteArray(historyListWithPlayerName[chosenGame].OMoveHistory, historyListWithPlayerName[chosenGame].OMoveHistory.ToString().Length);

                Console.Clear();
                ConsoleOutputs.ScrollHistoryGameDisplay(xMoveHistory, oMoveHistory);
                ConsoleUserInterface.ScrollChosenGame(xMoveHistory, oMoveHistory);
            }
        }
    }

}
