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
    /// Interaction logic for CounterTimer.xaml
    /// </summary>
    public partial class CounterTimer : UserControl
    {
        private int time = 0;
        private DateTime _myDateTime;
        DispatcherTimer timer;

        public CounterTimer()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TickTock;
            _myDateTime = DateTime.Now;
            timer.Start();
        }

        private void TickTock(object sender, EventArgs e)
        {
            time++;

            var diff = DateTime.Now.Subtract(_myDateTime);
            tCounter.Text = diff.ToString(@"hh\:mm\:ss");
        }
        
        private void TimerReset()
        {
            timer.Stop();
        }
    }
}
