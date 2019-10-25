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
        private PLAYFIELD_SIZE GridSize;
        MemoryGrid grid;
        public GameGrid()
        {
            InitializeComponent();
            this.GridSize = PLAYFIELD_SIZE.SMALL;
            
            this.grid = new MemoryGrid(GameGridref, this.GridSize);
           
        }

        public enum PLAYFIELD_SIZE
        {
            SMALL = 4,
            MEDIUM = 6,
            BIG = 8
        }
    }
}
