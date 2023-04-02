using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    class UzelRegistruPrazdnychPoli
    {
        private UzelRegistruPrazdnychPoli DalsiUzel = null;
        private Pole PoleUzlu = null;
        public UzelRegistruPrazdnychPoli(Pole PoleUzlu)
        { this.PoleUzlu = PoleUzlu; }
        public void NastavitDalsiUzel(UzelRegistruPrazdnychPoli DalsiUzel)
        { this.DalsiUzel = DalsiUzel; }
        public UzelRegistruPrazdnychPoli VratDalsiUzel()
        {return this.DalsiUzel; }
        public Pole VratPole()
        { return this.PoleUzlu; }
    }
}