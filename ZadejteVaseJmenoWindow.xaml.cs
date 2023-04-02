﻿using System;
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
    /// Interaction logic for ZadejteVaseJmenoWindow.xaml
    /// </summary>
    public partial class ZadejteVaseJmenoWindow : Window
    {
        Hra hra;
        bool MuzuZavrit = false;
        public ZadejteVaseJmenoWindow(Hra hra)
        {
            InitializeComponent();
            hracovoJmenoTextBox.Focus();
            hracovoJmenoTextBox.Select(0, hracovoJmenoTextBox.Text.Length);
            this.hra = hra;
        }

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            NastavitHracovoJmenoAZavritDialog();
        }
        private void NastavitHracovoJmenoAZavritDialog()
        {
            
            hra.NastavHracovoJmeno(hracovoJmenoTextBox.Text);
            MuzuZavrit = true;
            this.Close();

        }


        private void hracovoJmenoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    {
                        NastavitHracovoJmenoAZavritDialog();
                    }; break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MuzuZavrit) { e.Cancel = false; } else { e.Cancel = true; };
        }
    }
}
