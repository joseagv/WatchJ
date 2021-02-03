using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;

namespace WatchJ
{
    public static class UploaderApi
    {
        readonly static DropboxClient _Dbx = new DropboxClient("");
        static string _VersionDbx = "";

        private static DropboxClient Dbx { get => _Dbx;}
        public static string VersionDbx { get => _VersionDbx; } 

        public static async Task  CheckVersion()
        {
            var list = await Dbx.Files.ListFolderAsync(string.Empty);

            var version = (from item in list.Entries
                           where item.Name.ToLower().Contains("version")
                           select item).SingleOrDefault();
            string outString = version.Name.ToLower().Replace("version", "").Replace("-", ".").Replace(".txt", "");
            _VersionDbx = outString;
        }

        public static async Task DownloadUpdateFiles()
        {
            var list = await Dbx.Files.ListFolderAsync(@"/Update");
            var fileList = (from item in list.Entries
                            where !item.Name.ToLower().Contains("version")
                            select item).ToList();

            Console.WriteLine("New Version: " + VersionDbx);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("New files:");
            foreach (var item in fileList)
            {
                Console.WriteLine("-- [ " + item.Name.ToString() + " ]");
            }
            Console.WriteLine("-------------------------------------------------");

            foreach (var item in fileList)
            {
                using (var response = await Dbx.Files.DownloadAsync("/Update/" + item.Name))
                {
                    using (var fileStream = File.Create(Environment.CurrentDirectory + @"\" + item.Name))
                    {
                        (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                        Console.WriteLine("Downloading: " + response.Response.Name);
                    }
                }
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Finished.");
        }

        public static async Task DownloadInstallFiles()
        {
            var list = await Dbx.Files.ListFolderAsync(@"/Install", true);
            var fileList = (from item in list.Entries
                            where item.Name.ToLower().Contains(".zip")
                            select item).ToList();

            Console.WriteLine("Version: " + VersionDbx);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Install files:");
            foreach (var item in fileList)
            {
                Console.WriteLine("-- [ " + item.Name.ToString() + " ]");
            }
            Console.WriteLine("-------------------------------------------------");

            foreach (var item in fileList)
            {
                using (var response = await Dbx.Files.DownloadAsync("/Install/" + item.Name))
                {
                    using (var fileStream = File.Create(Environment.CurrentDirectory + @"\" + item.Name))
                    {
                        (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                        Console.WriteLine("Downloading: " + response.Response.Name);
                    }
                }
                Console.WriteLine("Uncompressing: " + item.Name);
                ZipFile.ExtractToDirectory(Environment.CurrentDirectory + "\\" + item.Name, Environment.CurrentDirectory);
                File.Delete(Environment.CurrentDirectory + "\\" + item.Name);
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Finished.");
        }
    }
}
