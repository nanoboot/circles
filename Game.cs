using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace Balls
{
    public class Game
    {
        // zde začínají vlastnosti dané hry- její sestava
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
        private bool DuhoveBalls;
        private bool ZdvojnasobujiciBalls;
        private String TvarSkupinyMicuKteraExploduje;
        private int MinimalniDelkaLinky;

        GameComposition sestavaHry;
        private Queue FrontaPrikazu = new Queue();// Vytvoří se fronta příkazů, do které logická vrstva bude ukládat formou zpráv veškeré změny, aplikační vrstva si bude brát informace z této fronty, aby věděla, co má ukazovat hráči.
        private CellManager spravcePoli;
        private BallManager spravceMicu;
        private DatabaseManager spravceDatabaze;
        private ScoreManager spravceVysledku;
        private BallExploder odpalovacMicu;
        private Stack<Cell> ZasobnikOdpalenychMicu = new Stack<Cell>();//Zde se dočasně ukládají pole.
        private Stack<Cell> ZasobnikPoliKtereUzNemajiBytAktivni=new Stack<Cell>();//Zde se dočasně ukládají pole, která budou při dalším kroku mít nastavené pozadí na normální.
        private enum StavHry// Zde se ukládá stav hry.
        {
            Zacatek = 0,
            CekaNaAktivaciPlnehoPole = 1,
            CekaNaAktivaciPrazdnehoDostupnehoPole = 2,
            Konec = 10,
        }
        private StavHry stavHry = StavHry.Zacatek;


        public Game(
            GameComposition sestavaHry,
            int Vyska,
            int Sirka,
            bool SvetleZelena,
            bool Cervena,
            bool TmaveModra,
            bool Zluta,
            bool SvetleModra,
            bool Fialova,
            bool Hneda,
            bool Ruzova,
            bool Zelena,
            bool Zlata,
            bool Oranzova,
            bool Bila,
            bool Sediva,
            bool Cerna,
            bool Modra,
            bool VojenskaZelena,
            int PocetHazenychMicuNaZacatkuHry,
            int PocetHazenychMicuBehemHry,
            bool DuhoveBalls,
            bool ZdvojnasobujiciBalls,
            string TvarSkupinyMicuKteraExploduje,
            int MinimalniDelkaLinky)
        {
            this.sestavaHry = sestavaHry;
            this.Vyska = Vyska;
            this.Sirka = Sirka;
            this.SvetleZelena = SvetleZelena;
            this.Cervena = Cervena;
            this.TmaveModra = TmaveModra;
            this.Zluta = Zluta;
            this.SvetleModra = SvetleModra;
            this.Fialova = Fialova;
            this.Hneda = Hneda;
            this.Ruzova = Ruzova;
            this.Zelena = Zelena;
            this.Zlata = Zlata;
            this.Oranzova = Oranzova;
            this.Bila = Bila;
            this.Sediva = Sediva;
            this.Cerna = Cerna;
            this.Modra = Modra;
            this.VojenskaZelena = VojenskaZelena;
            this.PocetHazenychMicuNaZacatkuHry = PocetHazenychMicuNaZacatkuHry;
            this.PocetHazenychMicuBehemHry = PocetHazenychMicuBehemHry;
            this.DuhoveBalls = DuhoveBalls;
            this.ZdvojnasobujiciBalls = ZdvojnasobujiciBalls;
            this.TvarSkupinyMicuKteraExploduje = TvarSkupinyMicuKteraExploduje;
            this.MinimalniDelkaLinky = MinimalniDelkaLinky;
            odpalovacMicu = new BallExploder(TvarSkupinyMicuKteraExploduje, MinimalniDelkaLinky);
            stavHry = StavHry.Zacatek;// V konstruktoru se zde nastaví stav hry na Zacatek.
            this.spravceDatabaze = new DatabaseManager();//Při zapnutí programu Míče se vytvoří instance třídy SpravceDatabaze, s kterým budou komunikovat objekty, které budou mít za úkol ukládat trvale data nebo je číst
            this.spravceVysledku = new ScoreManager(this.spravceDatabaze, this.sestavaHry);
            spravcePoli = new CellManager(this.Vyska, this.Sirka);//Sestaví desku polí podle počtu řádků a sloupců.


            // Následuje vytvoření instance správce míčů, který bude generovat pouze typy míčů, které si hráč před započetím hry vybral.
            spravceMicu = new BallManager(
            this.SvetleZelena,
            this.Cervena,
            this.TmaveModra,
            this.Zluta,
            this.SvetleModra,
            this.Fialova,
            this.Hneda,
            this.Ruzova,
            this.Zelena,
            this.Zlata,
            this.Oranzova,
            this.Bila,
            this.Sediva,
            this.Cerna,
            this.Modra,
            this.VojenskaZelena,
            this.DuhoveBalls = DuhoveBalls,
            this.ZdvojnasobujiciBalls = ZdvojnasobujiciBalls,
            this);
           
            insertCommand("HRA NOVA");
            insertCommand(String.Concat("DESKA ", this.Vyska, " ", this.Sirka));
            ZacatekHry();
        }
        public System.Data.DataSet VratPoleSerazenychVysledkuOdNejvetsihoZSestavyHrySDanymID() //Vrátí proměnnou typu DataSet, která bude mít v sobě výsledky seřazené dle bodů od největšího.
        {
            return spravceVysledku.VratPoleSerazenychVysledkuOdNejvetsihoZSestavyHrySDanymID();
        }
        private void ZacatekHry()// Vygeneruje míče do určitého počtu prazdnych poli. Tato metoda se volá pouze na začátku hry.
        {
            for (int i = 1; i <= this.PocetHazenychMicuNaZacatkuHry; i++)
            { VygenerujJedenMicAPotomHoPresunDoNejakehoPrazdnehoPole(false);
                if (!spravcePoli.ExistujePrazdnePole()) { NastavStavHry(10); break; }// Pokud již neexistuje prázdné pole, není kam míč umisťovat, potom se tento cyklus for předčasně ukončí a stav hry se nastaví na Konec.
            };
            if (spravcePoli.ExistujePrazdnePole()) NastavStavHry(1);//Pokud ještě existuje prázdné pole, je kam míč umisťovat, potom se stav hry se nastaví na CekaNaAktivaciPrazdnehoDostupnehoPole.
        }
        
        private void HazeniMicuBehemHry()// Vygeneruje míče do určitého počtu prazdnych poli a to během hry opakovaně.
        {for (int i = 1; i <= this.PocetHazenychMicuBehemHry  ; i++)
            { VygenerujJedenMicAPotomHoPresunDoNejakehoPrazdnehoPole(true);
                switch (i)
                {
                    case 1:
                        {
                            Ball novyMic = spravceMicu.generateNewBall();
                            spravceMicu.nextBalls.Enqueue(novyMic);
                            String I = "";

                            switch (novyMic.getColour())
                            {
                                case "SvetleZelena": { I = "1"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); } break;
                                case "Cervena": { I = "2"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                                case "TmaveModra": { I = "3"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                                case "Zluta": { I = "4"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                                case "SvetleModra": { I = "5"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                                case "Fialova": { I = "6"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;
                                case "Hneda": { I = "7"; insertCommand(String.Concat("MIC X ", I, " DALSI1")); }; break;

                            }


                        }
                        break;
                    case 2:
                        {
                            Ball novyMic = spravceMicu.generateNewBall();
                            spravceMicu.nextBalls.Enqueue(novyMic);
                            String I = "";
                            switch (novyMic.getColour())
                            {
                                case "SvetleZelena": { I = "1"; insertCommand(String.Concat("MIC X ", I," DALSI2")); } break;
                                case "Cervena": { I = "2"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                                case "TmaveModra": { I = "3"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                                case "Zluta": { I = "4"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                                case "SvetleModra": { I = "5"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                                case "Fialova": { I = "6"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;
                                case "Hneda": { I = "7"; insertCommand(String.Concat("MIC X ", I, " DALSI2")); }; break;

                            }


                        }
                        break;
                    case 3:
                        {
                            Ball novyMic = spravceMicu.generateNewBall();
                            spravceMicu.nextBalls.Enqueue(novyMic);
                            String I = "";
                            switch (novyMic.getColour())
                            {
                                case "SvetleZelena": { I = "1"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); } break;
                                case "Cervena": { I = "2"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                                case "TmaveModra": { I = "3"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                                case "Zluta": { I = "4"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                                case "SvetleModra": { I = "5"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                                case "Fialova": { I = "6"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;
                                case "Hneda": { I = "7"; insertCommand(String.Concat("MIC X ", I, " DALSI3")); }; break;

                            }


                        }
                        break;
                }
                if (!spravcePoli.ExistujePrazdnePole()) {
                    NastavStavHry(10);
                    
                    i = this.PocetHazenychMicuBehemHry + 1;};
};}
        private void NastavStavHry(int Stav)
        {
            switch(Stav)
            {
                case 0: { stavHry = StavHry.Zacatek; } break;
                case 1: { stavHry = StavHry.CekaNaAktivaciPlnehoPole; } break;
                case 2: { stavHry = StavHry.CekaNaAktivaciPrazdnehoDostupnehoPole; } break;
                case 10: { stavHry = StavHry.Konec; } break;
                default: break;
            }
            
        }
        private int VratStavHry()
        {
            return (int)this.stavHry;
         }
        private void KonecHry()
        { }

        
        private Cell VygenerujJedenMicAPotomHoPresunDoNejakehoPrazdnehoPole(bool LogickaHodnota)
        {
            Cell PoleKamUmistimMic = spravcePoli.VratNahodnePrazdnePole();
            Ball MicKteryVlozimDoPole;
            if (LogickaHodnota)
            { MicKteryVlozimDoPole = spravceMicu.nextBalls.Dequeue(); }
            else
            { MicKteryVlozimDoPole = spravceMicu.generateNewBall(); };
            PoleKamUmistimMic.setBall(MicKteryVlozimDoPole);
            insertCommand(String.Concat("MIC ", PoleKamUmistimMic.getRow(), " ", PoleKamUmistimMic.getColumn(), " NOVY ", MicKteryVlozimDoPole.getType().ToUpper(), " NAFOUKNOUT"));

            ZasobnikOdpalenychMicu.Clear();
            ZasobnikOdpalenychMicu = odpalovacMicu.checkAndExplodedIfNeeded(PoleKamUmistimMic);
            if (ZasobnikOdpalenychMicu.Count > 0)
            {
                spravceVysledku.SpoctiBody(ZasobnikOdpalenychMicu, this, this.spravcePoli, this.ZasobnikPoliKtereUzNemajiBytAktivni);

            }
                return PoleKamUmistimMic;
            }
        public void NastavHracovoJmeno(String HracovoJmeno)
        {
            this.spravceVysledku.NastavHracovoJmeno(HracovoJmeno);
            
        }
        public void insertCommand(String Prikaz)//Metoda, která vloží další příkaz do fronty
        { FrontaPrikazu.Enqueue(Prikaz);
        }
        public string VratPrikaz()//Metoda, která z fronty vrátí první přidaný příkaz
        {
            if (FrontaPrikazu.Count > 0)//Pokud fronta není prázdná, vrátí daný příkaz
            { return Convert.ToString(FrontaPrikazu.Dequeue()); }
            else return "DNO";//Pokud fronta je prázdná, vrátí příkaz "DNO". Ve frontě v tento okamžik nejsou žádné příkazy.
        }
        public void AktivujPole(int Radek, int Sloupec)// Zaktivuje pole, na které hráč kliknul.
        {
            Cell poleKtereByloAktivovano = spravcePoli.VratPole(Radek, Sloupec);

           while (ZasobnikPoliKtereUzNemajiBytAktivni.Count != 0)// U polí, která mají mít pozadí již normální, se pošle příslušný příkaz do fronty příkazů.
            {
                Cell aktualniPole;
                aktualniPole = ZasobnikPoliKtereUzNemajiBytAktivni.Pop();
                insertCommand((String.Concat("POLE ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " POZADI NEZVYRAZNENE")));
            }

            switch (VratStavHry())// Podle stavu hry vybere správný algoritmus.
            {
                case 1:// Stav byl, že se čeká na aktivaci pole, ve kterém je míč.
                    {
                        if (!(poleKtereByloAktivovano.isEmpty()))//Pokud pole není prázdné (Aby se mohl míč přesunout , musí v tomto poli nějaký být)
                        {
                            spravcePoli.NastavAktivniPoleOdkud(poleKtereByloAktivovano);// Nastaví se aktivní pole odkud
                            NastavStavHry(2);// Změnil se stav hry a to se musí někde zaznamenat.
                            insertCommand(String.Concat("MIC ", poleKtereByloAktivovano.getRow(), " ", poleKtereByloAktivovano.getColumn(), " SKAKEJ "));// Příkaz, který způsobí, že reprezentace míče v aplikační vrstvě v tomto poli bude skákat.
                        }
                       
                        ;
                    } break;
                case 2:// Stav byl, že se čeká na aktivaci prázdného pole, do kterého chceme přesunout míč, který skáká.
                    {
                        if (!(poleKtereByloAktivovano.isEmpty()))//Pokud pole není prázdné, tak se změní aktivované pole odkud na toto pole, skákat teď bude pouze míč v tomto poli.
                        {
                            spravcePoli.VratAktivniPoleOdkud().getBallAndDoNotRemoveIt().dontJump();// Jelikož se bude měnit aktivní pole odkud, je potřeba, aby staré aktivní pole nařídilo svému míči přestat skákat.
                            insertCommand(String.Concat("MIC ", spravcePoli.VratAktivniPoleOdkud().getRow(), " ", spravcePoli.VratAktivniPoleOdkud().getColumn(), " NESKAKEJ "));// Příkaz, který způsobí, že reprezentace míče v aplikační vrstvě v poli, které již není aktivní, přestane skákat.

                            spravcePoli.NastavAktivniPoleOdkud(poleKtereByloAktivovano);// Nastaví se aktivní pole odkud na nové. Vlastně se stalo to, že jsem dříve aktivovali nějaké pole, míč v tomto poli začal skákat. Nyní jsem však aktivovali jiné pole, pole se souřadnicemi, na které jsme kliknuli naposledy.
                                                                                        /*NastavStavHry(2);*/// Stav hry se nezměnil, stále se čeká na aktivaci neprázdného pole. Proto je kód na tomto řádku zakomentován, protože je zbytečný.
                            insertCommand(String.Concat("MIC ", poleKtereByloAktivovano.getRow(), " ", poleKtereByloAktivovano.getColumn(), " SKAKEJ "));// Příkaz, který způsobí, že reprezentace míče v aplikační vrstvě v tomto poli bude skákat.

                            
                        }
                        if ((poleKtereByloAktivovano.isEmpty()))//Pokud pole je prázdné.
                        {
                            
                            spravcePoli.NastavAktivniPoleKam(poleKtereByloAktivovano);// Nastaví se aktivní pole kam na nové.
                            PathFinder hledacCesty = new PathFinder(spravcePoli.VratAktivniPoleOdkud(),spravcePoli.VratAktivniPoleKam(),this,this.ZasobnikPoliKtereUzNemajiBytAktivni);// Vytvoří se nový hledač cesty.
                            if (hledacCesty.Hledej())// Pokud hledač cesty našel cestu.
                            { 
                                spravcePoli.VratAktivniPoleOdkud().getBallAndDoNotRemoveIt().dontJump();// Cesta se našla, míč se bude přesouvat a proto se mu pošle příkaz, aby již neskákal.
                                insertCommand(String.Concat("MIC ", spravcePoli.VratAktivniPoleOdkud().getRow(), " ", spravcePoli.VratAktivniPoleOdkud().getColumn(), " NESKAKEJ"));// Příkaz, který způsobí, že reprezentace míče v aplikační vrstvě v poli, které již není aktivní, přestane skákat.
                                Ball micKterySePresouva=spravcePoli.VratAktivniPoleOdkud().getBallAndRemoveIt();//Je nutné odstranit míč z pole, odkud ho chceme přesunout.
                                spravcePoli.VlozPrazdnePoleAbychONemVedel(spravcePoli.VratAktivniPoleOdkud());//Potom je nutné toto pole zařadit do registru prázdných polí.
                                insertCommand(String.Concat("MIC ", spravcePoli.VratAktivniPoleOdkud().getRow(), " ", spravcePoli.VratAktivniPoleOdkud().getColumn(), " ODSTRANIT"));//Prezentační vrstvě zašleme příkaz o změně.
                                spravcePoli.VratAktivniPoleKam().setBall(micKterySePresouva);//Míč se přesune do svého nového pole.
                                spravcePoli.VlozPlnePoleAbychONemVedel(spravcePoli.VratAktivniPoleKam());//Pole, kam jsme přesunuli míč, již není prázdné a musíme o tom informovat správce polí.

                                insertCommand(String.Concat("MIC ", spravcePoli.VratAktivniPoleKam().getRow(), " ", spravcePoli.VratAktivniPoleKam().getColumn(), " NOVY ", micKterySePresouva.getType().ToUpper()," NAFOUKNUT"));
                                //Prezentační vrstvě zašleme další příkaz o změně.
                                Stack<Cell> zasobnikPoliKamCestovalMic =hledacCesty.VratZasobnikPoliOdkudKam();

                                Cell aktualniPole;
                                while (zasobnikPoliKamCestovalMic.Count!=0)
                                {
                                    aktualniPole=zasobnikPoliKamCestovalMic.Pop();
                                    ZasobnikPoliKtereUzNemajiBytAktivni.Push(aktualniPole);
                                    insertCommand((String.Concat("POLE ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " POZADI ZVYRAZNENE"))); }
                                //Tento příkaz způsobí, že pole, přes která přešel míč, dočasně ztmavnou.
                                ZasobnikOdpalenychMicu.Clear();
                                ZasobnikOdpalenychMicu = odpalovacMicu.checkAndExplodedIfNeeded(spravcePoli.VratAktivniPoleKam());
                                
                                if (ZasobnikOdpalenychMicu.Count > 1)
                                {
                                    this.spravceVysledku.SpoctiBody(ZasobnikOdpalenychMicu,this,this.spravcePoli,this.ZasobnikPoliKtereUzNemajiBytAktivni);

                                }
                                else {
                                    HazeniMicuBehemHry();// Hodí se příslušný počet míčů do prázdných polí.
                                    
                                }

                                NastavStavHry(1);//Už se nečeká na aktivaci prázdného pole. Teď se opět čeká na aktivaci pole, ve kterém je míč.
                                if (spravcePoli.ExistujePrazdnePole())
                                {

                                }
                                else
                                {

                                    NastavStavHry(10);
                                    insertCommand("HRA KONEC");
                                }

                            }
                            else {
                                //MessageBox.Show("Nenašel jsem cestu");
                            };
                            // Pokud hledač cesty nenašel cestu.

                        }

                    ;
                    }
                    break;
                default: break;

            }
            
        }
        public int VratSirku()
        {
            return this.Sirka;
        }
        public int VratVysku()
        {
            return this.Vyska;
        }
        
        
    }
}