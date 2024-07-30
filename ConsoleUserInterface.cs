using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;

internal class ConsoleUserInterface
{
    public static void DisplayGamesWithPlayerName(string playerName, IHistoryService historyService)
    {
        List<History> historyListWithPlayerName = historyService.FilterHistoriesByPlayerName(historyService.ReadHistoryFile(Constants.fileName), playerName);

        if (historyListWithPlayerName.Count == 0)
        {
            ConsoleOutputs.DisplayLine(Constants.Error404Line);
        }
        else
        {
            int gamenumber = 1;
            byte[] xMoveHistory, oMoveHistory;

            foreach (History history in historyListWithPlayerName)
            {
                xMoveHistory = Tools.IntegerToByteArray(history.XMoveHistory, history.XMoveHistory.ToString().Length);
                oMoveHistory = Tools.IntegerToByteArray(history.OMoveHistory, history.OMoveHistory.ToString().Length);

                ConsoleOutputs.DisplayLine(Constants.gameNumberLine + gamenumber++);
                ConsoleOutputs.DisplayLine(history.Date.ToString());
                ConsoleOutputs.DisplayWinOrDraw(xMoveHistory, oMoveHistory, history.PlayerXName, history.PlayerOName);
                ConsoleOutputs.ViewTicTacToeBoard(xMoveHistory, oMoveHistory);
            }
        }
    }

    public static byte[][] PlayComputerGame(string playerRole, string[] playerNames)
    {
        byte[] xMoves = new byte[5];
        byte[] oMoves = new byte[4];

        ConsoleOutputs.GameStartExplanation();

        if (Tools.isO(playerRole))
        {
            while (!Tools.IsWinCondition(xMoves, oMoves))
            {
                xMoves = ComputerOpponent.PlayXTurn(xMoves, oMoves);

                if (xMoves.Last() != 0)
                {
                    break;
                }

                ConsoleOutputs.ViewTicTacToeBoard(xMoves, oMoves);
                ConsoleOutputs.DisplayLine(Constants.oTurnLine);

                oMoves[oMoves.ToList().IndexOf(0)] = MoveInputWithErrorCheck(xMoves, oMoves);
            }

            ConsoleOutputs.GameEndOutputs(xMoves, oMoves, playerNames[0], playerNames[1]);
        }
        else if (Tools.isX(playerRole))
        {
            while (!Tools.IsWinCondition(xMoves, oMoves))
            {
                ConsoleOutputs.DisplayLine(Constants.xTurnLine);
                xMoves[xMoves.ToList().IndexOf(0)] = MoveInputWithErrorCheck(xMoves, oMoves);

                if (Tools.IsWinCondition(xMoves, oMoves) || xMoves.Last() != 0)
                {
                    break;
                }

                oMoves = ComputerOpponent.PlayOTurn(xMoves, oMoves);
                if (Tools.IsWinCondition(xMoves, oMoves))
                {
                    break;
                }
                ConsoleOutputs.ViewTicTacToeBoard(xMoves, oMoves);
            }

            ConsoleOutputs.GameEndOutputs(xMoves, oMoves, playerNames[0], playerNames[1]);
        }
        return [xMoves, oMoves];
    }

    public static void PlayPvPTurn(byte[] xSquares, byte[] oSquares, int turn)
    {
        if (Tools.IsXTurn(turn))
        {
            ConsoleOutputs.DisplayLine(Constants.xTurnLine);
        }
        else
        {
            ConsoleOutputs.DisplayLine(Constants.oTurnLine);
        }

        byte input = MoveInputWithErrorCheck(xSquares, oSquares);

        if (Tools.IsXTurn(turn))
        {
            xSquares[xSquares.ToList().IndexOf(0)] = input;
            return;
        }
        else
        {
            oSquares[oSquares.ToList().IndexOf(0)] = input;
            return;
        }
    }

    public static byte MoveInputWithErrorCheck(byte[] xSquares, byte[] oSquares)
    {
        byte playerMove = ConsoleInputs.GetConsoleByteInput();
        if (Tools.IsTakenSquareError(playerMove, xSquares, oSquares) || Tools.IsInputOutOfBoundsError(playerMove))
        {
            ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
            playerMove = ConsoleInputs.GetConsoleByteInput();
        }

        return playerMove;
    }

    public static string UserTeamChoiceWithErrorCheck()
    {
        ConsoleOutputs.DisplayLine("Play as X(input: X), Play as O(input: O)");
        string playerColor = ConsoleInputs.ReadConsoleStringInput();

        while (!Tools.isX(playerColor) && !Tools.isO(playerColor))
        {
            ConsoleOutputs.DisplayLine(Constants.invalidInputLine);
            playerColor = ConsoleInputs.ReadConsoleStringInput();
        }

        return playerColor;
    }

    public static string TakeUserName()
    {
        ConsoleOutputs.DisplayLine(Constants.inputPlayerName);
        
        return ConsoleInputs.ReadConsoleStringInput();
    }

    public static string[] TakeUserNames()
    {
        ConsoleOutputs.DisplayLine("Player X name:");
        string playerXName = ConsoleInputs.ReadConsoleStringInput();

        ConsoleOutputs.DisplayLine("Player O name:");
        string playerOName = ConsoleInputs.ReadConsoleStringInput();

        return [playerXName, playerOName];
    }

    public static void ScrollChosenGame(byte[] xMoveHistory, byte[] oMoveHistory)
    {
        byte[][] moveHistories = [xMoveHistory, oMoveHistory];

        byte[] fixedXMoveHistory = new byte[moveHistories[0].Length];
        byte[] fixedOMoveHistory = new byte[moveHistories[1].Length];

        Array.Copy(xMoveHistory, fixedXMoveHistory, xMoveHistory.Length);
        Array.Copy(oMoveHistory, fixedOMoveHistory, oMoveHistory.Length);

        int turn = Tools.CurrentTurn(xMoveHistory, oMoveHistory);

        while (true)
        {
            var pressedKey = Console.ReadKey().Key;
            if (pressedKey == ConsoleKey.LeftArrow)
            {
                moveHistories = LeftKeyPress(turn, moveHistories[0], moveHistories[1]);

                if (turn > 0) 
                {
                    turn--; 
                }

                Console.Clear();
                ConsoleOutputs.ScrollHistoryGameDisplay(moveHistories[0], moveHistories[1]);
            }
            else if (pressedKey == ConsoleKey.RightArrow)
            {
                moveHistories = RightKeyPress(turn, moveHistories[0], fixedXMoveHistory, moveHistories[1], fixedOMoveHistory);

                if (turn < Constants.maximumNumberOfTurnsAndInputValues) 
                { 
                    turn++; 
                }

                Console.Clear();
                ConsoleOutputs.ScrollHistoryGameDisplay(moveHistories[0], moveHistories[1]);
            }
            else if (pressedKey == ConsoleKey.Enter)
            {
                break;
            }
        }
    }

    public static int ChooseGameHistoryFile(List<History> historyList)
    {
        ConsoleOutputs.DisplayLine(Constants.chooseAGameLine);
        return ConsoleInputs.GetConsoleByteInput() - 1;
    }

    private static byte[][] RightKeyPress(int turn, byte[] xMoveHistory, byte[] fixedXMoveHistory, byte[] oMoveHistory, byte[] fixedOMoveHistory)
    {
        if (Tools.IsXTurn(turn))
        {
            xMoveHistory[TurnNumberToCorrectIndex(turn)] = fixedXMoveHistory[TurnNumberToCorrectIndex(turn)];
        }
        else
        {
            oMoveHistory[TurnNumberToCorrectIndex(turn)] = fixedOMoveHistory[TurnNumberToCorrectIndex(turn)];
        }

        return [xMoveHistory, oMoveHistory];
    }

    private static byte[][] LeftKeyPress(int turn, byte[] xMoveHistory, byte[] oMoveHistory)
    {
        if (Tools.IsXTurn(turn))
        {
            xMoveHistory[TurnNumberToCorrectIndex(turn)] = 0;
        }
        else 
        {
            oMoveHistory[TurnNumberToCorrectIndex(turn)] = 0;
        }

        return [xMoveHistory, oMoveHistory];
    }

    private static int TurnNumberToCorrectIndex(int turn)
    {
        if (turn == 0)
        {
            return 0;
        }
        else if (Tools.IsXTurn(turn))
        {
            return (turn - 1) / 2;
        }
        else
        {
            return (turn / 2) - 1;
        }
    }

}
