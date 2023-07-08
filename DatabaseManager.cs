using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows;


namespace Circles
{
    public class DatabaseManager
    {
        private SQLiteConnection sqlConnection;
        private SQLiteCommand sqlCommand;

        public DatabaseManager()
        {
            initCheck();
         }
        private void initCheck()//
        {
            if (File.Exists("./circles.circles"))
            {

            }
            else
            {
                SQLiteConnection.CreateFile("circles.circles");
                createDatabase();
                MessageBox.Show("You can show Help by pressing key F1. We wish you many successes.","Welcome, new player.");
            }
        }
        private void createDatabase()
        { 
            executeSqlStatement("CREATE TABLE Vysledky ( ID INT, Hrac TEXT, Vysledek INT, SestavaHry INT, DatumACas TEXT)");
            executeSqlStatement("CREATE TABLE SestavyHry (ID INT, Vyska INT, Sirka INT, LightGreen INT, Red INT, DarkBlue INT, Yellow INT, LightBlue INT, Purple INT, Brown INT, Pink INT, Green INT, Gold INT, Orange INT, White INT, Grey INT, Black INT, Blue INT, ArmyGreen INT, PocetHazenychMicuNaZacatkuHry INT, PocetHazenychMicuBehemHry INT, RainbowBalls INT, DoubleScoreBalls INT, TvarSkupinyMicuKteraExploduje TEXT, MinimalniDelkaLinky INT)");
            executeSqlStatement("INSERT INTO SestavyHry VALUES (1, 9, 9, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0, 0, 'linka',5)");
        }
        private void initConnection()
        {
            sqlConnection = new SQLiteConnection
                  ("Data Source=circles.circles;Version=3;New=False;Compress=True; default timeout=10; Pooling=True; Max Pool Size=100;");
        }
        public void executeSqlStatement(string statement)
        {
            initConnection();
            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlStatement = new SQLiteCommand(statement, sqlConnection))
                {
                    sqlStatement.ExecuteNonQuery();
                }
                  
            } 
        }
        public bool returnsAtLeastOneRow(string statement)
        {
            initConnection();

            bool atLeastOneRowExists=false;

            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlStatement = new SQLiteCommand(statement, sqlConnection))
                {
                    SQLiteDataReader reader = sqlStatement.ExecuteReader();
                    atLeastOneRowExists=reader.Read();
                    while (reader.Read())
                    { atLeastOneRowExists = true; break; };

                }

            }
            return atLeastOneRowExists;
        }
        public int getMaxIdForTable(string tableName)
        {
            initConnection();
            int maxId = 0;


            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlStatement = new SQLiteCommand(String.Concat("SELECT MAX(ID) AS maxid FROM ", tableName, ";"), sqlConnection))
                {
                    maxId = Convert.ToInt32(sqlStatement.ExecuteScalar());
                }

            }
            return maxId;
        }
        public int getIdOfFirstFoundRow(string statement)
        {
            initConnection();
            int idKey = 0;


            using (sqlConnection)
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlStatement = new SQLiteCommand(statement, sqlConnection))
                {
                    SQLiteDataReader reader = sqlStatement.ExecuteReader();
                    while (reader.Read())
                    { idKey = Convert.ToInt32(reader["ID"]); }
                }

            }
            return idKey;
        }
        public DataSet getScoreListForGivenGameComposition(int gameCompositionId)
        {
            initConnection();
            int idKey = gameCompositionId;

            DataSet dset = new DataSet();

            using (sqlConnection)
            {
                sqlConnection.Open();
                String statement = String.Concat(
                    "SELECT Hrac AS \"Hráč\", Vysledek  AS \"Výsledek\", DatumACas AS \"Datum a čas\" FROM Vysledky WHERE SestavaHry= ",
                    idKey.ToString()," ",
                    "ORDER BY Vysledek DESC;"
                    
                    );
                using (SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(statement, sqlConnection))
                {myAdapter.Fill(dset);}

            }
            return dset;
        }

    }
}