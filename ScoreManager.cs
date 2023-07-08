using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Circles
{
    class ScoreManager
    {
        private int score = 0;
        private String playerName = "";
        private DatabaseManager databaseManager;
        private GameComposition gameComposition;
        private String sqlStatementSelectGameComposition = "";
        private String sqlStatementInsertGameComposition = "";
        private String sqlStatementInsertScore = "";
        private int keyIdOfGameComposition=0;
        public ScoreManager(DatabaseManager databaseManagerArg, GameComposition gameCompositionArg)
        { this.databaseManager = databaseManagerArg;
            this.gameComposition = gameCompositionArg;
        }
        public int countPoints(Stack<Cell> explodedBalls, Game game, CellManager cellManager, Stack<Cell> cellsWhichShouldNotBeActiveAnymore)// Count the points according to which balls and how many are in the tray.
        {
            Cell currentCell;
            int ballCount = explodedBalls.Count;
            int coutnOfDoubleBalls = 0;
            bool isDouble = false;
            while (explodedBalls.Count != 0)
            {
                currentCell = explodedBalls.Pop();
                Ball checkedBall = currentCell.getBallAndRemoveIt();
                if (checkedBall.getType().Contains("DoubleScore")) { isDouble = true; } else { isDouble = false; }

                cellManager.addEmptyCell(currentCell);// Then it is necessary to include this field in the registry of empty fields.

                game.insertCommand(String.Concat("BALL ", currentCell.getRow(), " ", currentCell.getColumn(), " ODSTRANIT"));// We send a change command to the presentation layer.

                cellsWhichShouldNotBeActiveAnymore.Push(currentCell);
                game.insertCommand((String.Concat("POLE ", currentCell.getRow(), " ", currentCell.getColumn(), " POZADI CERVENE")));

                if (isDouble) { ++coutnOfDoubleBalls; } else { }; ;


            }
            int countPoints = 0;
            if (gameComposition.getShape()== "linka")
            {
                switch (ballCount - gameComposition.getMinLineLength())
                {
                    case 0: { countPoints = 10; }; break;
                    case 1: { countPoints = 12; }; break;
                    case 2: { countPoints = 18; }; break;
                    case 3: { countPoints = 28; }; break;
                    case 4: { countPoints = 42; }; break;
                    default:
                        {
                            countPoints = 42 + (((ballCount - gameComposition.getMinLineLength()) - 4) * 5)
                             ;
                        }; break;
                };
            }
            else { countPoints = 10; }

            countPoints = countPoints * (int)Math.Pow(2, coutnOfDoubleBalls);// Based on the number of doubling balls in the stack, it multiplies the calculated points by a power of two.
            addPointsToTheScore(countPoints,game);// Adds points to the current score.
            return countPoints;
        }

        private void addPointsToTheScore(int pointCount,Game game)// Adds points to the current score.
        {
            this.score = this.score + pointCount;
            game.insertCommand(String.Concat("VYSLEDEK ", score));
        }
        public void setPlayerName(String playerName)
        {
            this.playerName = playerName;

            saveScore(playerName, this.score);
        }
        public void saveScore(String playerName, int score)
        {
            createStatementToSaveGameCompositionIntoDatabase();
            if (doesExistThisGameCompositinAlreadyInDatabase())
            {
                keyIdOfGameComposition=databaseManager.getIdOfFirstFoundRow(sqlStatementSelectGameComposition);
            }
            else
            {
                keyIdOfGameComposition = databaseManager.getMaxIdForTable("SestavyHry");
                ++keyIdOfGameComposition;
                SestavPrikazNaVlozeniNoveSestavyHryDoDatabaze(keyIdOfGameComposition);
                databaseManager.executeSqlStatement(sqlStatementInsertGameComposition);

            };
            createStatementToAddNewScoreToDatabase(playerName, score, keyIdOfGameComposition);
            databaseManager.executeSqlStatement(sqlStatementInsertScore);
            
        }
        private bool doesExistThisGameCompositinAlreadyInDatabase()
        {
            return databaseManager.returnsAtLeastOneRow(sqlStatementSelectGameComposition);
                }
        private String createStatementToSaveGameCompositionIntoDatabase()
        {
            String sql1 = "SELECT ID FROM SestavyHry WHERE ";
            String sql2 = String.Concat(
                String.Concat("Vyska = ", gameComposition.getHeight().ToString(), " AND "),
                String.Concat("Sirka = ", gameComposition.getWidth().ToString(), " AND "),
                String.Concat("LightGreen = ", Convert.ToInt32(gameComposition.getLightGreen()).ToString(), " AND "),
                String.Concat("Red = ", Convert.ToInt32(gameComposition.isRed()).ToString(), " AND "),
                String.Concat("DarkBlue = ", Convert.ToInt32(gameComposition.isDarkBlue()).ToString(), " AND "),
                String.Concat("Yellow = ", Convert.ToInt32(gameComposition.isYellow()).ToString(), " AND "),
                String.Concat("LightBlue = ", Convert.ToInt32(gameComposition.isLightBlue()).ToString(), " AND "),
                String.Concat("Purple = ", Convert.ToInt32(gameComposition.isPurple()).ToString(), " AND "),
                String.Concat("Brown = ", Convert.ToInt32(gameComposition.isBrown()).ToString(), " AND "),
                String.Concat("Pink = ", Convert.ToInt32(gameComposition.isPink()).ToString(), " AND "),
                String.Concat("Green = ", Convert.ToInt32(gameComposition.isGreen()).ToString(), " AND "),
                String.Concat("Gold = ", Convert.ToInt32(gameComposition.isGold()).ToString(), " AND "),
                String.Concat("Orange = ", Convert.ToInt32(gameComposition.isOrange()).ToString(), " AND "),
                String.Concat("White = ", Convert.ToInt32(gameComposition.isWhite()).ToString(), " AND "),
                String.Concat("Grey = ", Convert.ToInt32(gameComposition.isGrey()).ToString(), " AND "),
                String.Concat("Black = ", Convert.ToInt32(gameComposition.isBlack()).ToString(), " AND "),
                String.Concat("Blue = ", Convert.ToInt32(gameComposition.isBlue()).ToString(), " AND "),
                String.Concat("ArmyGreen = ", Convert.ToInt32(gameComposition.isArmyGreen()).ToString(), " AND "),
                String.Concat("PocetHazenychMicuNaZacatkuHry = ", gameComposition.getStartBallCount().ToString(), " AND "),
                String.Concat("PocetHazenychMicuBehemHry = ", gameComposition.getNextBallCount().ToString(), " AND "),
                String.Concat("RainbowBalls = ", Convert.ToInt32(gameComposition.isJokerBalls()).ToString(), " AND "),
                String.Concat("DoubleScoreBalls = ", Convert.ToInt32(gameComposition.isDoubleScoreBalls()).ToString(), " AND "),
                String.Concat("TvarSkupinyMicuKteraExploduje = ", "'", gameComposition.getShape(), "'", " AND "),
                String.Concat("MinimalniDelkaLinky = ", gameComposition.getMinLineLength().ToString())
        );
            String sql3 = ";";
            this.sqlStatementSelectGameComposition = String.Concat(sql1, sql2, sql3);
            return this.sqlStatementSelectGameComposition;
        }
        private String createStatementToAddNewScoreToDatabase(String playerName, int score, int klicIDSestavyHry)
        {
            DateTime datumACas = DateTime.Now;
            String otiskVCase = datumACas.ToString("yyyy MM dd HH:mm:ss");
            int klicIDVysledky = 0;
            if (databaseManager.returnsAtLeastOneRow("SELECT * FROM Vysledky;"))
            { klicIDVysledky = (databaseManager.getMaxIdForTable("Vysledky"));++klicIDVysledky; }
            else { klicIDVysledky = 1; };
            String sqlPrikazPrvniCast = "INSERT INTO Vysledky VALUES ";
            String sqlPrikazDruhaCast = String.Concat(
                "(",
                String.Concat(klicIDVysledky.ToString(),","),
                String.Concat("'", playerName, "',"),
                String.Concat(score.ToString(), ","),
                String.Concat(klicIDSestavyHry.ToString(), ","),
                String.Concat("'", otiskVCase, "'"),
                ")");
            String sqlPrikazTretiCast = ";";
            this.sqlStatementInsertScore = String.Concat(sqlPrikazPrvniCast, sqlPrikazDruhaCast, sqlPrikazTretiCast);
            return this.sqlStatementInsertScore;

        }
        private void SestavPrikazNaVlozeniNoveSestavyHryDoDatabaze(int keyIdOfGameCompositionArg)
        {
            
            
            String sql1 = "INSERT INTO SestavyHry VALUES ";
            String sql2 = String.Concat(
                "(",
                String.Concat(keyIdOfGameCompositionArg.ToString(), " , "),
                String.Concat(gameComposition.getHeight().ToString(), " , "),
                String.Concat(gameComposition.getWidth().ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.getLightGreen()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isRed()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isDarkBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isYellow()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isLightBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isPurple()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isBrown()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isPink()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isGreen()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isGold()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isOrange()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isWhite()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isGrey()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isBlack()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isBlue()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isArmyGreen()).ToString(), " , "),
                String.Concat(gameComposition.getStartBallCount().ToString(), " , "),
                String.Concat(gameComposition.getNextBallCount().ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isJokerBalls()).ToString(), " , "),
                String.Concat(Convert.ToInt32(gameComposition.isDoubleScoreBalls()).ToString(), " , "),
                String.Concat("'", gameComposition.getShape(), "'", " , "),
                String.Concat(gameComposition.getMinLineLength().ToString()),
                ")");
            String sql3 = ";";
            this.sqlStatementInsertGameComposition = String.Concat(sql1, sql2, sql3);
            
        }
        public System.Data.DataSet getScoreListForGivenTGameCompositionWithGivenId()
        { int ID=databaseManager.getIdOfFirstFoundRow(createStatementToSaveGameCompositionIntoDatabase());
            return databaseManager.getScoreListForGivenGameComposition(ID);
        }
    }
}
