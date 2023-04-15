using System;

namespace Circles
{
    public static class RandomNumberGenerator
    {
        private static Random GeneratorCisel = new Random();//Slouží ke generování náhodných odkazů na pole
        public static int getRandomNumber(int DolniMezKteraJeSoucasti,int HorniMezKteraJizNeniSoucasti)
        { return GeneratorCisel.Next(DolniMezKteraJeSoucasti, HorniMezKteraJizNeniSoucasti); }
    }
}
