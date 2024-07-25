namespace TicTacToeGame.ClassPractice;

public interface IHistoryService
{
    void DisplayGamesByPlayerName(string playerName);
    void WriteHistoryFile(History history);
    List<History> ReadHistoryFile(string path);


}
