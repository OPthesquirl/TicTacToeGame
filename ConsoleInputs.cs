using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

public static class ConsoleInputs
{
    public static byte GetConsoleByteInput()
    {
        byte input = byte.Parse(Console.ReadLine());
        return input;
    }
    public static string GetConsoleStringInput()
    {
        string input = Console.ReadLine();
        return input;
    }
}
