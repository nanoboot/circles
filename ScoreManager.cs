using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Balls
{
    class ScoreManager
    {
        private int Vysledek = 0;
        private String HracovoJmeno = "";
        private DatabaseManager spravceDatabaze;
        private GameComposition sestavaHry;
        private String sqlPrikazSelectSestavyHry = "";
        private String sqlPrikazInsertSestavyHry = "";
        private String sqlPrikazInsertVysledky = "";
        private int klicIDSestavyHry=0;
        public ScoreManager(DatabaseManager spravceDatabaze, GameComposition sestavaHry)
        { this.spravceDatabaze = spravceDatabaze;
            this.sestavaHry = sestavaHry;
        }
        public int SpoctiBody(Stack<Cell> ZasobnikOdpalenychMicuPredany, Game hra, CellManager spravcePoli, Stack<Cell> ZasobnikPoliKtereUzNemajiBytAktivni)// Spočte body podle toho, jaké míče a kolik je v zásobníku.
        {
            Cell aktualniPole;
            int pocetMicu = ZasobnikOdpalenychMicuPredany.Count;
            int pocetZdvojnasobujicichMicu = 0;
            bool jeZdvojnasobujici = false;
            while (ZasobnikOdpalenychMicuPredany.Count != 0)
            {
                aktualniPole = ZasobnikOdpalenychMicuPredany.Pop();
                Ball zkoumanyMic = aktualniPole.getBallAndRemoveIt();
                if (zkoumanyMic.getType().Contains("Zdvojnasobujici")) { jeZdvojnasobujici = true; } else { jeZdvojnasobujici = false; }

                spravcePoli.VlozPrazdnePoleAbychONemVedel(aktualniPole);//Potom je nutné toto pole zařadit do registru prázdných polí.

                hra.insertCommand(String.Concat("MIC ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " ODSTRANIT"));//Prezentační vrstvě zašleme příkaz o změně.

                ZasobnikPoliKtereUzNemajiBytAktivni.Push(aktualniPole);
                hra.insertCommand((String.Concat("POLE ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " POZADI CERVENE")));

                if (jeZdvojnasobujici) { ++pocetZdvojnasobujicichMicu; } else { }; ;


            }
            int vypocteneBody = 0;
            if (sestavaHry.VratTvarSkupinyMicuKteraExploduje()== "linka")
            {
                switch (pocetMicu - sestavaHry.VratMinimalniDelkaLinky())
                {
                    case 0: { vypocteneBody = 10; }; break;
                    case 1: { vypocteneBody = 12; }; break;
                    case 2: { vypocteneBody = 18; }; break;
                    case 3: { vypocteneBody = 28; }; break;
                    case 4: { vypocteneBody = 42; }; break;
                    default:
                        {
                            vypocteneBody = 42 + (((pocetMicu - sestavaHry.VratMinimalniDelkaLinky()) - 4) * 5)
                             ;
                        }; break;
                };
            }
            else { vypocteneBody = 10; }

            vypocteneBody = vypocteneBody * (int)Math.Pow(2, pocetZdvojnasobujicichMicu);//Podle počtu zdvojnásobujících míčů v zásobníku vynásobí vypočtěné body mocninou dvou.
            PrictiBodyKVysledku(vypocteneBody,hra);// Přičte body k aktuálnímu výsledku.
            return vypocteneBody;
        }

        private void PrictiBodyKVysledku(int PocetBodu,Game hra)// Přičte body k aktuálnímu výsledku.
        {
            this.Vysledek = this.Vysledek + PocetBodu;
            hra.insertCommand(String.Concat("VYSLEDEK ", Vysledek));
        }
        public void NastavHracovoJmeno(String HracovoJmeno)
        {
            this.HracovoJmeno = HracovoJmeno;

            UlozVysledek(HracovoJmeno, this.Vysledek);
        }
        public void UlozVysledek(String HracovoJmeno, int Vysledek)
        {
            SestavPrikazNaSestavuHryProUlozeniDoDatabaze();
            if (ExistujeDanaSestavaHryJizVDatabazi())
            {
                klicIDSestavyHry=spravceDatabaze.VratHodnotuKliceIDPrvnihoZaznamuDanehoPrikazu(sqlPrikazSelectSestavyHry);
            }
            else
            {
                klicIDSestavyHry = spravceDatabaze.VratMaximalniHodnotuKliceIDZTabulky("SestavyHry");
                ++klicIDSestavyHry;
                SestavPrikazNaVlozeniNoveSestavyHryDoDatabaze(klicIDSestavyHry);
                spravceDatabaze.SpustPrikaz(sqlPrikazInsertSestavyHry);

            };
            SestavPrikazNaVlozeniVysledkuDoDatabaze(HracovoJmeno, Vysledek, klicIDSestavyHry);
            spravceDatabaze.SpustPrikaz(sqlPrikazInsertVysledky);
            
        }
        private bool ExistujeDanaSestavaHryJizVDatabazi()
        {
            return spravceDatabaze.SpustPrikazAZjistiZdaExistujeAlesponJedenZaznam(sqlPrikazSelectSestavyHry);
                }
        private String SestavPrikazNaSestavuHryProUlozeniDoDatabaze()
        {
            String sqlPrikazPrvniCast = "SELECT ID FROM SestavyHry WHERE ";
            String sqlPrikazDruhaCast = String.Concat(
                String.Concat("Vyska = ", sestavaHry.VratVyska().ToString(), " AND "),
                String.Concat("Sirka = ", sestavaHry.VratSirka().ToString(), " AND "),
                String.Concat("SvetleZelena = ", Convert.ToInt32(sestavaHry.VratSvetleZelena()).ToString(), " AND "),
                String.Concat("Cervena = ", Convert.ToInt32(sestavaHry.VratCervena()).ToString(), " AND "),
                String.Concat("TmaveModra = ", Convert.ToInt32(sestavaHry.VratTmaveModra()).ToString(), " AND "),
                String.Concat("Zluta = ", Convert.ToInt32(sestavaHry.VratZluta()).ToString(), " AND "),
                String.Concat("SvetleModra = ", Convert.ToInt32(sestavaHry.VratSvetleModra()).ToString(), " AND "),
                String.Concat("Fialova = ", Convert.ToInt32(sestavaHry.VratFialova()).ToString(), " AND "),
                String.Concat("Hneda = ", Convert.ToInt32(sestavaHry.VratHneda()).ToString(), " AND "),
                String.Concat("Ruzova = ", Convert.ToInt32(sestavaHry.VratRuzova()).ToString(), " AND "),
                String.Concat("Zelena = ", Convert.ToInt32(sestavaHry.VratZelena()).ToString(), " AND "),
                String.Concat("Zlata = ", Convert.ToInt32(sestavaHry.VratZlata()).ToString(), " AND "),
                String.Concat("Oranzova = ", Convert.ToInt32(sestavaHry.VratOranzova()).ToString(), " AND "),
                String.Concat("Bila = ", Convert.ToInt32(sestavaHry.VratBila()).ToString(), " AND "),
                String.Concat("Sediva = ", Convert.ToInt32(sestavaHry.VratSediva()).ToString(), " AND "),
                String.Concat("Cerna = ", Convert.ToInt32(sestavaHry.VratCerna()).ToString(), " AND "),
                String.Concat("Modra = ", Convert.ToInt32(sestavaHry.VratModra()).ToString(), " AND "),
                String.Concat("VojenskaZelena = ", Convert.ToInt32(sestavaHry.VratVojenskaZelena()).ToString(), " AND "),
                String.Concat("PocetHazenychMicuNaZacatkuHry = ", sestavaHry.VratPocetHazenychMicuNaZacatkuHry().ToString(), " AND "),
                String.Concat("PocetHazenychMicuBehemHry = ", sestavaHry.VratPocetHazenychMicuBehemHry().ToString(), " AND "),
                String.Concat("DuhoveBalls = ", Convert.ToInt32(sestavaHry.VratDuhoveBalls()).ToString(), " AND "),
                String.Concat("ZdvojnasobujiciBalls = ", Convert.ToInt32(sestavaHry.VratZdvojnasobujiciBalls()).ToString(), " AND "),
                String.Concat("TvarSkupinyMicuKteraExploduje = ", "'", sestavaHry.VratTvarSkupinyMicuKteraExploduje(), "'", " AND "),
                String.Concat("MinimalniDelkaLinky = ", sestavaHry.VratMinimalniDelkaLinky().ToString())
        );
            String sqlPrikazTretiCast = ";";
            this.sqlPrikazSelectSestavyHry = String.Concat(sqlPrikazPrvniCast, sqlPrikazDruhaCast, sqlPrikazTretiCast);
            return this.sqlPrikazSelectSestavyHry;
        }
        private String SestavPrikazNaVlozeniVysledkuDoDatabaze(String HracovoJmeno, int Vysledek, int klicIDSestavyHry)
        {
            DateTime datumACas = DateTime.Now;
            String otiskVCase = datumACas.ToString("yyyy MM dd HH:mm:ss");
            int klicIDVysledky = 0;
            if (spravceDatabaze.SpustPrikazAZjistiZdaExistujeAlesponJedenZaznam("SELECT * FROM Vysledky;"))
            { klicIDVysledky = (spravceDatabaze.VratMaximalniHodnotuKliceIDZTabulky("Vysledky"));++klicIDVysledky; }
            else { klicIDVysledky = 1; };
            String sqlPrikazPrvniCast = "INSERT INTO Vysledky VALUES ";
            String sqlPrikazDruhaCast = String.Concat(
                "(",
                String.Concat(klicIDVysledky.ToString(),","),
                String.Concat("'", HracovoJmeno, "',"),
                String.Concat(Vysledek.ToString(), ","),
                String.Concat(klicIDSestavyHry.ToString(), ","),
                String.Concat("'", otiskVCase, "'"),
                ")");
            String sqlPrikazTretiCast = ";";
            this.sqlPrikazInsertVysledky = String.Concat(sqlPrikazPrvniCast, sqlPrikazDruhaCast, sqlPrikazTretiCast);
            return this.sqlPrikazInsertVysledky;

        }
        private void SestavPrikazNaVlozeniNoveSestavyHryDoDatabaze(int klicIDSestavyHry)
        {
            
            
            String sqlPrikazPrvniCast = "INSERT INTO SestavyHry VALUES ";
            String sqlPrikazDruhaCast = String.Concat(
                "(",
                String.Concat(klicIDSestavyHry.ToString(), " , "),
                String.Concat(sestavaHry.VratVyska().ToString(), " , "),
                String.Concat(sestavaHry.VratSirka().ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratSvetleZelena()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratCervena()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratTmaveModra()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratZluta()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratSvetleModra()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratFialova()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratHneda()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratRuzova()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratZelena()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratZlata()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratOranzova()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratBila()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratSediva()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratCerna()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratModra()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratVojenskaZelena()).ToString(), " , "),
                String.Concat(sestavaHry.VratPocetHazenychMicuNaZacatkuHry().ToString(), " , "),
                String.Concat(sestavaHry.VratPocetHazenychMicuBehemHry().ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratDuhoveBalls()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.VratZdvojnasobujiciBalls()).ToString(), " , "),
                String.Concat("'", sestavaHry.VratTvarSkupinyMicuKteraExploduje(), "'", " , "),
                String.Concat(sestavaHry.VratMinimalniDelkaLinky().ToString()),
                ")");
            String sqlPrikazTretiCast = ";";
            this.sqlPrikazInsertSestavyHry = String.Concat(sqlPrikazPrvniCast, sqlPrikazDruhaCast, sqlPrikazTretiCast);
            
        }
        public System.Data.DataSet VratPoleSerazenychVysledkuOdNejvetsihoZSestavyHrySDanymID()
        { int ID=spravceDatabaze.VratHodnotuKliceIDPrvnihoZaznamuDanehoPrikazu(SestavPrikazNaSestavuHryProUlozeniDoDatabaze());
            return spravceDatabaze.VratPoleSerazenychVysledkuOdNejvetsihoZSestavyHrySDanymID(ID);
        }
    }
}
