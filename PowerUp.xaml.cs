using MemoryProject.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for PowerUp.xaml
    /// </summary>
    public partial class PowerUp : UserControl
    {
        public PowerUp(MemoryGrid currentGrid, Game currentGame)
        {
            InitializeComponent();
            InitializePlayerNames(currentGame);
            if (currentGame.Date != new DateTime()) 
            {
                RestoreLoadGame(currentGame);
            }
            currentGrid.OnPowerUpUpdate += new EventHandler<Dictionary<string, int>>(HandlePowerUp);
        }

        private void InitializePlayerNames(Game currentGame) 
        {
            lblPlayer1.Content = currentGame.Player1.Name;
            lblPlayer2.Content = currentGame.Player2.Name;
        }
        private void RestoreLoadGame(Game loadGame) 
        {
            lbl1x2.Content = loadGame.Player1.Powerups.X2;
            lbl1x4.Content = loadGame.Player1.Powerups.X4;
            lbl1x6.Content = loadGame.Player1.Powerups.X6;
            lbl2x2.Content = loadGame.Player2.Powerups.X2;
            lbl2x4.Content = loadGame.Player2.Powerups.X4;
            lbl2x6.Content = loadGame.Player2.Powerups.X6;
        }
        public Game GetPowerups(Game currentGame) 
        {
            currentGame.Player1.Powerups.X2 = GetLabelValue(lbl1x2.Content);
            currentGame.Player1.Powerups.X4 = GetLabelValue(lbl1x4.Content);
            currentGame.Player1.Powerups.X6 = GetLabelValue(lbl1x6.Content);
            currentGame.Player2.Powerups.X2 = GetLabelValue(lbl2x2.Content);
            currentGame.Player2.Powerups.X4 = GetLabelValue(lbl2x4.Content);
            currentGame.Player2.Powerups.X6 = GetLabelValue(lbl2x6.Content);
            return currentGame;
        }
        private int GetLabelValue(object labelContent) 
        {
            return Convert.ToInt32(labelContent);
        }

        private void HandlePowerUp(object sender, Dictionary<string, int> powerUp)
        {
            if (powerUp.ContainsKey("player1"))
            {
                int value;
                powerUp.TryGetValue("player1", out value);

                switch (value)
                {
                    case 2:
                        SetLabelContent(lbl1x2);
                        break;
                    case 4:
                        SetLabelContent(lbl1x4);
                        break;
                    case 6:
                        SetLabelContent(lbl1x6);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                int value;
                powerUp.TryGetValue("player2", out value);

                switch (value)
                {
                    case 2:
                        SetLabelContent(lbl2x2);
                        break;
                    case 4:
                        SetLabelContent(lbl2x4);
                        break;
                    case 6:
                        SetLabelContent(lbl2x6);
                        break;
                    default:
                        break;
                }
            }
        }
        private void SetLabelContent(Label label)
        {
            var currentlabelvalue = label.Content;
            label.Content = (label.Content);
            if (label.Content.ToString() == "0")
            {
                label.Content = 1;
            }
            else
            {
                label.Content = (int)currentlabelvalue + 1;
            }
        }
    }
}
