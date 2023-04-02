using System;
using System.Collections.Generic;
using System.Linq;

namespace Circles
{
    class BallManager
    {
        List<String> ballTypes = new List<String>();
        private String[] colours = new String [16];
        private int indexForColours = 0;
        public Queue<Ball> nextBalls = new Queue<Ball>();
        public Ball nextBall1 = null;
        public Ball nextBall2 = null;
        public Ball nextBall3 = null;
        public BallManager(
       bool lightGreen,
       bool red,
       bool darkBlue,
       bool yellow,
       bool lightBlue,
       bool purple,
       bool brown,
       bool pink,
       bool green,
       bool gold,
       bool orange,
       bool white,
       bool grey,
       bool black,
       bool blue,
       bool armyGreen,
       bool jokerBalls,
       bool doubleScoreBalls,
       Game game)
        {
            fillColours(
       lightGreen,
       red,
       darkBlue,
       yellow,
       lightBlue,
       purple,
       brown,
       pink,
       green,
       gold,
       orange,
       white,
       grey,
       black,
       blue,
       armyGreen); 
            fillBallTypeList(jokerBalls,doubleScoreBalls);
            nextBall1 = generateNewBall();
            nextBall2 = generateNewBall();
            nextBall3 = generateNewBall();
            nextBalls.Enqueue(nextBall1);
            nextBalls.Enqueue(nextBall2);
            nextBalls.Enqueue(nextBall3);

            
            String I = "";

            switch (nextBall1.getColour())
            {
                case "SvetleZelena": { I = "1"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); } break;
                case "Cervena": { I = "2"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "TmaveModra": { I = "3"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Zluta": { I = "4"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "SvetleModra": { I = "5"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Fialova": { I = "6"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                case "Hneda": { I = "7"; game.insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;

            }

            switch (nextBall2.getColour())
            {
                case "SvetleZelena": { I = "1"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); } break;
                case "Cervena": { I = "2"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "TmaveModra": { I = "3"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Zluta": { I = "4"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "SvetleModra": { I = "5"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Fialova": { I = "6"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                case "Hneda": { I = "7"; game.insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;

            }

            switch (nextBall3.getColour())
            {
                case "SvetleZelena": { I = "1"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); } break;
                case "Cervena": { I = "2"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "TmaveModra": { I = "3"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Zluta": { I = "4"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "SvetleModra": { I = "5"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Fialova": { I = "6"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                case "Hneda": { I = "7"; game.insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;

            }
        }
        private void fillColours(
       bool lightGreen,
       bool red,
       bool darkBlue,
       bool yellow,
       bool lightBlue,
       bool purple,
       bool brown,
       bool pink,
       bool green,
       bool gold,
       bool orange,
       bool white,
       bool grey,
       bool black,
       bool blue,
       bool armyGreen)
        {
            Boolean[] coloursAsBooleanValues = {
       lightGreen,
       red,
       darkBlue,
       yellow,
       lightBlue,
       purple,
       brown,
       pink,
       green,
       gold,
       orange,
       white,
       grey,
       black,
       blue,
       armyGreen};
            String[] coloursAsTextValues = {
       "SvetleZelena",
       "Cervena",
       "TmaveModra",
       "Zluta",
       "SvetleModra",
       "Fialova",
       "Hneda",
       "Ruzova",
       "Zelena",
       "Zlata",
       "Oranzova",
       "Bila",
       "Sediva",
       "Cerna",
       "Modra",
       "VojenskaZelena"};

            for (int i = 0; i <= 15; i++)
            {
                addNextColourUsingBooleanValues(coloursAsBooleanValues[i], coloursAsTextValues[i]);
            }
        }
        private void addNextColourUsingBooleanValues(bool colour, String textOfColour)
        { if (colour) { colours[indexForColours] = textOfColour; indexForColours++; } }
        private bool fillBallTypeList(bool jokerBalls, bool doubleScoreBalls)
        {
            if (jokerBalls) { addXTimeThisTextToUsedBallsList(5, "Duhove"); }
            for (int i = 0; i <= indexForColours - 1; i++)

            {
                addXTimeThisTextToUsedBallsList(10, colours[i]);
                if (doubleScoreBalls) { addXTimeThisTextToUsedBallsList(1, String.Concat("Zdvojnasobujici", colours[i])); }
            }
            return true;

        }
        private void addXTimeThisTextToUsedBallsList(int xTimes, String givenText)
        {
            for (int i = 1; i <= xTimes; i++)
            {
                ballTypes.Add(givenText);
            };
        }

        public Ball generateNewBall()
        {
            String usedBallTypes = ballTypes[RandomNumberGenerator.getRandomNumber(0, ballTypes.Count())];
            Ball newBall= setBallBasedOnTheType(usedBallTypes);

             return newBall;
        }
        private Ball setBallBasedOnTheType(String usedBallTypes)
        {
            Ball newBall;
            if (usedBallTypes.Contains("Duhove"))
            {
                newBall = new Ball(true);
            } else
            {
                if (usedBallTypes.Contains("Zdvojnasobujici"))
                {
                    String usedBallTypesWithoutDoubleScore = usedBallTypes.Substring(15);
                    newBall = new Ball(usedBallTypesWithoutDoubleScore, true);
                }
                else
                {
                    newBall = new Ball(usedBallTypes);
                }
            }
            
            newBall.setType(usedBallTypes);
            return newBall;
        }

    }
}