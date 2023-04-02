using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    public class Cell
    {
        
        private int Radek;
        private int Sloupec;
        private Ball mic =null;

        public Cell PoleNahore;
        public Cell PoleVpravo;
        public Cell PoleDole;
        public Cell PoleVlevo;
        public void VlozMic(Ball NovyMic)
        { if (this.JePrazdne()==true)this.mic = NovyMic; }
        public Ball VratMicANeodstranujHo()
        { if (this.JePrazdne() != true) return this.mic;
            else return new Ball("aa");// Zde udělat výjimku.
        }
        
        public Ball OdstranMicZPoleAVratHo()
        { if (this.JePrazdne() == false) { Ball docasnyMic = this.mic;
                this.mic= null;return docasnyMic; }
            else {return new Ball("aa"); };// Pokud pole je prázdné, hoď novou výjimku.
        }
        public bool JePrazdne()
        { if(this.mic==null) { return true; } else { return false; } }
        public void NastavPoleNahore(Cell PridavanePole)
        {
            this.PoleNahore = PridavanePole;
        }
        public void NastavPoleVpravo(Cell PridavanePole)
        {
            this.PoleVpravo = PridavanePole;
        }
        public void NastavPoleDole(Cell PridavanePole)
        {
            this.PoleDole = PridavanePole;
        }
        public void NastavPoleVlevo(Cell PridavanePole)
        {
            this.PoleVlevo = PridavanePole;
        }
        public Cell VratPoleNahore()
        {
            return this.PoleNahore;
        }
        public Cell VratPoleVpravo()
        {
            return this.PoleVpravo;
        }
        public Cell VratPoleDole()
        {
            return this.PoleDole;
        }
        public Cell VratPoleVlevo()
        {
            return this.PoleVlevo;
        }
        public Cell VratPoleSikmoVlevoNahore()
        {
            if ((this.VratRadek() == 1) || (this.VratSloupec() == 1)) { return null; } else //Pole vlevo nahoře šikmo od tohoto pole neexistuje.
            { return this.VratPoleVlevo().VratPoleNahore(); };
        }
        public Cell VratPoleSikmoVpravoNahore()
        {
            if ((this.VratRadek() == 1) || (this.VratPoleVpravo() == null)) { return null; }
            else 
            { return this.VratPoleVpravo().VratPoleNahore(); };
        }
        public Cell VratPoleSikmoVlevoDole()
        {
            if ((this.VratPoleDole() == null) || (this.VratSloupec() == 1)) { return null; }
            else
            { return this.VratPoleVlevo().VratPoleDole(); };
        }
        public Cell VratPoleSikmoVpravoDole()
        {
            if ((this.VratPoleVpravo() == null) || (this.VratPoleDole() == null)) { return null; }
            else
            { return this.VratPoleVpravo().VratPoleDole(); };
        }
        public int VratSloupec()
        { return this.Sloupec; }
        public int VratRadek()
        { return this.Radek; }
        public Cell(int Radek, int Sloupec)
        {
            this.Radek= Radek;
            this.Sloupec= Sloupec;
            this.PoleNahore = null;
            this.PoleVpravo = null;
            this.PoleDole = null;
            this.PoleVlevo = null;
        }
        public Cell()
        {
            this.PoleNahore = null;
            this.PoleVpravo = null;
            this.PoleDole = null;
            this.PoleVlevo = null;
        }

    }
}