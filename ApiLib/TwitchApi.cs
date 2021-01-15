using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ.ApiLib
{
    //Didn´t use since you need a OAuth/Server 
    public class TwitchApi : IApiFactory
    {
        private static TwitchApi _Instance;

        public static TwitchApi GetInstance()
        {
            if(_Instance == null)
            {
                _Instance = new TwitchApi();
            }
            return _Instance;
        }

        public string Name { get; set; } = "Twitch";
        public string Key { get; set; } = "";

        public bool Connection()
        {
            throw new NotImplementedException();
        }
    }
}
