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
            executeSqlStatement("CREATE TABLE Score ( ID INT, Player TEXT, PointCount INT, GameComposition INT, DateAndTime TEXT)");
            executeSqlStatement("CREATE TABLE GameComposition (ID INT, Height INT, Width INT, LightGreen INT, Red INT, DarkBlue INT, Yellow INT, LightBlue INT, Purple INT, Brown INT, Pink INT, Green INT, Gold INT, Orange INT, White INT, Grey INT, Black INT, Blue INT, ArmyGreen INT, StartBallCount INT, NextBallCount INT, RainbowBalls INT, DoubleScoreBalls INT, ShapeOfBallGroupWhichWillExplode TEXT, MinLineLength INT)");
            executeSqlStatement("INSERT INTO GameComposition VALUES (1, 9, 9, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0, 0, 'line',5)");
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
                    "SELECT Player AS \"Player\", PointCount  AS \"Point count\", DateAndTime AS \"Date and Time\" FROM Score WHERE GameComposition= ",
                    idKey.ToString()," ",
                    "ORDER BY PointCount DESC;"
                    
                    );
                using (SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(statement, sqlConnection))
                {myAdapter.Fill(dset);}

            }
            return dset;
        }

    }
}