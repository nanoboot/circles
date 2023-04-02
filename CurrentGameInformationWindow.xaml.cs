using System;
using System.Windows;
using System.Windows.Input;

namespace Balls
{
    /// <summary>
    /// Interaction logic for InformationAboutCurrentGameWindow.xaml
    /// </summary>
    public partial class InformationAboutCurrentGameWindow : Window
    {
        public InformationAboutCurrentGameWindow(GameComposition gameCompositioin)
        {
            InitializeComponent();
            vyskaLabel.Content = gameCompositioin.getHeight().ToString();
            sirkaLabel.Content = gameCompositioin.getWidth().ToString();
            svetleZelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.getLightGreen());
            cervenaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isRed());
            tmaveModraLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isDarkBlue());
            zlutaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isYellow());
            svetleModraLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isLightBlue());
            fialovaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isPurple());
            hnedaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isBrown());
            ruzovaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isPink());
            zelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isGreen());
            zlataLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isGold());
            oranzovaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isOrange());
            bilaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isWhite());
            sedivaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isGrey());
            cernaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isBlack());
            modraLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isBlue());
            vojenskaZelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isArmyGreen());
            pocetHazenychMicuNaZacatkuHryLabel.Content = gameCompositioin.getStartBallCount().ToString();
            pocetHazenychMicuBehemHryLabel.Content = gameCompositioin.getNextBallCount().ToString();
            duhoveBallsLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isJokerBalls());
            zdvojnasobujiciBallsLabel.Content = PrevedLogickouHodnotuNaRetezec(gameCompositioin.isDoubleScoreBalls());

            if (gameCompositioin.getShape() == "linka")
            {
                String tvarSlovaBalls = "";
                if (gameCompositioin.getMinLineLength() <= 4)
                { tvarSlovaBalls = "míče"; }  else { tvarSlovaBalls = "míčů"; };
                tvarSkupinyMicuKteraExplodujeLabel.Content = String.Concat(gameCompositioin.getShape(), " s minimální délkou ", gameCompositioin.getMinLineLength(), " ",tvarSlovaBalls);
            }
            else
            {
                tvarSkupinyMicuKteraExplodujeLabel.Content = gameCompositioin.getShape();
            };

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private String PrevedLogickouHodnotuNaRetezec(bool LogickaHodnota)
        { if (LogickaHodnota) { return "ano"; } else { return "ne"; }; }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: { this.Close(); }; break;
            }
        }
    }
}
