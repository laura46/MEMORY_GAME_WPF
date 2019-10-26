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
            this.GridSize = gridSize;
            this.grid = new MemoryGrid(GameGridref, GridSize);
        }

        public void ResetGameGrid(object sender, RoutedEventArgs e)
        {
            GameGridref.Children.Clear();
            GameGridref.ColumnDefinitions.Clear();
            GameGridref.RowDefinitions.Clear();
            this.grid = new MemoryGrid(GameGridref, GridSize);
        }


    }
}
