using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Míče
{
    /// <summary>
    /// Interaction logic for InformaceOAktuálníHreWindow.xaml
    /// </summary>
    public partial class InformaceOAktualniHreWindow : Window
    {
        public InformaceOAktualniHreWindow(GameComposition sestavaHry)
        {
            InitializeComponent();
            vyskaLabel.Content = sestavaHry.VratVyska().ToString();
            sirkaLabel.Content = sestavaHry.VratSirka().ToString();
            svetleZelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratSvetleZelena());
            cervenaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratCervena());
            tmaveModraLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratTmaveModra());
            zlutaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratZluta());
            svetleModraLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratSvetleModra());
            fialovaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratFialova());
            hnedaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratHneda());
            ruzovaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratRuzova());
            zelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratZelena());
            zlataLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratZlata());
            oranzovaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratOranzova());
            bilaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratBila());
            sedivaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratSediva());
            cernaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratCerna());
            modraLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratModra());
            vojenskaZelenaLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratVojenskaZelena());
            pocetHazenychMicuNaZacatkuHryLabel.Content = sestavaHry.VratPocetHazenychMicuNaZacatkuHry().ToString();
            pocetHazenychMicuBehemHryLabel.Content = sestavaHry.VratPocetHazenychMicuBehemHry().ToString();
            duhoveBallsLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratDuhoveBalls());
            zdvojnasobujiciBallsLabel.Content = PrevedLogickouHodnotuNaRetezec(sestavaHry.VratZdvojnasobujiciBalls());

            if (sestavaHry.VratTvarSkupinyMicuKteraExploduje() == "linka")
            {
                String tvarSlovaBalls = "";
                if (sestavaHry.VratMinimalniDelkaLinky() <= 4)
                { tvarSlovaBalls = "míče"; }  else { tvarSlovaBalls = "míčů"; };
                tvarSkupinyMicuKteraExplodujeLabel.Content = String.Concat(sestavaHry.VratTvarSkupinyMicuKteraExploduje(), " s minimální délkou ", sestavaHry.VratMinimalniDelkaLinky(), " ",tvarSlovaBalls);
            }
            else
            {
                tvarSkupinyMicuKteraExplodujeLabel.Content = sestavaHry.VratTvarSkupinyMicuKteraExploduje();
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
