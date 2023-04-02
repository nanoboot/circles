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
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NovaHraSVlastnimNastavenimWindow : Window
    {
        private SestavaHry sestavaVlastniHry;
        private String TvarSkupinyMicuKteraExploduje = "linka";
        public NovaHraSVlastnimNastavenimWindow(SestavaHry sestavaHry)
        {
            InitializeComponent();
            this.sestavaVlastniHry = sestavaHry;
        }

        public void NastavVychoziHodnoty()// Tato metoda přepíše všechny hodnoty ve formuláři na jejich výchozí hodnoty.
        {

            vyskaComboBox.SelectedIndex = 6;
            sirkaComboBox.SelectedIndex = 6;
            svetleZelenaCheckBox.IsChecked = true;
            cervenaCheckBox.IsChecked = true;
            tmaveModraCheckBox.IsChecked = true;
            zlutaCheckBox.IsChecked = true;
            svetleModraCheckBox.IsChecked = true;
            fialovaCheckBox.IsChecked = true;
            hnedaCheckBox.IsChecked = true;
            ruzovaCheckBox.IsChecked = false;
            zelenaCheckBox.IsChecked = false;
            zlataCheckBox.IsChecked = false;
            oranzovaCheckBox.IsChecked = false;
            bilaCheckBox.IsChecked = false;
            sedivaCheckBox.IsChecked = false;
            cernaCheckBox.IsChecked = false;
            modraCheckBox.IsChecked = false;
            vojenskaZelenaCheckBox.IsChecked = false;
            pocetHazenychMicuBehemHryComboBox.SelectedIndex = 2;
            pocetHazenychMicuNaZacatkuHryComboBox.SelectedIndex = 4;
            duhoveMiceCheckBox.IsChecked = false;
            zdvojnasobujiciMiceCheckBox.IsChecked = false;
            linkaRadioButton.IsChecked = true;
            minimalniDelkaLinkyComboBox.SelectedIndex = 3;
        }

        public void NastavNahodneHodnoty()
        {
            Random generatorCisel = new Random();
            vyskaComboBox.SelectedIndex = generatorCisel.Next(1, 29);
            sirkaComboBox.SelectedIndex = generatorCisel.Next(1, 29);
            svetleZelenaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            cervenaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            tmaveModraCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            zlutaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            svetleModraCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            fialovaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            hnedaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            ruzovaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            zelenaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            zlataCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            oranzovaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            bilaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            sedivaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            cernaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            modraCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            vojenskaZelenaCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            pocetHazenychMicuBehemHryComboBox.SelectedIndex = (generatorCisel.Next(1, 9));
            pocetHazenychMicuNaZacatkuHryComboBox.SelectedIndex = (generatorCisel.Next(1, 9));
            duhoveMiceCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            zdvojnasobujiciMiceCheckBox.IsChecked = Convert.ToBoolean(generatorCisel.Next(2));
            
            
                switch (generatorCisel.Next(0, 3))
            
            {
                case 0:
                    linkaRadioButton.IsChecked = true;
                    minimalniDelkaLinkyComboBox.SelectedIndex = 
                        (
                        generatorCisel.Next(0, 2+Math.Min(
                                                        sirkaComboBox.SelectedIndex, vyskaComboBox.SelectedIndex
                                                        )
                                            )
                        );
                break;
                case 1:
                ctverecRadioButton.IsChecked = true;
                break;
                case 2:
                krouzekRadioButton.IsChecked = true;
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

        private void ctverecRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.TvarSkupinyMicuKteraExploduje = "ctverec";
        }

        private void krouzekRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.TvarSkupinyMicuKteraExploduje = "krouzek";
        }

        private void linkaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.TvarSkupinyMicuKteraExploduje = "linka";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.NastavNahodneHodnoty();
        }

        private void cervenaCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cernaCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void sedivaCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void nastavitVychoziHodnotyButton_Click(object sender, RoutedEventArgs e)
        {
            this.NastavVychoziHodnoty();
        }

        private void svetleZelenaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void zacitNovouHruSVlastnimNastavenimButton_Click(object sender, RoutedEventArgs e)
        {

            sestavaVlastniHry.NastavVyska(Convert.ToInt32(vyskaComboBox.Text));
            sestavaVlastniHry.NastavSirka(Convert.ToInt32(sirkaComboBox.Text));
            sestavaVlastniHry.NastavSvetleZelena(Convert.ToBoolean(svetleZelenaCheckBox.IsChecked));
            sestavaVlastniHry.NastavCervena(Convert.ToBoolean(cervenaCheckBox.IsChecked));
            sestavaVlastniHry.NastavTmaveModra(Convert.ToBoolean(tmaveModraCheckBox.IsChecked));
            sestavaVlastniHry.NastavZluta(Convert.ToBoolean(zlutaCheckBox.IsChecked));
            sestavaVlastniHry.NastavSvetleModra(Convert.ToBoolean(svetleModraCheckBox.IsChecked));
            sestavaVlastniHry.NastavFialova(Convert.ToBoolean(fialovaCheckBox.IsChecked));
            sestavaVlastniHry.NastavHneda(Convert.ToBoolean(hnedaCheckBox.IsChecked));
            sestavaVlastniHry.NastavRuzova(Convert.ToBoolean(ruzovaCheckBox.IsChecked));
            sestavaVlastniHry.NastavZelena(Convert.ToBoolean(zelenaCheckBox.IsChecked));
            sestavaVlastniHry.NastavZlata(Convert.ToBoolean(zlataCheckBox.IsChecked));
            
            sestavaVlastniHry.NastavOranzova(Convert.ToBoolean(oranzovaCheckBox.IsChecked));
            sestavaVlastniHry.NastavBila(Convert.ToBoolean(bilaCheckBox.IsChecked));
            sestavaVlastniHry.NastavSediva(Convert.ToBoolean(sedivaCheckBox.IsChecked));
            sestavaVlastniHry.NastavCerna(Convert.ToBoolean(cernaCheckBox.IsChecked));
            sestavaVlastniHry.NastavModra(Convert.ToBoolean(modraCheckBox.IsChecked));
            sestavaVlastniHry.NastavVojenskaZelena(Convert.ToBoolean(vojenskaZelenaCheckBox.IsChecked));
            sestavaVlastniHry.NastavPocetHazenychMicuNaZacatkuHry(Convert.ToInt32(pocetHazenychMicuNaZacatkuHryComboBox.Text));
            sestavaVlastniHry.NastavPocetHazenychMicuBehemHry(Convert.ToInt32(pocetHazenychMicuBehemHryComboBox.Text));
            sestavaVlastniHry.NastavDuhoveMice(Convert.ToBoolean(duhoveMiceCheckBox.IsChecked));
            sestavaVlastniHry.NastavZdvojnasobujiciMice(Convert.ToBoolean(zdvojnasobujiciMiceCheckBox.IsChecked));
            sestavaVlastniHry.NastavTvarSkupinyMicuKteraExploduje(this.TvarSkupinyMicuKteraExploduje);
            sestavaVlastniHry.NastavMinimalniDelkaLinky(Convert.ToInt32(minimalniDelkaLinkyComboBox.Text));
            sestavaVlastniHry.NastavZmeneno(true);
            Close();
        }

        private void novaHraSVlastnimNastavenimWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
           
        
    }
    }
}
