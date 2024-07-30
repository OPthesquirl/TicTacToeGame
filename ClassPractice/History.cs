namespace TicTacToeGame.ClassPractice;

public class History
{
    public int XMoveHistory { get; set; }

    public int OMoveHistory { get; set; }

    public DateTime Date { get; set; }

    public string PlayerXName { get; set; }

    public string PlayerOName { get; set; }

    public static History HistoryConstructor(byte[] xMoveHistory, byte[] oMoveHistory, string playerXName, string playerOName)
    {
        return new History()
        {
            XMoveHistory = Tools.ByteArrayToInt(xMoveHistory),
            OMoveHistory = Tools.ByteArrayToInt(oMoveHistory),
            PlayerXName = playerXName,
            PlayerOName = playerOName,
            Date = DateTime.Now
        };
    }
}