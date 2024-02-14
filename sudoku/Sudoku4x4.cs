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
        var (solver, situation) = BuildSudoku4X4();


        situation.S("a", "3", V3);
        situation.S("a", "4", V4);
        situation.S("d", "1", V4);
        situation.S("d", "2", V2);
        
        

        Console.WriteLine("Initial");
        Console.WriteLine(situation);
        
        while (! situation.IsSolved())
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                var ass = solver.SolveNextStep();
                Console.WriteLine(ass);
                Console.WriteLine(situation);
            }

            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
        // Situation solved = solver.SolveCompletely();
        // Console.WriteLine("\nSolved");
        // Console.WriteLine(solved);
    }

    private static (Solver solver, Sudoku situation) BuildSudoku4X4()
    {
        HashSet<object> values = [V1, V2, V3, V4];

        SudokuField a1 = new("a1", values), b1 = new("b1", values), c1 = new("c1", values), d1 = new("d1", values);
        SudokuField a2 = new("a2", values), b2 = new("b2", values), c2 = new("c2", values), d2 = new("d2", values);
        SudokuField a3 = new("a3", values), b3 = new("b3", values), c3 = new("c3", values), d3 = new("d3", values);
        SudokuField a4 = new("a4", values), b4 = new("b4", values), c4 = new("c4", values), d4 = new("d4", values);

        var index = new SudokuField[][]
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
            new Entanglement(a1, b1, c1, d1),
            new Entanglement(a2, b2, c2, d2),
            new Entanglement(a3, b3, c3, d3),
            new Entanglement(a4, b4, c4, d4),
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
        var situation =  new Sudoku(allParticles, allEntanglements, values);
        Solver solver = new Solver(situation);

        return (solver, situation);
    }
}