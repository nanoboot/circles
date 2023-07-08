using System;
using System.Collections.Generic;
using System.Collections;

namespace Circles
{
    public class Game
    {
        // the properties of the given game start here - its composition
        private int width;
        private int height;
        private bool lightGreen;
        private bool red;
        private bool darkBlue;
        private bool yellow;
        private bool lightBlue;
        private bool purple;
        private bool brown;
        private bool pink;
        private bool green;
        private bool gold;
        private bool orange;
        private bool white;
        private bool grey;
        private bool black;
        private bool blue;
        private bool armyGreen;
        private int startBallCount;
        private int nextBallCount;
        private bool jokerBalls;
        private bool doubleScoreBalls;
        private String shapeOfGroupOfBallsWhichExplode;
        private int minLineLength;

        GameComposition gameComposition;
        private Queue commandQueue = new Queue();// A command queue will be created in which the logical layer will store all changes in the form of messages, the application layer will take information from this queue to know what to show the player.
        private CellManager cellManager;
        private BallManager ballManager;
        private DatabaseManager databaseManager;
        private ScoreManager scoreManager;
        private BallExploder ballExploder;
        private Stack<Cell> explodedBalls = new Stack<Cell>();//Fields are temporarily stored here.
        private Stack<Cell> cellsWhichAreNoMoreActive=new Stack<Cell>();//Fields are temporarily stored here, which will have their background set to normal in the next step.
        private enum GameState// Game state is saved here.
        {
            Start = 0,
            WaitingForActivationOfFullCell = 1,
            WaitingFroActivationOfEmptyAvailableCell = 2,
            End = 10,
        }
        private GameState gameState = GameState.Start;


        public Game(
            GameComposition sestavaHry,
            int height,
            int width,
            bool lightGreen,
            bool red,
            bool darkBlue,
            bool yellow,
            bool lightBlue,
            bool purple,
            bool brown,
            bool pink,
            bool green,
            bool gold,
            bool orange,
            bool white,
            bool grey,
            bool black,
            bool blue,
            bool armyGreen,
            int startBallCount,
            int PocetHazenychMicuBehemHry,
            bool nextBallCount,
            bool doubleScoreBalls,
            string shapeOfGroupOfBallsWhichExplode,
            int minLineLength)
        {
            this.gameComposition = sestavaHry;
            this.height = height;
            this.width = width;
            this.lightGreen = lightGreen;
            this.red = red;
            this.darkBlue = darkBlue;
            this.yellow = yellow;
            this.lightBlue = lightBlue;
            this.purple = purple;
            this.brown = brown;
            this.pink = pink;
            this.green = green;
            this.gold = gold;
            this.orange = orange;
            this.white = white;
            this.grey = grey;
            this.black = black;
            this.blue = blue;
            this.armyGreen = armyGreen;
            this.startBallCount = startBallCount;
            this.nextBallCount = PocetHazenychMicuBehemHry;
            this.jokerBalls = nextBallCount;
            this.doubleScoreBalls = doubleScoreBalls;
            this.shapeOfGroupOfBallsWhichExplode = shapeOfGroupOfBallsWhichExplode;
            this.minLineLength = minLineLength;
            ballExploder = new BallExploder(shapeOfGroupOfBallsWhichExplode, minLineLength);
            gameState = GameState.Start;// In the constructor, the state of the game is set to Start.
            this.databaseManager = new DatabaseManager();//When the Balls program is turned on, an instance of the Database Manager class is created, with which objects will communicate, which will have the task of permanently storing data or reading them
            this.scoreManager = new ScoreManager(this.databaseManager, this.gameComposition);
            cellManager = new CellManager(this.height, this.width);//Builds the table of fields according to the number of rows and columns.

            // This is followed by creating an instance of the ball manager, which will only generate the types of balls the player selected before starting the game.
            ballManager = new BallManager(
            this.lightGreen,
            this.red,
            this.darkBlue,
            this.yellow,
            this.lightBlue,
            this.purple,
            this.brown,
            this.pink,
            this.green,
            this.gold,
            this.orange,
            this.white,
            this.grey,
            this.black,
            this.blue,
            this.armyGreen,
            this.jokerBalls = nextBallCount,
            this.doubleScoreBalls = doubleScoreBalls,
            this);
           
            insertCommand("HRA NOVA");
            insertCommand(String.Concat("DESKA ", this.height, " ", this.width));
            startGame();
        }
        public System.Data.DataSet getScoreListForGivenTGameCompositionWithGivenId()//Returns a variable of type DataSet, which will contain the results sorted by points from the largest.
        {
            return scoreManager.getScoreListForGivenTGameCompositionWithGivenId();
        }
        private void startGame()// Generates balls into a specified number of empty fields. This method is only called at the beginning of the game.
        {
            for (int i = 1; i <= this.startBallCount; i++)
            { generateOneBallAndInsertItIntoAnEmptyCell(false);
                if (!cellManager.hasAtLeastOneEmptyCell()) { setGameState(10); break; }// If there is no longer an empty field, there is nowhere to place the ball, then this for loop will terminate early and the game state will be set to End.
            };
            if (cellManager.hasAtLeastOneEmptyCell()) setGameState(1);//If there is still an empty field to place the ball, then the game state will be set to AwaitingEmptyAvailableFieldActivation.
        }
        
        private void throwBallDuringGameIntoEmptyCells()
        {for (int i = 1; i <= this.nextBallCount  ; i++)
            { generateOneBallAndInsertItIntoAnEmptyCell(true);
                switch (i)
                {
                    case 1:
                        {
                            Ball newBall = ballManager.generateNewBall();
                            ballManager.nextBalls.Enqueue(newBall);
                            String I = "";

                            switch (newBall.getColour())
                            {
                                case "LightGreen": { I = "1"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); } break;
                                case "Red": { I = "2"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                                case "DarkBlue": { I = "3"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                                case "Yellow": { I = "4"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                                case "LightBlue": { I = "5"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                                case "Purple": { I = "6"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;
                                case "Brown": { I = "7"; insertCommand(String.Concat("BALL X ", I, " NEXT1")); }; break;

                            }


                        }
                        break;
                    case 2:
                        {
                            Ball newBall = ballManager.generateNewBall();
                            ballManager.nextBalls.Enqueue(newBall);
                            String I = "";
                            switch (newBall.getColour())
                            {
                                case "LightGreen": { I = "1"; insertCommand(String.Concat("BALL X ", I," NEXT2")); } break;
                                case "Red": { I = "2"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                                case "DarkBlue": { I = "3"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                                case "Yellow": { I = "4"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                                case "LightBlue": { I = "5"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                                case "Purple": { I = "6"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;
                                case "Brown": { I = "7"; insertCommand(String.Concat("BALL X ", I, " NEXT2")); }; break;

                            }


                        }
                        break;
                    case 3:
                        {
                            Ball newBall = ballManager.generateNewBall();
                            ballManager.nextBalls.Enqueue(newBall);
                            String I = "";
                            switch (newBall.getColour())
                            {
                                case "LightGreen": { I = "1"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); } break;
                                case "Red": { I = "2"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                                case "DarkBlue": { I = "3"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                                case "Yellow": { I = "4"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                                case "LightBlue": { I = "5"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                                case "Purple": { I = "6"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;
                                case "Brown": { I = "7"; insertCommand(String.Concat("BALL X ", I, " NEXT3")); }; break;

                            }


                        }
                        break;
                }
                if (!cellManager.hasAtLeastOneEmptyCell()) {
                    setGameState(10);
                    
                    i = this.nextBallCount + 1;};
};}
        private void setGameState(int Stav)
        {
            switch(Stav)
            {
                case 0: { gameState = GameState.Start; } break;
                case 1: { gameState = GameState.WaitingForActivationOfFullCell; } break;
                case 2: { gameState = GameState.WaitingFroActivationOfEmptyAvailableCell; } break;
                case 10: { gameState = GameState.End; } break;
                default: break;
            }
            
        }
        private int getGameState()
        {
            return (int)this.gameState;
         }
        private void endGame()
        { }

        
        private Cell generateOneBallAndInsertItIntoAnEmptyCell(bool loginBooleanValue)
        {
            Cell randomEmptyCell = cellManager.getRandomEmptyCell();
            Ball ballWhichWillBeInsertedIntoRandomEmptyCell;
            if (loginBooleanValue)
            { ballWhichWillBeInsertedIntoRandomEmptyCell = ballManager.nextBalls.Dequeue(); }
            else
            { ballWhichWillBeInsertedIntoRandomEmptyCell = ballManager.generateNewBall(); };
            randomEmptyCell.setBall(ballWhichWillBeInsertedIntoRandomEmptyCell);
            insertCommand(String.Concat("BALL ", randomEmptyCell.getRow(), " ", randomEmptyCell.getColumn(), " NOVY ", ballWhichWillBeInsertedIntoRandomEmptyCell.getType().ToUpper(), " NAFOUKNOUT"));

            explodedBalls.Clear();
            explodedBalls = ballExploder.checkAndExplodeIfNeeded(randomEmptyCell);
            if (explodedBalls.Count > 0)
            {
                scoreManager.countPoints(explodedBalls, this, this.cellManager, this.cellsWhichAreNoMoreActive);

            }
                return randomEmptyCell;
            }
        public void setPlayerName(String playerName)
        {
            this.scoreManager.setPlayerName(playerName);
            
        }
        public void insertCommand(String command)//Method that puts the next command in the queue
        { commandQueue.Enqueue(command);
        }
        public string getCommand()//Method that returns the first command added from the queue
        {
            if (commandQueue.Count > 0)//If the queue is not empty, returns the given command
            { return Convert.ToString(commandQueue.Dequeue()); }
            else return "DNO";//If the queue is empty, it returns the command "DNO". There are no orders in the queue at this time.
        }
        public void activateCell(int row, int column)//Cell, which was clicked by player, will be activated.
        {
            Cell cellWhichWasActivated = cellManager.getCell(row, column);

           while (cellsWhichAreNoMoreActive.Count != 0)// For fields that should already have a normal background, the corresponding command is sent to the command queue.
            {
                Cell currentCell;
                currentCell = cellsWhichAreNoMoreActive.Pop();
                insertCommand((String.Concat("POLE ", currentCell.getRow(), " ", currentCell.getColumn(), " POZADI NEZVYRAZNENE")));
            }

            switch (getGameState())// It will select the correct algorithm according to the state of the game.
            {
                case 1:// The status was waiting for the field the ball is in to be activated.
                    {
                        if (!(cellWhichWasActivated.isEmpty()))// If the field is not empty (There must be one in this field for the ball to move)
                        {
                            cellManager.setActiveCellFrom(cellWhichWasActivated);// The active from field is set
                            setGameState(2);// The state of the game has changed and that must be recorded somewhere.
                            insertCommand(String.Concat("BALL ", cellWhichWasActivated.getRow(), " ", cellWhichWasActivated.getColumn(), " SKAKEJ "));// A command that causes the application layer's representation of the ball in this box to bounce.
                        }
                       
                        ;
                    } break;
                case 2:// The status was waiting for the activation of an empty field to which we want to move the bouncing ball.
                    {
                        if (!(cellWhichWasActivated.isEmpty()))// If the field is not empty, then the activated field will change from where to this field, now only the ball in this field will jump.
                        {
                            cellManager.getActiveCellFrom().getBallAndDoNotRemoveIt().dontJump();// Since the active field will change from where, the old active field needs to tell its ball to stop jumping.
                            insertCommand(String.Concat("BALL ", cellManager.getActiveCellFrom().getRow(), " ", cellManager.getActiveCellFrom().getColumn(), " NESKAKEJ "));// A command that causes the application layer's representation of a ball in a field that is no longer active to stop bouncing.

                            cellManager.setActiveCellFrom(cellWhichWasActivated);// The active field from where to new is set. Actually what happened was that I activated some field earlier, the ball started bouncing in that field. However, I have now activated another field, the field with the coordinates we clicked on last time.
                            
                            insertCommand(String.Concat("BALL ", cellWhichWasActivated.getRow(), " ", cellWhichWasActivated.getColumn(), " SKAKEJ "));// A command that causes the application layer's representation of the ball in this box to bounce.


                        }
                        if ((cellWhichWasActivated.isEmpty()))// If the field is empty.
                        {
                            
                            cellManager.setActiveCellTo(cellWhichWasActivated);// The active where field is set to the new one.
                            PathFinder pathFinder = new PathFinder(cellManager.getActiveCellFrom(),cellManager.getActiveCellTo(),this,this.cellsWhichAreNoMoreActive);// A new path finder will be created.
                            if (pathFinder.search())// If the path finder found a path.
                            { 
                                cellManager.getActiveCellFrom().getBallAndDoNotRemoveIt().dontJump();// A path has been found, the ball will move and therefore a command will be sent to it not to jump anymore.
                                insertCommand(String.Concat("BALL ", cellManager.getActiveCellFrom().getRow(), " ", cellManager.getActiveCellFrom().getColumn(), " NESKAKEJ"));// A command that causes the application layer's representation of a ball in a field that is no longer active to stop bouncing.
                                Ball ballWhichIsBeingMoved=cellManager.getActiveCellFrom().getBallAndRemoveIt();// It is necessary to remove the ball from the field from where we want to move it.
                                cellManager.addEmptyCell(cellManager.getActiveCellFrom());// Then it is necessary to include this field in the registry of empty fields.
                                insertCommand(String.Concat("BALL ", cellManager.getActiveCellFrom().getRow(), " ", cellManager.getActiveCellFrom().getColumn(), " ODSTRANIT"));// We send a change command to the presentation layer.
                                cellManager.getActiveCellTo().setBall(ballWhichIsBeingMoved);// The ball moves to its new field.
                                cellManager.addFullCell(cellManager.getActiveCellTo());// The field we moved the ball to is no longer empty and we need to inform the field manager.

                                insertCommand(String.Concat("BALL ", cellManager.getActiveCellTo().getRow(), " ", cellManager.getActiveCellTo().getColumn(), " NOVY ", ballWhichIsBeingMoved.getType().ToUpper()," NAFOUKNUT"));
                                // We send another change command to the presentation layer.
                                Stack<Cell> cellsVisitedByTheBall =pathFinder.getCellsFromTo();

                                Cell currentCell;
                                while (cellsVisitedByTheBall.Count!=0)
                                {
                                    currentCell=cellsVisitedByTheBall.Pop();
                                    cellsWhichAreNoMoreActive.Push(currentCell);
                                    insertCommand((String.Concat("POLE ", currentCell.getRow(), " ", currentCell.getColumn(), " POZADI ZVYRAZNENE"))); }
                                // This command causes the fields the ball passed through to temporarily darken.
                                explodedBalls.Clear();
                                explodedBalls = ballExploder.checkAndExplodeIfNeeded(cellManager.getActiveCellTo());
                                
                                if (explodedBalls.Count > 1)
                                {
                                    this.scoreManager.countPoints(explodedBalls,this,this.cellManager,this.cellsWhichAreNoMoreActive);

                                }
                                else {
                                    throwBallDuringGameIntoEmptyCells();// Throw the appropriate number of balls into the empty fields.

                                }

                                setGameState(1);// No more waiting for an empty field to be activated. Now we are waiting again for the activation of the field in which the ball is.
                                if (cellManager.hasAtLeastOneEmptyCell())
                                {

                                }
                                else
                                {

                                    setGameState(10);
                                    insertCommand("HRA KONEC");
                                }

                            }
                            else {
                                //MessageBox.Show("I didn't find the way");
                            };
                            // If the path finder did not find a path.

                        }

                    ;
                    }
                    break;
                default: break;

            }
            
        }
        public int getWidth()
        {
            return this.width;
        }
        public int getHeight()
        {
            return this.height;
        }
        
        
    }
}