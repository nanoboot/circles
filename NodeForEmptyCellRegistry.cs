using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
{
    class NodeForEmptyCellRegistry
    {
        private NodeForEmptyCellRegistry DalsiUzel = null;
        private Cell PoleUzlu = null;
        public NodeForEmptyCellRegistry(Cell PoleUzlu)
        { this.PoleUzlu = PoleUzlu; }
        public void addNewNode(NodeForEmptyCellRegistry DalsiUzel)
        { this.DalsiUzel = DalsiUzel; }
        public NodeForEmptyCellRegistry getNextNode()
        {return this.DalsiUzel; }
        public Cell getCell()
        { return this.PoleUzlu; }
    }
}