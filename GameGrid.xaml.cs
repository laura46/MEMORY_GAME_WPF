using MemoryProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        GridSizeOptions.GRID_SIZES GridSize;
        private string Player1Name;
        private string Player2Name;

        public GameGrid(GridSizeOptions.GRID_SIZES gridSize, string player1Name, string player2Name)
        {
            InitializeComponent();
            InitializeGameGrid(gridSize, player1Name, player2Name);
            InitializeNavbar();
            InitializeScorebord();
            InitializePlayerTurn();
            InitializePowerups();
            InitializeTimer();
        }
        private void InitializeTimer() 
        {
            this.Timer = new CounterTimer();
            TimerFrame.Content = this.Timer;
        }
        private void InitializePowerups() 
        {
            this.PowerUp = new PowerUp(this.grid);
            powerupFrame.Content = this.PowerUp;
        }
        private void InitializeGameGrid(GridSizeOptions.GRID_SIZES gridSize, string player1Name, string player2Name) 
        {
            this.GridSize = gridSize;
            Player1Name = player1Name;
            Player2Name = player2Name;
            this.grid = new MemoryGrid(GameGridref, GridSize);
            grid.OnPairMade += new EventHandler<string>(ShowPopup);
        }
        private void InitializeNavbar() 
        {
            TopNavBar navbar = new TopNavBar();
            navbar.OnResetGame += new EventHandler(ResetGameGrid);
            navbar.OnSaveGame += new EventHandler(SaveGame);
            NavbarFrame.Content = navbar;
        }

        private void SaveGame(object sender, EventArgs e) 
        {




            Player player1 = new Player
            {
                Name = this.Player1Name,
                Score = this.Scorebord.GetScore(true),
                Powerups = this.PowerUp.GetPowerups(true)
            };

            Player player2 = new Player
            {
                Name = this.Player2Name,
                Score = this.Scorebord.GetScore(false),
                Powerups = this.PowerUp.GetPowerups(false)
            };

            Models.Grid playGrid = new Models.Grid
            {
                GridSize = this.GridSize,
                Timer = this.Timer.GetTimerTime()
            };

            Game Game = new Game
            {
                Player1 = player1,
                Player2 = player2,
                Grid = playGrid
            };
            string json = JsonConvert.SerializeObject(Game);

            File.WriteAllText(@"Storage\"+ Guid.NewGuid() +".txt", json);
            
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
        public void InitializeScorebord()
        {   
            this.Scorebord = new Scorebord(this.grid, this.Player1Name, this.Player2Name);
            ScoreFrame.Content = this.Scorebord;
        }
        public void InitializePlayerTurn()
        {
            turnFrame.Content = new PlayerTurn(this.grid, this.Player1Name, this.Player2Name);
        }

        public void ShowPopup(object sender, string uitkomst)
        {
            if(uitkomst == "fout")
            {
                foutpop.IsOpen = true;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
                timer.Tick += delegate (object senders, EventArgs e)
                {
                    ((DispatcherTimer)timer).Stop();
                    if (foutpop.IsOpen)
                    {
                        foutpop.IsOpen = false;
                    }
                };
            }

            if (uitkomst == "goed")
            {
                goedpop.IsOpen = true;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
                timer.Tick += delegate (object senders, EventArgs e)
                {
                    ((DispatcherTimer)timer).Stop();
                    if (goedpop.IsOpen)
                    {
                        goedpop.IsOpen = false;
                    }
                };
            }

            if (uitkomst == "dubbelKlik")
            {
                dubbelKlik.IsOpen = true;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Start();
                timer.Tick += delegate (object senders, EventArgs e)
                {
                    ((DispatcherTimer)timer).Stop();
                    if (dubbelKlik.IsOpen)
                    {
                        dubbelKlik.IsOpen = false;
                    }
                };
            }

            if (uitkomst == "alGeklikt")
            {
                alGeklikt.IsOpen = true;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Start();
                timer.Tick += delegate (object senders, EventArgs e)
                {
                    ((DispatcherTimer)timer).Stop();
                    if (alGeklikt.IsOpen)
                    {
                        alGeklikt.IsOpen = false;
                    }
                };
            }
        }
    }
}

