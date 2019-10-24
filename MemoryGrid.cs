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

namespace MemoryProject
{
    class MemoryGrid
    {
        private Grid grid;
        const int rows = 4;
        const int cols = 4;
        int nrOfClickedCards = 0;
        int previousCard;

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            InitializeGameGrid(cols, rows);
            AddImages(cols, rows);
        }

        //Zet alle kaartjes als Achterkant kaart
        private void AddImages(int kolom, int rij)
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rij; row++)
            {
                for (int column = 0; column < kolom; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
                    backgroundImage.Tag = images.First();
                    images.RemoveAt(0);
                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);
                }
            }
        }

        //Draait geklikte kaartjes om.
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            if (nrOfClickedCards < 2)
            {
                nrOfClickedCards++;
                Image card = (Image)sender;
                ImageSource front = (ImageSource)card.Tag;
                card.Source = front;

                if (nrOfClickedCards == 2)
                {
                    //TODO: vergelijk twee aangeklikte kaartjes!!!
                    MessageBox.Show("max aantal");
                    //TODO: terug naar achterkant moet ook voor de vorige kaart gelden.
                    ImageSource back = (ImageSource)new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
                    card.Source = back;
                }
                //TODO: als twee aangeklikte kaartjes gelijk zijn, haal weg
                //TODO: als twee aangeklikte kaartjes NIET gelijk zijn, draai ze weer om.
            }
        }

            private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i= 0; i < (rows * cols); i++)
            {
                int imageNr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Kaartjes/" + imageNr + ".png", UriKind.Relative));
                images.Add(source);
            }
            //shuffle
            Random random = new Random();
            for (int i = 0; i < (rows * cols); i++)
            {
                int r = random.Next(0, (rows * cols));
                ImageSource source = images[r];
                images[r] = images[i];
                images[i] = source;
            }

                return images;

        }
        private void InitializeGameGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


        public void MemoryGrid2()
        {
        }
    }
}
