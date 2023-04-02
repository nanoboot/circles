using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    class SberacSouradnic
    {
        int Sloupec=0;
        int Radek;
        public void VlozSouradniciSloupce(int Sloupec)
        { this.Sloupec = Sloupec; }
        public void VlozSouradniciRadku(int Radek)
        { this.Radek = Radek; }
        public int VratSouradniciSloupce()
        { return this.Sloupec; }
        public int VratSouradniciRadku()
        { return this.Radek; }
    }
}
