using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    class UzelHledaceCesty
    {
        UzelHledaceCesty RodicUzelHledaceCesty;
        private Pole PoleUzlu;
       public Pole VratPoleUzlu()
        { return this.PoleUzlu; }
        public UzelHledaceCesty(UzelHledaceCesty RodicUzelHledaceCesty, Pole PoleUzlu)
        {
            this.RodicUzelHledaceCesty = RodicUzelHledaceCesty;
            this.PoleUzlu = PoleUzlu; }
        public UzelHledaceCesty VytvorDite(Pole PoleUzlu)
        {
            return new UzelHledaceCesty(this, PoleUzlu);
        }
        public UzelHledaceCesty VratRodice()
        {
            return this.RodicUzelHledaceCesty;
        }
    }
}
