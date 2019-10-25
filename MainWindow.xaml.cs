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

    public partial class MainWindow : Window
    {
        private PLAYFIELD_SIZE GridSize;
        MemoryGrid grid;
        
        public MainWindow()
        {
            InitializeComponent();
            grid = new MemoryGrid(GameGrid, GridSize, GridSize);
        }

        public enum PLAYFIELD_SIZE {
            SMALL = 4,
            MEDIUM = 6,
            BIG = 8
        }
    }
}
