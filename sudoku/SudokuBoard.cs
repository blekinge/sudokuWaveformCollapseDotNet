using System.Text;
using waveformCollapse;

namespace sudoku;

public class SudokuBoard(
    ICollection<SudokuField> allParticles,
    HashSet<Entanglement> allEntanglements,
    List<object> allPossibleValues)
    : Situation(allParticles.Cast<Particle>().ToList(), allEntanglements, allPossibleValues)
{
    private const string AnsiReset = "\u001B[0m";

    private const string AnsiGreen = "\u001B[32m";
    public const string AnsiFramed = "\u001B[51m";
    private const string AnsiBold = "\u001B[1m";

    public void S(string column, string row, object? v)
    {
        Particle? p = GetNamedParticle(column + row);
        if (v == null) return;
        if (p == null) return;
        p.Value = v;
        p.Derived = false;
    }


    public override string ToString()
    {
        var result = new StringBuilder();
        var width = (int)Math.Round(Math.Sqrt(AllParticles.Count()));
        var boxWidth = (int)Math.Round(Math.Sqrt(Math.Sqrt(AllParticles.Count())));
        var particles = AllParticles
            .Cast<SudokuField>()
            .OrderBy(particle => particle?.Row)
            .ThenBy(particle => particle?.Column)
            .ToList();
        var columnNames = AllParticles.Cast<SudokuField>().Select(p => p.Column).Distinct().Order().ToList();
        result.Append("  ");
        foreach (var columnName in columnNames)
        {
            result.Append($" {columnName}  ");
        }

        var line = 0;
        var column = 0;
        foreach (var particle in particles)
        {
            if (column % width == 0)
            {
                result.Append('\n');
                line++;
                if (line % boxWidth == 1)
                {
                    result.Append(' ')
                        .Append(String.Join("",
                            Enumerable.Range(0, (3 * boxWidth + 2 * (boxWidth - 1) + 1) * boxWidth)
                                .Select(i1 => "-")))
                        .Append('\n');
                }

                result.Append($"{particle.Row}|");
            }

            result.Append('\'');
            ShowValue(particle, particle.Value?.ToString());
            result.Append('\'');

            result.Append((column + 1) % boxWidth == 0 ? "|" : " ");

            column++;
        }

        return result.ToString();

        void ShowValue(Particle particle, string? value)
        {
            if (value is null)
            {
                result.Append('-');
            }
            else if (LastSet is not null && particle == LastSet)
            {
                result.Append(AnsiBold)
                    .Append(value)
                    .Append(AnsiReset);
                ;
            }
            else if (particle.Derived is true)
            {
                result.Append(AnsiGreen)
                    .Append(value)
                    .Append(AnsiReset);
            }
            else
            {
                result.Append(value);
            }
        }
    }
}