using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WatchJ
{
    public class Command
    {
        private ProcessStartInfo _Cmd;
        Process _InstancedCmd;

        public Command(string url, string mpvPath)
        {
            string param = "";
            mpvPath = mpvPath.Replace("\\", "\\\\");
            param = "/c \"" + mpvPath + "\" " + url + " --ytdl --ytdl-format=303+bestaudio/best[ext=webm]/best ";
            _Cmd = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = param
            };       
        }

        public void Start()
        {
            _InstancedCmd = Process.Start(_Cmd);
        }

        public bool IsRunning()
        {
            if (_InstancedCmd == null)
            {
                return false;
            }
            else
            {
                try
                {
                    Process.GetProcessById(_InstancedCmd.Id);
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
