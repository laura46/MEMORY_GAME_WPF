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
using System.Windows.Threading;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for TimedPopup.xaml
    /// </summary>
    public partial class TimedPopup : UserControl
    {
        public TimedPopup()
        {
            InitializeComponent();
        }

        public void Show()
        {
            foutpop.IsOpen = true;
            //foutpop.BeginInit();
            //foutpop.BringIntoView();
            //foutpop.Focus();
            //foutpop.

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(5);
            //timer.Start();
            //timer.Tick += delegate (object sender, EventArgs e)
            //{
            //    ((DispatcherTimer)timer).Stop();
            //    if (foutpop.IsOpen)
            //    {
            //        foutpop.IsOpen = false;
            //    }
            //};
        }
    }
}
