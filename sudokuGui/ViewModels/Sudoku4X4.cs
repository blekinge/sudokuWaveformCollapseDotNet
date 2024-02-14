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
    private SudokuBoard situation { get; }
    private Solver solver { get; }
    public List<List<SudokuField>> index { get; }

    public void SolveNextStep()
    {
        var ass = solver.SolveNextStep();
        Console.WriteLine(ass);
        var propertyChangingEventArgs = new PropertyChangingEventArgs(nameof(index));
        ((IReactiveObject)this).RaisePropertyChanging(propertyChangingEventArgs);

    }

    public Sudoku4X4()
    {
        (solver, situation) = SudokuGame.BuildSudoku(2);
        
        
        situation.S("a", "3", "3");
        situation.S("a", "4", "4");
        situation.S("d", "1", "4");
        situation.S("d", "2", "2");

        index = new List<List<SudokuField>>();
        foreach (var row in situation.AllParticles.Cast<SudokuField>().Select(p => p.row).Distinct().Order().ToList())
        {
            var list = situation.AllParticles.Cast<SudokuField>().Where(p => p.row == row).OrderBy(p => p.name)
                .ToList();
            index.Add(list);
        }

    }
}