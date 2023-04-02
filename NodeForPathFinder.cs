
namespace Circles
{
    class NodeForPathFinder
    {
        NodeForPathFinder parentForNodeForPathFinder;
        private Cell cell;
       public Cell getCell()
        { return this.cell; }
        public NodeForPathFinder(NodeForPathFinder parentForNodeForPathFinder, Cell cell)
        {
            this.parentForNodeForPathFinder = parentForNodeForPathFinder;
            this.cell = cell; }
        public NodeForPathFinder createChild(Cell parentForNodeForPathFinder)
        {
            return new NodeForPathFinder(this, parentForNodeForPathFinder);
        }
        public NodeForPathFinder getParent()
        {
            return this.parentForNodeForPathFinder;
        }
    }
}
