using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

public static class ConsoleInputs
{
    public static bool IsKeyPressed(ConsoleKey key)
    {
        return Console.ReadKey(true).Key == key;
    }
    public static byte GetConsoleByteInput()
    {
        return byte.Parse(Console.ReadLine());
    }
    public static string ReadConsoleStringInput()
    {
        return Console.ReadLine();
    }
}
