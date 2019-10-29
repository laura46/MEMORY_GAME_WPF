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
        public PlayerTurn(MemoryGrid currentGrid)
        {
            InitializeComponent();
            currentGrid.OnPlayerTurn += new EventHandler<bool>(UpdateTurnLabel);
        }
        private void UpdateTurnLabel(object sender,bool player1) 
        {
            lblPlayer.Content = (player1) ? "Player 1" : "Player 2";
        }
    }
}
