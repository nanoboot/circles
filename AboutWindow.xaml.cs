﻿
using System.Windows;

namespace Circles
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
