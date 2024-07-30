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
        public static byte[] PlayXTurn(byte[] xMoves, byte[] oMoves)
        {
            if (xMoves[0] == 0)
            {
                xMoves[0] = 5;
                return xMoves;
            }
            else
            {
                byte[] algorithmArray = FindCorrectXMoveAlgorithm(oMoves);
                int index = xMoves.ToList().IndexOf(0);

                xMoves[index] = algorithmArray[index];

                return xMoves;
            }

        }

        public static byte[] PlayOTurn(byte[] xMoves, byte[] oMoves)
        {
            int currentIndex = oMoves.ToList().IndexOf(0);

            oMoves = TryForWinCondition(xMoves, oMoves, currentIndex);
            if (oMoves[currentIndex] == 0)
            {
                oMoves[currentIndex] = FindBestEmptySquare(xMoves, oMoves);
            }

            return oMoves;
        }

        private static byte[] FindCorrectXMoveAlgorithm(byte[] Moves)
        {
            bool isCorrectArray = true;

            foreach (var element in Constants.oMoveAlgorithm)
            {
                for (int i = 0; i < element.Length; i++)
                {
                    if (Moves[i] == 0)
                    {
                        isCorrectArray = true;
                        break;
                    }
                    if (element[i] != Moves[i])
                    {
                        isCorrectArray = false;
                        break;
                    }
                }

                if (isCorrectArray)
                {
                    return Constants.xMoveAlgorithm[Constants.oMoveAlgorithm.ToList().IndexOf(element)];
                }
            }
            return [];
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
