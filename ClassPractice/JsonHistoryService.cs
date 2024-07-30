using System.Text.Json;

namespace TicTacToeGame.ClassPractice;

public class JsonHistoryService : IHistoryService
{
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

    public List<History> FilterHistoriesByPlayerName(List<History> histories, string playerName)
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

    private static bool IsStoredPlayerName(History history, string playerName)
    {
        return history.PlayerXName.Equals(playerName) || history.PlayerOName.Equals(playerName);
    }
}
