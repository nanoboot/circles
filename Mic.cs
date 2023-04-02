using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    public class Mic
    {
        private bool Skaka;
        public String Barva { get;  }
        public String Typ;
        bool Duhovy;
        bool Zdvojnasobujici;
        public void Skakej()
        { this.Skaka = true; }
        public void Neskakej()
        { this.Skaka = false; }
        public bool JeZdvojnasobujici()
        { return this.Zdvojnasobujici; }
        
       
        public bool jeJehoBrava(String Barva)
        {
            if (!(this.VratTyp() == "Duhove"))
            { if (Barva == this.Barva) { return true; } else { return false; }; }
            else return true;
            }
    public Mic(bool Duhovy)
        {
            this.Barva = "";
            this.Zdvojnasobujici = false;
            this.Duhovy = Duhovy;
        }
        public Mic(String Barva)
        {
            this.Barva = Barva;
            this.Zdvojnasobujici = false;
            this.Duhovy = false;
        }
        public Mic(String Barva, bool Zdvojnasobujici)
        {
            this.Barva = Barva;
            this.Zdvojnasobujici = Zdvojnasobujici;
            this.Duhovy = false;
        }
        public void NastavTyp(String Typ)
        { this.Typ = Typ; }
        public String VratTyp()
        { return this.Typ; }
        public String VratBarvu()
        { return this.Barva; }
}

}
