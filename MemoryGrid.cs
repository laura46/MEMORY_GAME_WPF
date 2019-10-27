using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace MemoryProject
{
    class MemoryGrid
    {
        //Grid size
        private Grid grid;
        private GridSizeOptions.GRID_SIZES GridSize;

        //Class voor kaarten
        private List<Card> cards = new List<Card>();

        //Variabelen voor CardClick
        private int nrOfClickedCards = 0;
        private int previousCard;
        public MemoryGrid(Grid grid, GridSizeOptions.GRID_SIZES gridSize)
        {
            this.grid = grid;
            this.GridSize = gridSize;
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
                    grid.Children.Add(image);
                }
            }
        }

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
                    //TODO: bugfix controleer kaartjes
                    if (cards[previousCard].Show() == cards[index].Show())
                    {
                        cards[previousCard].MakeInvisible();
                        cards[index].MakeInvisible();
                        MessageBox.Show("GOED!");
                    }
                    else
                    {
                        cards[previousCard].FlipToBack();
                        cards[index].FlipToBack();
                        MessageBox.Show("Fout!");
                    }
                    nrOfClickedCards = 0;
                }
                else
                    previousCard = index;
                ShowCards();
            }
        }

        //Maakt lijst met voorkanten aan.
        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();

            for (int i= 0; i < ((int)GridSize * (int)GridSize); i++)
            {
                int imageNr = i % 8 + 1;
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
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < (int)GridSize; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


    }
}
