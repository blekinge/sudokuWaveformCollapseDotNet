using sudoku;

namespace SudokuTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        // Assert.Pass();
        var (solver, situation) = SudokuGame.BuildSudoku(2);
        
        situation.S("a", "3", "3");
        situation.S("a", "4", "4");
        situation.S("d", "1", "4");
        situation.S("d", "2", "2");
        while (!situation.IsSolved())
        {
            solver.SolveNextStep();
        }

        Assert.That(situation.GetNamedParticle("b1")?.Value is "3", situation.ToString);


    }
}