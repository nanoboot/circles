using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
{
    class NodeForPathFinder
    {
        NodeForPathFinder RodicUzelHledaceCesty;
        private Cell PoleUzlu;
       public Cell VratPoleUzlu()
        { return this.PoleUzlu; }
        public NodeForPathFinder(NodeForPathFinder RodicUzelHledaceCesty, Cell PoleUzlu)
        {
            this.RodicUzelHledaceCesty = RodicUzelHledaceCesty;
            this.PoleUzlu = PoleUzlu; }
        public NodeForPathFinder VytvorDite(Cell PoleUzlu)
        {
            return new NodeForPathFinder(this, PoleUzlu);
        }
        public NodeForPathFinder VratRodice()
        {
            return this.RodicUzelHledaceCesty;
        }
    }
}
