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

namespace WatchJ
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataXml data;
        private UpdateLog log;
        private string myVersion = "1.0.8";
        private static string newVersion;
        private string myTitle = "";
        private string streamUrl = "";

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
