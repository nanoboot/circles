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

namespace Circles
{
    /// <summary>
    /// Interaction logic for PrehledVysledkuWindow.xaml
    /// </summary>
    public partial class ScoreListWindow : Window
    {
        System.Data.DataSet dset;
        public ScoreListWindow(Game hra, GameComposition sestavaHry)
        {
            InitializeComponent();
            dset = hra.getScoreListForGivenTGameCompositionWithGivenId();
            dataGrid.ItemsSource = dset.Tables[0].DefaultView;
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: { this.Close(); }; break;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
