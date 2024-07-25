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
        if (Console.ReadKey(true).Key == key) {  return true; } else { return false; }
    }
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
