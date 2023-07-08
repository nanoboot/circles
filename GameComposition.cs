using System;

namespace Circles
{
    public class GameComposition
    {
        private int width;
        private int height;
        private bool lightGreen;
        private bool red;
        private bool darkBlue;
        private bool yellow;
        private bool lightBlue;
        private bool purple;
        private bool brown;
        private bool pink;
        private bool green;
        private bool gold;
        private bool orange;
        private bool white;
        private bool grey;
        private bool black;
        private bool blue;
        private bool armyGreen;
        private int startBallCount;
        private int nextBallCount;
        private bool jokerBalls;
        private bool doubleScoreBalls;
        private string shape;
        private int minLineLength;
        private bool changed;
        public GameComposition()
        {
            setDefaultValues();
            this.changed = false;
        }
        public void setChanged(bool changed)
        { this.changed = changed; }
        public bool isChanged()
        { return this.changed; }
        public void setWidth(int width)
        { this.width = width; }
        public void setHeight(int height)
        { this.height = height; }
        public void setLightGreen(bool lightGreen)
        { this.lightGreen=lightGreen;}
        public void setRed(bool red)
        { this.red = red; }
        public void setDarkBlue(bool darkBlue)
        { this.darkBlue = darkBlue; }
        public void setYellow(bool yellow)
        { this.yellow = yellow; }
        public void setLightBlue(bool lightBlue)
        { this.lightBlue = lightBlue; }
        public void setPurple(bool purple)
        { this.purple = purple; }
        public void setBrown(bool brown)
        { this.brown = brown; }
        public void setPink(bool pink)
        { this.pink = pink; }
        public void setGreen(bool green)
        { this.green = green; }
        public void setGold(bool gold)
        { this.gold = gold; }
        public void setOrange(bool orange)
        { this.orange = orange; }
        public void setWhite(bool white)
        { this.white = white; }
        public void setGrey(bool grey)
        { this.grey = grey; }
        public void setBlack(bool black)
        { this.black = black; }
        public void setBlue(bool blue)
        { this.blue = blue; }
        public void setArmyGreen(bool armyGreen)
        { this.armyGreen = armyGreen; }
        public void setNextBallCount(int nextBallCount)
        { this.nextBallCount = nextBallCount; }
        public void setStartBallCount(int startBallCount)
        { this.startBallCount = startBallCount; }
        public void setJokerBalls(bool jokerBalls)
        { this.jokerBalls = jokerBalls; }
        public void setDoubleScoreBalls(bool doubleScoreBalls)
        { this.doubleScoreBalls = doubleScoreBalls; }
        public void setShape(String shape)
        { this.shape = shape; }
        public void setMinLineLength(int minLineLength)
        { this.minLineLength = minLineLength; }

        public int getWidth()
        { return this.width; }
        public int getHeight() { return this.height; }
        public bool getLightGreen() { return this.lightGreen; }
        public bool isRed() { return this.red; }
        public bool isDarkBlue() { return this.darkBlue; }
        public bool isYellow() { return this.yellow; }
        public bool isLightBlue() { return this.lightBlue; }
        public bool isPurple() { return this.purple; }
        public bool isBrown() { return this.brown; }
        public bool isPink() { return this.pink; }
        public bool isGreen() { return this.green; }
        public bool isGold() { return this.gold; }
        public bool isOrange() { return this.orange; }
        public bool isWhite() { return this.white; }
        public bool isGrey() { return this.grey; }
        public bool isBlack() { return this.black; }
        public bool isBlue() { return this.blue; }
        public bool isArmyGreen() { return this.armyGreen; }
        public int getStartBallCount() { return this.startBallCount; }
        public int getNextBallCount() { return this.nextBallCount; }
        public bool isJokerBalls() { return this.jokerBalls; }
        public bool isDoubleScoreBalls() { return this.doubleScoreBalls; }
        public string getShape() { return this.shape; }
        public int getMinLineLength() { return this.minLineLength; }
        public void setDefaultValues()
        {

            this.setHeight(9);
            this.setWidth(9);
            this.setLightGreen(true);
            this.setRed(true);
            this.setDarkBlue(true);
            this.setYellow(true);
            this.setLightBlue(true);
            this.setPurple(true);
            this.setBrown(true);
            this.setPink(false);
            this.setGreen(false);
            this.setGold(false);
            this.setOrange(false);
            this.setWhite(false);
            this.setGrey(false);
            this.setBlack(false);
            this.setBlue(false);
            this.setArmyGreen(false);
            this.setStartBallCount(5);
            this.setNextBallCount(3);
            this.setJokerBalls(false);
            this.setDoubleScoreBalls(false);
            this.setShape("line");
            this.setMinLineLength(5);
            
        }

    }
}
