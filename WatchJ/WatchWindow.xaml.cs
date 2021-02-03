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
using System.Windows.Shapes;

namespace WatchJ
{
    /// <summary>
    /// Lógica de interacción para WatchWindow.xaml
    /// </summary>
    public partial class WatchWindow : Window
    {
        public WatchWindow()
        {
            InitializeComponent();
            urlTB.Focusable = true;
            urlTB.Focus();
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GetUrl(urlTB.Text);
            this.Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
