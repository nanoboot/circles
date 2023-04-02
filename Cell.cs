using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
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
        { if (this.isEmpty()==true)this.mic = NovyMic; }
        public Ball getBallAndDoNotRemoteIt()
        { if (this.isEmpty() != true) return this.mic;
            else return new Ball("aa");// Zde udělat výjimku.
        }
        
        public Ball OdstranMicZPoleAVratHo()
        { if (this.isEmpty() == false) { Ball docasnyMic = this.mic;
                this.mic= null;return docasnyMic; }
            else {return new Ball("aa"); };// Pokud pole je prázdné, hoď novou výjimku.
        }
        public bool isEmpty()
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
        public Cell getTopCell()
        {
            return this.PoleNahore;
        }
        public Cell getRightCell()
        {
            return this.PoleVpravo;
        }
        public Cell getBottomCell()
        {
            return this.PoleDole;
        }
        public Cell getLeftCell()
        {
            return this.PoleVlevo;
        }
        public Cell getTopLeftCell()
        {
            if ((this.VratRadek() == 1) || (this.VratSloupec() == 1)) { return null; } else //Pole vlevo nahoře šikmo od tohoto pole neexistuje.
            { return this.getLeftCell().getTopCell(); };
        }
        public Cell getTopRightCell()
        {
            if ((this.VratRadek() == 1) || (this.getRightCell() == null)) { return null; }
            else 
            { return this.getRightCell().getTopCell(); };
        }
        public Cell getBottomLeftCell()
        {
            if ((this.getBottomCell() == null) || (this.VratSloupec() == 1)) { return null; }
            else
            { return this.getLeftCell().getBottomCell(); };
        }
        public Cell getBottomRightCell()
        {
            if ((this.getRightCell() == null) || (this.getBottomCell() == null)) { return null; }
            else
            { return this.getRightCell().getBottomCell(); };
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