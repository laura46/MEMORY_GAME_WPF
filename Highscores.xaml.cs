using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MemoryProject.Models;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
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
    /// Interaction logic for Highscoress.xaml
    /// </summary>
    public partial class Highscores : Page
    {
        int maxValue;
        int plek = 1;
        int scores = 0;

        string path = @"Storage/Finish/";
        DirectoryInfo storageDirectory;
        public Highscores()
        {
            InitializeComponent();
            navbarFrame.Content = new MenuNavbar();
            DirectoryStorage();
            GetFiles();
        }

        private void DirectoryStorage()
        {
            storageDirectory = new DirectoryInfo(path);
            if (!storageDirectory.Exists)
            {
                Directory.CreateDirectory(path);
            }
        }

        private void GetFiles()
        {
            List<string> playerData = new List<string>();
            List<int> scoreList = new List<int>();
            int counter = 0;
            int topTien = 0;
            var regel = "";

            foreach (var file in storageDirectory.GetFiles("*.txt"))
            {
                string content = File.ReadAllText(path + file.Name);
                Game finishedGame = JsonConvert.DeserializeObject<Game>(content);
                Player[] players = { finishedGame.Player1, finishedGame.Player2 };
                foreach (var player in players)
                {
                    scores = player.Score ?? default(int); // zorgt ervoor dat alle scores nu in een int vorm zitten en gebruikt kunnen worden
                    scoreList.Add(scores);
                    scoreList.Sort();
                    scoreList.Reverse();


                    if (scoreList[0] > maxValue)
                    {
                        maxValue = scoreList.Max();
                    }
                    regel = " Naam: " + player.Name + " Score: " + scores + " Tijd: " + finishedGame.Grid.Timer.ToString().Split(new char[] { '.' })[0];
                    playerData.Add(regel);
                }
            }

            var ordered = playerData.Select(s => new { Str = s, Split = s.Split(' ') })
            .OrderByDescending(x => int.Parse(x.Split[4]))
            .ThenBy(x => x.Split[6])
            .Select(x => x.Str)
            .ToList();
            int indexData = 0;
            int maxAmount = (playerData.Count < 10) ? playerData.Count : 10;
            foreach (string data in playerData)
            {
                while (topTien < maxAmount)
                {
                    RowDefinition row = new RowDefinition
                    {
                        Height = new GridLength(67)
                    };


                    scoreGrid.RowDefinitions.Add(row);
                    Label game = new Label
                    {
                        Content = "#" + plek + ordered[indexData],
                        FontSize = 18,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Center

                    };
                    plek += 1;
                    indexData++;
                    System.Windows.Controls.Grid.SetRow(game, counter);
                    scoreGrid.Children.Add(game);
                    counter++;
                    topTien++;
                }
            }
            
            
        }

        //private void GetFiles()
        //{
        //    int counter = 0;
        //    foreach (var file in storageDirectory.GetFiles("*.txt"))
        //    {

        //        string content = File.ReadAllText(path + file.Name);
        //        Game finishedGame = JsonConvert.DeserializeObject<Game>(content);
        //        Player[] players = { finishedGame.Player1, finishedGame.Player2 };
        //        foreach (var player in players)
        //        {
        //            scores = player.Score ?? default(int); // zorgt ervoor dat alle scores nu in een int vorm zitten en gebruikt kunnen worden
        //            int[] scoreArray = { scores };

        //            Array.Sort(scoreArray);
        //            Array.Reverse(scoreArray);
        //            plek += 1;
        //            maxValue = scoreArray.Max();

        //            var regel = "#" + plek + " Naam: " + player.Name + " Score: " + scoreArray[0] + " Tijd: " + finishedGame.Grid.Timer.ToString().Split(new char[] { '.' })[0];

        //            RowDefinition row = new RowDefinition
        //            {
        //                Height = new GridLength(80)
        //            };


        //            scoreGrid.RowDefinitions.Add(row);
        //            Label game = new Label
        //            {
        //                Content = regel,
        //                FontSize = 20,
        //                FontWeight = FontWeights.Bold,
        //                Margin = new Thickness(10),
        //                HorizontalAlignment = HorizontalAlignment.Center

        //            };
        //            System.Windows.Controls.Grid.SetRow(game, counter);
        //            scoreGrid.Children.Add(game);
        //            counter++;

        //        }
        //    }
        //}
        //List<string> scorePlayer = new List<string>();
        //private void GetFiles()
        //{
        //    foreach (var file in storageDirectory.GetFiles("*.txt"))
        //    {
        //        string content = File.ReadAllText(path + file.Name);
        //        Game finishedGame = JsonConvert.DeserializeObject<Game>(content);
        //        Player[] players = { finishedGame.Player1, finishedGame.Player2 };
        //        foreach (var player in players)
        //        {
        //            scores = player.Score ?? default(int); // zorgt ervoor dat alle scores nu in een int vorm zitten en gebruikt kunnen worden
        //            int[] scoreArray = { scores };

        //            Array.Sort(scoreArray);
        //            Array.Reverse(scoreArray);
        //            plek += 1;
        //            maxValue = scoreArray.Max();

        //            var regel = "#" + plek + " Naam: " + player.Name + " Score: " + scoreArray[0] + " Tijd: " + finishedGame.Grid.Timer.ToString().Split(new char[] { '.' })[0];

        //            scorePlayer.Add(regel);
        //            //foreach txt file
        //            //foreach player
        //            //zet regel
        //            //store in list
        //        }
        //    }

        //}
        //private void CheckScores(string playerScores)
        //{
        //    int scores = 0;
        //    foreach(string score in scorePlayer)
        //    {
        //        //ga door alle items in de lijst
        //        // split op de score
        //        //
        //    }
        //}
    }
}


//        private void InitializeSavedGames()
//        {


//        }

//        private void Game_MouseDown(object sender, MouseButtonEventArgs e)
//        {
//            foreach (var file in storageDirectory.GetFiles())
//            {
//                Label lbl = (Label)sender;
//                if (file.Name == lbl.Uid)
//                {
//                    var fileContent = File.ReadAllText(path + file.Name);
//                    Game gameToLoad = JsonConvert.DeserializeObject<Game>(fileContent);
//                    OnLoadGame?.Invoke(this, gameToLoad);
//                    return;
//                }
//                return;
//            }
//        }
//    }
//}
