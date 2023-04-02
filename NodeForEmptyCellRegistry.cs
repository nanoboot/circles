
namespace Circles
{
    class NodeForEmptyCellRegistry
    {
        private NodeForEmptyCellRegistry nextNode = null;
        private Cell cell = null;
        public NodeForEmptyCellRegistry(Cell cell)
        { this.cell = cell; }
        public void addNewNode(NodeForEmptyCellRegistry nextNode)
        { this.nextNode = nextNode; }
        public NodeForEmptyCellRegistry getNextNode()
        {return this.nextNode; }
        public Cell getCell()
        { return this.cell; }
    }
}