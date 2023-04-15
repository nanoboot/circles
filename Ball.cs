using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles
{
    public class Ball
    {
        private bool isJumping;
        public String colour { get;  }
        public String type;
        bool joker;
        bool doubleScore;
        public void jump()
        { this.isJumping = true; }
        public void dontJump()
        { this.isJumping = false; }
        public bool isDoubleScore()
        { return this.doubleScore; }
        
       
        public bool hasColour(String colour)
        {
            if (!(this.getType() == "Duhove"))
            { if (colour == this.colour) { return true; } else { return false; }; }
            else return true;
            }
    public Ball(bool joker)
        {
            this.colour = "";
            this.doubleScore = false;
            this.joker = joker;
        }
        public Ball(String colour)
        {
            this.colour = colour;
            this.doubleScore = false;
            this.joker = false;
        }
        public Ball(String colour, bool doubleScore)
        {
            this.colour = colour;
            this.doubleScore = doubleScore;
            this.joker = false;
        }
        public void setType(String type)
        { this.type = type; }
        public String getType()
        { return this.type; }
        public String getColour()
        { return this.colour; }
}

}
