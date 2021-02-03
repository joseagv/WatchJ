using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ
{
    interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string Title { get; set; }
        string Comment { get; set; }
    }
}
