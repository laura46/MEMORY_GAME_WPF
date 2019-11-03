using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MemoryProject.Models;

namespace MemoryProject
{
    public class MemoryGrid
    {
        //Grid size
        public System.Windows.Controls.Grid Grid;
        private GridSizeOptions.GRID_SIZES GridSize;

        //Class voor kaarten
        private List<Card> cards = new List<Card>();

        //Variabelen voor CardClick
        private int nrOfClickedCards = 0;
        Image firstClickedImage;
        Image secondClickedImage;
        public EventHandler<string> OnPairMade;
        public EventHandler<string> OnOpenCardClicked;

        //variabelen voor de score
        bool player1turn = true;
        int score1 = 0;
        int score2 = 0;
        public string player1Name;
        public string player2Name;
        int player1ConsecutiveWins = 0;
        int player2ConsecutiveWins = 0;
        public EventHandler<bool> OnPlayerTurn;
        public EventHandler<int> OnScore1Update;
        public EventHandler<int> OnScore2Update;
        public EventHandler<Dictionary<string, int>> OnPowerUpUpdate;
        public EventHandler<String> OnPowerup;
        public EventHandler<bool> OnEndGame;

        public object BlockInput { get; private set; }

        public MemoryGrid(System.Windows.Controls.Grid grid)
        {
            Grid = grid;
            InitializeGameGrid();
        }
        public MemoryGrid(System.Windows.Controls.Grid grid, GridSizeOptions.GRID_SIZES gridSize)
        {
            Grid = grid;
            GridSize = gridSize;
            InitializeGameGrid();
        }


        //Maakt grid aan.
        private void InitializeGameGrid()
        {
            for (int i = 0; i < (int)GridSize; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < (int)GridSize; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            ShowCards();
        }

        //Kaartjes inladen.
        private void ShowCards()
        {
            List<ImageSource> images = GetImagesList();
            for (int i = 0; i < (int)GridSize; i++)
            {
                for (int j = 0; j < (int)GridSize; j++)
                {
                    Image image = new Image();
                    image.MouseDown += new MouseButtonEventHandler(CardClick);
                    image.Source = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
                    image.Uid = images.ElementAt(j * (int)GridSize + i).ToString();
                    image.Tag = j * (int)GridSize + i;
                    System.Windows.Controls.Grid.SetColumn(image, j);
                    System.Windows.Controls.Grid.SetRow(image, i);
                    this.Grid.Children.Add(image);
                }
            }
        }

        //Maakt lijst met voorkanten aan.
        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();

            for (int i = 0; i < ((int)GridSize * (int)GridSize); i++)
            {
                //imageNr formule moet per grid anders zijn anders krijgen we niet alle verschillende plaatjes
                int imageNr = i % ((int)GridSize * (int)GridSize / 2) + 1;
                ImageSource source = new BitmapImage(new Uri("Kaartjes/" + imageNr + ".png", UriKind.Relative));
                images.Add(source);
            }
            //shuffle!
            Random random = new Random();
            for (int i = 0; i < ((int)GridSize * (int)GridSize); i++)
            {
                int r = random.Next(0, ((int)GridSize * (int)GridSize));
                ImageSource source = images[r];
                images[r] = images[i];
                images[i] = source;
            }
            return images;
        }

        //Draait geklikte kaartjes om.
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            
            Image clickedImage = (Image)sender;

            if (clickedImage.Source.ToString().Contains("Achterkant.png"))
            {
                //eerste kaart klik
                if (nrOfClickedCards < 1)
                {
                    FlipCard(clickedImage, true);
                    firstClickedImage = clickedImage;
                    nrOfClickedCards++;
                }
                //tweede kaart klik
                else if (nrOfClickedCards == 1)
                {
                    FlipCard(clickedImage, true);
                    secondClickedImage = clickedImage;
                    DispatcherTimer timeout = new DispatcherTimer();
                    timeout.Interval = TimeSpan.FromMilliseconds(150);
                    timeout.Start();
                    timeout.Tick += delegate (object senders, EventArgs eventArgs)
                    {
                        timeout.Stop();
                        TimeoutCard();
                    };
                    nrOfClickedCards = 0;
                }else
                    FlipCard(clickedImage, true);
            }
            else
            {
                OnOpenCardClicked?.Invoke(this, "alGeklikt");
            }
        }

        //Gaat naar CheckPair.
        private void TimeoutCard()
        {
            CheckPair(firstClickedImage, secondClickedImage);
        }

        //Zet de benodigde kant van de kaart.
        private void FlipCard(Image imageToFlip, bool show)
        {
            if (show)
            {
                imageToFlip.Source = new BitmapImage(new Uri(imageToFlip.Uid, UriKind.Relative));
            } else
            {
                imageToFlip.Source = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
            }
            
        }

        //Controlleerd of de kaartjes dezelfde naam hebben.
        private void CheckPair(Image firstCard, Image secondCard)
        {
            System.Threading.Thread.Sleep(250);
            //check de strings
            string kaart1String = firstCard.Source.ToString();
            string kaart2String = secondCard.Source.ToString();

            //Als het een duplicatie is van de uri (waarbij er een extentie op de naam komt) wordt deze eraf gehaald.
            if (kaart1String.Contains("pack"))
            {
                kaart1String = Regex.Split(kaart1String, @"(;component/)")[2];
            }

            if (kaart2String.Contains("pack"))
            {
                kaart2String = Regex.Split(kaart2String, @"(;component/)")[2];
            }

            
            if (kaart1String == kaart2String)
            {
                OnPairMade?.Invoke(this, "goed");

                if (player1turn == true)
                {
                    score1 += 200;
                    UpdateScore(score1, player1turn);
                    HandlePowerup(player1turn, true);
                }
                else 
                {
                    score2 += 200;
                    UpdateScore(score2, player1turn);
                    HandlePowerup(player1turn, true);
                }
                
                if (IsGameFinished()) { EndGame(); }
            }
            else
            {
                OnPairMade?.Invoke(this, "fout");
                HandlePowerup(player1turn, false);
                FlipCard(firstCard, false);
                FlipCard(secondCard, false);
                SetPlayerTurn(!player1turn);
            }
        }

        private void HandlePowerup(bool isPlayer1Turn, bool win) 
        {
            if (win)
            {
                Dictionary<string, int> powerup = new Dictionary<string, int>();

                if (isPlayer1Turn)
                {
                    player1ConsecutiveWins += 1;
                    if (player1ConsecutiveWins == 2 || player1ConsecutiveWins == 4 || player1ConsecutiveWins == 6) 
                    {
                        powerup.Add("player1", player1ConsecutiveWins);
                        OnPowerUpUpdate?.Invoke(this, powerup);
                        OnPowerup?.Invoke(this, "powerup");
                    }
                }
                else
                {                   
                    player2ConsecutiveWins += 1;
                    if (player2ConsecutiveWins == 2 || player2ConsecutiveWins == 4 || player2ConsecutiveWins == 6) 
                    {
                        powerup.Add("player2", player2ConsecutiveWins);
                        OnPowerUpUpdate?.Invoke(this, powerup);
                        OnPowerup?.Invoke(this, "powerup");
                    }
                }
            }
            else 
            {
                player1ConsecutiveWins = 0;
                player2ConsecutiveWins = 0;
            }
        }

        //Gebeurt als het spel is afgelopen.
        private void EndGame()
        {
            List<Player> players = new List<Player>();
            Player player1 = new Player {
                Name = player1Name,
                Score = score1
            };
            Player player2 = new Player
            {
                Name = player2Name,
                Score = score2
            };
            players.Add(player1);
            players.Add(player2);
            OnEndGame?.Invoke(this, true); 

        }

        //Controlleerd of het spel is afgelopen.
        private bool IsGameFinished()
        {
            List<Image> imagesFacingDown = new List<Image>();
            foreach (var image in Grid.Children) 
            {
               
                Image thisImage = (Image)image;
                if (thisImage.Source.ToString().Contains("Achterkant.png")) 
                {
                    imagesFacingDown.Add(thisImage);
                }
            }
            
            return (imagesFacingDown.Count == 0) ? true : false;
        }

        //Houdt de player turn bij
        private void SetPlayerTurn(bool IsPlayer1Turn) 
        {
            player1turn = IsPlayer1Turn;
            OnPlayerTurn?.Invoke(this, player1turn);
        }

        //Houdt de score bij
        private void UpdateScore(int score, bool isScore1) 
        {
            if (isScore1) 
            {
                this.OnScore1Update?.Invoke(this, score);
            } else 
            {
                this.OnScore2Update?.Invoke(this, score);
            }
        }
    }
}