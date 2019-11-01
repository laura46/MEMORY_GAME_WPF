﻿using MemoryProject.Models;
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
    /// Interaction logic for EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Page
    {
        public EndScreen(Player player1, Player player2, TimeSpan time)
        {
            InitializeComponent();
            player1Frame.Content = player1.Name + " " + player1.Score + " " + player1.Powerups.ToString();
            player2Frame.Content = player2.Name + " " + player2.Score + " " + player2.Powerups.ToString();
            timerFrame.Content = time;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new MainMenu();
        }
    }
}