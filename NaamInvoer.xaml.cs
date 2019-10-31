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
        public EventHandler<String> OnInputNames;
        private string Player1;
        private string Player2;
        public NaamInvoer()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
         Player1 = Textbox1.Text;

        
        }
        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
           

            Player2 = Textbox2.Text;
           
        }




        private void GoToSizeOptions(object sender, RoutedEventArgs e)
        {
            OnGoToSizeOptions?.Invoke(this, EventArgs.Empty);
            OnInputNames?.Invoke(this, Player1 + ";" + Player2);
        }
    }
}
