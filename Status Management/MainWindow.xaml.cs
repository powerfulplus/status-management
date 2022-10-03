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
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace Status_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path;
        public string path2;
        public static string processName;
        private static Timer aTimer;
        private static MainWindow mainWindow;
        public string BgBrush { get; set; }

        public MainWindow()
        {
            // https://www.tutorialspoint.com/xaml/xaml_data_binding.htm
            mainWindow = this;
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 5000;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;

            path = System.IO.File.ReadAllText(@".\path.txt");
            path2 = path + 'e';
            processName = System.IO.Path.GetFileName(path);
            processName = processName.Substring(0, processName.Length - 4);
            Process[] workers = Process.GetProcessesByName(processName);

            foreach (Process process in workers)
            {
                //string path = process.StartInfo.FileName;
                //string path = process.MainModule.FileName;

                System.IO.File.Move(path, path2);
                process.Kill();
            }

            this.Topmost = true;
            // this.Background = Brushes.Blue;
            // this.Background = Brushes.Red;
            //BgBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
            BgBrush = "#FFEE1122";
            //InitializeComponent();
            // this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
            DataContext = this;
        }

        public void Window_Closing(object sender, EventArgs e)
        {
            System.IO.File.Move(path2, path);
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (Process.GetProcessesByName(processName).Length == 0)
            {
                // show sign
                mainWindow.BgBrush = "#FF0088cc";
                //mainWindow.BgBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x88, 0xcc));
            }
        }
    }
}
