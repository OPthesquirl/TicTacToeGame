using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;

internal class Tools
{
    public static void ExitLoop()
    {
        ConsoleOutputs.DisplayLine(Constants.exitExplanationLine);
        while (!ConsoleInputs.IsKeyPressed(ConsoleKey.Enter)) { }
    }
    public static bool IsWinCondition(byte[] xSquares, byte[] oSquares)
    {
        foreach (var element in Constants.winningCombinationsOfCoördinates)
        {
            if (IsSubsetOf(element, xSquares))
            {
                return true;
            }
            else if (IsSubsetOf(element, oSquares))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsDraw(byte[] xSquares, byte[] oSquares)
    {
        if (!IsWinCondition(xSquares, oSquares) && xSquares.Last() != 0)
        {
            return true;
        }
        return false;
    }

    public static void StoreHistoryFile(string playerXName, string playerOName, byte[]xMoveHistory, byte[] oMoveHistory)
    {
        var history = new History()
        {
            Date = DateTime.Now,
            PlayerXName = playerXName,
            PlayerOName = playerOName,
            XMoveHistory = Tools.ByteArrayToInt(xMoveHistory),
            OMoveHistory = Tools.ByteArrayToInt(oMoveHistory)
        };
        IHistoryService historyService = new JsonHistoryService();
        historyService.WriteHistoryFile(history);
    }

    public static string[] OrderNamesToWinnerLoser(byte[] xMoveHistory, byte[] oMoveHistory, string playerXName, string playerOName)
    {
        foreach (var element in Constants.winningCombinationsOfCoördinates)
        {
            if (Tools.IsSubsetOf(element, xMoveHistory))
            {
                return [playerXName, playerOName];
            }
            else
            {
                return [playerOName, playerXName];
            }
        }
        return ["", ""];
    }

    public static int CurrentTurn(byte[] xMoveHistory, byte[] oMoveHistory)
    {
        if (xMoveHistory.ToList().IndexOf(0) == -1) { return xMoveHistory.Length + oMoveHistory.Length - 2; }
        else if (oMoveHistory.ToList().IndexOf(0) == -1) { return xMoveHistory.Length + oMoveHistory.Length - 3; }
        else
        {
            return xMoveHistory.ToList().IndexOf(0) + oMoveHistory.ToList().IndexOf(0) + 2;
        }
    }

    public static byte[] IntegerToByteArray(int integer, int arraySize)
    {
        byte[] output = new byte[arraySize];
        int index = 0;

        for(int i = arraySize-1; i >= 0; i--)
        {
            byte value = 0;
            while(integer >= Math.Pow(10, i))
            {
                integer -= (int)Math.Pow(10, i);
                value++;
            }
            output[index++] = value;
        }

        return output;

    }

    public static int ByteArrayToInt(byte[] array)
    {
        int integer = 0;
        int power = array.Length - 1;
        for (int i = 0; i < array.Length; i++)
        {
            integer += array[i] * (int)Math.Pow(10, power);
            power--;
        }
        return integer;
    }

    public static bool IsInputError(byte input, byte[] xSquares, byte[] oSquares)
    {
        if (IsInputOutOfBoundsError(input) || IsOccupiedSquare(input, oSquares, xSquares))
        {
            return true;
        }
        return false;
    }

    public static bool IsInputOutOfBoundsError(int input)
    {
        if (input > Constants.maximumNumberOfTurnsAndInputValues || input < Constants.minimumInputValue)
        {
            return true;

        }
        return false;
    }
    public static bool IsOccupiedSquare(byte input, byte[] xSquares, byte[] oSquares)
    {
        foreach (var element in xSquares)
        {
            if (input == element)
            {
                return true;
            }
        }
        foreach (var element in oSquares)
        {
            if (input == element)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsSubsetOf(byte[] subsetArray, byte[] fullArray)
    {
        foreach (byte b in subsetArray)
        {
            if (!IsElementOf(fullArray, b))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsElementOf(byte[] array, byte element)
    {
        foreach (var el in array)
        {
            if (el == element) return true;
        }

        return false;
    }

    public static byte FirstEmptyIndex(byte[] array)
    {
        byte i;
        for (i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                return i;
            }
        }
        return i;
    }

}
