using System;
using System.Collections.Generic;
using System.Linq;
using waveformCollapse;

namespace sudokuGui.ViewModels;

public class Sudoku4X4 : ViewModelBase
{
    private Situation situation { get; }
    public Solver solver { get; }
    public List<List<Field>> index { get; }

    public void SolveNextStep()
    {
        var ass = solver.SolveNextStep();
        Console.WriteLine(ass);
        if (ass is null) return;
        var row = (ass.Particle as Field).Row;
        var column = (ass.Particle as Field).Column;
        index[row][column].Value = ass?.Value;
    }

    public Sudoku4X4()
    {
        var v1 = "1";
        var v2 = "2";
        var v3 = "3";
        var v4 = "4";
        List<object> values = [v1, v2, v3, v4];

        Field a1 = new("a1", values), b1 = new("b1", values), c1 = new("c1", values), d1 = new("d1", values);
        Field a2 = new("a2", values), b2 = new("b2", values), c2 = new("c2", values), d2 = new("d2", values);
        Field a3 = new("a3", values), b3 = new("b3", values), c3 = new("c3", values), d3 = new("d3", values);
        Field a4 = new("a4", values), b4 = new("b4", values), c4 = new("c4", values), d4 = new("d4", values);

        a3.Value = v3;
        a4.Value = v4;
        d1.Value = v4;
        d2.Value = v2;

        index = new List<List<Field>>()
        {
            new() { a1, b1, c1, d1 },
            new() { a2, b2, c2, d2 },
            new() { a3, b3, c3, d3 },
            new() { a4, b4, c4, d4 }
        };

        ICollection<Particle?> allParticles = index.SelectMany(f => f).Cast<Particle?>().ToHashSet();
        HashSet<Entanglement> allEntanglements =
        [
            //Boxes
            new Box(a1, b1,
                    a2, b2),
            new Box(c1, d1,
                    c2, d2
            ),
            new Box(a3, b3,
                    a4, b4
            ),
            new Box(c3, d3,
                    c4, d4
            ),
            //Rows
            new Row(a1, b1, c1, d1),
            new Row(a2, b2, c2, d2),
            new Row(a3, b3, c3, d3),
            new Row(a4, b4, c4, d4),
            //Columns
            new Column(a1,
                       a2,
                       a3,
                       a4
            ),
            new Column(b1,
                       b2,
                       b3,
                       b4
            ),
            new Column(c1,
                       c2,
                       c3,
                       c4
            ),
            new Column(d1,
                       d2,
                       d3,
                       d4)
        ];
        situation = new Situation(allParticles, allEntanglements, values);
        solver = new Solver(situation);
    }
}