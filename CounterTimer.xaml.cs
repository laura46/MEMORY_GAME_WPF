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
        private DispatcherTimer Timer;
        public CounterTimer()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += TickTock;
            Timer.Start();
        }

        private void TickTock(object sender, EventArgs e)
        {
            if (time > 8)
            {
                time++;
                tCounter.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
            }
            else
            {
                time++;
                tCounter.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);
            }
        }

        private void ResetTimer()
        {
            
        }
    }
}
