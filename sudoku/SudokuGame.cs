using waveformCollapse;

namespace sudoku;

public abstract class SudokuGame
{
    private static readonly string V1 = "1";
    private static readonly string V2 = "2";
    private static readonly string V3 = "3";
    private static readonly string V4 = "4";
    private static readonly string V5 = "5";
    private static readonly string V6 = "6";
    private static readonly string V7 = "7";
    private static readonly string V8 = "8";
    private static readonly string V9 = "9";

    public static void Main(string[] args)
    {
        var (solver, situation) = BuildSudoku(2);

        situation.S("a", "3", V3);
        situation.S("a", "4", V4);
        situation.S("d", "1", V4);
        situation.S("d", "2", V2);

        Console.WriteLine("Initial");
        Console.WriteLine(situation);

        while (!situation.IsSolved())
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                var (particle, value) = solver.SolveNextStep() ?? default;
                Console.WriteLine(particle);
                Console.WriteLine(situation);
            }

            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }

        Console.WriteLine("\nSolved");
    }


    public static (Solver solver, SudokuBoard situation) BuildSudoku(int dimension)
    {
        var values = Enumerable.Range(1, dimension * dimension).Select(i => $"{i}").Cast<object>().ToList();

        var rows = Enumerable.Range(1, dimension * dimension).Select(i => i.ToString()).ToList();
        var columns = Enumerable.Range('a', dimension * dimension).Select(i => Convert.ToChar(i).ToString()).ToList();

        HashSet<SudokuField> allParticles = [];
        foreach (var field in
                 from column in columns
                 from row in rows
                 select new SudokuField(column + row))
        {
            allParticles.Add(field);
        }


        HashSet<Entanglement> allEntanglements = [];
        foreach (var column in columns)
        {
            allEntanglements.Add(
                new Entanglement(name: $"column-{column}", particles: allParticles.Where(p => p.Column == column).Cast<Particle>().ToArray()));
        }

        foreach (var row in rows)
        {
            allEntanglements.Add(
                new Entanglement(name: $"row-{row}", particles: allParticles.Where(p => p.Row == row).Cast<Particle>().ToArray()));
        }

        for (var i = 0; i < dimension * dimension; i += dimension)
        {
            for (var j = 0; j < dimension * dimension; j += dimension)
            {
                var boxColumns = columns[new Range(i, i + dimension)];
                var boxRows = rows[new Range(j, j + dimension)];

                allEntanglements.Add(
                    new Entanglement(
                        name: $"box-{boxColumns.First()}-{boxColumns.Last()},{boxRows.First()}-{boxRows.Last()}",
                        particles: allParticles
                        .Where(p => boxColumns.Contains(p.Column))
                        .Where(p => boxRows.Contains(p.Row))
                        .Cast<Particle>()
                        .ToArray()));
            }
        }

        SudokuBoard sudokuBoard = new(allParticles, allEntanglements, values);

        Situation situation = sudokuBoard;
        return (new Solver(situation), sudokuBoard);
    }
}