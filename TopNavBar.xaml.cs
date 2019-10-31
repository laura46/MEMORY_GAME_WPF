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
    /// Interaction logic for TopNavBar.xaml
    /// </summary>
    public partial class TopNavBar : UserControl
    {
        public EventHandler OnResetGame;
        public EventHandler OnSaveGame;
        public TopNavBar()
        {
            InitializeComponent();
        }

        private void ResetGame(object sender, MouseButtonEventArgs e)
        {
            OnResetGame?.Invoke(this, EventArgs.Empty);
        }
        private void SaveGame(object sender, MouseButtonEventArgs e)
        {
            OnSaveGame?.Invoke(this, EventArgs.Empty);
        }
        private void LoadGame(object sender, MouseButtonEventArgs e)
        {

        }
        private void BackToMainMenu(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Content = new MainMenu();
        }
    }
}
