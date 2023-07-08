using System;


namespace Circles
{
    class EmptyCellsRegistry
    {
        private NodeForEmptyCellRegistry firstNode = null;
        private NodeForEmptyCellRegistry tmpNode = null;
        private NodeForEmptyCellRegistry previousTmpNode = null;
        private int nodeCount = 0;
        private void nodeCountPlusOne()// The number of register fields is incremented by one.
        { nodeCount = ++nodeCount;
            //Record.Record("The number of nodes has been increased to ", this.ReturnNodeNumber().ToString());
        }
        private void nodeCountMinusOne()// The number of register fields is reduced by one.
        { nodeCount = --nodeCount; }
        public int getCountOfNodes()
        {
            return this.nodeCount; }

        public void addCell(Cell newCell)//Puts the field into the register.
        {

            if (firstNode == null) { firstNode = new NodeForEmptyCellRegistry(newCell); }
            if (firstNode != null) { tmpNode = firstNode;
                this.firstNode = new NodeForEmptyCellRegistry(newCell);
                this.firstNode.addNewNode(tmpNode);
            }
            nodeCountPlusOne();
        }
        public Cell getCell(int order)//Returns the given empty field from the registry and at the same time deletes this field from the registry.
        {
            if (order <= getCountOfNodes())
            {
                if (order == 1)
                {
                    tmpNode = this.firstNode;
                    firstNode = firstNode.getNextNode();
                    nodeCountMinusOne();
                    return tmpNode.getCell();
                }
                if (order > 1)
                {
                    tmpNode = this.firstNode;

                    for (int i = 1; i < order; i++)
                    {
                        previousTmpNode = tmpNode;
                        tmpNode = tmpNode.getNextNode();
                    }
                    previousTmpNode.addNewNode(tmpNode.getNextNode());
                    nodeCountMinusOne();
                    return tmpNode.getCell();
                }
                else return new Cell();//throw new ArgumentOutOfRangeException()
                // Fix here somehow.


            }
            else
            { throw new ArgumentOutOfRangeException(); }
        }
        public void removeCell(Cell newFullCell)//Returns the given empty field from the registry and at the same time deletes this field from the registry.
        {
           tmpNode = firstNode;
            while((tmpNode.getNextNode()!=null)&&(tmpNode.getCell()!= newFullCell))
                { previousTmpNode = tmpNode;
                tmpNode = tmpNode.getNextNode();
            }
            if (tmpNode.getCell()==newFullCell)
            { previousTmpNode.addNewNode(tmpNode.getNextNode());
                nodeCountMinusOne();
            }
        }
    }
}
