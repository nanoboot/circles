
namespace Circles
{
    class CellManager
    {

        EmptyCellsRegistry emptyCellsRegistry = new EmptyCellsRegistry();// All empty fields are registered here
        Cell activeCellFrom = null;
        Cell activeCellTo = null;
        Cell cellRow1Column1 = null;//array that refers to the Array at coordinate 1 and 1 of column and row
        public CellManager(int height, int width)
        { buildBoard(height, width); }

        public void setActiveCellFrom(Cell activeCellFrom)
        {this.activeCellFrom=activeCellFrom;
            activeCellFrom.getBallAndDoNotRemoveIt().jump();
        }
        public void addEmptyCell(Cell newEmptyCell)// Inserts the given field into the empty field register.
        { emptyCellsRegistry.addCell(newEmptyCell); }
        public void addFullCell(Cell newFullCell)//Remove the given field from the empty field registry.
        { emptyCellsRegistry.removeCell(newFullCell); }
        public Cell getActiveCellFrom()
        { return this.activeCellFrom; }

        public void setActiveCellTo(Cell activeCellTo)
        { this.activeCellTo = activeCellTo; }

        public Cell getActiveCellTo()
        { return this.activeCellTo; }

        public Cell getCell(int row, int column)//returns the array located at the given row and column coordinate
        {
            Cell currentCell = new Cell();
            currentCell=cellRow1Column1;

            while(currentCell.getColumn()!=column)
            { currentCell = currentCell.getRightCell(); }// Moves to the field that is in the searched column.
            while (currentCell.getRow() != row)
            { currentCell = currentCell.getBottomCell(); }// Moves to the field that is in the searched row.
            return currentCell;
        }
        public Cell getRandomEmptyCell()//return a random field but empty to select the field where the ball will be placed.
        {
            int i = RandomNumberGenerator.getRandomNumber(1, emptyCellsRegistry.getCountOfNodes());
            return emptyCellsRegistry.getCell(i);

        }
        public bool hasAtLeastOneEmptyCell()
        { if (emptyCellsRegistry.getCountOfNodes()!=0) {return true; } else return false; }
        private void buildBoard(int height, int width)// Constructs a dynamically linked array of arrays.
        {
            Cell oldCell = null;// Here is the field that was new the last time another field became the new field.
            Cell newCell = null;// Here is the newly created field.
            Cell firstCellOfCurrentRow = null; // This is where the first field of the current row is stored so that when the current row is finished, it can continue creating the next row.
            Cell topCell = null;// From the new field.
            Cell leftCell = null;// From the new field.
            for (int row = 1; row <= height; row++)// The row variable contains the value of the row whose some field will be formed.
            {
                for (int column = 1; column <= width; column++)// The column variable contains the value of the column whose some field will be formed.
                {
                    oldCell = newCell;// The new field will no longer be new but old.
                    newCell = new Cell(row, column);//A new array is created with the given row and column.
                    emptyCellsRegistry.addCell(newCell);// This field is inserted into the empty fields register, since there is no ball in it yet.
                    if (row == 1)// If row is 1.
                    {
                        topCell = null;//If row is first, references to all fields up will be null
                        if (column == 1) { firstCellOfCurrentRow = newCell; leftCell = null; cellRow1Column1 = newCell; }// The first field of the current line is set to a new field because a new line has started. The field on the left does not exist. Field1A1 is set to the new field.
                        if ((column > 1) & (column < width)) { leftCell = oldCell; leftCell.setRightCell(newCell); }// The field on the left is set to the old one. The field to the right of the field to the left is set to new.
                        if (column == width) { leftCell = oldCell; leftCell.setRightCell(newCell); newCell.setRightCell(null); }// The field on the left is set to the old one. The field to the right of the field to the left is set to new. The field to the right of the new field does not exist.
                    }
                    if ((row > 1) & (row < height))// If the row is greater than 1 and at the same time the row is not the last.
                    {
                        if (column == 1) { topCell = firstCellOfCurrentRow; topCell.setBottomCell(newCell); firstCellOfCurrentRow = newCell; leftCell = null; }// The field above the new field is set to the first field of the previous row. The field below the field above is set to the new field. The first field of the current row is set to a new field because a new row has started. The field on the left does not exist.
                        if ((column > 1) & (column < width)) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); }// The field above the new field is set to the field to the right of the current field above. The field below the field above is set to the new field. The field to the left of the new field is set to the old field. The field to the right of the field to the left of the new field is set to the new field.
                        if (column == width) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); newCell.setRightCell(null); }

                    }
                    if (row == height) // If the row is the last.
                    {
                        if (column == 1) { topCell = firstCellOfCurrentRow; topCell.setBottomCell(newCell); firstCellOfCurrentRow = newCell; leftCell = null; }// The first field of the current line is set to a new field because a new line has started.
                        if ((column > 1) & (column < width)) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); }
                        if (column == width) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); newCell.setRightCell(null); }
                        newCell.setBottomCell(null);
                    }
                    
                    newCell.setTopCell(topCell);
                    newCell.setLeftCell(leftCell);

                }
            }
        }
    }
}
