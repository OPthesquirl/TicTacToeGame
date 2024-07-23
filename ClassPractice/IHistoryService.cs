namespace TicTacToeGame.ClassPractice;

public interface IHistoryService
{
    void DisplayGamesByPlayerName(string playerName);
    void DisplayLastGame();
    void WriteHistoryFile(History history);
    void ReadHistoryFile(string path);


}
