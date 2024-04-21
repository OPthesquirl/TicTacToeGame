using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

public static class ConsoleInputs
{
    public static byte GetConsoleInput()
    {
        byte input = byte.Parse(Console.ReadLine());
        return input;
    }
}
