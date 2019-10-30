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
        public NaamInvoer NaamInvoer;
        private string player1Name;
        private string player2Name;

        GridSizeOptions SizeOptions;
        public MainMenu()
        {

            InitializeComponent();
            ShowStartButton();
            InitializeGridSizeOptions();
            InitializeNameInput();

        }
        private void InitializeNameInput() 
        {
            this.NaamInvoer = new NaamInvoer();
            this.NaamInvoer.OnGoToSizeOptions += new EventHandler(ShowGridSizeOptions);
            this.NaamInvoer.OnInputNames += new EventHandler<string>(SetNames);
        }

        private void InitializeGridSizeOptions() 
        {
            this.SizeOptions = new GridSizeOptions();
            this.SizeOptions.OnGridSizeChosen += new EventHandler<ChooseGridSizeEventArgs>(GoToGameGrid);
        }

        private void ShowsNaamInvoer(object sender, MouseButtonEventArgs e)
        {
            startFrame.Content = this.NaamInvoer;
        }

        private void ShowGridSizeOptions(object sender, EventArgs e) 
        {
            startFrame.Content = this.SizeOptions;
        }
        private void SetNames(object sender, string names)
        {
            player1Name = names.Split(new char[] { ';' })[0];
            player2Name = names.Split(new char[] { ';' })[1];
        }
        private void GoToGameGrid(object sender, ChooseGridSizeEventArgs chosenSize) 
        {
            Application.Current.MainWindow.Content = new GameGrid(chosenSize.GridSize, player1Name, player2Name);
        }

        private void ShowStartButton() 
        {
            Image startButton = new Image();
            startButton.Source = new BitmapImage(new Uri("Assets/play.png", UriKind.Relative));
            startButton.MouseDown += new MouseButtonEventHandler(ShowsNaamInvoer);
            startButton.Cursor = Cursors.Hand;
            startFrame.Content = startButton;
        }
    }
}
