using System.Windows;
using System.Windows.Input;

namespace Circles
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreListWindow : Window
    {
        System.Data.DataSet dset;
        public ScoreListWindow(Game game, GameComposition gameComposition)
        {
            InitializeComponent();
            dset = game.getScoreListForGivenTGameCompositionWithGivenId();
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
