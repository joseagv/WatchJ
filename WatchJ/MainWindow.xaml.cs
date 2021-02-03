using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            myTitle = this.Title;
            data = new DataXml(@".\Data\Data.xml");
            log = new UpdateLog();
            VersionL.Content = "v. " + myVersion;
            CfgMpvTB.Text = data.MpvPath();
            LoadVids();
            updateLogTB.Text = log.Log;
            var task0 = Task.Run((Func<Task>)Version);
            task0.Wait();
            CheckVersion();
        }

        //////////////////////////////////
        //          Methos             //
        ////////////////////////////////

        private void LoadVids()
        {
            dgMyList.ItemsSource = data.UserList();
        }

        public void GetUrl(string url)
        {
            streamUrl = url;
            if (!streamUrl.ToLower().Equals(""))
            {
                Command cmd = new Command(streamUrl, data.MpvPath());
                cmd.Start();
                this.Title = myTitle + " - Status: Starting ...";
                System.Threading.Thread.Sleep(1000);
                if (!cmd.IsRunning())
                {
                    MessageBox.Show("The process isn't running! Check the config and requeriments. ", "TomatoTV Alert!");
                    this.Title = myTitle + " - Status: Failed";
                }
                else
                {
                    this.Title = myTitle + " - Status: Watching [ " + url + " ]";
                }
            }
            else
            {
                MessageBox.Show("You need a url!", "TomatoTV Alert!");
            }
            streamUrl = "";
        }

        private static async Task Version()
        {
            await UploaderApi.CheckVersion();
            newVersion = UploaderApi.VersionDbx;
                
        }

        private void CheckVersion()
        {
            if (!newVersion.Equals(myVersion))
                VersionCheckL.Content = "There is a new version (" + newVersion + ").";
        }


        private void StreamStart(User row_list)
        {
            if (!streamUrl.ToLower().Equals(""))
            {
                Command cmd = new Command(streamUrl, data.MpvPath());
                cmd.Start();
                this.Title = myTitle + " - Status: Starting ...";
                System.Threading.Thread.Sleep(1000);
                if (!cmd.IsRunning())
                {
                    MessageBox.Show("The process isn't running! Check the config and requeriments. ", "TomatoTV Alert!");
                    this.Title = myTitle + " - Status: Failed";
                }
                else
                {
                    this.Title = myTitle + " - Status: Watching [ " + row_list.Name + " - " + row_list.Title + " ]";
                }
            }
            else
            {
                MessageBox.Show("You need to select a row with a url!", "TomatoTV Alert!");
            }
        }

        //////////////////////////////////
        //          Buttons            //
        ////////////////////////////////

        private void SaveMvpPathB(object sender, RoutedEventArgs e)
        {
            data.SaveMpv(CfgMpvTB.Text);
            MessageBox.Show("Saved!", "TomatoTV Alert!");
        }

        private void Button_Click_Watch(object sender, RoutedEventArgs e)
        {
            WatchWindow watchWindow = new WatchWindow();
            watchWindow.Show();

        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            if (dgMyList.SelectedItem == null)
            {
                MessageBox.Show("You need to select a row!", "TomatoTV Alert!");
            }
            else
            {
                try
                {
                    var row_list = (User)dgMyList.SelectedItem;
                    streamUrl = row_list.Url;
                    StreamStart(row_list);
                }
                catch { }
            }
            streamUrl = "";
        }

        private void Button_Click_Play1(object sender, RoutedEventArgs e)
        {
            if (dgSearch.SelectedItem == null)
            {
                MessageBox.Show("You need to select a row!", "TomatoTV Alert!");
            }
            else
            {
                try
                {
                    var row_list = (User)dgSearch.SelectedItem;
                    streamUrl = row_list.Url;
                    StreamStart(row_list);
                }
                catch { }
            }
            streamUrl = "";
        }

        private void Button_Click_Play_List(object sender, RoutedEventArgs e)
        {
            if (dgMyList.SelectedItems == null)
            {
                MessageBox.Show("You need to select more than a row!", "TomatoTV Alert!");
            }
            else if (dgMyList.SelectedItems.Count < 2 )
            {
                MessageBox.Show("You need to select more than a row!", "TomatoTV Alert!");
            }
            else
            {
                try
                {
                    List<User> myList = dgMyList.SelectedItems.Cast<User>().ToList();
                    foreach (User item in myList)
                    {
                        streamUrl += item.Url + " ";
                    }
                    if (!streamUrl.ToLower().Equals(""))
                    {
                        Command cmd = new Command(streamUrl, data.MpvPath());
                        cmd.Start();
                        this.Title = myTitle + " - Status: Starting ...";
                        System.Threading.Thread.Sleep(1000);
                        if (!cmd.IsRunning())
                        {
                            MessageBox.Show("The process isn't running! Check the config and requeriments. ", "TomatoTV Alert!");
                            this.Title = myTitle + " - Status: Failed ...";
                        }
                        else
                        {
                            this.Title = myTitle + " - Status: Watching [ Play list ... ]";
                        }
                    }
                    else
                    {
                        MessageBox.Show("You need to select a row with a url!", "TomatoTV Alert!");
                    }
                }
                catch { }
            }
            streamUrl = "";
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            List<User> auxList = new List<User>();
            int cont = 0;
            foreach (User items in dgMyList.Items.OfType<User>().ToList())
            {
                User auxUser = new User() { Id = cont++, Name = items.Name, Url = items.Url, Title = items.Title, Comment = items.Comment };
                if (auxUser.Url != null)
                {
                    auxList.Add(auxUser);
                }
            }
            data.SaveData(auxList);
            dgMyList.ItemsSource = null;
            dgMyList.ItemsSource = data.UserList();
            MessageBox.Show("All items have been saved!", "TomatoTV Alert!");
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            List<User> userSearchList = (from vid in data.UserList()
                               where vid.Name.ToLower().Contains(searchTB.Text.ToLower()) ||
                               vid.Title.ToLower().Contains(searchTB.Text.ToLower()) ||
                               vid.Url.ToLower().Contains(searchTB.Text.ToLower()) ||
                               vid.Comment.ToLower().Contains(searchTB.Text.ToLower())
                               select vid).ToList();
            dgSearch.ItemsSource = userSearchList;
        }

        private void Button_Click_Open_MPVPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog;
            if (!File.Exists(data.MpvPath()))
            {
                openFileDialog = new OpenFileDialog
                {
                    Filter = "mpv player |mpv.exe"
                };
            }
            else
            {
                openFileDialog = new OpenFileDialog
                {
                    Filter = "mpv player |mpv.exe",
                    InitialDirectory = @"" + data.MpvPath()
                };
            }
            if (openFileDialog.ShowDialog() == true)
            {
                data.SaveMpv(openFileDialog.FileName);
                CfgMpvTB.Text = openFileDialog.FileName;
            }
               
        }
    }
}
