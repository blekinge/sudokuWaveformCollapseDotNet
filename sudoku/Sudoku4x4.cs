using waveformCollapse;

namespace sudoku;

public abstract class Sudoku4X4
{
    private static readonly SudokuValue V1 = new("1");
    private static readonly SudokuValue V2 = new("2");
    private static readonly SudokuValue V3 = new("3");
    private static readonly SudokuValue V4 = new("4");

    public static void Main(string[] args)
    {
        Sudoku situation = BuildSudoku4X4();

        situation.S("a", "3", V3);
        situation.S("a", "4", V4);
        situation.S("d", "1", V4);
        situation.S("d", "2", V2);

        Console.WriteLine("Initial");
        Console.WriteLine(situation);

        Solver solver = new Solver(situation);
        // Situation solved = solver.SolveCompletely();
        // Console.WriteLine("\nSolved");
        // Console.WriteLine(solved);
    }

    private static Sudoku BuildSudoku4X4()
    {
        HashSet<object> values = [V1, V2, V3, V4];

        Particle a1 = new(values), b1 = new(values), c1 = new(values), d1 = new(values);
        Particle a2 = new(values), b2 = new(values), c2 = new(values), d2 = new(values);
        Particle a3 = new(values), b3 = new(values), c3 = new(values), d3 = new(values);
        Particle a4 = new(values), b4 = new(values), c4 = new(values), d4 = new(values);

        var index = new Particle[][]
        {
            [a1, b1, c1, d1],
            [a2, b2, c2, d2],
            [a3, b3, c3, d3],
            [a4, b4, c4, d4]
        };
        var allParticles = index.AsEnumerable()
            .SelectMany(particles => particles.AsEnumerable())
            .ToHashSet();

        HashSet<Entanglement> allEntanglements =
        [
            //Boxes
            new Entanglement(a1, b1,
                             a2, b2),
            new Entanglement(c1, d1,
                             c2, d2
            ),
            new Entanglement(a3, b3,
                             a4, b4
            ),
            new Entanglement(c3, d3,
                             c4, d4
            ),
            //Rows
            new Entanglement(index[0]),
            new Entanglement(index[1]),
            new Entanglement(index[2]),
            new Entanglement(index[3]),
            //Columns
            new Entanglement(a1,
                             a2,
                             a3,
                             a4
            ),
            new Entanglement(b1,
                             b2,
                             b3,
                             b4
            ),
            new Entanglement(c1,
                             c2,
                             c3,
                             c4
            ),
            new Entanglement(d1,
                             d2,
                             d3,
                             d4)
        ];
        return new Sudoku(allParticles, allEntanglements, values);
    }
}