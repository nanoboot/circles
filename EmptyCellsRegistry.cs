using System;


namespace Balls
{
    class EmptyCellsRegistry
    {
        private NodeForEmptyCellRegistry firstNode = null;
        private NodeForEmptyCellRegistry tmpNode = null;
        private NodeForEmptyCellRegistry previousTmpNode = null;
        private int nodeCount = 0;
        private void nodeCountPlusOne()
        { nodeCount = ++nodeCount;
            
        }
        private void nodeCountMinusOne()
        { nodeCount = --nodeCount; }
        public int getCountOfNodes()
        {
            return this.nodeCount; }

        public void addCell(Cell newCell)
        {

            if (firstNode == null) { firstNode = new NodeForEmptyCellRegistry(newCell); }
            if (firstNode != null) { tmpNode = firstNode;
                this.firstNode = new NodeForEmptyCellRegistry(newCell);
                this.firstNode.addNewNode(tmpNode);
            }
            nodeCountPlusOne();
        }
        public Cell getCell(int order)
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


            }
            else
            { throw new ArgumentOutOfRangeException(); }
        }
        public void removeCell(Cell newFullCell)
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
