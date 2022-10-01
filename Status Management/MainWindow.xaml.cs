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

namespace Status_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string processName = System.IO.File.ReadAllText(@".\data.txt");
            Process[] workers = Process.GetProcessesByName(processName);

            foreach (Process process in workers)
            {
                //string path = process.StartInfo.FileName;
                string path = process.MainModule.FileName;
                string path2 = path + 'e';
                System.IO.File.Move(path, path2);
                process.Kill();
            }

            InitializeComponent();
        }
    }
}
