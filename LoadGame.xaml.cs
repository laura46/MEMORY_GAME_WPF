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
        
        string path = @"Storage/Load";
        DirectoryInfo storageDirectory;
        private EventHandler<Game> onLoadGame;

        internal EventHandler<Game> OnLoadGame { get => onLoadGame; set => onLoadGame = value; }

        public LoadGame()
        {
            InitializeComponent();
            InitializeSavedGames();
            navbarFrame.Content = new MenuNavbar();
        }
        private void InitializeSavedGames() 
        {
            storageDirectory = new DirectoryInfo(path);
            if (!storageDirectory.Exists)
            {
                Directory.CreateDirectory(path);
            }
            int counter = 0;
            foreach (var file in storageDirectory.GetFiles("*.txt"))
            {
                string content = File.ReadAllText(path + file.Name);
                Game savedGame = JsonConvert.DeserializeObject<Game>(content);
                
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(80)
                };

                savedGames.RowDefinitions.Add(row);
                Label game = new Label
                {
                    Content = savedGame.Player1.Name + " en " + savedGame.Player2.Name + " " + savedGame.Date,
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(10),
                    Cursor = Cursors.Hand,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Uid = file.Name
                    
                };
                game.MouseDown += Game_MouseDown;
                
                System.Windows.Controls.Grid.SetRow(game, counter);
                savedGames.Children.Add(game);
                counter++;
            }
        }

        private void Game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var file in storageDirectory.GetFiles())
            {
                Label lbl = (Label)sender;
                if (file.Name == lbl.Uid)
                {
                    var fileContent = File.ReadAllText(path + file.Name);
                    Game gameToLoad = JsonConvert.DeserializeObject<Game>(fileContent);
                    OnLoadGame?.Invoke(this, gameToLoad);
                    return;
                }
                return;
            }
        }
    }
}
