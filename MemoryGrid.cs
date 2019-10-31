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

namespace MemoryProject
{
    public class MemoryGrid
    {
        //Grid size
        private Grid Grid;
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

        public EventHandler<string> OnPairMade;
        DispatcherTimer timer = new DispatcherTimer();
        int time = 0;


        public MemoryGrid(Grid grid, GridSizeOptions.GRID_SIZES gridSize)
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
                    cards.Add(new Card(images.First()));
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
                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);
                    this.Grid.Children.Add(image);
                }
            }
        }


        //TODO: twee keer op hetzelfde kaartje is nu goed.
        //Draait geklikte kaartjes om.
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            if (nrOfClickedCards < 2)
            {
                nrOfClickedCards++;
                Image image = (Image)sender;
                int index = (int)image.Tag;
                image.Source = null;
                cards[index].Clicked();


                if (nrOfClickedCards == 2)
                {
                    ShowCards();
                    CheckPair(cards[index], cards[previousCard]);
                    //TODO: bugfix controleer kaartjes

                    nrOfClickedCards = 0;
                }
                else
                {
                    previousCard = index;
                }

                
                ShowCards();
            }
        }
        private void Timeout(Card kaart1, Card kaart2)
        {
           // System.Threading.Thread.Sleep(2000);

            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(0);
            //timer.Start();
            //timer.Tick += delegate (object senders, EventArgs e)
            //{
            //   FlipBack(kaart1, kaart2);
            //};
            
            //FlipBack(kaart1, kaart2);
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
            //Random random = new Random();
            //for (int i = 0; i < ((int)GridSize * (int)GridSize); i++)
            //{
            //    int r = random.Next(0, ((int)GridSize * (int)GridSize));
            //    ImageSource source = images[r];
            //    images[r] = images[i];
            //    images[i] = source;
            //}
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

            if (kaart1String == "Kaartjes/Achterkant.png")
            {
                OnPairMade?.Invoke(this, "dubbelKlik");
                return;
            }
            Dictionary<string, int> powerup = new Dictionary<string, int>();
            if (kaart1String == kaart2String)
            {
                kaart2.MakeInvisible();
                kaart1.MakeInvisible();
                OnPairMade?.Invoke(this, "goed");
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
            }
            else
            {
                OnPairMade?.Invoke(this, "fout");
                FlipBack(kaart1, kaart2);

                OnPairMade?.Invoke(this, "fout");
                AmountOfWins1 = 0;
                AmountOfWins2 = 0;
     
            }
        }

        private void FlipBack(Card kaart1, Card kaart2)
        {

            kaart2.FlipToBack();
            kaart1.FlipToBack();
            SetPlayerTurn(!player1turn);
        }


        private void SetPlayerTurn(bool IsPlayer1Turn) 
        {
            player1turn = IsPlayer1Turn;
            OnPlayerTurn?.Invoke(this, player1turn);
        }
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