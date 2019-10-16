using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MemoryProject
{
    class MemoryGrid
    {
        private Grid grid;
        public RowDefinition row;
        public ColumnDefinition col;

        public void doGrid() { }

        public MemoryGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            InitializeGameGrid(cols, rows);
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
