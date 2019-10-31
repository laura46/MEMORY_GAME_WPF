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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for PowerUp.xaml
    /// </summary>
    public partial class PowerUp : UserControl
    {
        public PowerUp(MemoryGrid currentGrid)
        {
            InitializeComponent();
            currentGrid.OnPowerUpUpdate += new EventHandler<Dictionary<string, int>>(HandlePowerUp);

        }
        public Dictionary<string, int> GetPowerups(bool isPlayer1) 
        {
            
            object[] player1Labels = { lbl1x2.Content, lbl1x4.Content, lbl1x6.Content };
            object[] player2Labels = { lbl2x2.Content, lbl2x4.Content, lbl2x6.Content };
            return (isPlayer1) ? GetLabelValues(player1Labels, 1) : GetLabelValues(player2Labels, 2);
        }
        private Dictionary<string, int> GetLabelValues(object[] labels, int player) 
        {
            Dictionary<string, int> powerups = new Dictionary<string, int>();
            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] != null)
                {
                    powerups.Add("p" + player + (i + i + 2), (int)labels[i]);
                }
            }
            return powerups;
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
            if (label.Content == null)
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
