using MemoryProject.Models;
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
using Newtonsoft.Json;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for LoadGame.xaml
    /// </summary>
    public partial class LoadGame : Page
    {
        public LoadGame()
        {
            InitializeComponent();

            DirectoryInfo dir = new DirectoryInfo(@"Storage/");
            int counter = 0;
            foreach (var file in dir.GetFiles("*.txt"))
            {
                string content = File.ReadAllText(@"Storage/" + file.Name);
                Game savedGame = JsonConvert.DeserializeObject<Game>(content);
                savedGames.RowDefinitions.Add(new RowDefinition());
                Label dateLabel = new Label();
                dateLabel.Content = savedGame.Date;
                System.Windows.Controls.Grid.SetRow(dateLabel, counter);
                savedGames.Children.Add(dateLabel);
                counter++;
            }


        }
    }
}
