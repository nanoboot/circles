using System;

namespace Míče
{
    public static class GeneratorNahodnychCisel
    {
        private static Random GeneratorCisel = new Random();//Slouží ke generování náhodných odkazů na pole
        public static int VratNahodneCislo(int DolniMezKteraJeSoucasti,int HorniMezKteraJizNeniSoucasti)
        { return GeneratorCisel.Next(DolniMezKteraJeSoucasti, HorniMezKteraJizNeniSoucasti); }
    }
}
