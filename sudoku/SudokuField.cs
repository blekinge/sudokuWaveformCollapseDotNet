using System.Text.RegularExpressions;
using waveformCollapse;

namespace sudoku;

public class SudokuField(string name, ICollection<object> possibleValues) : Particle(name, possibleValues)
{
    public String column { get; } = Regex.Replace(name, "\\d", "");
    public String row { get; } = Regex.Replace(name, "[a-z]", "");
}