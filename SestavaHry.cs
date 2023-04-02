using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Míče
{
    public class SestavaHry
    {
        private int Sirka;
        private int Vyska;
        private bool SvetleZelena;
        private bool Cervena;
        private bool TmaveModra;
        private bool Zluta;
        private bool SvetleModra;
        private bool Fialova;
        private bool Hneda;
        private bool Ruzova;
        private bool Zelena;
        private bool Zlata;
        private bool Oranzova;
        private bool Bila;
        private bool Sediva;
        private bool Cerna;
        private bool Modra;
        private bool VojenskaZelena;
        private int PocetHazenychMicuNaZacatkuHry;
        private int PocetHazenychMicuBehemHry;
        private bool DuhoveMice;
        private bool ZdvojnasobujiciMice;
        private string TvarSkupinyMicuKteraExploduje;
        private int MinimalniDelkaLinky;
        private bool Zmeneno;
        public SestavaHry()
        {
            NastavVychoziHodnoty();
            this.Zmeneno = false;
        }
        public void NastavZmeneno(bool Zmeneno)
        { this.Zmeneno = Zmeneno; }
        public bool VratZmeneno()
        { return this.Zmeneno; }
        public void NastavSirka(int Sirka)
        { this.Sirka = Sirka; }
        public void NastavVyska(int Vyska)
        { this.Vyska = Vyska; }
        public void NastavSvetleZelena(bool SvetleZelena)
        { this.SvetleZelena=SvetleZelena;}
        public void NastavCervena(bool Cervena)
        { this.Cervena = Cervena; }
        public void NastavTmaveModra(bool TmaveModra)
        { this.TmaveModra = TmaveModra; }
        public void NastavZluta(bool Zluta)
        { this.Zluta = Zluta; }
        public void NastavSvetleModra(bool SvetleModra)
        { this.SvetleModra = SvetleModra; }
        public void NastavFialova(bool Fialova)
        { this.Fialova = Fialova; }
        public void NastavHneda(bool Hneda)
        { this.Hneda = Hneda; }
        public void NastavRuzova(bool Ruzova)
        { this.Ruzova = Ruzova; }
        public void NastavZelena(bool Zelena)
        { this.Zelena = Zelena; }
        public void NastavZlata(bool Zlata)
        { this.Zlata = Zlata; }
        public void NastavOranzova(bool Oranzova)
        { this.Oranzova = Oranzova; }
        public void NastavBila(bool Bila)
        { this.Bila = Bila; }
        public void NastavSediva(bool Sediva)
        { this.Sediva = Sediva; }
        public void NastavCerna(bool Cerna)
        { this.Cerna = Cerna; }
        public void NastavModra(bool Modra)
        { this.Modra = Modra; }
        public void NastavVojenskaZelena(bool VojenskaZelena)
        { this.VojenskaZelena = VojenskaZelena; }
        public void NastavPocetHazenychMicuBehemHry(int PocetHazenychMicuBehemHry)
        { this.PocetHazenychMicuBehemHry = PocetHazenychMicuBehemHry; }
        public void NastavPocetHazenychMicuNaZacatkuHry(int PocetHazenychMicuNaZacatkuHry)
        { this.PocetHazenychMicuNaZacatkuHry = PocetHazenychMicuNaZacatkuHry; }
        public void NastavDuhoveMice(bool DuhoveMice)
        { this.DuhoveMice = DuhoveMice; }
        public void NastavZdvojnasobujiciMice(bool ZdvojnasobujiciMice)
        { this.ZdvojnasobujiciMice = ZdvojnasobujiciMice; }
        public void NastavTvarSkupinyMicuKteraExploduje(String TvarSkupinyMicuKteraExploduje)
        { this.TvarSkupinyMicuKteraExploduje = TvarSkupinyMicuKteraExploduje; }
        public void NastavMinimalniDelkaLinky(int MinimalniDelkaLinky)
        { this.MinimalniDelkaLinky = MinimalniDelkaLinky; }

        public int VratSirka()
        { return this.Sirka; }
        public int VratVyska() { return this.Vyska; }
        public bool VratSvetleZelena() { return this.SvetleZelena; }
        public bool VratCervena() { return this.Cervena; }
        public bool VratTmaveModra() { return this.TmaveModra; }
        public bool VratZluta() { return this.Zluta; }
        public bool VratSvetleModra() { return this.SvetleModra; }
        public bool VratFialova() { return this.Fialova; }
        public bool VratHneda() { return this.Hneda; }
        public bool VratRuzova() { return this.Ruzova; }
        public bool VratZelena() { return this.Zelena; }
        public bool VratZlata() { return this.Zlata; }
        public bool VratOranzova() { return this.Oranzova; }
        public bool VratBila() { return this.Bila; }
        public bool VratSediva() { return this.Sediva; }
        public bool VratCerna() { return this.Cerna; }
        public bool VratModra() { return this.Modra; }
        public bool VratVojenskaZelena() { return this.VojenskaZelena; }
        public int VratPocetHazenychMicuNaZacatkuHry() { return this.PocetHazenychMicuNaZacatkuHry; }
        public int VratPocetHazenychMicuBehemHry() { return this.PocetHazenychMicuBehemHry; }
        public bool VratDuhoveMice() { return this.DuhoveMice; }
        public bool VratZdvojnasobujiciMice() { return this.ZdvojnasobujiciMice; }
        public string VratTvarSkupinyMicuKteraExploduje() { return this.TvarSkupinyMicuKteraExploduje; }
        public int VratMinimalniDelkaLinky() { return this.MinimalniDelkaLinky; }
        public void NastavVychoziHodnoty()// Tato metoda přepíše všechny hodnoty ve formuláři na jejich výchozí hodnoty.
        {

            this.NastavVyska(9);
            this.NastavSirka(9);
            this.NastavSvetleZelena(true);
            this.NastavCervena(true);
            this.NastavTmaveModra(true);
            this.NastavZluta(true);
            this.NastavSvetleModra(true);
            this.NastavFialova(true);
            this.NastavHneda(true);
            this.NastavRuzova(false);
            this.NastavZelena(false);
            this.NastavZlata(false);
            this.NastavOranzova(false);
            this.NastavBila(false);
            this.NastavSediva(false);
            this.NastavCerna(false);
            this.NastavModra(false);
            this.NastavVojenskaZelena(false);
            this.NastavPocetHazenychMicuNaZacatkuHry(5);
            this.NastavPocetHazenychMicuBehemHry(3);
            this.NastavDuhoveMice(false);
            this.NastavZdvojnasobujiciMice(false);
            this.NastavTvarSkupinyMicuKteraExploduje("linka");
            this.NastavMinimalniDelkaLinky(5);
            
        }

    }
}
