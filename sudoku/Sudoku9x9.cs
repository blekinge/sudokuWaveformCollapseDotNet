using waveformCollapse;

namespace sudoku;

public class Sudoku9X9
{
    public static Sudoku BuildSudoku9X9()
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
        SudokuField a1 = new("a1", values), b1 = new("b1", values), c1 = new("c1", values);
        SudokuField a2 = new("a2", values), b2 = new("b2", values), c2 = new("c2", values);
        SudokuField a3 = new("a3", values), b3 = new("b3", values), c3 = new("c3", values);
        SudokuField a4 = new("a4", values), b4 = new("b4", values), c4 = new("c4", values);
        SudokuField a5 = new("a5", values), b5 = new("b5", values), c5 = new("c5", values);
        SudokuField a6 = new("a6", values), b6 = new("b6", values), c6 = new("c6", values);
        SudokuField a7 = new("a7", values), b7 = new("b7", values), c7 = new("c7", values);
        SudokuField a8 = new("a8", values), b8 = new("b8", values), c8 = new("c8", values);
        SudokuField a9 = new("a9", values), b9 = new("b9", values), c9 = new("c9", values);


        SudokuField d1 = new("d1", values), e1 = new("e1", values), f1 = new("f1", values);
        SudokuField d2 = new("d2", values), e2 = new("e2", values), f2 = new("f2", values);
        SudokuField d3 = new("d3", values), e3 = new("e3", values), f3 = new("f3", values);
        SudokuField d4 = new("d4", values), e4 = new("e4", values), f4 = new("f4", values);
        SudokuField d5 = new("d5", values), e5 = new("e5", values), f5 = new("f5", values);
        SudokuField d6 = new("d6", values), e6 = new("e6", values), f6 = new("f6", values);
        SudokuField d7 = new("d7", values), e7 = new("e7", values), f7 = new("f7", values);
        SudokuField d8 = new("d8", values), e8 = new("e8", values), f8 = new("f8", values);
        SudokuField d9 = new("d9", values), e9 = new("e9", values), f9 = new("f9", values);

        SudokuField g1 = new("g1", values), h1 = new("h1", values), i1 = new("i1", values);
        SudokuField g2 = new("g2", values), h2 = new("h2", values), i2 = new("i2", values);
        SudokuField g3 = new("g3", values), h3 = new("h3", values), i3 = new("i3", values);
        SudokuField g4 = new("g4", values), h4 = new("h4", values), i4 = new("i4", values);
        SudokuField g5 = new("g5", values), h5 = new("h5", values), i5 = new("i5", values);
        SudokuField g6 = new("g6", values), h6 = new("h6", values), i6 = new("i6", values);
        SudokuField g7 = new("g7", values), h7 = new("h7", values), i7 = new("i7", values);
        SudokuField g8 = new("g8", values), h8 = new("h8", values), i8 = new("i8", values);
        SudokuField g9 = new("g9", values), h9 = new("h9", values), i9 = new("i9", values);

        var index = new SudokuField[][]
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
        return new Sudoku(allParticles, allEntanglements, values);

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