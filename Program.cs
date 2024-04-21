// See https://aka.ms/new-console-template for more information
namespace TicTacToeGame;
internal class Program
{
    static void Main(string[] args)
    {
        byte[] xSquares = new byte[5];
        byte[] oSquares = new byte[4];

        ConsoleOutputs.GameStartExplanation();

        PlayGame(xSquares, oSquares);
        CheckForDraw(xSquares, oSquares);

        ExitLoop();

    }



    static void PlayGame(byte[] xSquares, byte[] oSquares)
    {
        while (IsNotWinCondition(xSquares, oSquares) && xSquares.Last() == 0)
        {
            PlayTurn(xSquares, oSquares);
            ConsoleUserInterface.ViewTicTacToeBoard(xSquares, oSquares);
        }
        if (IsNotWinCondition(xSquares, oSquares) == false)
        {
            ConsoleOutputs.DeclareWinner(xSquares, oSquares);
        }
    }

    static void CheckForDraw(byte[] xSquares, byte[] oSquares)
    {
        if (IsNotWinCondition(xSquares, oSquares) && xSquares.Last() != 0)
        {
            ConsoleUserInterface.ViewTicTacToeBoard(xSquares, oSquares);
            ConsoleOutputs.DisplayDraw();
        }
    }

    static void PlayTurn(byte[] xSquares, byte[] oSquares)
    {
        int xIndex = Tools.FirstEmptyIndex(xSquares);

        int oIndex = Tools.FirstEmptyIndex(oSquares);

        if (xIndex == oIndex) { ConsoleOutputs.DisplayXTurn(); }
        else { ConsoleOutputs.DisplayOTurn(); }

        byte input = ConsoleUserInterface.TakeUserInput(xSquares, oSquares);

        if (xIndex == oIndex)
        {
            xSquares[xIndex] = input;
            return;
        }
        else
        {
            oSquares[oIndex] = input;
            return;
        }
    }

    static void ExitLoop()
    {
        while (true)
        {
            ConsoleOutputs.DisplayExit();
            if (ConsoleInputs.GetConsoleInput() != 0)
            {
                break;
            }
        }
    }



    static bool IsNotWinCondition(byte[] xSquares, byte[] oSquares)
    {
        foreach (var element in Constants.winningCombinationsOfCoördinates)
        {
            if (Tools.IsSubsetOf(element, xSquares))
            {
                return false;
            }
            else if (Tools.IsSubsetOf(element, oSquares))
            {
                return false;
            }
        }

        return true;
    }

}
