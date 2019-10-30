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
    /// Interaction logic for NaamInvoer.xaml
    /// </summary>
    public partial class NaamInvoer : UserControl
    {
        public EventHandler OnGoToSizeOptions;
        public NaamInvoer()
        {
            InitializeComponent();
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            String Player1 = Textbox1.Text;
            String Player2 = Textbox2.Text;
        }

        private void GoToSizeOptions(object sender, RoutedEventArgs e)
        {
            OnGoToSizeOptions?.Invoke(this, EventArgs.Empty);
        }
    }
}
