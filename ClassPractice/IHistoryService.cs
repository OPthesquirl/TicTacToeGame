namespace TicTacToeGame.ClassPractice;

public interface IHistoryService
{
    List<History> FilterHistoriesByPlayerName(List<History> histories, string playerName);

    void WriteHistoryFile(History history);

    List<History> ReadHistoryFile(string path);

}
