using System.Windows;
using System.Windows.Input;

namespace Circles
{
    /// <summary>
    /// Interaction logic for ZadejteVaseJmenoWindow.xaml
    /// </summary>
    public partial class EnterYourNameWindow : Window
    {
        Game hra;
        bool canIClose = false;
        public EnterYourNameWindow(Game hra)
        {
            InitializeComponent();
            playerNameTextBox.Focus();
            playerNameTextBox.Select(0, playerNameTextBox.Text.Length);
            this.hra = hra;
        }

        private void oKButton_Click(object sender, RoutedEventArgs e)
        {
            setPlayerNameAndCloseWindow();
        }
        private void setPlayerNameAndCloseWindow()
        {
            
            hra.setPlayerName(playerNameTextBox.Text);
            canIClose = true;
            this.Close();

        }


        private void playerNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    {
                        setPlayerNameAndCloseWindow();
                    }; break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (canIClose) { e.Cancel = false; } else { e.Cancel = true; };
        }
    }
}
