using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    internal class ComputerOpponent
    {
        public void computerGame(byte[] xSquares, byte[] oSquares, string playerColor)
        {
            if (playerColor == "x" || playerColor == "X")
            {
                ConsoleOutputs.DisplayXTurn();
                xSquares[0] = ConsoleInputs.GetConsoleByteInput();
                if (xSquares[0]!= 5)
                {
                    oSquares[0] = 5;
                    ConsoleUserInterface.ViewTicTacToeBoard(xSquares, oSquares);
                }
            }
        }
        
        public int getCurrentTurn(byte[] xSquares, byte[] oSquares)
        {
            int currentTurn;
            if (xSquares.Last() != 0)
            {
                currentTurn = 7;
            }
            else
            {
                currentTurn = Array.IndexOf(xSquares, 0) + Array.IndexOf(oSquares, 0);
            }
            return currentTurn; ;
        }
    }
}
