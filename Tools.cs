
namespace TicTacToeGame;

internal class Tools
{
    public static int CurrentTurn(byte[] xMoveHistory, byte[] oMoveHistory)
    {
        int currentTurn = 0;
        for (int i = 0; i < xMoveHistory.Count(); i++)
        {
            if (xMoveHistory[i] != 0)
            {
                currentTurn++;
            }

            if (i == xMoveHistory.Count() - 1)
            {
                break;
            }

            if (oMoveHistory[i] != 0)
            {
                currentTurn++;
            }
        }
        return currentTurn;
    }

    public static bool IsWinCondition(byte[] xSquares, byte[] oSquares)
    {
        foreach (var element in Constants.winningCombinationsOfCoordinates)
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

    public static bool IsTakenSquareError(byte input, byte[] xSquares, byte[] oSquares)
    {
        return xSquares.Contains(input) || oSquares.Contains(input);
    }

    public static bool IsInputOutOfBoundsError(byte input)
    {
        return input > Constants.maximumNumberOfTurnsAndInputValues || input < Constants.minimumInputValue;
    }

    public static bool IsXTurn(int turn)
    {
        return turn == 0 || turn % 2 != 0;
    }

    public static bool isX(string playerRole)
    {
        return playerRole == "x" || playerRole == "X";
    }

    public static bool isO(string playerRole)
    {
        return playerRole == "o" || playerRole == "O";
    }

    public static string WinnerName(byte[] xMoveHistory, byte[] oMoveHistory, string playerXName, string playerOName)
    {
        foreach (var element in Constants.winningCombinationsOfCoordinates)
        {
            if (IsSubsetOf(element, xMoveHistory))
            {
                return playerXName;
            }
        }

        return playerOName;
    }

    public static string LoserName(byte[] xMoveHistory, byte[] oMoveHistory, string playerXName, string playerOName)
    {
        foreach (var element in Constants.winningCombinationsOfCoordinates)
        {
            if (IsSubsetOf(element, xMoveHistory))
            {
                return playerOName;
            }
        }

        return playerXName;
    }


}
