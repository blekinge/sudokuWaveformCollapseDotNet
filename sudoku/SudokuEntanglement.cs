using waveformCollapse;

namespace sudoku;

public class SudokuEntanglement(HashSet<SudokuField> allParticles, List<String> columns, List<String> rows)
    : Entanglement(allParticles.AsEnumerable()
                       .Where(particle => columns.Contains(particle.Column))
                       .Where(particle => rows.Contains(particle.Row))
                       .Cast<Particle>()
                       .ToArray());