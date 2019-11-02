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
    /// Interaction logic for PlayerTurn.xaml
    /// </summary>
    public partial class PlayerTurn : UserControl
    {
        private string Player1Name;
        private string Player2Name;
        private bool IsPlayer1Turn;
        public PlayerTurn(MemoryGrid currentGrid, string player1Name, string player2Name)
        {
            InitializeComponent();
            SetPlayerNames(player1Name, player2Name);
            turnFrame.Content = player1Name;
            currentGrid.OnPlayerTurn += new EventHandler<bool>(UpdateTurnLabel);
        }
        public PlayerTurn(MemoryGrid currentGrid,Game loadGame)
        {
            InitializeComponent();
            SetPlayerNames(loadGame.Player1.Name, loadGame.Player2.Name);
            turnFrame.Content = (loadGame.Player1.IsMyTurn) ? loadGame.Player1.Name : loadGame.Player2.Name;
            currentGrid.OnPlayerTurn += new EventHandler<bool>(UpdateTurnLabel);
        }
        public Game GetTurn(Game currentGame) 
        {
            if (IsPlayer1Turn) 
            {
                currentGame.Player1.IsMyTurn = true;
                currentGame.Player2.IsMyTurn = false;
            } else 
            {
                currentGame.Player1.IsMyTurn = false;
                currentGame.Player2.IsMyTurn = true;
            }
            return currentGame;
        } 
        private void SetPlayerNames(string player1Name, string player2Name) 
        {
            this.Player1Name = player1Name;
            this.Player2Name = player2Name;
        }
        private void UpdateTurnLabel(object sender,bool player1) 
        {
            IsPlayer1Turn = player1;
            turnFrame.Content = (player1) ? Player1Name : Player2Name;
        }
    }
}
