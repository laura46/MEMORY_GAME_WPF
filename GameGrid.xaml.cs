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
    /// Interaction logic for GameGrid.xaml
    /// </summary>
    public partial class GameGrid : Page
    {

        MemoryGrid grid;
        GridSizeOptions.GRID_SIZES GridSize;
        private string Player1Name;
        private string Player2Name;
        public GameGrid(GridSizeOptions.GRID_SIZES gridSize, string player1Name, string player2Name)
        {
            InitializeComponent();
            InitializeGameGrid(gridSize);
            InitializeNavbar();
            InitializeScorebord(player1Name, player2Name);
            InitializePlayerTurn(player1Name, player2Name);
            TimerFrame.Content = new CounterTimer();
            Player1Name = player1Name;
            Player2Name = player2Name;
            powerupFrame.Content = new PowerUp(this.grid);

        }
        private void InitializeGameGrid(GridSizeOptions.GRID_SIZES gridSize) 
        {
            this.GridSize = gridSize;
            this.grid = new MemoryGrid(GameGridref, GridSize);
        }
        private void InitializeNavbar() 
        {
            TopNavBar navbar = new TopNavBar();
            navbar.OnResetGame += new EventHandler(ResetGameGrid);
            NavbarFrame.Content = navbar;
        }

        public void ResetGameGrid(object sender, EventArgs e)
        {
            GameGridref.Children.Clear();
            GameGridref.ColumnDefinitions.Clear();
            GameGridref.RowDefinitions.Clear();
            this.grid = new MemoryGrid(GameGridref, GridSize);
            TimerFrame.Content = new CounterTimer();
            ScoreFrame.Content = new Scorebord(this.grid, this.Player1Name, this.Player2Name);
            turnFrame.Content = new PlayerTurn(this.grid, this.Player1Name, this.Player2Name);

        }
        public void InitializeScorebord(string player1Name, string player2Name)
        {   
            ScoreFrame.Content = new Scorebord(this.grid, player1Name, player2Name);
        }
        public void InitializePlayerTurn(string player1Name, string player2Name)
        {
            turnFrame.Content = new PlayerTurn(this.grid, player1Name, player2Name);
        }
    }
}
