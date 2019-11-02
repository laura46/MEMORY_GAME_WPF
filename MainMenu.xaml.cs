using MemoryProject.Models;
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
        public EventHandler OnGoToHighscores;
        public NaamInvoer NaamInvoer;
        public LoadGame loadGame;
        private Game newGame;

        GridSizeOptions SizeOptions;
        public MainMenu()
        {
            
            InitializeComponent();
            InitializeGame();
            ShowStartButton();
            InitializeGridSizeOptions();
            InitializeNameInput();
            InitializeLoadGame();
        }
        private void InitializeGame()
        {
            this.newGame = new Game
            {
                Player1 = new Player(),
                Player2 = new Player()
            };
            this.newGame.Player1.Powerups = new Models.PowerUp();
            this.newGame.Player2.Powerups = new Models.PowerUp();
            this.newGame.Grid = new Models.Grid();
        }
        private void InitializeLoadGame()
        {
            loadGame = new LoadGame();
            loadGame.OnLoadGame += new EventHandler<Game>(LoadGame);
        }
        private void LoadGame(object sender, Game game)
        {
            Application.Current.MainWindow.Content = new GameGrid(game);
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
        public void ShowSavedGames(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = this.loadGame;
        }
        private void ShowGridSizeOptions(object sender, EventArgs e)
        {
            startFrame.Content = this.SizeOptions;
        }
        private void SetNames(object sender, string names)
        {
            this.newGame.Player1.Name = names.Split(new char[] { ';' })[0];
            this.newGame.Player2.Name = names.Split(new char[] { ';' })[1];
        }
        private void GoToGameGrid(object sender, ChooseGridSizeEventArgs chosenSize)
        {
            this.newGame.Grid.GridSize = chosenSize.GridSize;
            Application.Current.MainWindow.Content = new GameGrid(this.newGame);
        }

        private void ShowStartButton()
        {
            Image startButton = new Image
            {
                Source = new BitmapImage(new Uri("Assets/play.png", UriKind.Relative)),
                Cursor = Cursors.Hand
            };
            startButton.MouseDown += new MouseButtonEventHandler(ShowsNaamInvoer);

            startFrame.Content = startButton;
        }
        private void GoToHighscores(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new Highscores();


        }

    }
}
