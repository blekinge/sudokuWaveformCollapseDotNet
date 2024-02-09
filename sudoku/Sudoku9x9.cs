using waveformCollapse;

namespace sudoku;

public class Sudoku9X9(
    Particle[][] index,
    ICollection<SudokuField> allParticles,
    ICollection<Entanglement> allEntanglements,
    ICollection<object> allPossibleValues)
    : Sudoku(allParticles, allEntanglements, allPossibleValues)
{
    public static Sudoku9X9 BuildSudoku9X9()
    {
        SudokuValue v1 = new("1");
        SudokuValue v2 = new("2");
        SudokuValue v3 = new("3");
        SudokuValue v4 = new("4");
        SudokuValue v5 = new("5");
        SudokuValue v6 = new("6");
        SudokuValue v7 = new("7");
        SudokuValue v8 = new("8");
        SudokuValue v9 = new("9");
        HashSet<object> values = [v1, v2, v3, v4, v5, v6, v7, v8, v9];


        //column1
        Particle a1 = new(values), b1 = new(values), c1 = new(values);
        Particle a2 = new(values), b2 = new(values), c2 = new(values);
        Particle a3 = new(values), b3 = new(values), c3 = new(values);
        Particle a4 = new(values), b4 = new(values), c4 = new(values);
        Particle a5 = new(values), b5 = new(values), c5 = new(values);
        Particle a6 = new(values), b6 = new(values), c6 = new(values);
        Particle a7 = new(values), b7 = new(values), c7 = new(values);
        Particle a8 = new(values), b8 = new(values), c8 = new(values);
        Particle a9 = new(values), b9 = new(values), c9 = new(values);


        Particle d1 = new(values), e1 = new(values), f1 = new(values);
        Particle d2 = new(values), e2 = new(values), f2 = new(values);
        Particle d3 = new(values), e3 = new(values), f3 = new(values);
        Particle d4 = new(values), e4 = new(values), f4 = new(values);
        Particle d5 = new(values), e5 = new(values), f5 = new(values);
        Particle d6 = new(values), e6 = new(values), f6 = new(values);
        Particle d7 = new(values), e7 = new(values), f7 = new(values);
        Particle d8 = new(values), e8 = new(values), f8 = new(values);
        Particle d9 = new(values), e9 = new(values), f9 = new(values);

        Particle g1 = new(values), h1 = new(values), i1 = new(values);
        Particle g2 = new(values), h2 = new(values), i2 = new(values);
        Particle g3 = new(values), h3 = new(values), i3 = new(values);
        Particle g4 = new(values), h4 = new(values), i4 = new(values);
        Particle g5 = new(values), h5 = new(values), i5 = new(values);
        Particle g6 = new(values), h6 = new(values), i6 = new(values);
        Particle g7 = new(values), h7 = new(values), i7 = new(values);
        Particle g8 = new(values), h8 = new(values), i8 = new(values);
        Particle g9 = new(values), h9 = new(values), i9 = new(values);

        var index = new Particle[][]
        {
            [a1, b1, c1, d1, e1, f1, g1, h1, i1],
            [a2, b2, c2, d2, e2, f2, g2, h2, i2],
            [a3, b3, c3, d3, e3, f3, g3, h3, i3],
            [a4, b4, c4, d4, e4, f4, g4, h4, i4],
            [a5, b5, c5, d5, e5, f5, g5, h5, i5],
            [a6, b6, c6, d6, e6, f6, g6, h6, i6],
            [a7, b7, c7, d7, e7, f7, g7, h7, i7],
            [a8, b8, c8, d8, e8, f8, g8, h8, i8],
            [a9, b9, c9, d9, e9, f9, g9, h9, i9]
        };
        var allParticles = index.AsEnumerable()
            .SelectMany(particles => particles.AsEnumerable())
            .ToHashSet();

        HashSet<Entanglement> allEntanglements =
        [
            //Boxes
            new SudokuEntanglement(allParticles, ["a", "b", "c"], ["1", "2", "3"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c"], ["4", "5", "6"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c"], ["7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["d", "e", "f"], ["1", "2", "3"]),
            new SudokuEntanglement(allParticles, ["d", "e", "f"], ["4", "5", "6"]),
            new SudokuEntanglement(allParticles, ["d", "e", "f"], ["7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["g", "h", "i"], ["1", "2", "3"]),
            new SudokuEntanglement(allParticles, ["g", "h", "i"], ["4", "5", "6"]),
            new SudokuEntanglement(allParticles, ["g", "h", "i"], ["7", "8", "9"]),

            //Rows
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["1"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["2"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["3"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["4"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["5"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["6"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["7"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["8"]),
            new SudokuEntanglement(allParticles, ["a", "b", "c", "d", "e", "f", "g", "h", "i"], ["9"]),

            //Columns
            new SudokuEntanglement(allParticles, ["a"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["b"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["c"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["d"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["e"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["f"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["g"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["h"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"]),
            new SudokuEntanglement(allParticles, ["i"], ["1", "2", "3", "4", "5", "6", "7", "8", "9"])
        ];
        return new Sudoku9X9(index, allParticles, allEntanglements, values);

    }
    
/*

public void readOds(Path odsFile) throws IOException {
SpreadSheet spread = new SpreadSheet(odsFile.toFile());

List<Sheet> sheets = spread.getSheets();

Sheet sheet = spread.getSheet("sudoku");
com.github.miachm.sods.Range range = sheet.getDataRange();
List<Integer> rows = List.of(0, 1, 2, 3, 4, 5, 6, 7, 8);
List<Integer> columns = List.of(0, 1, 2, 3, 4, 5, 6, 7, 8);
for (Integer row : rows) {
    for (Integer column : columns) {
        com.github.miachm.sods.Range cell = range.getCell(row, column);
        s(getIndex()[row][column], toValue(cell.getValue()));
    }
}
}


private Value toValue(Object value) {
if (value == null) {
    return null;
}
if (value instanceof Double) {
    Double d = (Double) value;
    return new Value("" + d.intValue());
}
return new Value("" + (value.toString()));
}

public void writeOds(Path odsFile) throws IOException {
SpreadSheet spread = new SpreadSheet(odsFile.toFile());

Style style1 = new Style();
style1.setTextAligment(Style.TEXT_ALIGMENT.Center);


Style style2 = new Style();
style2.setTextAligment(Style.TEXT_ALIGMENT.Center);
style2.setFontColor(new Color(0, 255, 0));

Sheet sheet = new Sheet("solution");
sheet.appendColumns(9);
sheet.appendRows(9);
sheet.setColumnWidths(0, 9, 4.52);
sheet.setRowHeights(0, 9, 4.62);

com.github.miachm.sods.Range range = sheet.getRange(0, 0, 9, 9);
List<Integer> rows = List.of(0, 1, 2, 3, 4, 5, 6, 7, 8);
List<Integer> columns = List.of(0, 1, 2, 3, 4, 5, 6, 7, 8);
for (Integer row : rows) {
    for (Integer column : columns) {
        var cell = range.getCell(row, column);
        Particle p = getIndex()[row][column];
        if (p.getValue() != null) {
            cell.setValue(p.getValue().getValue());
            if (p.getDerived()) {
                cell.setStyle(style2);
            } else {
                cell.setStyle(style1);
            }
        }


    }
}

spread.appendSheet(sheet);

spread.save(odsFile.toFile());
}
*/
}