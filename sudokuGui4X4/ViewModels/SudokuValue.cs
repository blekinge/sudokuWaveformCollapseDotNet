using System;

namespace sudokuGui.ViewModels;

public class SudokuValue(String number)
{
    public override string ToString()
    {
        return $"{number}";
    }
}