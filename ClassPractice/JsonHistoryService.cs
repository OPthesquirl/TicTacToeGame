using System.Text.Json;

namespace TicTacToeGame.ClassPractice;

public class JsonHistoryService : IHistoryService
{
    public void WriteHistoryFile(History history)
    {
        string jsonString = File.ReadAllText(Constants.fileName);
        var storedHistoryList = JsonSerializer.Deserialize<List<History>>(jsonString);

        storedHistoryList.Add(history);
        var updatedJson = JsonSerializer.Serialize(storedHistoryList);

        File.WriteAllText(Constants.fileName, updatedJson);
    }

    public void ReadHistoryFile(string path)
    {
        Console.WriteLine(File.ReadAllText(path));
    }

}
