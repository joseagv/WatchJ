using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ
{
    class Program
    {
        private static int _Opt = -1;
        private static string _Ver = "";

        static void Main(string[] args)
        {
            var task0 = Task.Run((Func<Task>)Version);
            task0.Wait();
            Menu();
            Console.ReadLine();
        }

        private static void Menu()
        {
            do
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("TomatoTV - Updater - Newer version: " + _Ver);
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Options:");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Install the whole application.");
                Console.WriteLine("2. Update the application.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Write an option:");
                try
                {
                    _Opt = Int32.Parse(Console.ReadLine());
                    OptionSelector();
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Write a suitable option.");
                    Console.Clear();
                    _Opt = -1;
                }
            } while (_Opt == -1);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("");
        }

        private static void OptionSelector()
        {
            switch (_Opt)
            {
                case 1:
                    var task1 = Task.Run((Func<Task>)Install);
                    task1.Wait();
                    break;
                case 2:
                    var task2 = Task.Run((Func<Task>)DownLoad);
                    task2.Wait();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
            _Opt = -1;
        }

        private static async Task Version()
        {
            await UploaderApi.CheckVersion();
            _Ver = UploaderApi.VersionDbx;
        }

        private static async Task Install()
        {
            await UploaderApi.DownloadInstallFiles();
            Console.WriteLine("The current version (1.0.3) is old , you should update the aplication. Newer version: " + _Ver);
        }

        private static async Task DownLoad()
        {
            await UploaderApi.DownloadUpdateFiles();
        }
    }
}
