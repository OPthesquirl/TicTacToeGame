namespace TicTacToeGame.ClassPractice;

public interface IHistoryService
{
    void WriteHistoryFile(History history);
    void ReadHistoryFile(string path);


}
