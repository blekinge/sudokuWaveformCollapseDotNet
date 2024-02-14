using System.Text.RegularExpressions;
using waveformCollapse;

namespace sudoku;

public class SudokuField(string name, ICollection<object> possibleValues) : Particle(name, possibleValues)
{
    public String Column { get; } = Regex.Replace(name, "\\d", "");
    public String Row { get; } = Regex.Replace(name, "[a-z]", "");
}