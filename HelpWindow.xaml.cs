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

namespace Balls
{
    /// <summary>
    /// Interaction logic for NapovedaWindow.xaml
    /// </summary>
    public partial class NapovedaWindow : Window
    {
        public NapovedaWindow()
        {
            InitializeComponent();
        }

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
