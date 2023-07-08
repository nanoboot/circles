using System;
using System.Collections.Generic;

namespace Circles
{
    class BallExploder
    {
        private string shape;
        private int minLineLength;
        private Stack<Cell> explodedBalls = new Stack<Cell>();
        private Cell cellToBeChecked = null;
        String colour;
        bool shapeIsFound = false;
        Cell currentCell;
        public BallExploder(String shape,int minLineLength)
        { this.shape = shape;
            this.minLineLength = minLineLength;
        }
        public Stack<Cell> checkAndExplodeIfNeeded(Cell cellToBeChecked)
        {
            this.cellToBeChecked = cellToBeChecked;
            currentCell=cellToBeChecked;
            colour = cellToBeChecked.getBallAndDoNotRemoveIt().getColour();
            shapeIsFound = false;
            switch (this.shape)
            {
                case "line": explodeLine(); break;
                case "ctverec": explodeSquare(); break;
                case "krouzek": explodeCircle(); break;
                default: { }; break;
            }
            return this.explodedBalls;//
        }
        private void explodeLine()
        {
            String[] cellPosition = { "vertical", "horizontal", "slantfromleft", "slantfromright" };
            foreach (String position in cellPosition)
            { if (shapeIsFound) { break; } else { checkLine(position); } }

        }
        private void checkLine(String position)
        {
            {
                currentCell = cellToBeChecked;
                while ((currentCell != null) && (!currentCell.isEmpty()) && (currentCell.getBallAndDoNotRemoveIt().hasColour(colour)))
                {
                    explodedBalls.Push(currentCell);
                    switch (position)
                    {
                        case "vertical":
                            currentCell = currentCell.getTopCell(); break;
                        case "horizontal":
                            currentCell = currentCell.getLeftCell(); break;
                        case "slantfromleft":
                            currentCell = currentCell.getTopLeftCell(); break;
                        case "slantfromright":
                            currentCell = currentCell.getTopRightCell(); break;
                            
                            } 
                };
                currentCell = cellToBeChecked;
                switch (position)
                {
                    case "vertical":
                        currentCell = currentCell.getBottomCell(); break;
                    case "horizontal":
                        currentCell = currentCell.getRightCell(); break;
                    case "slantfromleft":
                        currentCell = currentCell.getBottomRightCell(); break;
                    case "slantfromright":
                        currentCell = currentCell.getBottomLeftCell(); break;

                }
                
                while ((currentCell != null) && (!currentCell.isEmpty()) && (currentCell.getBallAndDoNotRemoveIt().hasColour(colour)))
                {
                    explodedBalls.Push(currentCell);
                    switch (position)
                    {
                        case "vertical":
                            currentCell = currentCell.getBottomCell(); break;
                        case "horizontal":
                            currentCell = currentCell.getRightCell(); break;
                        case "slantfromleft":
                            currentCell = currentCell.getBottomRightCell(); break;
                        case "slantfromright":
                            currentCell = currentCell.getBottomLeftCell(); break;
                }
                };

            }
            if (explodedBalls.Count >= minLineLength) { shapeIsFound = true; } else { explodedBalls.Clear(); };
        }
        
        private void explodeSquare()
        {
            String[] pozicePole = { "row1column1", "row1column2", "row2column1", "row2column2" };
            foreach (String pozice in pozicePole)
            { if (shapeIsFound) { break; } else { checkSquare(pozice); } }
        }
        private void checkSquare(String position)
        {
            currentCell = cellToBeChecked;
            Cell cell1 = null;
            Cell cell2 = null;
            Cell cell3 = null;
            switch (position)
            {
                case "row1column1":
                    {
                        cell1 = currentCell.getRightCell();
                        cell2 = currentCell.getBottomRightCell();
                        cell3 = currentCell.getBottomCell();
                    }; break;
                case "row1column2":
                    {
                        cell1 = currentCell.getLeftCell();
                        cell2 = currentCell.getBottomLeftCell();
                        cell3 = currentCell.getBottomCell();
                    }; break;
                case "row2column1":
                    {
                        cell1 = currentCell.getTopCell();
                        cell2 = currentCell.getTopRightCell();
                        cell3 = currentCell.getRightCell();
                    }; break;
                case "row2column2":
                    {
                        cell1 = currentCell.getTopLeftCell();
                        cell2 = currentCell.getTopCell();
                        cell3 = currentCell.getLeftCell();
                    }; break;

            }
            if ((cell1 != null) && (cell2 != null) && (cell3 != null))
            {

                if (
                    ((!cell1.isEmpty()) && (cell1.getBallAndDoNotRemoveIt().hasColour(colour))) &&
                    ((!cell2.isEmpty()) && (cell2.getBallAndDoNotRemoveIt().hasColour(colour))) &&
                    ((!cell3.isEmpty()) && (cell3.getBallAndDoNotRemoveIt().hasColour(colour)))
                    )
                {
                    shapeIsFound = true;
                    explodedBalls.Push(currentCell);
                    explodedBalls.Push(cell1);
                    explodedBalls.Push(cell2);
                    explodedBalls.Push(cell3);
                }
                else { explodedBalls.Clear(); };

                ;
            }
        }
        private void explodeCircle()
        {
            String[] cellPosition = { "row1column2", "row2column1", "row2column3", "row3column2" };
            foreach (String position in cellPosition)
            { if (shapeIsFound) { break; } else { checkCircle(position); } }
        }
        private void checkCircle(String position)
        {
            currentCell = cellToBeChecked;
            Cell cell1 = null;
            Cell cell2 = null;
            Cell cell3 = null;
            Cell cellNextToCell3 = null;

            switch (position)
            {
                case "row1column2":
                    {
                        cell1 = currentCell.getBottomLeftCell();
                        cell2 = currentCell.getBottomRightCell();
                        cellNextToCell3 = currentCell.getBottomCell();
                        if (cellNextToCell3!=null) cell3 = cellNextToCell3.getBottomCell();
                    }; break;
                case "row2column1":
                    {
                        cell1 = currentCell.getTopRightCell();
                        cell2 = currentCell.getBottomRightCell();
                        cellNextToCell3 = currentCell.getRightCell();
                        if (cellNextToCell3 != null) cell3 = cellNextToCell3.getRightCell();
                    }; break;
                case "row2column3":
                    {
                        cell1 = currentCell.getTopLeftCell();
                        cell2 = currentCell.getBottomLeftCell();
                        cellNextToCell3 = currentCell.getLeftCell();
                        if (cellNextToCell3 != null) cell3 = cellNextToCell3.getLeftCell();
                    }; break;
                case "row3column2":
                    {
                        cell1 = currentCell.getTopLeftCell();
                        cell2 = currentCell.getTopRightCell();
                        cellNextToCell3 = currentCell.getTopCell();
                        if (cellNextToCell3 != null) cell3 = cellNextToCell3.getTopCell();
                    }; break;
            }
            if ((cell1 != null) && (cell2 != null) && (cellNextToCell3 != null) && (cell3 != null))
            {

                if (
                    ((!cell1.isEmpty()) && (cell1.getBallAndDoNotRemoveIt().hasColour(colour))) &&
                    ((!cell2.isEmpty()) && (cell2.getBallAndDoNotRemoveIt().hasColour(colour))) &&
                    ((!cell3.isEmpty()) && (cell3.getBallAndDoNotRemoveIt().hasColour(colour)))
                    )
                {
                    shapeIsFound = true;
                    explodedBalls.Push(currentCell);
                    explodedBalls.Push(cell1);
                    explodedBalls.Push(cell2);
                    explodedBalls.Push(cell3);
                }
                else { explodedBalls.Clear(); };

                ;
            }
        }
    }
}