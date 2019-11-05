using MemoryProject.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Page
    {
        public EndScreen(Player player1, Player player2, TimeSpan time)
        {
            InitializeComponent();
            Label lblPlayer1 = new Label();
            lblPlayer1.FontSize = 18;
            lblPlayer1.FontWeight = FontWeights.Bold;
            lblPlayer1.Content = player1.Name + " " + player1.Score + " \nPowerups: \n2X: " + player1.Powerups.X2 + " \n4X: " + player1.Powerups.X4 + " \n6X: " + player1.Powerups.X6;
            Label lblPLayer2 = new Label();
            lblPLayer2.FontSize = 18;
            lblPLayer2.FontWeight = FontWeights.Bold;
            lblPLayer2.Content = player2.Name + " " + player2.Score + " \nPowerups: \n2X: " + player2.Powerups.X2 + " \n4X: " + player2.Powerups.X4 + " \n6X: " + player2.Powerups.X6;
            player1Frame.FontSize = 200;
            player1Frame.Content = lblPlayer1;
            player2Frame.Content = lblPLayer2;
            Label lblTimer = new Label();
            lblTimer.FontSize = 18;
            lblTimer.FontWeight = FontWeights.Bold;
            lblTimer.Content = time.ToString().Split(new char[] { '.' })[0];
            timerFrame.Content = lblTimer;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new MainMenu();
        }
    }
}
