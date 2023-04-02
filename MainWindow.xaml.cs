using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace Circles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid[,] cell = new Grid[32, 32];// An array property held in a two-dimensional array to identify Grid objects representing a single game field. Thanks to this, it will be possible to identify the field whose properties will be changed.
        private Game game;// One game instance.
        private GameComposition gameComposition = new GameComposition();// An instance of the currently playing game
        private CellCoordination cellCoordination;// A coordinate collector instance to store the coordinates of the box the player clicked on for later use.
        private double widthAndHeightOfEllipse;// A property in which the dynamically calculated diameter of the ball according to the number of rows and columns of the board will be stored.
        private bool fullScreenMode = false;
        // The definition of the brushes of the respective colors begins.
        System.Windows.Media.SolidColorBrush lightGreenBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
        System.Windows.Media.SolidColorBrush redBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
        System.Windows.Media.SolidColorBrush darkBlueBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
        System.Windows.Media.SolidColorBrush yellowBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
        System.Windows.Media.SolidColorBrush lightBlueBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Cyan);
        System.Windows.Media.SolidColorBrush purpleBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.BlueViolet);
        System.Windows.Media.SolidColorBrush brownBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Maroon);
        System.Windows.Media.SolidColorBrush pinkBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Pink);
        System.Windows.Media.SolidColorBrush greenBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.ForestGreen);
        System.Windows.Media.SolidColorBrush goldBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gold);
        System.Windows.Media.SolidColorBrush orangeBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkOrange);
        System.Windows.Media.SolidColorBrush whiteBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
        System.Windows.Media.SolidColorBrush greyBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
        System.Windows.Media.SolidColorBrush blackBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
        System.Windows.Media.SolidColorBrush blueBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DodgerBlue);
        System.Windows.Media.SolidColorBrush armyGreenBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Olive);
        
        LinearGradientBrush lightGreenLinearGradientBrush= new LinearGradientBrush();
        LinearGradientBrush redLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush darkBlueLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush yellowLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush lightBlueLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush purpleLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush brownLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush pinkLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush greenLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush goldLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush orangeLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush whiteLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush greyLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush blackLinearGradientBrush = new LinearGradientBrush();
        LinearGradientBrush blue = new LinearGradientBrush();
        LinearGradientBrush armyGreenLinearGradientBrush = new LinearGradientBrush();

        LinearGradientBrush jokerLinearGradientBrush = new LinearGradientBrush();

        System.Windows.Media.SolidColorBrush cellBackgroundBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Silver);
        System.Windows.Media.SolidColorBrush activatedCellBackgroundBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGray);
        System.Windows.Media.SolidColorBrush activatedCellBackgroundRedBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.IndianRed);

        private void buildLinearGradientBrushes()
        {
            System.Windows.Media.Color[] lightColours = {
                Colors.LawnGreen,
                Colors.Red,
                Colors.Blue,
                Colors.Yellow,
                Colors.Cyan,
                Colors.BlueViolet,
                Colors.SaddleBrown,
                Colors.Pink,
                Colors.ForestGreen,
                Colors.Gold,
                Colors.DarkOrange,
                Colors.White,
                Colors.Gray,
                Color.FromRgb(95,95,95),
                Colors.DeepSkyBlue,
                Colors.Olive
                
            };
            System.Windows.Media.Color[] darkColours = {
                Colors.Green,
                Colors.DarkRed,
                Colors.DarkBlue,
                Colors.Gold,
                Colors.DarkCyan,
                Colors.DarkViolet,
                Colors.Maroon,
                Colors.Plum,
                Colors.DarkGreen,
                Colors.Goldenrod,
                Colors.Chocolate,
                Colors.LightGray,
                Colors.DimGray,
                Color.FromRgb(32,32,32),
                Colors.DodgerBlue,
                Colors.DarkOliveGreen
            };
            LinearGradientBrush[] linearGradientBrushes = {
            lightGreenLinearGradientBrush,
            redLinearGradientBrush,
            darkBlueLinearGradientBrush,
            yellowLinearGradientBrush,
            lightBlueLinearGradientBrush,
            purpleLinearGradientBrush,
            brownLinearGradientBrush,
            pinkLinearGradientBrush,
            greenLinearGradientBrush,
            goldLinearGradientBrush,
            orangeLinearGradientBrush,
            whiteLinearGradientBrush,
            greyLinearGradientBrush,
            blackLinearGradientBrush,
            blue,
            armyGreenLinearGradientBrush
        };
            for (int i=0; i<=15;i++)
            { changeLinearGradientBrush(linearGradientBrushes[i],lightColours[i],darkColours[i]); }
            NastavStetecSDuhouSLinearnimPrechodem();        }
        private void changeLinearGradientBrush(LinearGradientBrush linearGradientBrush, System.Windows.Media.Color Color1, System.Windows.Media.Color Color2)
        {
            GradientStopCollection linearStops = new GradientStopCollection();

            GradientStop Stop1 = new GradientStop( Color1, 0.0);
            GradientStop Stop2 = new GradientStop(Color1, 0.1);
            GradientStop Stop3 = new GradientStop(Color2, 0.9);
            GradientStop Stop4 = new GradientStop(Color2, 1);

            linearStops.Add(Stop1);
            linearStops.Add(Stop2);
            linearStops.Add(Stop3);
            linearStops.Add(Stop4);

            linearGradientBrush.StartPoint = new Point(0.5, 0.1);
            linearGradientBrush.EndPoint = new Point(0.5, 0.9);

            linearGradientBrush.GradientStops = linearStops;
        }
        private void NastavStetecSDuhouSLinearnimPrechodem()
        {
            GradientStopCollection linearStops = new GradientStopCollection();

            GradientStop Stop1 = new GradientStop(Colors.Red, ((double)1 / 6) * 0);
            GradientStop Stop2 = new GradientStop(Colors.Orange, ((double)1 / 6) * 1);
            GradientStop Stop3 = new GradientStop(Colors.Yellow, ((double)1 / 6) * 2);
            GradientStop Stop4 = new GradientStop(Colors.Green, ((double)1 / 6) * 3);
            GradientStop Stop5 = new GradientStop(Colors.Blue, ((double)1 / 6) * 4);
            GradientStop Stop6 = new GradientStop(Colors.Purple, ((double)1 / 6) * 5);
            GradientStop Stop7 = new GradientStop(Colors.Magenta, ((double)1 / 6) * 6);
            
            linearStops.Add(Stop1);
            linearStops.Add(Stop2);
            linearStops.Add(Stop3);
            linearStops.Add(Stop4);
            linearStops.Add(Stop5);
            linearStops.Add(Stop6);
            linearStops.Add(Stop7);

            jokerLinearGradientBrush.StartPoint = new Point(0.5, 0.0);
            jokerLinearGradientBrush.EndPoint = new Point(0.5, 1);

            jokerLinearGradientBrush.GradientStops = linearStops;
        }
        public MainWindow()
        {
            buildLinearGradientBrushes();
            InitializeComponent();
            newGame();// A new game will start when the window is initialized.
        }

        private void newGame()// This method is executed every time a new game starts.
        {
            resetEnvironment();// The window must be cleaned of everything left in the window after the new game
            game = new Game(
        this.gameComposition,
        gameComposition.getHeight(),
        gameComposition.getWidth(),
        gameComposition.getLightGreen(),
        gameComposition.isRed(),
        gameComposition.isDarkBlue(),
        gameComposition.isYellow(),
        gameComposition.isLightBlue(),
        gameComposition.isPurple(),
        gameComposition.isBrown(),
        gameComposition.isPink(),
        gameComposition.isGreen(),
        gameComposition.isGold(),
        gameComposition.isOrange(),
        gameComposition.isWhite(),
        gameComposition.isGrey(),
        gameComposition.isBlack(),
        gameComposition.isBlue(),
        gameComposition.isArmyGreen(),
        gameComposition.getStartBallCount(),
        gameComposition.getNextBallCount(),
        gameComposition.isJokerBalls(),
        gameComposition.isDoubleScoreBalls(),
        gameComposition.getShape(),
        gameComposition.getMinLineLength()

                );// Creates a game according to the properties of the Game assembly window property.
            int boardWidth = 520;
            double whatWillBeTheRatioOfTheDiameterOfTheCircleToTheLengthOfTheField = 0.75;
            this.widthAndHeightOfEllipse = whatWillBeTheRatioOfTheDiameterOfTheCircleToTheLengthOfTheField * boardWidth / Math.Max(game.getHeight(), game.getWidth());// Here a value will be calculated and stored which will mean the width and height of the ball which will be calculated dynamically based on the height and width of the board

            executeCommands();// What the logic layer has calculated will now be reflected in the application layer
        }
        public String executeCommand()// Executes one command in the command queue from the logical layer
        {
            String command = game.getCommand();// Takes one command from the in-game command queue.
            //MessageBox.Show(command);
            string[] commandParts = command.Split(' ');// Here, the command is divided by spaces into individual parameters
            switch (commandParts[0])// The 0th parameter is examined here
            {
                case "HRA":
                    switch (commandParts[1])// The 1st parameter is examined here
                    {
                        case "NOVA":
                            { resetEnvironment(); }
                            break;

                        case "KONEC":
                            EnterYourNameWindow enterYourNameWindow = new EnterYourNameWindow(this.game);
                            enterYourNameWindow.ShowDialog();// Displays a dialog for entering the player's name.

                            newGame();// Game over, results saved, now a new game begins
                            break;
                        default:
                            ;
                            break;
                    }
                    break;

                case "DESKA":
                    {
                        buildBoard(Int32.Parse(commandParts[1]), Int32.Parse(commandParts[2]));

                    };
                    break;
                case "DNO":
                    {

                    };
                    break;
                case "MIC":
                    {
                        switch (commandParts[3])// The 3rd parameter is examined here
                        {
                            case "NOVY":
                                {
                                    createNewBall(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2]), commandParts[4], commandParts[5]);
                                }
                                break;
                            case "SKAKEJ":
                                ballStartJumping(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2]));
                                break;
                            case "NESKAKEJ":
                                ballStopJumping(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2])); break;
                            case "ODSTRANIT":
                                clearCell(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2])); break;
                            case "DALSI1":
                                {
                                    switch(Convert.ToInt32(commandParts[2]))
                                    {
                                        case 1: nextBall1.Fill = lightGreenLinearGradientBrush; break;
                                        case 2: nextBall1.Fill = redLinearGradientBrush; break;
                                        case 3: nextBall1.Fill = darkBlueLinearGradientBrush; break;
                                        case 4: nextBall1.Fill = yellowLinearGradientBrush; break;
                                        case 5: nextBall1.Fill = lightBlueLinearGradientBrush; break;
                                        case 6: nextBall1.Fill = purpleLinearGradientBrush; break;
                                        case 7: nextBall1.Fill = brownLinearGradientBrush; break;
                                    }
                                }
                                 break;
                            case "DALSI2":
                                switch (Convert.ToInt32(commandParts[2]))
                                {
                                    case 1: nextBall2.Fill = lightGreenLinearGradientBrush; break;
                                    case 2: nextBall2.Fill = redLinearGradientBrush; break;
                                    case 3: nextBall2.Fill = darkBlueLinearGradientBrush; break;
                                    case 4: nextBall2.Fill = yellowLinearGradientBrush; break;
                                    case 5: nextBall2.Fill = lightBlueLinearGradientBrush; break;
                                    case 6: nextBall2.Fill = purpleLinearGradientBrush; break;
                                    case 7: nextBall2.Fill = brownLinearGradientBrush; break;
                                }
                                break;
                            case "DALSI3":
                                switch (Convert.ToInt32(commandParts[2]))
                                {
                                    case 1: nextBall3.Fill = lightGreenLinearGradientBrush; break;
                                    case 2: nextBall3.Fill = redLinearGradientBrush; break;
                                    case 3: nextBall3.Fill = darkBlueLinearGradientBrush; break;
                                    case 4: nextBall3.Fill = yellowLinearGradientBrush; break;
                                    case 5: nextBall3.Fill = lightBlueLinearGradientBrush; break;
                                    case 6: nextBall3.Fill = purpleLinearGradientBrush; break;
                                    case 7: nextBall3.Fill = brownLinearGradientBrush; break;
                                }
                                break;
                            default: ;break;
                        }
                    };
                    break;
                case "POLE":
                    {
                        switch (commandParts[3])// The 3rd parameter is examined here
                        {
                            case "POZADI":
                                {
                                    switch (commandParts[4])
                                    {
                                        case "ZVYRAZNENE": { highlightCell(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2])); }; break;
                                        case "NEZVYRAZNENE": { stopHighlightingCell(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2])); }; break;
                                        case "CERVENE": { highlightCellWithRedBackground(Convert.ToInt32(commandParts[1]), Convert.ToInt32(commandParts[2])); }; break;
                                        default: { }; break;
                                    }
                                }; break;
                        }
                    }; break;
                case "VYSLEDEK":
                    { changeScore(Convert.ToInt32(commandParts[1])); }
                    break;
                default:
                    MessageBox.Show("Error: The logic layer generated a change that the graphics layer does not understand"); ;
                    break;

            }
            return command;
        }
        /*
        -------------------------------------------------------------------------------
        It begins with a description of the command syntax of the special language generated by the logic layer.
        -------------------------------------------------------------------------------
        By what the logic layer generates, it informs others about what elements have changed and how.
        Command words are capitalized.
        What is in the angle brackets must be replaced with a specific number or text string.
        Strings are written without double or single quotes.
        Example: The following command displays a new black ball in the 4th row and 6th column field by applying the inflate effect.
        MIC 4 6 NOVY CERNY NAFOUKNOUT
        ------------------------------------------------------------------------------

        Command:   HRA NOVA
        Description:    The presentation layer must, at the behest of this command, prepare the presentation layer for the new game and delete everything left over from the last game.

        Command:   HRA KONEC
        Description:    The presentation layer must, at the behest of this command, prepare the presentation layer for operations performed after the game is finished.

        Command:   DESKA <výška> <šířka> 
        Description:    This command must immediately follow the GAME NOVA command. According to this command, the presentation layer builds a board with the given dimensions.

        Command:   DNO
        Description:    This command is not generated by the logic layer, this command is generated by the presentation layer automatically if all the commands in the command queue have already been executed and this queue is now empty. The application layer is informed by this command that the current form of the presentation layer is the same as how the elements of the game are represented in the logic layer.

        Command:   MIC <řádek> <sloupec> NOVY <typ> NAFOUKNOUT
        Description:    A new ball of the given type will appear in the board at the given position and will be inflated, meaning that it will be small at first and gradually increase to its final size.

        Command:   MIC <řádek> <sloupec> NOVY <typ> 
        Description:    A new ball of the given type will appear in the board at the given position and will not be inflated, meaning it will appear already inflated to its final form.

        Command:   MIC <řádek> <sloupec> SKAKEJ
        Description:    The given ball starts to bounce.

        Command:   MIC <řádek> <sloupec> NESKAKEJ
        Description:    The given ball stops bouncing.

        Command:   MIC <řádek> <sloupec> ODSTRANIT
        Description:    The given ball will be removed.
            
        Command:   POLE <řádek> <sloupec> POZADI ZVYRAZNENE
        Description:    The given field will be highlighted.

        Command:   POLE <řádek> <sloupec> POZADI NEZVYRAZNENE
        Description:    The given field will be unhighlighted.

        Command:   POLE <řádek> <sloupec> POZADI CERVENE
        Description:    The given field will be highlighted in red after the explosion.

        Command:   VYSLEDEK <počet bodů>
        Description:    It informs that the number of points of the current result has changed.

        ------------------------------------------------------------------------------
        The description of the command syntax of the special language generated by the logic layer ends.
        ------------------------------------------------------------------------------
        */


        public void executeCommands() // Executes all commands from the command queue.
        {
            bool shouldIContinue = true;
            do
            {// Do.
                if (executeCommand() == "DNO") { shouldIContinue = false; };
            }// If the command in the command queue is not DNO, do not change the value of the continue variable, otherwise set it to true.
            while (shouldIContinue);// As long as the value of the variable continues to be true.

        }
        private void highlightCell(int row, int column)// Sets the background of the given field in the presentation layer.
        {
            cell[row - 1, column - 1].Background = activatedCellBackgroundBrush;
        }
        private void stopHighlightingCell(int row, int column)// Sets the background of the given field in the presentation layer.
        {
            cell[row - 1, column - 1].Background = cellBackgroundBrush;
        }
        private void highlightCellWithRedBackground(int row, int column)// Sets the background of the given field in the presentation layer.
        {
            cell[row - 1, column - 1].Background = activatedCellBackgroundRedBrush;
        }
        public void createNewBall(int row, int column, String type, String effect)// Creates in the presentation layer a new ball in the given field of the given type and with the given effect.
                                                                                  // Always before starting a new game, this method resets the graphical environment to its default form. In other words, it cleans up what was left after the previous game.
        {
            Ellipse newBall = new Ellipse();
            newBall.Height = widthAndHeightOfEllipse;
            newBall.Width = widthAndHeightOfEllipse;
            TextBlock popisekTextBlock = new TextBlock { Text = "2×", FontSize = 24, VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center };// If the ball is doubling, it will have this label in front of it.
            switch (type)// Depending on the type of ball, the corresponding ball is set.
            {
                case "SVETLEZELENA":
                    {
                        newBall.Fill = lightGreenLinearGradientBrush;
                    }; break;
                case "CERVENA":
                    {
                        newBall.Fill = redLinearGradientBrush;
                    }; break;
                case "TMAVEMODRA":
                    {
                        newBall.Fill = darkBlueLinearGradientBrush;
                    }; break;
                case "ZLUTA":
                    {
                        newBall.Fill = yellowLinearGradientBrush;
                    }; break;
                case "SVETLEMODRA":
                    {
                        newBall.Fill = lightBlueLinearGradientBrush;
                    }; break;
                case "FIALOVA":
                    {
                        newBall.Fill = purpleLinearGradientBrush;
                    }; break;
                case "HNEDA":
                    {
                        newBall.Fill = brownLinearGradientBrush;
                    }; break;
                case "RUZOVA":
                    {
                        newBall.Fill = pinkLinearGradientBrush;
                    }; break;
                case "ZELENA":
                    {
                        newBall.Fill = greenLinearGradientBrush;
                    }; break;
                case "ZLATA":
                    {
                        newBall.Fill = goldLinearGradientBrush;
                    }; break;
                case "ORANZOVA":
                    {
                        newBall.Fill = orangeLinearGradientBrush;
                    }; break;
                case "BILA":
                    {
                        newBall.Fill = whiteLinearGradientBrush;
                    }; break;
                case "SEDIVA":
                    {
                        newBall.Fill = greyLinearGradientBrush;
                    }; break;
                case "CERNA":
                    {
                        newBall.Fill = blackLinearGradientBrush;
                    }; break;
                case "MODRA":
                    {
                        newBall.Fill = blue;
                    }; break;
                case "VOJENSKAZELENA":
                    {
                        newBall.Fill = armyGreenLinearGradientBrush;
                    }; break;
                case "DUHOVE":
                    {
                        newBall.Fill = jokerLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICISVETLEZELENA":
                    {
                        newBall.Fill = lightGreenLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICICERVENA":
                    {
                        newBall.Fill = redLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICITMAVEMODRA":
                    {
                        newBall.Fill = darkBlueLinearGradientBrush;
                        popisekTextBlock.Foreground = whiteBrush;// Here the text color must be set to something other than black to show that the ball is doubling.
                    }; break;
                case "ZDVOJNASOBUJICIZLUTA":
                    {
                        newBall.Fill = yellowLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICISVETLEMODRA":
                    {
                        newBall.Fill = lightBlueLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIFIALOVA":
                    {
                        newBall.Fill = purpleLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIHNEDA":
                    {
                        newBall.Fill = brownLinearGradientBrush;
                        popisekTextBlock.Foreground = whiteBrush;// Here the text color must be set to something other than black to show that the ball is doubling.
                    }; break;
                case "ZDVOJNASOBUJICIRUZOVA":
                    {
                        newBall.Fill = pinkLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIZELENA":
                    {
                        newBall.Fill = greenLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIZLATA":
                    {
                        newBall.Fill = goldLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIORANZOVA":
                    {
                        newBall.Fill = orangeLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICIBILA":
                    {
                        newBall.Fill = whiteLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICISEDIVA":
                    {
                        newBall.Fill = greyLinearGradientBrush;
                    }; break;
                case "ZDVOJNASOBUJICICERNA":
                    {
                        newBall.Fill = blackLinearGradientBrush;
                        popisekTextBlock.Foreground = whiteBrush;// Here the text color must be set to something other than black to show that the ball is doubling.
                    }; break;
                case "ZDVOJNASOBUJICIMODRA":
                    {
                        newBall.Fill = blue;
                    }; break;
                case "ZDVOJNASOBUJICIVOJENSKAZELENA":
                    {
                        newBall.Fill = armyGreenLinearGradientBrush;
                    }; break;
                default:
                    {
                        newBall.Fill = redBrush;
                    }; break;
            }
            if (effect == "NAFOUKNOUT")// If the ball appeared, it does not move, it needs to be inflated
            {
                newBall.Height = (int)(widthAndHeightOfEllipse / 2.5);// First, the ball appears deflated.
                newBall.Width = (int)(widthAndHeightOfEllipse / 2.5);// The width is the same as the height. The width and height only change if the ball bounces, here the ball doesn't bounce.

            }
            cell[row - 1, column - 1].Children.Add(newBall);// The given ball is added to the appropriate box.
            if (type.Contains("ZDVOJNASOBUJICI")) { cell[row - 1, column - 1].Children.Add(popisekTextBlock); };// If the ball is doubling, it will have this label in front of it.

            if (effect == "NAFOUKNOUT")// If the ball appeared, it does not move, it needs to be inflated
            {
                double ratioOfBallDiameterBeforeInflationToDiameterAfterInflation = 2.5;
                double duration = 0.25;
                DoubleAnimation inflationAnimation = new DoubleAnimation();
                inflationAnimation.From = (int)(widthAndHeightOfEllipse / ratioOfBallDiameterBeforeInflationToDiameterAfterInflation);
                inflationAnimation.To = widthAndHeightOfEllipse;
                inflationAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
                inflationAnimation.AutoReverse = false;// The animation will not repeat
                newBall.BeginAnimation(Ellipse.HeightProperty, inflationAnimation);
                newBall.BeginAnimation(Ellipse.WidthProperty, inflationAnimation);
            }

        }
        private void ballStartJumping(int row, int column)// The purpose of this method is to turn on the animation of the ellipse creating a jumping effect.
        {
            Ellipse jumpingBall = cell[row - 1, column - 1].Children.OfType<Ellipse>().FirstOrDefault();
            double durationOfAnimation = 300;
            double minimalAverageOfBall = 0.8;
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = widthAndHeightOfEllipse * minimalAverageOfBall;
            heightAnimation.To = widthAndHeightOfEllipse;
            heightAnimation.RepeatBehavior = RepeatBehavior.Forever;
            heightAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationOfAnimation));
            heightAnimation.AutoReverse = true;

            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = widthAndHeightOfEllipse;
            widthAnimation.To = widthAndHeightOfEllipse * minimalAverageOfBall;
            widthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            widthAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationOfAnimation));
            widthAnimation.AutoReverse = true;

            double valueFrom = widthAndHeightOfEllipse * 0.2;
            double valueTo = -2;
            TranslateTransform jumpTransform = new TranslateTransform();
            jumpingBall.RenderTransform = jumpTransform;
            DoubleAnimation jumpAnimation = new DoubleAnimation(valueFrom, valueTo, TimeSpan.FromMilliseconds(durationOfAnimation));
            jumpAnimation.RepeatBehavior = RepeatBehavior.Forever;
            jumpAnimation.AutoReverse = true;

            jumpTransform.BeginAnimation(TranslateTransform.YProperty, jumpAnimation);
            jumpingBall.BeginAnimation(Ellipse.HeightProperty, heightAnimation);
            jumpingBall.BeginAnimation(Ellipse.WidthProperty, widthAnimation);
        }
        private void ballStopJumping(int row, int column)
        {
            Ellipse jumpingBall = cell[row - 1, column - 1].Children.OfType<Ellipse>().FirstOrDefault();
            jumpingBall.RenderTransform = null;
            jumpingBall.BeginAnimation(Ellipse.HeightProperty, null);
            jumpingBall.BeginAnimation(Ellipse.WidthProperty, null);
            jumpingBall.Height = widthAndHeightOfEllipse;
            jumpingBall.Width = widthAndHeightOfEllipse;
        }
        private void clearCell(int row, int column)// Removes all children of this array.
        {
            cell[row - 1, column - 1].Children.Clear();
        }

        public void resetEnvironment()// Always before starting a new game, this method resets the graphical environment to its default form. In other words, it cleans up what was left after the previous game.
        {
            boardGrid.Children.Clear();
            boardGrid.ColumnDefinitions.Clear();
            boardGrid.RowDefinitions.Clear();
            scoreLabel.Content = 0;// The result is set to zero.
        }
        public void changeScore(int currentScore) // Sets the result to the current form.
        { scoreLabel.Content = currentScore; }

        public void buildBoard(int height, int width)// Assembles the board array in the application layer into a Grid control named Board
        {
            int maxFromHeightAndWidth = Math.Max(height, width);
            // start: board assembly
            for (int i = 0; i <= maxFromHeightAndWidth - 1; i++) // It starts adding columns and rows.
            {
                ColumnDefinition columnDefinition1 = new ColumnDefinition();
                boardGrid.ColumnDefinitions.Add(columnDefinition1);

                RowDefinition rowDefinition1 = new RowDefinition();
                boardGrid.RowDefinitions.Add(rowDefinition1);
            };
            // end: build the board
            // Starts adding rows and columns identified by row and column
            for (int r = 0; r <= maxFromHeightAndWidth - 1; r++)
            {

                for (int s = 0; s <= maxFromHeightAndWidth - 1; s++)
                {

                    cell[r, s] = new Grid();
                    boardGrid.Children.Add(cell[r, s]);
                    Grid.SetRow(cell[r, s], r);
                    Grid.SetColumn(cell[r, s], s);
                    // It starts adding handlers to fields in the application layer, when the player clicks on a field, that field informs the logic layer that it has been activated and uses the field's row and column coordinates to identify itself.
                    switch (s + 1)
                    {
                        case 1: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn1)); } break;
                        case 2: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn2)); } break;
                        case 3: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn3)); } break;
                        case 4: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn4)); } break;
                        case 5: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn5)); } break;
                        case 6: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn6)); } break;
                        case 7: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn7)); } break;
                        case 8: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn8)); } break;
                        case 9: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn9)); } break;
                        case 10: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn10)); } break;
                        case 11: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn11)); } break;
                        case 12: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn12)); } break;
                        case 13: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn13)); } break;
                        case 14: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn14)); } break;
                        case 15: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn15)); } break;
                        case 16: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn16)); } break;
                        case 17: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn17)); } break;
                        case 18: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn18)); } break;
                        case 19: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn19)); } break;
                        case 20: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn20)); } break;
                        case 21: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn21)); } break;
                        case 22: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn22)); } break;
                        case 23: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn23)); } break;
                        case 24: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn24)); } break;
                        case 25: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn25)); } break;
                        case 26: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn26)); } break;
                        case 27: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn27)); } break;
                        case 28: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn28)); } break;
                        case 29: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn29)); } break;
                        case 30: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn30)); } break;
                        case 31: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn31)); } break;
                        case 32: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtColumn32)); } break;
                        default: { } break;
                    };
                    switch (r + 1)
                    {
                        case 1: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow1)); } break;
                        case 2: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow2)); } break;
                        case 3: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow3)); } break;
                        case 4: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow4)); } break;
                        case 5: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow5)); } break;
                        case 6: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow6)); } break;
                        case 7: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow7)); } break;
                        case 8: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow8)); } break;
                        case 9: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow9)); } break;
                        case 10: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow10)); } break;
                        case 11: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow11)); } break;
                        case 12: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow12)); } break;
                        case 13: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow13)); } break;
                        case 14: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow14)); } break;
                        case 15: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow15)); } break;
                        case 16: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow16)); } break;
                        case 17: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow17)); } break;
                        case 18: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow18)); } break;
                        case 19: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow19)); } break;
                        case 20: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow20)); } break;
                        case 21: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow21)); } break;
                        case 22: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow22)); } break;
                        case 23: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow23)); } break;
                        case 24: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow24)); } break;
                        case 25: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow25)); } break;
                        case 26: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow26)); } break;
                        case 27: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow27)); } break;
                        case 28: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow28)); } break;
                        case 29: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow29)); } break;
                        case 30: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow30)); } break;
                        case 31: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow31)); } break;
                        case 32: { cell[r, s].AddHandler(Grid.MouseLeftButtonDownEvent, new RoutedEventHandler(ClickedAtRow32)); } break;
                        default: { } break;
                    };

                    cell[r, s].Margin = new Thickness(1);// The length of the outer distance of the field space is set.
                    if ((r <= height - 1) && (s <= width - 1))// If the square is outside the squares to be played with,
                    { cell[r, s].Background = cellBackgroundBrush; }
                    else
                    { cell[r, s].Visibility = Visibility.Collapsed; }// so the visibility of the field is set to collapsed.

                };
            };
            // end: filling the field plate

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void switchToFullScreenMode()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            fullscreenModeMenuItem.Header = "Window Mode";
        }
        private void switchToWindowMode()
        {
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.CanResizeWithGrip;
            fullscreenModeMenuItem.Header = "Fullscreen mode";
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (fullScreenMode)
            {
                switchToWindowMode();
            }
            else

            {
                switchToFullScreenMode();
            }
            fullScreenMode = !fullScreenMode;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)// A method that ensures that something is done when certain keys are pressed.
        {
            switch (e.Key)
            {
                case Key.F1:
                    {
                        HelpWindow helpWindow = new HelpWindow();
                        helpWindow.Show();
                    }; break;
                case Key.F4: { newGameWithDefaultGameCompositionMenuItem_Click(sender, e); }; break;
                case Key.F5: { MenuItem_Click_5(sender, e); }; break;
                case Key.F6: { newGameMenuItem_Click(sender, e); }; break;
                case Key.F8: { MenuItem_Click_4(sender, e); }; break;
                case Key.F11: { MenuItem_Click(sender, e); }; break;
            }
        }

        private void newGameMenuItem_Click(object sender, RoutedEventArgs e)// A window will open where the player can set the game to play.
        {
            NewGameWithCustomGameCompositionWindow newGameWithCustomGameCompositionWindow = new NewGameWithCustomGameCompositionWindow(this.gameComposition);
            newGameWithCustomGameCompositionWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();// Terminates the Circles application completely.
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        //Click events on a certain column or row are triggered, these events send the coordinates to the coordinate collector. When all the coordinates are sent to the coordinate collector, then the following information is sent to the logic layer that a certain field has been activated
        private void setColumnCoordination(int columnNumber)
        {
            cellCoordination = new CellCoordination();
            cellCoordination.setColumn(columnNumber);
        }
        private void ClickedAtColumn1(object sender, RoutedEventArgs e) { setColumnCoordination(1); }
        private void ClickedAtColumn2(object sender, RoutedEventArgs e) { setColumnCoordination(2); }
        private void ClickedAtColumn3(object sender, RoutedEventArgs e) { setColumnCoordination(3); }
        private void ClickedAtColumn4(object sender, RoutedEventArgs e) { setColumnCoordination(4); }
        private void ClickedAtColumn5(object sender, RoutedEventArgs e) { setColumnCoordination(5); }
        private void ClickedAtColumn6(object sender, RoutedEventArgs e) { setColumnCoordination(6); }
        private void ClickedAtColumn7(object sender, RoutedEventArgs e) { setColumnCoordination(7); }
        private void ClickedAtColumn8(object sender, RoutedEventArgs e) { setColumnCoordination(8); }
        private void ClickedAtColumn9(object sender, RoutedEventArgs e) { setColumnCoordination(9); }
        private void ClickedAtColumn10(object sender, RoutedEventArgs e) { setColumnCoordination(10); }
        private void ClickedAtColumn11(object sender, RoutedEventArgs e) { setColumnCoordination(11); }
        private void ClickedAtColumn12(object sender, RoutedEventArgs e) { setColumnCoordination(12); }
        private void ClickedAtColumn13(object sender, RoutedEventArgs e) { setColumnCoordination(13); }
        private void ClickedAtColumn14(object sender, RoutedEventArgs e) { setColumnCoordination(14); }
        private void ClickedAtColumn15(object sender, RoutedEventArgs e) { setColumnCoordination(15); }
        private void ClickedAtColumn16(object sender, RoutedEventArgs e) { setColumnCoordination(16); }
        private void ClickedAtColumn17(object sender, RoutedEventArgs e) { setColumnCoordination(17); }
        private void ClickedAtColumn18(object sender, RoutedEventArgs e) { setColumnCoordination(18); }
        private void ClickedAtColumn19(object sender, RoutedEventArgs e) { setColumnCoordination(19); }
        private void ClickedAtColumn20(object sender, RoutedEventArgs e) { setColumnCoordination(20); }
        private void ClickedAtColumn21(object sender, RoutedEventArgs e) { setColumnCoordination(21); }
        private void ClickedAtColumn22(object sender, RoutedEventArgs e) { setColumnCoordination(22); }
        private void ClickedAtColumn23(object sender, RoutedEventArgs e) { setColumnCoordination(23); }
        private void ClickedAtColumn24(object sender, RoutedEventArgs e) { setColumnCoordination(24); }
        private void ClickedAtColumn25(object sender, RoutedEventArgs e) { setColumnCoordination(25); }
        private void ClickedAtColumn26(object sender, RoutedEventArgs e) { setColumnCoordination(26); }
        private void ClickedAtColumn27(object sender, RoutedEventArgs e) { setColumnCoordination(27); }
        private void ClickedAtColumn28(object sender, RoutedEventArgs e) { setColumnCoordination(28); }
        private void ClickedAtColumn29(object sender, RoutedEventArgs e) { setColumnCoordination(29); }
        private void ClickedAtColumn30(object sender, RoutedEventArgs e) { setColumnCoordination(30); }
        private void ClickedAtColumn31(object sender, RoutedEventArgs e) { setColumnCoordination(31); }
        private void ClickedAtColumn32(object sender, RoutedEventArgs e) { setColumnCoordination(32); }


        private void setRowCoordination(int rowNumber)
        {
            cellCoordination.setRow(rowNumber);
            game.activateCell(cellCoordination.getRow(), cellCoordination.getColumn());
            executeCommands();
        }

        private void ClickedAtRow1(object sender, RoutedEventArgs e) { setRowCoordination(1); }
        private void ClickedAtRow2(object sender, RoutedEventArgs e) { setRowCoordination(2); }
        private void ClickedAtRow3(object sender, RoutedEventArgs e) { setRowCoordination(3); }
        private void ClickedAtRow4(object sender, RoutedEventArgs e) { setRowCoordination(4); }
        private void ClickedAtRow5(object sender, RoutedEventArgs e) { setRowCoordination(5); }
        private void ClickedAtRow6(object sender, RoutedEventArgs e) { setRowCoordination(6); }
        private void ClickedAtRow7(object sender, RoutedEventArgs e) { setRowCoordination(7); }
        private void ClickedAtRow8(object sender, RoutedEventArgs e) { setRowCoordination(8); }
        private void ClickedAtRow9(object sender, RoutedEventArgs e) { setRowCoordination(9); }
        private void ClickedAtRow10(object sender, RoutedEventArgs e) { setRowCoordination(10); }
        private void ClickedAtRow11(object sender, RoutedEventArgs e) { setRowCoordination(11); }
        private void ClickedAtRow12(object sender, RoutedEventArgs e) { setRowCoordination(12); }
        private void ClickedAtRow13(object sender, RoutedEventArgs e) { setRowCoordination(13); }
        private void ClickedAtRow14(object sender, RoutedEventArgs e) { setRowCoordination(14); }
        private void ClickedAtRow15(object sender, RoutedEventArgs e) { setRowCoordination(15); }
        private void ClickedAtRow16(object sender, RoutedEventArgs e) { setRowCoordination(16); }
        private void ClickedAtRow17(object sender, RoutedEventArgs e) { setRowCoordination(17); }
        private void ClickedAtRow18(object sender, RoutedEventArgs e) { setRowCoordination(18); }
        private void ClickedAtRow19(object sender, RoutedEventArgs e) { setRowCoordination(19); }
        private void ClickedAtRow20(object sender, RoutedEventArgs e) { setRowCoordination(20); }
        private void ClickedAtRow21(object sender, RoutedEventArgs e) { setRowCoordination(21); }
        private void ClickedAtRow22(object sender, RoutedEventArgs e) { setRowCoordination(22); }
        private void ClickedAtRow23(object sender, RoutedEventArgs e) { setRowCoordination(23); }
        private void ClickedAtRow24(object sender, RoutedEventArgs e) { setRowCoordination(24); }
        private void ClickedAtRow25(object sender, RoutedEventArgs e) { setRowCoordination(25); }
        private void ClickedAtRow26(object sender, RoutedEventArgs e) { setRowCoordination(26); }
        private void ClickedAtRow27(object sender, RoutedEventArgs e) { setRowCoordination(27); }
        private void ClickedAtRow28(object sender, RoutedEventArgs e) { setRowCoordination(28); }
        private void ClickedAtRow29(object sender, RoutedEventArgs e) { setRowCoordination(29); }
        private void ClickedAtRow30(object sender, RoutedEventArgs e) { setRowCoordination(30); }
        private void ClickedAtRow31(object sender, RoutedEventArgs e) { setRowCoordination(31); }
        private void ClickedAtRow32(object sender, RoutedEventArgs e) { setRowCoordination(32); }
        
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)// Will open the About window.
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)// Will open the Help window.
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void newGameWithDefaultGameCompositionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        private void MainWindow_Window_Activated(object sender, EventArgs e)// If the game setup was changed after the window was activated, a new game will be started with this setup.
        {
            if (gameComposition.isChanged())
            {
                gameComposition.setChanged(false);
                newGame();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            InformationAboutCurrentGameWindow informationAboutCurrentGameWindow = new InformationAboutCurrentGameWindow(this.gameComposition);
            informationAboutCurrentGameWindow.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            ScoreListWindow scoreListWindow = new ScoreListWindow(this.game, this.gameComposition);
            scoreListWindow.ShowDialog();
        }
        
    }
}