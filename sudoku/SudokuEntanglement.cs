using waveformCollapse;

namespace sudoku;

public class SudokuEntanglement(IEnumerable<SudokuField> allParticles, ICollection<string> columns, ICollection<string> rows)
    : Entanglement("", allParticles.AsEnumerable()
                       .Where(particle => columns.Contains(particle.Column))
                       .Where(particle => rows.Contains(particle.Row))
                       .Cast<Particle>()
                       .ToArray());