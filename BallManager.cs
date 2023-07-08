using System;
using System.Collections.Generic;
using System.Linq;

namespace Circles
{
    class BallManager
    {
        List<String> ballTypes = new List<String>();// All used ball types are registered here, only balls whose types are in this register will be generated.
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
                case "LightGreen": { I = "1"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); } break;
                case "Red": { I = "2"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                case "DarkBlue": { I = "3"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                case "Yellow": { I = "4"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                case "LightBlue": { I = "5"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                case "Purple": { I = "6"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                case "Brown": { I = "7"; game.insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;

            }

            switch (nextBall2.getColour())
            {
                case "LightGreen": { I = "1"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); } break;
                case "Red": { I = "2"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                case "DarkBlue": { I = "3"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                case "Yellow": { I = "4"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                case "LightBlue": { I = "5"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                case "Purple": { I = "6"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                case "Brown": { I = "7"; game.insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;

            }

            switch (nextBall3.getColour())
            {
                case "LightGreen": { I = "1"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); } break;
                case "Red": { I = "2"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                case "DarkBlue": { I = "3"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                case "Yellow": { I = "4"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                case "LightBlue": { I = "5"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                case "Purple": { I = "6"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                case "Brown": { I = "7"; game.insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;

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
       "LightGreen",
       "Red",
       "DarkBlue",
       "Yellow",
       "LightBlue",
       "Purple",
       "Brown",
       "Pink",
       "Green",
       "Gold",
       "Orange",
       "White",
       "Grey",
       "Black",
       "Blue",
       "ArmyGreen"};

            for (int i = 0; i <= 15; i++)
            {
                addNextColourUsingBooleanValues(coloursAsBooleanValues[i], coloursAsTextValues[i]);
            }
        }
        private void addNextColourUsingBooleanValues(bool colour, String textOfColour)
        { if (colour) { colours[indexForColours] = textOfColour; indexForColours++; } }
        private bool fillBallTypeList(bool jokerBalls, bool doubleScoreBalls)
        {
            if (jokerBalls) { addXTimeThisTextToUsedBallsList(5, "Rainbow"); }
            for (int i = 0; i <= indexForColours - 1; i++)

            {
                addXTimeThisTextToUsedBallsList(10, colours[i]);
                if (doubleScoreBalls) { addXTimeThisTextToUsedBallsList(1, String.Concat("DoubleScore", colours[i])); }
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
            if (usedBallTypes.Contains("Rainbow"))
            {
                newBall = new Ball(true);
            } else
            {
                if (usedBallTypes.Contains("DoubleScore"))
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