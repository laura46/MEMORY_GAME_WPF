﻿using System;
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
    /// Interaction logic for MenuNavbar.xaml
    /// </summary>
    public partial class MenuNavbar : UserControl
    {
        public MenuNavbar()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new MainMenu();
        }
    }
}
