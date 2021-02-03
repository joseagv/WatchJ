using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ.ApiLib
{
    interface IApiFactory
    {
        string Name { get; set; }
        string Key { get; set; }
        bool Connection();
    }
}
