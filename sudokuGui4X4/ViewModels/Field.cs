using System.Collections.Generic;
using System.Text.RegularExpressions;
using waveformCollapse;

namespace sudokuGui.ViewModels;

public class Field(string name, ICollection<object> possibleValues) : Particle(possibleValues)
{
    public int Column { get; } = Regex.Replace(name, "\\d", "").ToCharArray()[0]-'a';
    public int Row { get; } = int.Parse(Regex.Replace(name, "[a-z]", ""))-1;

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(name)}: {name}, {nameof(Column)}: {Column}, {nameof(Row)}: {Row}";
    }
}