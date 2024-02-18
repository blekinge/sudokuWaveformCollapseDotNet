using System;
using Avalonia.Controls;
using Avalonia.Input;
using sudoku;

namespace sudokuGui.Views;

public partial class SudokuWindow : Window
{
    public SudokuWindow()
    {
        InitializeComponent();
    }

    private void OptionalValueClicked(object? sender, TappedEventArgs e)
    {
        switch (sender)
        {
            case null:
                return;
            case TextBlock { DataContext: SudokuField sudokuField } textBlock:
                sudokuField.Value = textBlock.Text;
                break;
        }
    }

    private void UnselectClicked(object? sender, TappedEventArgs e)
    {
        switch (sender)
        {
            case null:
                return;
            case TextBlock { DataContext: SudokuField sudokuField }:
                sudokuField.Value = null;
                break;
        }    }
}