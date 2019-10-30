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
    /// Interaction logic for Scorebord.xaml
    /// </summary>
    public partial class Scorebord : UserControl
    {
        public Scorebord(MemoryGrid currentGrid, string player1Name, string player2Name)
        {
            InitializeComponent();
            SetPlayerNames(player1Name, player2Name);
            currentGrid.OnScore1Update += new EventHandler<int>(UpdateScore1);
            currentGrid.OnScore2Update += new EventHandler<int>(UpdateScore2);
        }

        private void SetPlayerNames(string player1Name, string player2Name)
        {
            player1.Content = player1Name;
            player2.Content = player2Name;
        }

        private void UpdateScore1(object sender, int score) 
        {
            lblscore1.Content = score;
        }
        private void UpdateScore2(object sender, int score) 
        {
            lblscore2.Content = score;
        }
    }
}
