using System.Data;
using System.Diagnostics.Tracing;
using System.Text.Json;

namespace TicTacToeGame.ClassPractice;

public class JsonHistoryService : IHistoryService
{
    public History CreateHistoryFile(string playerXName, string playerOName, byte[] xMoveHistory, byte[] oMoveHistory)
    {
        return new History()
        {
            Date = DateTime.Now,
            PlayerXName = playerXName,
            PlayerOName = playerOName,
            XMoveHistory = Tools.ByteArrayToInt(xMoveHistory),
            OMoveHistory = Tools.ByteArrayToInt(oMoveHistory)
        };
    }

    public void DisplayGamesByPlayerName(string playerName)
    {
        List<History> historyListWithPlayerName = GetHistoriesWithPlayerName(ReadHistoryFile(Constants.fileName), playerName);

        if (historyListWithPlayerName.Count == 0) { ConsoleOutputs.DisplayLine(Constants.Error404Line); }
        else
        {
            int gamenumber = 1;
            foreach (History history in historyListWithPlayerName)
            {
                byte[] xMoveHistory = Tools.IntegerToByteArray(history.XMoveHistory, history.XMoveHistory.ToString().Length);
                byte[] oMoveHistory = Tools.IntegerToByteArray(history.OMoveHistory, history.OMoveHistory.ToString().Length);

                ConsoleOutputs.GameHistoryNumberDateLine(history.Date, gamenumber++);
                ConsoleOutputs.DisplayWinOrDraw(xMoveHistory, oMoveHistory, history.PlayerXName, history.PlayerOName);
                ConsoleOutputs.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
            }

            ConsoleOutputs.DisplayLine(Constants.pressEnterForSpecificGameLine);
            if (ConsoleInputs.IsKeyPressed(ConsoleKey.Enter)) 
            {
                var chosenGame = ConsoleUserInterface.ChooseAndDisplayGameHistoryFile(historyListWithPlayerName);
                ScrollChosenGame(historyListWithPlayerName, chosenGame);
            }
        }
    }

    public void WriteHistoryFile(History history)
    {
        var storedHistoryList = ReadHistoryFile(Constants.fileName);

        storedHistoryList.Add(history);
        var updatedJson = JsonSerializer.Serialize(storedHistoryList);

        File.WriteAllText(Constants.fileName, updatedJson);
    }

    public List<History> ReadHistoryFile(string path)
    {
        string jsonString = File.ReadAllText(path);
        var storedHistoryList = JsonSerializer.Deserialize<List<History>>(jsonString);
        return storedHistoryList;
    }



    private static List<History> GetHistoriesWithPlayerName(List<History> histories, string playerName)
    {
        List<History> historyListWithPlayerName = new List<History>();
        foreach (History history in histories)
        {
            if (IsStoredPlayerName(history, playerName))
            {
                historyListWithPlayerName.Add(history);
            }
        }
        return historyListWithPlayerName;
    }

    private static void ScrollChosenGame(List<History> historyList, int chosenGame)
    {
        byte[] xMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].XMoveHistory, historyList[chosenGame].XMoveHistory.ToString().Length);
        byte[] oMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].OMoveHistory, historyList[chosenGame].OMoveHistory.ToString().Length);
        byte[] fixedXMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].XMoveHistory, historyList[chosenGame].XMoveHistory.ToString().Length);
        byte[] fixedOMoveHistory = Tools.IntegerToByteArray(historyList[chosenGame].OMoveHistory, historyList[chosenGame].OMoveHistory.ToString().Length);

        int turn = Tools.CurrentTurn(xMoveHistory, oMoveHistory);

        bool EnterPressed = false;
        while (!EnterPressed)
        {
            var pressedKey = Console.ReadKey().Key;
            if (pressedKey == ConsoleKey.LeftArrow)
            {
                if (Tools.IsInputOutOfBoundsError(turn)) 
                { 
                    ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
                }
                else if (turn % 2 != 0) 
                { 
                    xMoveHistory[TurnNumberToCorrectIndex(turn)] = 0; 
                }
                else 
                { 
                    oMoveHistory[TurnNumberToCorrectIndex(turn)] = 0; 
                }

                Console.Clear();
                ConsoleOutputs.ScrollHistoryGameDisplay(xMoveHistory, oMoveHistory);
                turn--;
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                if (Tools.IsInputOutOfBoundsError(turn)) 
                {
                    ConsoleOutputs.DisplayLine(Constants.invalidInputLine); turn--; 
                }
                else if (turn % 2 != 0) { xMoveHistory[TurnNumberToCorrectIndex(turn)] = fixedXMoveHistory[TurnNumberToCorrectIndex(turn)]; }
                else { oMoveHistory[TurnNumberToCorrectIndex(turn)] = fixedOMoveHistory[TurnNumberToCorrectIndex(turn)]; }

                Console.Clear();
                ConsoleOutputs.ScrollHistoryGameDisplay(xMoveHistory, oMoveHistory);
                turn++;
            }
            else if (pressedKey == ConsoleKey.Enter)
            {
                EnterPressed = true;
            }
        }
    }

    private static int TurnNumberToCorrectIndex(int turn)
    {
        if (turn % 2 != 0) { return (turn - 1) / 2; }
        else { return (turn / 2) - 1; }
    }

    private static bool IsStoredPlayerName(History history, string playerName)
    {
        if (history.PlayerXName == playerName || history.PlayerOName == playerName) { return true; }
        else { return false; }
    }

}
