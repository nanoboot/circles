
namespace Balls
{
    class CellManager
    {

        EmptyCellsRegistry emptyCellsRegistry = new EmptyCellsRegistry();
        Cell activeCellFrom = null;
        Cell activeCellTo = null;
        Cell cellRow1Column1 = null;
        public CellManager(int height, int width)
        { buildBoard(height, width); }

        public void setActiveCellFrom(Cell activeCellFrom)
        {this.activeCellFrom=activeCellFrom;
            activeCellFrom.getBallAndDoNotRemoveIt().jump();
        }
        public void addEmptyCell(Cell newEmptyCell)
        { emptyCellsRegistry.addCell(newEmptyCell); }
        public void addFullCell(Cell newFullCell)
        { emptyCellsRegistry.removeCell(newFullCell); }
        public Cell getActiveCellFrom()
        { return this.activeCellFrom; }

        public void setActiveCellTo(Cell activeCellTo)
        { this.activeCellTo = activeCellTo; }

        public Cell getActiveCellTo()
        { return this.activeCellTo; }

        public Cell getCell(int row, int column)
        {
            Cell currentCell = new Cell();
            currentCell=cellRow1Column1;

            while(currentCell.getColumn()!=column)
            { currentCell = currentCell.getRightCell(); }
            while (currentCell.getRow() != row)
            { currentCell = currentCell.getBottomCell(); }
            return currentCell;
        }
        public Cell getRandomEmptyCell()
        {
            int i = RandomNumberGenerator.getRandomNumber(1, emptyCellsRegistry.getCountOfNodes());
            return emptyCellsRegistry.getCell(i);

        }
        public bool hasAtLeastOneEmptyCell()
        { if (emptyCellsRegistry.getCountOfNodes()!=0) {return true; } else return false; }
        private void buildBoard(int height, int width)
        {
            Cell oldCell = null;
            Cell newCell = null;
            Cell firstCellOfCurrentRow = null;
            Cell topCell = null;
            Cell leftCell = null;
            for (int row = 1; row <= height; row++)
            {
                for (int column = 1; column <= width; column++)
                {
                    oldCell = newCell;
                    newCell = new Cell(row, column);
                    emptyCellsRegistry.addCell(newCell);
                    if (row == 1)
                    {
                        topCell = null;
                        if (column == 1) { firstCellOfCurrentRow = newCell; leftCell = null; cellRow1Column1 = newCell; }
                        if ((column > 1) & (column < width)) { leftCell = oldCell; leftCell.setRightCell(newCell); }
                        if (column == width) { leftCell = oldCell; leftCell.setRightCell(newCell); newCell.setRightCell(null); }
                    }
                    if ((row > 1) & (row < height))
                    {
                        if (column == 1) { topCell = firstCellOfCurrentRow; topCell.setBottomCell(newCell); firstCellOfCurrentRow = newCell; leftCell = null; }
                        if ((column > 1) & (column < width)) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); }
                        if (column == width) { topCell = topCell.getRightCell(); topCell.setBottomCell(newCell); leftCell = oldCell; leftCell.setRightCell(newCell); newCell.setRightCell(null); }

                    }
                    if (row == height)
                    {
                        if (column == 1) { topCell = firstCellOfCurrentRow; topCell.setBottomCell(newCell); firstCellOfCurrentRow = newCell; leftCell = null; }
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
