using System;
using System.Collections.Generic;
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

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for Scorebord.xaml
    /// </summary>
    public partial class Scorebord : UserControl
    {
        public Scorebord(MemoryGrid currentGrid)
        {
            InitializeComponent();

            currentGrid.OnScore1Update += new EventHandler<int>(UpdateScore1);
            currentGrid.OnScore2Update += new EventHandler<int>(UpdateScore2);
        }

        private void UpdateScore1(object sender, int score) 
        {
            //hier heb je nu de score1
        }
        private void UpdateScore2(object sender, int score) 
        {
            //hier heb je nu de score2
        }
    }
}
