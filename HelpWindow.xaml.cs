using System.Windows;

namespace Circles
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
