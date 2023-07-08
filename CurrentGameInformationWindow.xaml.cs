using System;
using System.Windows;
using System.Windows.Input;

namespace Circles
{
    /// <summary>
    /// Interaction logic for InformationAboutCurrentGameWindow.xaml
    /// </summary>
    public partial class InformationAboutCurrentGameWindow : Window
    {
        public InformationAboutCurrentGameWindow(GameComposition gameCompositioin)
        {
            InitializeComponent();
            heightLabel.Content = gameCompositioin.getHeight().ToString();
            widthLabel.Content = gameCompositioin.getWidth().ToString();
            lightGreenLabel.Content = convertBooleanToString(gameCompositioin.getLightGreen());
            redLabel.Content = convertBooleanToString(gameCompositioin.isRed());
            darkBlueLabel.Content = convertBooleanToString(gameCompositioin.isDarkBlue());
            yellowLabel.Content = convertBooleanToString(gameCompositioin.isYellow());
            lightBlueLabel.Content = convertBooleanToString(gameCompositioin.isLightBlue());
            purpleLabel.Content = convertBooleanToString(gameCompositioin.isPurple());
            brownLabel.Content = convertBooleanToString(gameCompositioin.isBrown());
            pinkLabel.Content = convertBooleanToString(gameCompositioin.isPink());
            greenLabel.Content = convertBooleanToString(gameCompositioin.isGreen());
            goldLabel.Content = convertBooleanToString(gameCompositioin.isGold());
            orangeLabel.Content = convertBooleanToString(gameCompositioin.isOrange());
            whiteLabel.Content = convertBooleanToString(gameCompositioin.isWhite());
            greyLabel.Content = convertBooleanToString(gameCompositioin.isGrey());
            blackLabel.Content = convertBooleanToString(gameCompositioin.isBlack());
            blueLabel.Content = convertBooleanToString(gameCompositioin.isBlue());
            armyGreenLabel.Content = convertBooleanToString(gameCompositioin.isArmyGreen());
            startBallCountLabel.Content = gameCompositioin.getStartBallCount().ToString();
            nextBallCountLabel.Content = gameCompositioin.getNextBallCount().ToString();
            jokerBallsLabel.Content = convertBooleanToString(gameCompositioin.isJokerBalls());
            doubleScoreBallsLabel.Content = convertBooleanToString(gameCompositioin.isDoubleScoreBalls());

            if (gameCompositioin.getShape() == "line")
            {
                shapeLabel.Content = String.Concat(gameCompositioin.getShape(), " with minimal length ", gameCompositioin.getMinLineLength(), " ","balls");
            }
            else
            {
                shapeLabel.Content = gameCompositioin.getShape();
            };

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private String convertBooleanToString(bool logicValue)
        { return logicValue ? "yes":"no"; }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: { this.Close(); }; break;
            }
        }
    }
}
