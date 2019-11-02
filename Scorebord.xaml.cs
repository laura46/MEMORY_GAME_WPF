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
    /// Interaction logic for Scorebord.xaml
    /// </summary>
    public partial class Scorebord : UserControl
    {
        Game CurrentGame;
        public Scorebord(MemoryGrid currentGrid, Game currentGame)
        {
            InitializeComponent();
            CurrentGame = currentGame;
            SetPlayerNames();
            currentGrid.OnScore1Update += new EventHandler<int>(UpdateScore1);
            currentGrid.OnScore2Update += new EventHandler<int>(UpdateScore2);

            if (CurrentGame.Player1.Score != null) 
            {
                SetScoresFromLoadedGame();
            }
        }
        private void SetScoresFromLoadedGame() 
        {
            UpdateScore1(this, (int)CurrentGame.Player1.Score);
            UpdateScore2(this, (int)CurrentGame.Player2.Score);
        }

        public Game GetScore(Game currentGame) 
        {
            currentGame.Player1.Score = Convert.ToInt32(lblscore1.Content);
            currentGame.Player2.Score = Convert.ToInt32(lblscore2.Content);
            return currentGame;
        }

        private void SetPlayerNames()
        {
            player1.Content = CurrentGame.Player1.Name;
            player2.Content = CurrentGame.Player2.Name;
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
