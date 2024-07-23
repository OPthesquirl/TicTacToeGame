using System.Text.Json;

namespace TicTacToeGame.ClassPractice;

public class JsonHistoryService : IHistoryService
{
    public void DisplayGamesByPlayerName(string playerName)
    {
        var storedHistoryList = Tools.GetJsonHistoryList(Constants.fileName);
        bool isInDatabase = false; 

        foreach (var storedHistory in storedHistoryList)
        {
            if (storedHistory.playerXName == playerName || storedHistory.playerOName == playerName)
            {
                isInDatabase = true;

                var xMoveHistory = Tools.ConvertIntegerToByteArray(storedHistory.XMoveHistory, storedHistory.XMoveHistory.ToString().Length);
                var oMoveHistory = Tools.ConvertIntegerToByteArray(storedHistory.OMoveHistory, storedHistory.OMoveHistory.ToString().Length);

                if (Program.IsWinCondition(xMoveHistory, oMoveHistory))
                {
                    if (xMoveHistory.ToList().IndexOf(0) > oMoveHistory.ToList().IndexOf(0))
                    {
                        Console.WriteLine(storedHistory.date);
                        Console.WriteLine(storedHistory.playerXName + Constants.wonVsLine + storedHistory.playerOName);
                        ConsoleUserInterface.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
                    }
                    else
                    {
                        Console.WriteLine(storedHistory.date);
                        Console.WriteLine(storedHistory.playerOName + Constants.wonVsLine + storedHistory.playerXName);
                        ConsoleUserInterface.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
                    }
                }
                else
                {
                    Console.WriteLine(storedHistory.date);
                    Console.WriteLine(Constants.gameDrawLine + storedHistory.playerXName + Constants.andLine + storedHistory.playerOName);
                    ConsoleUserInterface.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
                }
            }
        }
        if (!isInDatabase)
        {
            Console.WriteLine("Playername Not Found");
        }
    }

    public void DisplayLastGame()
    {
        var storedHistoryList = Tools.GetJsonHistoryList(Constants.fileName);

        var lastGame = storedHistoryList.Last();
        var xMoveHistory = Tools.ConvertIntegerToByteArray(lastGame.XMoveHistory, lastGame.XMoveHistory.ToString().Length);
        var oMoveHistory = Tools.ConvertIntegerToByteArray(lastGame.OMoveHistory, lastGame.OMoveHistory.ToString().Length);

        ConsoleUserInterface.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
    }

    public void WriteHistoryFile(History history)
    {
        var storedHistoryList = Tools.GetJsonHistoryList(Constants.fileName);

        storedHistoryList.Add(history);
        var updatedJson = JsonSerializer.Serialize(storedHistoryList);

        File.WriteAllText(Constants.fileName, updatedJson);
    }

    public void ReadHistoryFile(string path)
    {
        Console.WriteLine(File.ReadAllText(path));
    }

}
