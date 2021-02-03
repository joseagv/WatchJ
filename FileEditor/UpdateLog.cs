using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ
{
    public class UpdateLog
    {
        private string _Log;
        private readonly string _LogPath = @"\UpdatedLog.txt";

        public UpdateLog()
        {
            string configPath = Environment.CurrentDirectory.ToString() + LogPath;
            StreamReader file = new StreamReader(configPath);
            _Log = file.ReadToEnd();
            file.Close();
        }

        private string LogPath { get => _LogPath; }
        public string Log { get => _Log; set => _Log = value; }
    }
}
