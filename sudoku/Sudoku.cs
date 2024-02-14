using System.Text;
using waveformCollapse;

namespace sudoku;

public class Sudoku(
    ICollection<SudokuField> allParticles,
    HashSet<Entanglement> allEntanglements,
    HashSet<object> allPossibleValues)
    : Situation(allParticles.Cast<Particle>().ToList(), allEntanglements, allPossibleValues)
{
    private const String ANSI_RESET = "\u001B[0m";

    private const String ANSI_GREEN = "\u001B[32m";
    public const String ANSI_FRAMED = "\u001B[51m";
    private const String ANSI_BOLD = "\u001B[1m";

    private void S(Particle? p, object? v)
    {
        if (v == null) return;
        if (p == null) return;
        p.value = v;
        p.derived = false;
    }

    public void S(String column, String row, object v)
    {
        S(GetNamedParticle(column+row),v);
        // var p = AllParticles
        //             .Cast<SudokuField>()
        //             .Where(p2 => p2.Row == row)
        //             .FirstOrDefault(p2 => p2.Column == column)
        //         ?? throw new IndexOutOfRangeException("Index " +
        //                                               row +
        //                                               "," +
        //                                               column +
        //                                               " out of bounds");
        // S(p, v);
    }


    public override String ToString()
    {
        StringBuilder result = new StringBuilder();
        int width = (int)Math.Round(Math.Sqrt(AllParticles.Count()));
        int boxWidth = (int)Math.Round(Math.Sqrt(Math.Sqrt(AllParticles.Count())));
        int line = 0;
        int i = 0;
        var particles = AllParticles
            .Cast<SudokuField>()
            .OrderBy(particle => particle?.Row)
            .ThenBy(particle => particle?.Column)
            .ToList();
        foreach (var particle in particles)
        {
            if (i % width == 0)
            {
                result.Append("\n");
                line++;
                if (line % boxWidth == 1)
                {
                    result.Append(String.Join("",
                                              Enumerable.Range(0, ((3 * boxWidth + 2 * (boxWidth - 1) + 1) * boxWidth))
                                                  .Select(i1 => "-")))
                        .Append("\n");
                }
            }

            String finish = ", ";
            if ((i + 1) % boxWidth == 0)
            {
                finish = "|";
            }

            var value = particle.value?.ToString();
            if (value == null)
            {
                result.Append("' '");
            }
            else
            {
                result.Append('\'');
                if (lastSet is not null && particle == lastSet)
                {
                    result.Append(ANSI_BOLD).Append(value).Append(ANSI_RESET);
                }
                else if (particle.derived is not null && particle.derived == true)
                {
                    result.Append(ANSI_GREEN).Append(value).Append(ANSI_RESET);
                }
                else
                {
                    result.Append(value);
                }

                result.Append('\'');
            }

            result.Append(finish);
            i++;
        }

        return result.ToString().Trim();
    }
}