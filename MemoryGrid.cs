using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
using System.Windows.Diagnostics;
using MemoryProject.Models;

namespace MemoryProject
{
    public class MemoryGrid
    {
        //Grid size
        private System.Windows.Controls.Grid Grid;
        private GridSizeOptions.GRID_SIZES GridSize;

        //Class voor kaarten
        private List<Card> cards = new List<Card>();


        //Variabelen voor CardClick
        private int nrOfClickedCards = 0;
        private int previousCard;
        private bool turned;

        //variabelen voor de score
        bool player1turn = true;
        public string player1Name;
        public string player2Name;
        public EventHandler<bool> OnPlayerTurn;
        int score1 = 0;
        int score2 = 0;
        public EventHandler<int> OnScore1Update;
        public EventHandler<int> OnScore2Update;
        public int AmountOfWins1;
        public int AmountOfWins2;
        public EventHandler<Dictionary<string, int>> OnPowerUpUpdate;
        public EventHandler<bool> OnEndGame;

        //public EventHandler<string> OnPairMade;
        public EventHandler<string> OnOpenCardClicked;
        DispatcherTimer timer = new DispatcherTimer();


        public MemoryGrid(System.Windows.Controls.Grid grid, GridSizeOptions.GRID_SIZES gridSize)
        {
            Grid = grid;
            GridSize = gridSize;
            InitializeGameGrid();
            AddImages();
            ShowCards();

        }

        //Zet alle kaartjes als Achterkant kaart.
        private void AddImages()
        {
            List<ImageSource> images = GetImagesList();
            for (int i = 0; i < (int)GridSize; i++)
            {
                for (int j = 0; j < (int)GridSize; j++)
                {
                    Image image = new Image();
                    image.Source = images.First();
                    cards.Add(new Card(image));
                    images.RemoveAt(0);
                }
            }
        }

        private void ShowCards()
        {
            for (int i = 0; i < (int)GridSize; i++)
            {
                for (int j = 0; j < (int)GridSize; j++)
                {
                    Image image = new Image();
                    image.MouseDown += new MouseButtonEventHandler(CardClick);
                    image.Source = cards[j * (int)GridSize + i].Show();
                    image.Tag = j * (int)GridSize + i;
                    System.Windows.Controls.Grid.SetColumn(image, j);
                    System.Windows.Controls.Grid.SetRow(image, i);
                    this.Grid.Children.Add(image);
                }
            }
        }


        //Draait geklikte kaartjes om.
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            if (nrOfClickedCards < 2)
            {
                Image image = (Image)sender;
                int index = (int)image.Tag;
                image.Source = null;
                cards[index].Clicked();
                nrOfClickedCards++;

                if (nrOfClickedCards == 2)
                {
                    ShowCards();
                    CheckPair( cards[previousCard], cards[index]);

                    nrOfClickedCards = 0;
                }
                previousCard = index;
                ShowCards();
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
        }

        //Controlleerd of de kaartjes dezelfde naam hebben.
        private void CheckPair(Card kaart1, Card kaart2)
        {
            string kaart1String = kaart1.Show().ToString();
            string kaart2String = kaart2.Show().ToString();

            //Als het een duplicatie is van de uri (waarbij er een extentie op de naam komt) wordt deze eraf gehaald.
            if (kaart1String.Contains("pack"))
            {
                kaart1String = Regex.Split(kaart1String, @"(;component/)")[2];
            }

            if (kaart2String.Contains("pack"))
            {
                kaart2String = Regex.Split(kaart2String, @"(;component/)")[2];
            }

            if (kaart1.Show().ToString() == kaart2.Show().ToString())
            {
                MessageBox.Show("Je kunt niet twee keer dezelfde kaart aanklikken!");
                kaart1.FlipToBack();
                return;
            }

            Dictionary<string, int> powerup = new Dictionary<string, int>();
            if (kaart1String == kaart2String)
            {
                kaart2.MakeInvisible();
                kaart1.MakeInvisible();
                kaart2.Correct();
                kaart1.Correct();
                MessageBox.Show("Goed!");
                if (player1turn == true)
                {
                    score1 += 200;
                    UpdateScore(score1, player1turn);
                    AmountOfWins1 += 1;
                    
                    powerup.Add("player1", AmountOfWins1);
                    OnPowerUpUpdate?.Invoke(this, powerup);
                }
                if (player1turn == false)
                {
                    score2 += 200;
                    UpdateScore(score2, player1turn);
                    AmountOfWins2 += 1;

                    powerup.Add("player2", AmountOfWins2);
                    OnPowerUpUpdate?.Invoke(this, powerup);
                }
                powerup = new Dictionary<string, int>();
                if (IsGameFinished()) { EndGame(); }
            }
            else
            {
                MessageBox.Show("Fout!");
                kaart2.FlipToBack();
                kaart1.FlipToBack();
                
                SetPlayerTurn(!player1turn);
            }
        }
        private void OnClickOpenPair(object sender, Image image)
        {
            MessageBox.Show("Deze kaart is deel van een goed paar.");
            return;
        }

        
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
            
            //player1 en 2 namen, score, timer tijd, 

        }

        //Controlleerd of het spel over is.
        private bool IsGameFinished()
        {
            List<Card> clickedCards = new List<Card>();
            foreach (var card in cards)
            {
                if (card.clicked)
                {
                    clickedCards.Add(card);
                }
            }
            return (clickedCards.Count == cards.Count) ? true : false;
        }

        //Houdt de player turn bij.
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