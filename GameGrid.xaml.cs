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
            this.PowerUp = new PowerUp(this.grid);
            powerupFrame.Content = this.PowerUp;
        }
        private void InitializeGameGrid() 
        {
            this.grid = new MemoryGrid(GameGridref, currentGame.Grid.GridSize);
            //grid.OnPairMade += new EventHandler<string>(ShowPopup);
            grid.OnEndGame += new EventHandler<bool>(SaveGame);
            //grid.OnOpenCardClicked += new EventHandler<string>(ShowPopup);
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
            currentGame.Player1.Score = this.Scorebord.GetScore(true);
            currentGame.Player1.Powerups = this.PowerUp.GetPowerups(true);
            currentGame.Player2.Score = this.Scorebord.GetScore(false);
            currentGame.Player2.Powerups = this.PowerUp.GetPowerups(false);
            currentGame.Grid.Timer = this.Timer.GetTimerTime();
            currentGame.Date = DateTime.Now;

            string json = JsonConvert.SerializeObject(currentGame);
            if (!Directory.Exists(@"Storage\Finish\"))
            {
                Directory.CreateDirectory(@"Storage\Finish\");
            }
            File.WriteAllText(@"Storage\Finish\"+ Guid.NewGuid() +".txt", json);

            Application.Current.MainWindow.Content = new EndScreen(currentGame.Player1, currentGame.Player2, Timer.GetTimerTime());
            File.WriteAllText(@"Storage\" + Guid.NewGuid() + ".txt", json);
        }
        private void SaveGame(object sender, EventArgs e)
        {
            currentGame.Player1.Score = this.Scorebord.GetScore(true);
            currentGame.Player1.Powerups = this.PowerUp.GetPowerups(true);
            currentGame.Player2.Score = this.Scorebord.GetScore(false);
            currentGame.Player2.Powerups = this.PowerUp.GetPowerups(false);
            currentGame.Grid.Timer = this.Timer.GetTimerTime();
            currentGame.Date = DateTime.Now;

            string json = JsonConvert.SerializeObject(currentGame);
            File.WriteAllText(@"Storage\Load\" + Guid.NewGuid() + ".txt", json);
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
            turnFrame.Content = new PlayerTurn(this.grid, currentGame.Player1.Name, currentGame.Player2.Name);
        }

        //public void ShowPopup(object sender, string uitkomst)
        //{
        //    if(uitkomst == "fout")
        //    {
        //        foutpop.IsOpen = true;

        //        DispatcherTimer timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(1);
        //        timer.Start();
        //        timer.Tick += delegate (object senders, EventArgs e)
        //        {
        //            ((DispatcherTimer)timer).Stop();
        //            if (foutpop.IsOpen)
        //            {
        //                foutpop.IsOpen = false;
        //            }
        //        };
        //    }

        //    if (uitkomst == "goed")
        //    {
        //        goedpop.IsOpen = true;

        //        DispatcherTimer timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(1);
        //        timer.Start();
        //        timer.Tick += delegate (object senders, EventArgs e)
        //        {
        //            ((DispatcherTimer)timer).Stop();
        //            if (goedpop.IsOpen)
        //            {
        //                goedpop.IsOpen = false;
        //            }
        //        };
        //    }

        //    if (uitkomst == "dubbelKlik")
        //    {
        //        dubbelKlik.IsOpen = true;

        //        DispatcherTimer timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(2);
        //        timer.Start();
        //        timer.Tick += delegate (object senders, EventArgs e)
        //        {
        //            ((DispatcherTimer)timer).Stop();
        //            if (dubbelKlik.IsOpen)
        //            {
        //                dubbelKlik.IsOpen = false;
        //            }
        //        };
        //    }

        //    if (uitkomst == "alGeklikt")
        //    {
        //        alGeklikt.IsOpen = true;

        //        DispatcherTimer timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(2);
        //        timer.Start();
        //        timer.Tick += delegate (object senders, EventArgs e)
        //        {
        //            ((DispatcherTimer)timer).Stop();
        //            if (alGeklikt.IsOpen)
        //            {
        //                alGeklikt.IsOpen = false;
        //            }
        //        };
        //    }
        //}
    }
}

