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
    /// Interaction logic for GridSizeOptions.xaml
    /// </summary>
    public partial class GridSizeOptions : UserControl
    {
        public event EventHandler<ChooseGridSizeEventArgs> OnGridSizeChosen;
        public GridSizeOptions()
        {
            InitializeComponent();
        }
        public void StartGame(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            OnGridSizeChosen?.Invoke(this, new ChooseGridSizeEventArgs(clickedImage.Uid));
        }
        public enum GRID_SIZES 
        {
            SMALL = 4,
            MEDIUM = 6,
            LARGE = 8
        }
    }
    public class ChooseGridSizeEventArgs : EventArgs 
    {
        public GridSizeOptions.GRID_SIZES GridSize { get; set; }
        public ChooseGridSizeEventArgs(string gridSize) 
        {
            switch (gridSize)
            {
                case "4":
                    GridSize = GridSizeOptions.GRID_SIZES.SMALL;
                    break;
                case "6":
                    GridSize = GridSizeOptions.GRID_SIZES.MEDIUM;
                    break;
                case "8":
                    GridSize = GridSizeOptions.GRID_SIZES.LARGE;
                    break;
                default:
                    break;
            }
        }
    }
}
