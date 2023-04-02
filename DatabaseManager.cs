using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows;


namespace Míče
{
    public class DatabaseManager
    {
        private SQLiteConnection SqlPripojeni;
        private SQLiteCommand SqlPrikaz;

        public DatabaseManager()
        {
            PocatecniKontrola();
         }
        private void PocatecniKontrola()//Zkontroluje, zda existuje soubor mice.mice, pokud neexistuje, tak vytvoří nový a naplní ho patřičnou strukturou.
        {
            if (File.Exists("./mice.mice"))
            {

            }
            else
            {
                SQLiteConnection.CreateFile("mice.mice");
                SestavDatabazi();
                MessageBox.Show("Nápovědu k této hře zobrazíte stisknutím klávesy F1. Přeji hodně úspěchů.","Vítejte, nový hráči");
            }
        }
        private void SestavDatabazi()
        { 
            SpustPrikaz("CREATE TABLE Vysledky ( ID INT, Hrac TEXT, Vysledek INT, SestavaHry INT, DatumACas TEXT)");
            SpustPrikaz("CREATE TABLE SestavyHry (ID INT, Vyska INT, Sirka INT, SvetleZelena INT, Cervena INT, TmaveModra INT, Zluta INT, SvetleModra INT, Fialova INT, Hneda INT, Ruzova INT, Zelena INT, Zlata INT, Oranzova INT, Bila INT, Sediva INT, Cerna INT, Modra INT, VojenskaZelena INT, PocetHazenychMicuNaZacatkuHry INT, PocetHazenychMicuBehemHry INT, DuhoveBalls INT, ZdvojnasobujiciBalls INT, TvarSkupinyMicuKteraExploduje TEXT, MinimalniDelkaLinky INT)");
            SpustPrikaz("INSERT INTO SestavyHry VALUES (1, 9, 9, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0, 0, 'linka',5)");
        }
        private void NastavPripojeni()
        {
            SqlPripojeni = new SQLiteConnection
                  ("Data Source=mice.mice;Version=3;New=False;Compress=True; default timeout=10; Pooling=True; Max Pool Size=100;");
        }
        public void SpustPrikaz(string Prikaz)
        {
            NastavPripojeni();
            using (SqlPripojeni)
            {
                SqlPripojeni.Open();
                using (SQLiteCommand SqlPrikaz = new SQLiteCommand(Prikaz, SqlPripojeni))
                {
                    SqlPrikaz.ExecuteNonQuery();
                }
                  
            } 
        }
        public bool SpustPrikazAZjistiZdaExistujeAlesponJedenZaznam(string Prikaz)
        {
            NastavPripojeni();

            bool existujeZaznam=false;

            using (SqlPripojeni)
            {
                SqlPripojeni.Open();
                using (SQLiteCommand SqlPrikaz = new SQLiteCommand(Prikaz, SqlPripojeni))
                {
                    SQLiteDataReader reader = SqlPrikaz.ExecuteReader();
                    existujeZaznam=reader.Read();
                    while (reader.Read())
                    { existujeZaznam = true; break; };

                }

            }
            return existujeZaznam;
        }
        public int VratMaximalniHodnotuKliceIDZTabulky(string Tabulka)
        {
            NastavPripojeni();
            int maximalniID = 0;


            using (SqlPripojeni)
            {
                SqlPripojeni.Open();
                using (SQLiteCommand SqlPrikaz = new SQLiteCommand(String.Concat("SELECT MAX(ID) AS maxid FROM ", Tabulka, ";"), SqlPripojeni))
                {
                    maximalniID = Convert.ToInt32(SqlPrikaz.ExecuteScalar());
                }

            }
            return maximalniID;
        }
        public int VratHodnotuKliceIDPrvnihoZaznamuDanehoPrikazu(string Prikaz)
        {
            NastavPripojeni();
            int klicID = 0;


            using (SqlPripojeni)
            {
                SqlPripojeni.Open();
                using (SQLiteCommand SqlPrikaz = new SQLiteCommand(Prikaz, SqlPripojeni))
                {
                    SQLiteDataReader reader = SqlPrikaz.ExecuteReader();
                    while (reader.Read())
                    { klicID = Convert.ToInt32(reader["ID"]); }
                }

            }
            return klicID;
        }
        public DataSet VratPoleSerazenychVysledkuOdNejvetsihoZSestavyHrySDanymID(int ID)
        {
            NastavPripojeni();
            int klicID = ID;
            String [,] nejakaHodnotaZprehleduVysledkuPole = new String[1024,4 ];
            DataSet dset = new DataSet();

            using (SqlPripojeni)
            {
                SqlPripojeni.Open();
                String prikazKVyberuVysledku = String.Concat(
                    "SELECT Hrac AS \"Hráč\", Vysledek  AS \"Výsledek\", DatumACas AS \"Datum a čas\" FROM Vysledky WHERE SestavaHry= ",
                    klicID.ToString()," ",
                    "ORDER BY Vysledek DESC;"
                    
                    );
                using (SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(prikazKVyberuVysledku, SqlPripojeni))
                {myAdapter.Fill(dset);}

            }
            return dset;
        }

    }
}