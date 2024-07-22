using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame;

internal class Constants
{
    //Constants for logic
    static public int maximumNumberOfTurnsAndInputValues = 9;
    static public int minimumInputValue = 1;

    static public byte[][] winningCombinationsOfCoördinates = [[1, 2, 3], [4, 5, 6], [7, 8, 9], [1, 4, 7], [2, 5, 8], [3, 6, 9], [1, 5, 9], [3, 5, 7]];

    //Constants for output
    static public string xTurnLine = "X's turn, input:";
    static public string oTurnLine = "O's turn, input:";
    static public string invalidInputLine = "Invalid Input";
    static public string exitExplanationLine = "Input any non0 number to exit";

    //Constants for DisplayGameState
    static public char xChar = 'X';
    static public char oChar = 'O';
    static public char emptyChar = ' ';

    static public string verticalDisplayLine = " | ";
    static public string horizontalDisplayLine = "--+---+--";

    static public string fileName = @"C:\\Temp\\CSharpPractice.json";
}
