using System.Text.RegularExpressions;
using waveformCollapse;

namespace sudoku;

public class SudokuField(string name) : Particle(name)
{
    public string Column { get; } = Regex.Replace(name, "\\d", "");
    public string Row { get; } = Regex.Replace(name, "[a-z]", "");
}