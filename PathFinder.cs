using System;
using System.Collections.Generic;

namespace Circles
{
    public class PathFinder
    {
        Cell activeCellFrom;
        Cell activeCellTo;
        private Queue<NodeForPathFinder> queueBeforeTransformation = new Queue<NodeForPathFinder>();// A queue is created into which nodes are inserted before transformation. Further only as an upper front.
        private Queue<NodeForPathFinder> queueAfterTransformation = new Queue<NodeForPathFinder>();// A queue is created into which nodes are inserted after the transformation. Further only as a lower front.
        List<Cell> visitedCells = new List<Cell>();
        private Stack<Cell> cellsFromTo = new Stack<Cell>();
        private NodeForPathFinder currentNodeOfPathFinder;
        private NodeForPathFinder ifPathExistsItsLastNodeIsHereNodeOfPathFincer=null;
        Game game = null;
        private Stack<Cell> cellsWhichShouldNotBeActiveAnymore = new Stack<Cell>();// Fields are temporarily stored here, which will have their background set to normal in the next step.
        Cell checkedCell= null;

        bool wasPathAlreadyFound = false;

        public PathFinder(Cell activeCellFromArg, Cell activeCellToArg, Game game, Stack<Cell> cellsWhichShouldNotBeActiveAnymoreArg)
        {
            this.game = game;
            this.cellsWhichShouldNotBeActiveAnymore = cellsWhichShouldNotBeActiveAnymoreArg;
            this.activeCellFrom = activeCellFromArg;
            this.activeCellTo = activeCellToArg;
            cellsFromTo.Push(activeCellFromArg);
            cellsFromTo.Push(activeCellToArg);
        }
        
        public Stack<Cell> getCellsFromTo()
        {return this.cellsFromTo; }
        public bool search()
        {
            wasPathAlreadyFound = false;
            currentNodeOfPathFinder = new NodeForPathFinder(null, activeCellFrom);// I've created the very first pathfinder node that has a reference to the field we want to move the ball from.
            queueBeforeTransformation.Enqueue(currentNodeOfPathFinder);// I put the first node in the Queue Before Transformation queue
            while (!((queueBeforeTransformation.Count == 0) && (queueAfterTransformation.Count == 0)))// As long as the queues are not empty, there is something to transform.
            {
                // Takes all the nodes from the queueBeforeTransformation queue in turn
                if (queueBeforeTransformation.Count == 0)// If the top queue is empty.
                    moveNodesFromBottomToUpperQueue();// Everything from the lower queue is moved to the upper queue.
                wasPathAlreadyFound = takeOneNodeFromUpperQueueAndCheckIt();
                if (wasPathAlreadyFound) { return true; };
                ;
            };
            
            return false; //The fact that the queue has reached this far means we didn't find the path, so it will return false.
        }
        private void moveNodesFromBottomToUpperQueue()
        {
            while (queueAfterTransformation.Count != 0)// As long as the bottom queue is not empty,
                queueBeforeTransformation.Enqueue(queueAfterTransformation.Dequeue());// move nodes from the lower queue to the upper queue.
        }
        private bool takeOneNodeFromUpperQueueAndCheckIt()
        {
            currentNodeOfPathFinder = queueBeforeTransformation.Dequeue();// Takes one node from the top queue
                                                                          // I took one node from the top queue and now I will explore all possible paths from this node.
            bool logicValue = false;
            String[] cellDirection = { "nahore", "vpravo", "dole", "vlevo" };
            foreach (String direction in cellDirection)
            {
                logicValue = checkIfNodeFromUpperQueueIsThatOneWhereWeWantTheBallMoveTo(direction); if (logicValue) { return true; }
                else
                {
                    ifTheFieldInTheGivenDirectionFromTheCurrentFieldMeetsTheConditionsMoveItToTheLowerQueueAndToTheGenericListOfVisitedFields(direction);
                }
            }

            return false;
        }
        private bool checkIfNodeFromUpperQueueIsThatOneWhereWeWantTheBallMoveTo(String Smer)
        {
            switch (Smer)
            {
                case "nahore":
                    checkedCell = currentNodeOfPathFinder.getCell().getTopCell(); break;
                case "vpravo":
                    checkedCell = currentNodeOfPathFinder.getCell().getRightCell(); break;
                case "dole":
                    checkedCell = currentNodeOfPathFinder.getCell().getBottomCell(); break;
                case "vlevo":
                    checkedCell = currentNodeOfPathFinder.getCell().getLeftCell(); break;

            }
            if (checkedCell == activeCellTo)
            {
                this.ifPathExistsItsLastNodeIsHereNodeOfPathFincer = currentNodeOfPathFinder.createChild(checkedCell);

                {
                    while (currentNodeOfPathFinder.getParent() != null)
                    {
                        cellsFromTo.Push(currentNodeOfPathFinder.getCell());
                        currentNodeOfPathFinder = currentNodeOfPathFinder.getParent();
                    }
                }

                return true;
            };// If the field above the field of the current node is the field we want to move the ball to, we found the path and this method returns true and ends, otherwise it continues.
            return false;
        }
        private bool ifTheFieldInTheGivenDirectionFromTheCurrentFieldMeetsTheConditionsMoveItToTheLowerQueueAndToTheGenericListOfVisitedFields(String direction)
        {
            if ((checkedCell != null) && (checkedCell.isEmpty()) && (!(visitedCells.Contains(checkedCell))))// If the field at the top is empty, has not already been visited, and exists, then a new node is created with this field in the bottom queue and this field is added to the list of already visited fields.
            {
                queueAfterTransformation.Enqueue(currentNodeOfPathFinder.createChild(checkedCell));
                visitedCells.Add(checkedCell);

                //cellsWhichShouldNotBeActiveAnymore.Push(checkedCell);
                //game.insertCommand((String.Concat("POLE ", ZkoumanePole.getRow(), " ", ZkoumanePole.getColumn(), " POZADI CERVENE")));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    }