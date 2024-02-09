using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using sudokuGui.ViewModels;
using sudokuGui.Views;

namespace sudokuGui;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new SudokuWindow()
            {
                DataContext = new Sudoku4X4(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}