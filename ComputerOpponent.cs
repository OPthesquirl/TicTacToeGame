using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame
{
    internal class ComputerOpponent
    {
        public static byte[][] ComputerGame(string playerXorO, string[] playerNames)
        {
            if (playerXorO == "o" || playerXorO == "O") 
            { 
                return PlayAlgorithmAsX(playerNames); 
            }
            else if (playerXorO == "x" || playerXorO == "X") 
            { 
                return PlayAlgorithmAsO(playerNames); 
            }
            else return [];
        }

        public static byte[][] PlayAlgorithmAsX(string[] playerNames)
        {
            byte[] xSquares = new byte[5];
            byte[] oSquares = new byte[4];

            xSquares[0] = 5;

            ConsoleOutputs.GameStartExplanation();
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
            ConsoleOutputs.DisplayLine(Constants.oTurnLine);
            byte playerMove = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);

            oSquares[0] = playerMove; 

            // these are deterministic moveHistories [0][] being if Player plays 1 etc.
            byte[][] xMoveAlgorithm = [[5, 2, 6, 7, 9], [5, 3, 6, 4, 0], [5, 6, 8, 1, 7], [5, 1, 2, 6, 0], [5, 9, 8, 2, 0], [5, 4, 2, 9, 3], [5, 7, 4, 6, 0], [5, 8, 4, 3, 1]];
            byte[][] oMoveAlgorithm = [[1, 8, 4, 3], [2, 7, 9, 0], [3, 4, 2, 9], [4, 9, 3, 0], [6, 1, 7, 0], [7, 6, 8, 1], [8, 3, 1, 0], [9, 2, 6, 7]];

            for (int i = 0; i < oMoveAlgorithm.Length; i++)
            {
                if (oMoveAlgorithm[i][0].Equals(playerMove))
                {
                    xSquares[1] = xMoveAlgorithm[i][1];
                    ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
                    for (int j = 1; j < oMoveAlgorithm[i].Length; j++)
                    {
                        playerMove = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);

                        oSquares[j] = playerMove;
                        if (!playerMove.Equals(oMoveAlgorithm[i][j]))
                        {
                            xSquares[j+1] = oMoveAlgorithm[i][j];
                            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
                            break;
                        }
                        else
                        {
                            xSquares[j + 1] = xMoveAlgorithm[i][j + 1];
                            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
                        }

                    }
                    break;
                }
            }
            ConsoleOutputs.DisplayWinOrDraw(xSquares, oSquares, playerNames[0], playerNames[1]);
            return [xSquares, oSquares];
        }

        public static byte[][] PlayAlgorithmAsO(string[] playerNames)
        {
            byte[] xSquares = new byte[5];
            byte[] oSquares = new byte[4];

            int index = 0;
            while (true)
            {
                if (index == 0) 
                { 
                    ConsoleOutputs.GameStartExplanation(); 
                }
                else 
                { 
                    ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares); 
                }

                ConsoleOutputs.DisplayLine(Constants.xTurnLine);
                xSquares[index] = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);

                if (Tools.IsWinCondition(xSquares, oSquares) || xSquares.Last() != 0) { break; }

                oSquares = TryForWinCondition(xSquares, oSquares, index);
                if (oSquares[index] == 0) 
                { 
                    oSquares[index] = FindBestEmptySquare(xSquares, oSquares); 
                }

                if (Tools.IsWinCondition(xSquares, oSquares)) { break; }

                index++;
            }

            Console.Clear();
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
            playerNames = Tools.OrderNamesToWinnerLoser(xSquares, oSquares, playerNames[0], playerNames[1]);
            ConsoleOutputs.DisplayWinOrDraw(xSquares, oSquares, playerNames[0], playerNames[1]);

            return [xSquares, oSquares];
        }

        private static byte[] TryForWinCondition(byte[] xSquares, byte[] oSquares, int index)
        {
            foreach (var item in EmptySquaresArray(xSquares, oSquares))
            {
                if (item == 0) { break; }

                xSquares[index + 1] = item;
                if (Tools.IsWinCondition(xSquares, oSquares))
                {
                    xSquares[index + 1] = 0;
                    oSquares[index] = item;
                    break;
                }
                xSquares[index + 1] = 0;
                oSquares[index] = item;
                if (Tools.IsWinCondition(xSquares, oSquares))
                {
                    break;
                }
                oSquares[index] = 0;
            }
            return oSquares;
        }

        private static byte[] EmptySquaresArray(byte[] xSquares, byte[] oSquares)
        {
            byte[] emptyIndexes = new byte[8];

            for (byte i = (byte)Constants.minimumInputValue; i <= (byte)Constants.maximumInputValue; i++)
            {
                if(!xSquares.Contains(i) && !oSquares.Contains(i))
                {
                    emptyIndexes[emptyIndexes.ToList().IndexOf(0)] = i;
                }
            }
            return emptyIndexes;
        }

        private static byte FindBestEmptySquare(byte[] xSquares, byte[] oSquares)
        {
            byte[] priorityOrderedIndexes = [5, 1, 3, 7, 9, 2, 4, 6, 8]; // first center, then corners, then rest

            foreach (var element in priorityOrderedIndexes)
            {
                if (!xSquares.ToList().Contains(element) && !oSquares.ToList().Contains(element))
                {
                    return element;
                }
            }
            return 0;
        }
    }
}
