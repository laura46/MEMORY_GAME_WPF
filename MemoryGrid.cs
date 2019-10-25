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
        private Grid grid;
        private MainWindow.PLAYFIELD_SIZE GridSize;

        int nrOfClickedCards = 0;

        public MemoryGrid(Grid grid, MainWindow.PLAYFIELD_SIZE gridSize)
        {
            this.grid = grid;
            this.GridSize = gridSize;

            InitializeGameGrid();
            AddImages();
        }

        //Zet alle kaartjes als Achterkant kaart.
        private void AddImages()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < (int)GridSize; row++)
            {
                for (int column = 0; column < (int)GridSize; column++)
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
            Start:
            if (nrOfClickedCards < 2)
            {
                nrOfClickedCards++;
                Image card = (Image)sender;
                ImageSource front = (ImageSource)card.Tag;
                card.Source = front;

                if (nrOfClickedCards == 2)
                {
                    List<ImageSource> images = GetImagesList();
                    //TODO: vergelijk twee aangeklikte kaartjes!!!
                    MessageBox.Show("max aantal");

                    for (int row = 0; row < (int)GridSize; row++)
                    {
                        for (int column = 0; column < (int)GridSize; column++)
                        {
                            if (card.Source == front)
                            {
                                card.Source = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
                            }
                        }
                    }
                    nrOfClickedCards = 0;
                    goto Start;
                }
                //TODO: als twee aangeklikte kaartjes gelijk zijn, haal weg
                //TODO: als twee aangeklikte kaartjes NIET gelijk zijn, draai ze weer om.
            }
        }

         private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i= 0; i < (GridSize * GridSize); i++)
            {
                int imageNr = i % 8 + 1;
                ImageSource source = new BitmapImage(new Uri("Kaartjes/" + imageNr + ".png", UriKind.Relative));
                images.Add(source);
            }
            //shuffle!
            Random random = new Random();
            for (int i = 0; i < (GridSize * GridSize); i++)
            {
                int r = random.Next(0, (GridSize * GridSize));
                ImageSource source = images[r];
                images[r] = images[i];
                images[i] = source;
            }
             return images;
        }

        //Maakt grid aan.
        private void InitializeGameGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < GridSize; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


        public void MemoryGrid2()
        {
        }
        private void Button1_Click(object sender, RoutedEventArgs e)

        {

            //Code to Restart WPF Application

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();


            //Start New Application Before Closing Current

            Process.Start(Application.ResourceAssembly.Location);



            //Close the Current

            Application.Current.Shutdown();

        }
    }
}
