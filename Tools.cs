using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

internal class Tools
{
    public static byte[] ConvertIntegerToByteArray(int integer, int arraySize)
    {
        byte[] output = new byte[arraySize];

        for (int i = 0; i < arraySize - 1; i++)
        {
            var step = (int)Math.Pow(10, arraySize - 1 - i);
            while (integer >= step)
            {
                integer -= step;
                output[i]++;
            }
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
