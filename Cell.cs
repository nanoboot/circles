namespace Circles;

public class Cell
{
    
    private int row;
    private int column;
    private Ball ball =null;

    public Cell topCell;
    public Cell rightCell;
    public Cell bottomCell;
    public Cell leftCell;
    public void setBall(Ball newBall)
    { if (this.isEmpty()==true)this.ball = newBall; }
    public Ball getBallAndDoNotRemoveIt()
    { if (this.isEmpty() != true) return this.ball;
        else throw new System.Exception("Invalid state");
    }
    
    public Ball getBallAndRemoveIt()
    { if (this.isEmpty() == false) { Ball tmpBall = this.ball;
            this.ball= null;return tmpBall; }
        else { throw new System.Exception("Invalid state"); };
    }
    public bool isEmpty()
    { if(this.ball==null) { return true; } else { return false; } }
    public void setTopCell(Cell cellToBeSet)
    {
        this.topCell = cellToBeSet;
    }
    public void setRightCell(Cell cellToBeSet)
    {
        this.rightCell = cellToBeSet;
    }
    public void setBottomCell(Cell cellToBeSet)
    {
        this.bottomCell = cellToBeSet;
    }
    public void setLeftCell(Cell cellToBeSet)
    {
        this.leftCell = cellToBeSet;
    }
    public Cell getTopCell()
    {
        return this.topCell;
    }
    public Cell getRightCell()
    {
        return this.rightCell;
    }
    public Cell getBottomCell()
    {
        return this.bottomCell;
    }
    public Cell getLeftCell()
    {
        return this.leftCell;
    }
    public Cell getTopLeftCell()
    {
        if ((this.getRow() == 1) || (this.getColumn() == 1)) { return null; } else
        { return this.getLeftCell().getTopCell(); };
    }
    public Cell getTopRightCell()
    {
        if ((this.getRow() == 1) || (this.getRightCell() == null)) { return null; }
        else 
        { return this.getRightCell().getTopCell(); };
    }
    public Cell getBottomLeftCell()
    {
        if ((this.getBottomCell() == null) || (this.getColumn() == 1)) { return null; }
        else
        { return this.getLeftCell().getBottomCell(); };
    }
    public Cell getBottomRightCell()
    {
        if ((this.getRightCell() == null) || (this.getBottomCell() == null)) { return null; }
        else
        { return this.getRightCell().getBottomCell(); };
    }
    public int getColumn()
    { return this.column; }
    public int getRow()
    { return this.row; }
    public Cell(int row, int column)
    {
        this.row= row;
        this.column= column;
        this.topCell = null;
        this.rightCell = null;
        this.bottomCell = null;
        this.leftCell = null;
    }
    public Cell()
    {
        this.topCell = null;
        this.rightCell = null;
        this.bottomCell = null;
        this.leftCell = null;
    }

}