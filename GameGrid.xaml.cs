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
        public GameGrid(GridSizeOptions.GRID_SIZES gridSize)
        {
            InitializeComponent();
            InitializeGameGrid(gridSize);
            InitializeNavbar();
            InitializeScorebord();
            InitializePlayerTurn();
            TimerFrame.Content = new CounterTimer();
            ScoreFrame.Content = new Scorebord(this.grid);
            turnFrame.Content = new PlayerTurn(this.grid);
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
            ScoreFrame.Content = new Scorebord(this.grid);
            turnFrame.Content = new PlayerTurn(this.grid);

        }
        public void InitializeScorebord()
        {   
            ScoreFrame.Content = new Scorebord(this.grid);
        }
        public void InitializePlayerTurn()
        {
            turnFrame.Content = new PlayerTurn(this.grid);
        }
    }
}
