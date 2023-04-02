using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


namespace Balls
{
    class CellManager
    {

        EmptyCellsRegistry registrPrazdnychPoli = new EmptyCellsRegistry();// Zde jsou registrovány všechna prázdná pole
        Cell AktivniPoleOdkud = null;
        Cell AktivniPoleKam = null;
        Cell Pole1A1 = null;//pole, ktere odkazuje na Pole na souřadnici 1 a 1 sloupce a řádku
        public CellManager(int Vyska, int Sirka)
        { SestavDesku(Vyska, Sirka); }

        public void NastavAktivniPoleOdkud(Cell AktivniPoleOdkud)
        {this.AktivniPoleOdkud=AktivniPoleOdkud;
            AktivniPoleOdkud.VratMicANeodstranujHo().Skakej();
        }
        public void VlozPrazdnePoleAbychONemVedel(Cell novePrazdnePole)// Vloží dané pole do registru prázdných polí.
        { registrPrazdnychPoli.VlozPole(novePrazdnePole); }
        public void VlozPlnePoleAbychONemVedel(Cell novePlnePole)//Odstraní dané pole z registru prázdných polí.
        { registrPrazdnychPoli.OdstranPole(novePlnePole); }
        public Cell VratAktivniPoleOdkud()
        { return this.AktivniPoleOdkud; }

        public void NastavAktivniPoleKam(Cell AktivniPoleKam)
        { this.AktivniPoleKam = AktivniPoleKam; }

        public Cell VratAktivniPoleKam()
        { return this.AktivniPoleKam; }

        public Cell VratPole(int Radek, int Sloupec)//vrátí pole, které se nachází na dané souřadnici řádku a sloupce
        {
            Cell aktualniPole = new Cell();
            aktualniPole=Pole1A1;

            while(aktualniPole.VratSloupec()!=Sloupec)
            { aktualniPole = aktualniPole.VratPoleVpravo(); }// Přesune se do pole, které je v hledaném sloupci.
            while (aktualniPole.VratRadek() != Radek)
            { aktualniPole = aktualniPole.VratPoleDole(); }// Přesune se do pole, které je v hledaném řádku.
            return aktualniPole;
        }
        public Cell VratNahodnePrazdnePole()//vrátí náhodně pole, které je však prázdné, aby se vybralo pole, kam bude umístěn míč.
        {
            int i = RandomNumberGenerator.VratNahodneCislo(1, registrPrazdnychPoli.VratPocetUzlu());
            return registrPrazdnychPoli.VratPole(i);

        }
        public bool ExistujePrazdnePole()
        { if (registrPrazdnychPoli.VratPocetUzlu()!=0) {return true; } else return false; }
        private void SestavDesku(int Vyska, int Sirka)// Sestaví dynamicky desku navzájem propojených polí.
        {
            Cell poleStare = null;// Zde je pole, které bylo nové naposledy, než se stalo novým polem jiné pole.
            Cell poleNove = null;// Zde je nově vytvářené pole. 
            Cell polePrvniAktualnihoRadku = null;// Zde se ukládá první pole aktuálního řádku aby, až se dokončí aktuální řádek, se mohlo pokračovat s tvorbou řádku následujícího.
            Cell poleNahore = null;// Od nového pole.
            Cell poleVlevo = null;// Od nového pole.
            for (int radek = 1; radek <= Vyska; radek++)// Proměnná radek obsahuje hodnotu řádku, jehož nějaké pole bude tvořeno.
            {
                for (int sloupec = 1; sloupec <= Sirka; sloupec++)// Proměnná sloupec obsahuje hodnotu sloupce, jehož nějaké pole bude tvořeno.
                {
                    poleStare = poleNove;// Nové pole už nebude nové ale staré.
                    poleNove = new Cell(radek, sloupec);//Vytvoří se nové pole s daným řádkem a sloupcem.
                    registrPrazdnychPoli.VlozPole(poleNove);// Toto pole se vloží do registru prázdných polí, jelikož v něm ještě není žádný míč.
                    if (radek == 1) // Pokud je řádek 1.
                    {
                        poleNahore = null;//Pokud je řádek první, budou mít odkazy na všechna pole směrem nahoru hodnotu null
                        if (sloupec == 1) { polePrvniAktualnihoRadku = poleNove; poleVlevo = null; Pole1A1 = poleNove; }// První pole aktuálního řádku se nastaví na nové pole, protože začal nový řádek. Pole vlevo neexistuje. Pole1A1 se nastaví na nové pole.
                        if ((sloupec > 1) & (sloupec < Sirka)) { poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); }// Pole vlevo se nastaví na staré. Pole vpravo od pole vlevo se nastaví na nové.
                        if (sloupec == Sirka) { poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); poleNove.NastavPoleVpravo(null); }// Pole vlevo se nastaví na staré. Pole vpravo od pole vlevo se nastaví na nové. Pole vpravo od nového pole neexistuje.
                    }
                    if ((radek > 1) & (radek < Vyska)) // Pokud je řádek větší než 1 a zároveň není řádek poslední.
                    {
                        if (sloupec == 1) { poleNahore = polePrvniAktualnihoRadku; poleNahore.NastavPoleDole(poleNove); polePrvniAktualnihoRadku = poleNove; poleVlevo = null; }// Pole nahoře od nového pole se nastaví na první pole předchozího řádku. Pole dole od pole nahoře se nastaví na nové pole. První pole aktuálního řádku se nastaví na nové pole, protože začal nový řádek. Pole vlevo neexistuje. 
                        if ((sloupec > 1) & (sloupec < Sirka)) { poleNahore = poleNahore.VratPoleVpravo(); poleNahore.NastavPoleDole(poleNove); poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); }// Pole nahoře od nového pole se nastaví na pole vpravo od současného pole nahoře. Pole dole od pole nahoře se nastaví na nové pole. Pole vlevo od nového pole se nastaví na staré pole. Pole vpravo od pole vlevo od nového pole se nastaví na nové pole.
                        if (sloupec == Sirka) { poleNahore = poleNahore.VratPoleVpravo(); poleNahore.NastavPoleDole(poleNove); poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); poleNove.NastavPoleVpravo(null); }

                    }
                    if (radek == Vyska) // Pokud je řádek poslední.
                    {
                        if (sloupec == 1) { poleNahore = polePrvniAktualnihoRadku; poleNahore.NastavPoleDole(poleNove); polePrvniAktualnihoRadku = poleNove; poleVlevo = null; }// První pole aktuálního řádku se nastaví na nové pole, protože začal nový řádek.
                        if ((sloupec > 1) & (sloupec < Sirka)) { poleNahore = poleNahore.VratPoleVpravo(); poleNahore.NastavPoleDole(poleNove); poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); }
                        if (sloupec == Sirka) { poleNahore = poleNahore.VratPoleVpravo(); poleNahore.NastavPoleDole(poleNove); poleVlevo = poleStare; poleVlevo.NastavPoleVpravo(poleNove); poleNove.NastavPoleVpravo(null); }
                        poleNove.NastavPoleDole(null);
                    }
                    
                    poleNove.NastavPoleNahore(poleNahore);
                    poleNove.NastavPoleVlevo(poleVlevo);

                }
            }
        }
    }
}
