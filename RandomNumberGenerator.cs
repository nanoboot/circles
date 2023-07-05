using System;

namespace Circles
{
    public static class RandomNumberGenerator
    {
        private static Random randomNumberGenerator = new Random();// Used to generate random field references
        public static int getRandomNumber(int startIncluded,int endNotIncluded)
        { return randomNumberGenerator.Next(startIncluded, endNotIncluded); }
    }
}
