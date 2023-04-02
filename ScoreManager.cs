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

                spravcePoli.addEmptyCell(aktualniPole);//Potom je nutné toto pole zařadit do registru prázdných polí.

                hra.insertCommand(String.Concat("MIC ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " ODSTRANIT"));//Prezentační vrstvě zašleme příkaz o změně.

                ZasobnikPoliKtereUzNemajiBytAktivni.Push(aktualniPole);
                hra.insertCommand((String.Concat("POLE ", aktualniPole.getRow(), " ", aktualniPole.getColumn(), " POZADI CERVENE")));

                if (jeZdvojnasobujici) { ++pocetZdvojnasobujicichMicu; } else { }; ;


            }
            int vypocteneBody = 0;
            if (sestavaHry.getShape()== "linka")
            {
                switch (pocetMicu - sestavaHry.getMinLineLength())
                {
                    case 0: { vypocteneBody = 10; }; break;
                    case 1: { vypocteneBody = 12; }; break;
                    case 2: { vypocteneBody = 18; }; break;
                    case 3: { vypocteneBody = 28; }; break;
                    case 4: { vypocteneBody = 42; }; break;
                    default:
                        {
                            vypocteneBody = 42 + (((pocetMicu - sestavaHry.getMinLineLength()) - 4) * 5)
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
                String.Concat("Vyska = ", sestavaHry.getHeight().ToString(), " AND "),
                String.Concat("Sirka = ", sestavaHry.getWidth().ToString(), " AND "),
                String.Concat("SvetleZelena = ", Convert.ToInt32(sestavaHry.getLightGreen()).ToString(), " AND "),
                String.Concat("Cervena = ", Convert.ToInt32(sestavaHry.isRed()).ToString(), " AND "),
                String.Concat("TmaveModra = ", Convert.ToInt32(sestavaHry.isDarkBlue()).ToString(), " AND "),
                String.Concat("Zluta = ", Convert.ToInt32(sestavaHry.isYellow()).ToString(), " AND "),
                String.Concat("SvetleModra = ", Convert.ToInt32(sestavaHry.isLightBlue()).ToString(), " AND "),
                String.Concat("Fialova = ", Convert.ToInt32(sestavaHry.isPurple()).ToString(), " AND "),
                String.Concat("Hneda = ", Convert.ToInt32(sestavaHry.isBrown()).ToString(), " AND "),
                String.Concat("Ruzova = ", Convert.ToInt32(sestavaHry.isPink()).ToString(), " AND "),
                String.Concat("Zelena = ", Convert.ToInt32(sestavaHry.isGreen()).ToString(), " AND "),
                String.Concat("Zlata = ", Convert.ToInt32(sestavaHry.isGold()).ToString(), " AND "),
                String.Concat("Oranzova = ", Convert.ToInt32(sestavaHry.isOrange()).ToString(), " AND "),
                String.Concat("Bila = ", Convert.ToInt32(sestavaHry.isWhite()).ToString(), " AND "),
                String.Concat("Sediva = ", Convert.ToInt32(sestavaHry.isGrey()).ToString(), " AND "),
                String.Concat("Cerna = ", Convert.ToInt32(sestavaHry.isBlack()).ToString(), " AND "),
                String.Concat("Modra = ", Convert.ToInt32(sestavaHry.isBlue()).ToString(), " AND "),
                String.Concat("VojenskaZelena = ", Convert.ToInt32(sestavaHry.isArmyGreen()).ToString(), " AND "),
                String.Concat("PocetHazenychMicuNaZacatkuHry = ", sestavaHry.getStartBallCount().ToString(), " AND "),
                String.Concat("PocetHazenychMicuBehemHry = ", sestavaHry.getNextBallCount().ToString(), " AND "),
                String.Concat("DuhoveBalls = ", Convert.ToInt32(sestavaHry.isJokerBalls()).ToString(), " AND "),
                String.Concat("ZdvojnasobujiciBalls = ", Convert.ToInt32(sestavaHry.isDoubleScoreBalls()).ToString(), " AND "),
                String.Concat("TvarSkupinyMicuKteraExploduje = ", "'", sestavaHry.getShape(), "'", " AND "),
                String.Concat("MinimalniDelkaLinky = ", sestavaHry.getMinLineLength().ToString())
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
                String.Concat(sestavaHry.getHeight().ToString(), " , "),
                String.Concat(sestavaHry.getWidth().ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.getLightGreen()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isRed()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isDarkBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isYellow()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isLightBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isPurple()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isBrown()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isPink()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isGreen()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isGold()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isOrange()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isWhite()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isGrey()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isBlack()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isArmyGreen()).ToString(), " , "),
                String.Concat(sestavaHry.getStartBallCount().ToString(), " , "),
                String.Concat(sestavaHry.getNextBallCount().ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isJokerBalls()).ToString(), " , "),
                String.Concat(Convert.ToInt32(sestavaHry.isDoubleScoreBalls()).ToString(), " , "),
                String.Concat("'", sestavaHry.getShape(), "'", " , "),
                String.Concat(sestavaHry.getMinLineLength().ToString()),
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
