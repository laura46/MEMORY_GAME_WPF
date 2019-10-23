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

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            InitializeGameGrid(cols, rows);
            AddImages(cols, rows);
        }

        private void AddImages(int kolom, int rij)
        {
            for (int row = 0; row < rij; row++)
            {
                for (int column = 0; column < kolom; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Kaartjes/Achterkant.png", UriKind.Relative));
                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);
                }
            }
        }

        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Test");
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
