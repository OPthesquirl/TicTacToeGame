namespace TicTacToeGame.ClassPractice;

public interface IHistoryService
{
    void DisplayGamesByPlayerName(string playerName);
    void WriteHistoryFile(History history);
    List<History> ReadHistoryFile(string path);
    History CreateHistoryFile(string playerXName, string playerOName, byte[] xMoveHistory, byte[] oMoveHistory);
}
