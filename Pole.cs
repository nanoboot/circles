using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    public class Pole
    {
        
        private int Radek;
        private int Sloupec;
        private Mic mic =null;

        public Pole PoleNahore;
        public Pole PoleVpravo;
        public Pole PoleDole;
        public Pole PoleVlevo;
        public void VlozMic(Mic NovyMic)
        { if (this.JePrazdne()==true)this.mic = NovyMic; }
        public Mic VratMicANeodstranujHo()
        { if (this.JePrazdne() != true) return this.mic;
            else return new Mic("aa");// Zde udělat výjimku.
        }
        
        public Mic OdstranMicZPoleAVratHo()
        { if (this.JePrazdne() == false) { Mic docasnyMic = this.mic;
                this.mic= null;return docasnyMic; }
            else {return new Mic("aa"); };// Pokud pole je prázdné, hoď novou výjimku.
        }
        public bool JePrazdne()
        { if(this.mic==null) { return true; } else { return false; } }
        public void NastavPoleNahore(Pole PridavanePole)
        {
            this.PoleNahore = PridavanePole;
        }
        public void NastavPoleVpravo(Pole PridavanePole)
        {
            this.PoleVpravo = PridavanePole;
        }
        public void NastavPoleDole(Pole PridavanePole)
        {
            this.PoleDole = PridavanePole;
        }
        public void NastavPoleVlevo(Pole PridavanePole)
        {
            this.PoleVlevo = PridavanePole;
        }
        public Pole VratPoleNahore()
        {
            return this.PoleNahore;
        }
        public Pole VratPoleVpravo()
        {
            return this.PoleVpravo;
        }
        public Pole VratPoleDole()
        {
            return this.PoleDole;
        }
        public Pole VratPoleVlevo()
        {
            return this.PoleVlevo;
        }
        public Pole VratPoleSikmoVlevoNahore()
        {
            if ((this.VratRadek() == 1) || (this.VratSloupec() == 1)) { return null; } else //Pole vlevo nahoře šikmo od tohoto pole neexistuje.
            { return this.VratPoleVlevo().VratPoleNahore(); };
        }
        public Pole VratPoleSikmoVpravoNahore()
        {
            if ((this.VratRadek() == 1) || (this.VratPoleVpravo() == null)) { return null; }
            else 
            { return this.VratPoleVpravo().VratPoleNahore(); };
        }
        public Pole VratPoleSikmoVlevoDole()
        {
            if ((this.VratPoleDole() == null) || (this.VratSloupec() == 1)) { return null; }
            else
            { return this.VratPoleVlevo().VratPoleDole(); };
        }
        public Pole VratPoleSikmoVpravoDole()
        {
            if ((this.VratPoleVpravo() == null) || (this.VratPoleDole() == null)) { return null; }
            else
            { return this.VratPoleVpravo().VratPoleDole(); };
        }
        public int VratSloupec()
        { return this.Sloupec; }
        public int VratRadek()
        { return this.Radek; }
        public Pole(int Radek, int Sloupec)
        {
            this.Radek= Radek;
            this.Sloupec= Sloupec;
            this.PoleNahore = null;
            this.PoleVpravo = null;
            this.PoleDole = null;
            this.PoleVlevo = null;
        }
        public Pole()
        {
            this.PoleNahore = null;
            this.PoleVpravo = null;
            this.PoleDole = null;
            this.PoleVlevo = null;
        }

    }
}