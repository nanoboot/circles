using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Circles
{
    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWithCustomGameCompositionWindow : Window
    {
        private GameComposition customGameComposition;
        private String shapeOfGroupOfBallsWhichWillExplode = "linka";
        public NewGameWithCustomGameCompositionWindow(GameComposition sestavaHry)
        {
            InitializeComponent();
            this.customGameComposition = sestavaHry;
        }

        public void setDefaultValues()// This method overrides all values in the form to their default values.
        {

            heightComboBox.SelectedIndex = 6;
            widthComboBox.SelectedIndex = 6;
            lightGreenCheckBox.IsChecked = true;
            redCheckBox.IsChecked = true;
            darkBlueCheckBox.IsChecked = true;
            yellowCheckBox.IsChecked = true;
            lightBlueCheckBox.IsChecked = true;
            purpleCheckBox.IsChecked = true;
            brownCheckBox.IsChecked = true;
            pinkCheckBox.IsChecked = false;
            greenCheckBox.IsChecked = false;
            goldCheckBox.IsChecked = false;
            orangeCheckBox.IsChecked = false;
            whiteCheckBox.IsChecked = false;
            greyCheckBox.IsChecked = false;
            blackCheckBox.IsChecked = false;
            blueCheckBox.IsChecked = false;
            armyGreenCheckBox.IsChecked = false;
            duringGameBallCountComboBox.SelectedIndex = 2;
            startBallCountComboBox.SelectedIndex = 4;
            jokerBallsCheckBox.IsChecked = false;
            doubleScoreBallsCheckBox.IsChecked = false;
            lineRadioButton.IsChecked = true;
            minLineLengthComboBox.SelectedIndex = 3;
        }

        public void setRandomValues()
        {
            Random randomNumberGenerator = new Random();
            heightComboBox.SelectedIndex = randomNumberGenerator.Next(1, 29);
            widthComboBox.SelectedIndex = randomNumberGenerator.Next(1, 29);
            lightGreenCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            redCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            darkBlueCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            yellowCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            lightBlueCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            purpleCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            brownCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            pinkCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            greenCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            goldCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            orangeCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            whiteCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            greyCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            blackCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            blueCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            armyGreenCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            duringGameBallCountComboBox.SelectedIndex = (randomNumberGenerator.Next(1, 9));
            startBallCountComboBox.SelectedIndex = (randomNumberGenerator.Next(1, 9));
            jokerBallsCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            doubleScoreBallsCheckBox.IsChecked = Convert.ToBoolean(randomNumberGenerator.Next(2));
            
            
                switch (randomNumberGenerator.Next(0, 3))
            
            {
                case 0:
                    lineRadioButton.IsChecked = true;
                    minLineLengthComboBox.SelectedIndex = 
                        (
                        randomNumberGenerator.Next(0, 2+Math.Min(
                                                        widthComboBox.SelectedIndex, heightComboBox.SelectedIndex
                                                        )
                                            )
                        );
                break;
                case 1:
                squareRadioButton.IsChecked = true;
                break;
                case 2:
                circlesRadioButton.IsChecked = true;
                break;

            }
        }


        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void radioButton_Copy_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void squareRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.shapeOfGroupOfBallsWhichWillExplode = "ctverec";
        }

        private void circleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.shapeOfGroupOfBallsWhichWillExplode = "krouzek";
        }

        private void lineRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.shapeOfGroupOfBallsWhichWillExplode = "linka";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.setRandomValues();
        }

        private void redCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void blackCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void greyCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void setDefaultValuesButton_Click(object sender, RoutedEventArgs e)
        {
            this.setDefaultValues();
        }

        private void lightGreenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void startNewGameWithCustomGameCompositionButton_Click(object sender, RoutedEventArgs e)
        {

            customGameComposition.setHeight(Convert.ToInt32(heightComboBox.Text));
            customGameComposition.setWidth(Convert.ToInt32(widthComboBox.Text));
            customGameComposition.setLightGreen(Convert.ToBoolean(lightGreenCheckBox.IsChecked));
            customGameComposition.setRed(Convert.ToBoolean(redCheckBox.IsChecked));
            customGameComposition.setDarkBlue(Convert.ToBoolean(darkBlueCheckBox.IsChecked));
            customGameComposition.setYellow(Convert.ToBoolean(yellowCheckBox.IsChecked));
            customGameComposition.setLightBlue(Convert.ToBoolean(lightBlueCheckBox.IsChecked));
            customGameComposition.setPurple(Convert.ToBoolean(purpleCheckBox.IsChecked));
            customGameComposition.setBrown(Convert.ToBoolean(brownCheckBox.IsChecked));
            customGameComposition.setPink(Convert.ToBoolean(pinkCheckBox.IsChecked));
            customGameComposition.setGreen(Convert.ToBoolean(greenCheckBox.IsChecked));
            customGameComposition.setGold(Convert.ToBoolean(goldCheckBox.IsChecked));
            
            customGameComposition.setOrange(Convert.ToBoolean(orangeCheckBox.IsChecked));
            customGameComposition.setWhite(Convert.ToBoolean(whiteCheckBox.IsChecked));
            customGameComposition.setGrey(Convert.ToBoolean(greyCheckBox.IsChecked));
            customGameComposition.setBlack(Convert.ToBoolean(blackCheckBox.IsChecked));
            customGameComposition.setBlue(Convert.ToBoolean(blueCheckBox.IsChecked));
            customGameComposition.setArmyGreen(Convert.ToBoolean(armyGreenCheckBox.IsChecked));
            customGameComposition.setStartBallCount(Convert.ToInt32(startBallCountComboBox.Text));
            customGameComposition.setNextBallCount(Convert.ToInt32(duringGameBallCountComboBox.Text));
            customGameComposition.setJokerBalls(Convert.ToBoolean(jokerBallsCheckBox.IsChecked));
            customGameComposition.setDoubleScoreBalls(Convert.ToBoolean(doubleScoreBallsCheckBox.IsChecked));
            customGameComposition.setShape(this.shapeOfGroupOfBallsWhichWillExplode);
            customGameComposition.setMinLineLength(Convert.ToInt32(minLineLengthComboBox.Text));
            customGameComposition.setChanged(true);
            Close();
        }

        private void newGameWithCustomGameCompositionWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
           
        
    }
    }
}
