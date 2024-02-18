using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ReactiveUI;
using sudoku;
using waveformCollapse;

namespace sudokuGui.ViewModels;

public class Sudoku4X4 : ViewModelBase
{
    private SudokuBoard Situation { get; }
    private Solver Solver { get; }
    public List<List<SudokuField>> Index { get; }

    public void SolveNextStep()
    {
        var ass = Solver.SolveNextStep();
        Console.WriteLine(ass);
        // var propertyChangingEventArgs = new PropertyChangingEventArgs(nameof(Index));
        // ((IReactiveObject)this).RaisePropertyChanging(propertyChangingEventArgs);
    }

    public Sudoku4X4()
    {
        (Solver, Situation) = SudokuGame.BuildSudoku(2);

        Situation.S("a", "3", "3");
        Situation.S("a", "4", "4");
        Situation.S("d", "1", "4");
        Situation.S("d", "2", "2");

        Index = new List<List<SudokuField>>();
        var rows = Situation.AllParticles
            .Cast<SudokuField>()
            .Select(p => p.Row)
            .Distinct()
            .Order()
            .ToList();
        foreach (var row in rows)
        {
            Index.Add(Situation.AllParticles
                .Cast<SudokuField>()
                .Where(p => p.Row == row)
                .OrderBy(p => p.Name)
                .ToList());
        }
        
        Situation.AllEntanglements.ToList().ForEach(Console.WriteLine);
    }
}