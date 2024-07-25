using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame
{
    internal class ComputerOpponent
    {

        public static void ComputerGame(string playerXorO)
        {
            Console.WriteLine("Enter name:");
            string playerName = ConsoleInputs.GetConsoleStringInput();

            if (playerXorO == "o" || playerXorO == "O") { PlayAlgorithmAsX(playerName); }
            else { PlayAlgorithmAsO(playerName); }

            ConsoleUserInterface.ExitLoop();
        }

        private static byte MoveInputWithErrorCheck(byte[] xSquares, byte[] oSquares)
        {
            byte playerMove = ConsoleInputs.GetConsoleByteInput();
            if (Tools.IsInputError(playerMove, xSquares, oSquares))
            {
                ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
                playerMove = ConsoleInputs.GetConsoleByteInput();
            }
            return playerMove;
        }

        public static void PlayAlgorithmAsX(string playerName)
        {
            byte[] xSquares = new byte[5];
            byte[] oSquares = new byte[4];

            xSquares[0] = 5;
            ConsoleOutputs.ViewTicTacToeBoard(xSquares, oSquares);

            byte playerMove = MoveInputWithErrorCheck(xSquares, oSquares);

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
                        playerMove = MoveInputWithErrorCheck(xSquares, oSquares);

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

            IHistoryService historyService = new JsonHistoryService();
            History history = historyService.CreateHistoryFile("The Algorithm", playerName, xSquares, oSquares);
            historyService.WriteHistoryFile(history);
        }

        public static void PlayAlgorithmAsO(string playerName)
        {
            Console.WriteLine("Construction zone");
        }

    }
}
