using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TicTacToeGame.ClassPractice;

namespace TicTacToeGame;

internal class Tools
{
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

}
