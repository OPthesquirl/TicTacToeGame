using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TicTacToeGame.ClassPractice;

public class History
{
    public int XMoveHistory { get; set; }
    public int OMoveHistory { get; set; }
    public DateTime Date { get; set; }

    public string PlayerXName { get; set; }
    public string PlayerOName { get; set; }
}