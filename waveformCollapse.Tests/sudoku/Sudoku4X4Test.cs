using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using waveformCollapse.sudoku;

namespace waveformCollapse.Tests.sudoku;

[TestClass]
[TestSubject(typeof(Sudoku4X4))]
public class Sudoku4X4Test
{

    [TestMethod]
    public void Method()
    {
        Sudoku situation = Sudoku4X4.buildSudoku4X4();

        situation.S("a", "3", Sudoku4X4.V3);
        situation.S("a", "4", Sudoku4X4.V4);
        situation.S("d", "1", Sudoku4X4.V4);
        situation.S("d", "2", Sudoku4X4.V2);

        Console.WriteLine("Initial");
        Console.WriteLine(situation);

        Solver solver = new Solver();
        Situation solved = solver.Solve(situation);
        Console.WriteLine("\nSolved");
        Console.WriteLine(solved);
        
        Console.Write(solved.ToString());
    }

}