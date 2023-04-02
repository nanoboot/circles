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
        public void NastavitDalsiUzel(NodeForEmptyCellRegistry DalsiUzel)
        { this.DalsiUzel = DalsiUzel; }
        public NodeForEmptyCellRegistry VratDalsiUzel()
        {return this.DalsiUzel; }
        public Cell VratPole()
        { return this.PoleUzlu; }
    }
}