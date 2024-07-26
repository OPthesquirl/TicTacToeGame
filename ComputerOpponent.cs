using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame
{
    internal class ComputerOpponent
    {

        public static byte[][] ComputerGame(string playerXorO, string playerName)
        {

            if (playerXorO == "o" || playerXorO == "O") { return PlayAlgorithmAsX(); }
            else if (playerXorO == "x" || playerXorO == "X") { return PlayAlgorithmAsO(playerName); }
            else return [];
        }

        public static byte[][] PlayAlgorithmAsX()
        {
            byte[] xSquares = new byte[5];
            byte[] oSquares = new byte[4];

            xSquares[0] = 5;
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);

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
                            Console.WriteLine(Constants.computerWinsLine);
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
            if (!Tools.IsWinCondition(xSquares, oSquares))
            {
                Console.WriteLine(Constants.gameDrawLine);
            }
            return [xSquares, oSquares];
        }

        public static byte[][] PlayAlgorithmAsO(string playerName)
        {
            byte[] xSquares = new byte[5];
            byte[] oSquares = new byte[4];

            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
            ConsoleOutputs.DisplayLine(Constants.xTurnLine);
            xSquares[0] = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);

            if (xSquares[0] != 5) { oSquares[0] = 5; }
            else { oSquares[0] = 1; }

            int turn = 3;
            while(turn < Constants.maximumNumberOfTurnsAndInputValues && !Tools.IsWinCondition(xSquares, oSquares))
            {
                if (WinThreat(xSquares, oSquares) != 0) 
                {
                    oSquares[TurnNumberToCorrectIndex(turn++)] = WinThreat(xSquares, oSquares);
                }
                else
                {
                    oSquares[TurnNumberToCorrectIndex(turn++)] = FindBestEmptySquare(xSquares, oSquares);
                }
                ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);
                ConsoleOutputs.DisplayLine(Constants.xTurnLine);
                xSquares[TurnNumberToCorrectIndex(turn++)] = ConsoleUserInterface.MoveInputWithErrorCheck(xSquares, oSquares);
            }

            ConsoleOutputs.DisplayWinOrDraw(xSquares, oSquares, "Player", "The Algorithm");
            return [xSquares, oSquares];
        }

        private static byte FindBestEmptySquare(byte[] xSquares, byte[] oSquares)
        {
            byte[] cornerIndexes = [1, 3, 7, 9];
            byte[] otherIndexes = [2, 4, 6, 8]; //5 not included because it will always already be taken

            foreach (var element in cornerIndexes)
            {
                if (!xSquares.ToList().Contains(element) && !oSquares.ToList().Contains(element))
                {
                    return element;
                }
            }
            foreach (var element in otherIndexes)
            {
                if (!xSquares.ToList().Contains(element) && !oSquares.ToList().Contains(element))
                {
                    return element;
                }
            }
            return 0;
        }

        private static int TurnNumberToCorrectIndex(int turn)
        {
            if (turn % 2 != 0) { return (turn - 1) / 2; }
            else { return (turn / 2) - 1; }
        }

        public static byte WinThreat(byte[] xMoveHistory, byte[] oMoveHistory)
        {
            byte winningSquare = 0;
            if (oMoveHistory.ToList().IndexOf(0) == xMoveHistory.ToList().IndexOf(0)) 
            {
                foreach (var element in xMoveHistory)
                {
                    foreach (var data in xMoveHistory)
                    {
                        if (IsProperBytePair(element, data))
                        {
                            byte difference = (byte)Math.Abs(element - data);

                            winningSquare = CreatePotentialWin(element, data, difference);
                            if (winningSquare != 0 && !oMoveHistory.Contains(winningSquare)) { return winningSquare; }
                        }
                    }
                }
            }
            else
            {
                foreach (var element in oMoveHistory)
                {
                    foreach (var data in oMoveHistory)
                    {
                        if (IsProperBytePair(element, data))
                        {
                            byte difference = (byte)Math.Abs(element - data);

                            winningSquare = CreatePotentialWin(element, data, difference);
                            if (winningSquare != 0 && !xMoveHistory.Contains(winningSquare)) { return winningSquare; }
                        }
                    }
                }
            }
            return 0;
        } 

        private static byte CreatePotentialWin(byte element, byte data, byte difference)
        {
            byte[] potentialWin = new byte[3];

            if (element > data) { potentialWin[0] = element; potentialWin[1] = data; }
            else { potentialWin[1] = element; potentialWin[0] = data; }

            byte upperPossibility = (byte)(potentialWin[0] + difference);
            byte lowerPossibility = (byte)(potentialWin[1] - difference);
            byte output = 0;

            if (Constants.minimumInputValue <= upperPossibility && upperPossibility <= Constants.maximumInputValue)
            {
                potentialWin = [potentialWin[1], potentialWin[0], upperPossibility];
                output = FindWinThreatSquare(potentialWin, true);
                if (output != 0) { return output; }
            }
            if (Constants.minimumInputValue <= lowerPossibility && lowerPossibility <= Constants.maximumInputValue)
            {
                potentialWin = [lowerPossibility, potentialWin[1], potentialWin[0]];
                output = FindWinThreatSquare(potentialWin, false);
                if (output != 0) { return output; }
            }
            return output;
        }

        private static bool IsProperBytePair(byte element, byte data)
        {
            if (element != data && element != 0 && data != 0) { return true; }
            else { return false; }
        }

        private static byte FindWinThreatSquare(byte[] potentialWin, bool isUpper)
        {
            byte difference = 0;

            if (isUpper) { difference = potentialWin[2]; }
            else if (!isUpper) { difference = potentialWin[0];}

            foreach (byte[] array in Constants.winningCombinationsOfCoördinates)
            {
                if (array[0] == potentialWin[0] && array[1] == potentialWin[1] && array[2] == potentialWin[2])
                {
                    return difference;
                }
            }
            return 0;
        }
    }
}
