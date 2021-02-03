using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchJ
{
    public class User : IUser
    {
        private int _Id;
        private string _Name;
        private string _Url;
        private string _Title;
        private string _Comment;

        public int Id { get => _Id; set => _Id = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string Url { get => _Url; set => _Url = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string Comment { get => _Comment; set => _Comment = value; }
    }
}
