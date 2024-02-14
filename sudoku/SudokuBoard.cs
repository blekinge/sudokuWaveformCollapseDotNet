using System.Text;
using waveformCollapse;

namespace sudoku;

public class SudokuBoard(
    ICollection<SudokuField> allParticles,
    HashSet<Entanglement> allEntanglements,
    HashSet<object> allPossibleValues)
    : Situation(allParticles.Cast<Particle>().ToList(), allEntanglements, allPossibleValues)
{
    private const String AnsiReset = "\u001B[0m";

    private const String AnsiGreen = "\u001B[32m";
    public const String AnsiFramed = "\u001B[51m";
    private const String AnsiBold = "\u001B[1m";

    public void S(String column, String row, object? v)
    {
        Particle? p = GetNamedParticle(column + row);
        if (v == null) return;
        if (p == null) return;
        p.value = v;
        p.derived = false;
    }


    public override String ToString()
    {
        var result = new StringBuilder();
        var width = (int)Math.Round(Math.Sqrt(AllParticles.Count));
        var boxWidth = (int)Math.Round(Math.Sqrt(Math.Sqrt(AllParticles.Count)));
        var particles = AllParticles
            .Cast<SudokuField>()
            .OrderBy(particle => particle?.row)
            .ThenBy(particle => particle?.column)
            .ToList();
        var columnNames = AllParticles.Cast<SudokuField>().Select(p => p.column).Distinct().Order().ToList();
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

                result.Append($"{particle.row}|");
            }

            result.Append('\'');
            ShowValue(particle, particle.value?.ToString());
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
            else if (lastSet is not null && particle == lastSet)
            {
                result.Append(AnsiBold)
                    .Append(value)
                    .Append(AnsiReset);
                ;
            }
            else if (particle.derived is true)
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