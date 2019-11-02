using MemoryProject.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for GameGrid.xaml
    /// </summary>
    public partial class GameGrid : Page
    {

        MemoryGrid grid;
        Scorebord Scorebord;
        PowerUp PowerUp;
        CounterTimer Timer;
        PlayerTurn Turn;
        Game currentGame; 

        public GameGrid(Game game)
        {
            InitializeComponent();
            this.currentGame = game;

            InitializeGameGrid();
            InitializeNavbar();
            InitializeScorebord();
            InitializePlayerTurn();
            InitializePowerups();
            InitializeTimer();

        }
        private void InitializeTimer() 
        {
            this.Timer = new CounterTimer(this.currentGame);
            TimerFrame.Content = this.Timer;
        }
        private void InitializePowerups() 
        {
            if (currentGame.Date != new DateTime())
            {
                this.PowerUp = new PowerUp(this.grid, currentGame);
            }
            else {
                this.PowerUp = new PowerUp(this.grid);
            }
            powerupFrame.Content = this.PowerUp;
        }
        private void InitializeGameGrid() 
        {
            this.grid = new MemoryGrid(GameGridref, currentGame.Grid.GridSize);
            grid.OnPairMade += new EventHandler<string>(ShowPopup);
            grid.OnEndGame += new EventHandler<bool>(SaveGame);
            grid.OnOpenCardClicked += new EventHandler<string>(ShowPopup);
        }
        private void InitializeNavbar() 
        {
            TopNavBar navbar = new TopNavBar();
            navbar.OnResetGame += new EventHandler(ResetGameGrid);
            navbar.OnSaveGame += new EventHandler(SaveGame);
            NavbarFrame.Content = navbar;
        }

        public void SaveGame(object sender, bool isFinishedGame) 
        {
            currentGame = this.Scorebord.GetScore(currentGame);
            currentGame = this.PowerUp.GetPowerups(currentGame);
            currentGame = this.Turn.GetTurn(currentGame);

            currentGame.Grid.Timer = this.Timer.GetTimerTime();
            currentGame.Date = DateTime.Now;

            string json = JsonConvert.SerializeObject(currentGame);
            if (!Directory.Exists(@"Storage\Finish\"))
            {
                Directory.CreateDirectory(@"Storage\Finish\");
            }
            File.WriteAllText(@"Storage\Finish\"+ Guid.NewGuid() +".txt", json);

            Application.Current.MainWindow.Content = new EndScreen(currentGame.Player1, currentGame.Player2, Timer.GetTimerTime());
        }
        private void SaveGame(object sender, EventArgs e)
        {
            currentGame = this.Scorebord.GetScore(currentGame);
            currentGame = this.PowerUp.GetPowerups(currentGame);
            currentGame = this.Turn.GetTurn(currentGame);

            currentGame.Grid.Timer = this.Timer.GetTimerTime();
            currentGame.Date = DateTime.Now;

            string json = JsonConvert.SerializeObject(currentGame);
            File.WriteAllText(@"Storage\Load\" + Guid.NewGuid() + ".txt", json);

            ShowPopup(this, "opgeslagen");
        }

        public void ResetGameGrid(object sender, EventArgs e)
        {
            GameGridref.Children.Clear();
            GameGridref.ColumnDefinitions.Clear();
            GameGridref.RowDefinitions.Clear();
            this.grid = new MemoryGrid(GameGridref, currentGame.Grid.GridSize);
            TimerFrame.Content = new CounterTimer(currentGame);
            ScoreFrame.Content = new Scorebord(this.grid, currentGame);
            turnFrame.Content = new PlayerTurn(this.grid, currentGame.Player1.Name, currentGame.Player2.Name);

        }
        public void InitializeScorebord()
        {   
            this.Scorebord = new Scorebord(this.grid, currentGame);
            ScoreFrame.Content = this.Scorebord;
        }
        public void InitializePlayerTurn()
        {
            if (currentGame.Player1.IsMyTurn || currentGame.Player2.IsMyTurn)
            {
                this.Turn = new PlayerTurn(this.grid, currentGame);
            }
            else 
            {
                this.Turn = new PlayerTurn(this.grid, currentGame.Player1.Name, currentGame.Player2.Name);
            }
            turnFrame.Content = this.Turn;
        }

        public void ShowPopup(object sender, string uitkomst)
        {
            if(uitkomst == "fout")
            {
                OpenPopup(foutpop);
            }

            if (uitkomst == "goed")
            {
                OpenPopup(goedpop);
            }

            if (uitkomst == "dubbelKlik")
            {
                OpenPopup(dubbelKlik);
            }

            if (uitkomst == "alGeklikt")
            {
                OpenPopup(alGeklikt);
            }

            if (uitkomst == "opgeslagen")
            {
                OpenPopup(opgeslagen);
            }
        }
        private void OpenPopup(object sender)
        {
            Popup popup = (Popup)sender;
            popup.IsOpen = true;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
            timer.Tick += delegate (object senders, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (popup.IsOpen)
                {
                    popup.IsOpen = false;
                }
            };
        }
    }
}

