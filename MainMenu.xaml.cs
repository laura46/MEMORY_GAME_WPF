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
    public partial class MainMenu : Page
    {
        GridSizeOptions SizeOptions;
        public MainMenu()
        {
            InitializeComponent();
            ShowStartButton();
            this.SizeOptions = new GridSizeOptions();
            this.SizeOptions.OnGridSizeChosen += new EventHandler<ChooseGridSizeEventArgs>(GoToGameGrid);
        }

        public void ShowGridSizeOptions(object sender, MouseButtonEventArgs e) 
        {
            startFrame.Content = this.SizeOptions;
        }

        public void GoToGameGrid(object sender, ChooseGridSizeEventArgs chosenSize) 
        {
            Application.Current.MainWindow.Content = new GameGrid(chosenSize.GridSize);
        }

        public void ShowStartButton() 
        {
            Image startButton = new Image();
            startButton.Source = new BitmapImage(new Uri("Assets/play.png", UriKind.Relative));
            startButton.MouseDown += new MouseButtonEventHandler(ShowGridSizeOptions);
            startButton.Cursor = Cursors.Hand;
            startFrame.Content = startButton;
        }
    }
}
