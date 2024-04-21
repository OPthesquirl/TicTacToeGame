using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

internal class Tools
{
    public static bool IsInputError(byte input, byte[] xSquares, byte[] oSquares)
    {
        if (IsInputOutOfBoundsError(input) || IsOccupiedSquare(input, oSquares, xSquares))
        {
            return true;
        }
        return false;
    }

    public static bool IsInputOutOfBoundsError(byte input)
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

    public static int FirstEmptyIndex(byte[] array)
    {
        int counter = 0;
        foreach (byte b in array)
        {
            if (b == 0) { return counter; }
            if (counter != array.Length) { counter++; }
        }
        return -1;
    }

}
