using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WatchJ
{
    public class DataXml
    {
        private XDocument _XmlDocument = null;
        private string _DataPath = null;

        public DataXml(string dataPath)
        {
            _DataPath = dataPath;
            if ((_DataPath != null) && File.Exists(_DataPath))
            {
                try
                {
                    _XmlDocument = XDocument.Load(_DataPath);
                }
                catch
                {
                    
                }
            }
        }

        public XDocument XMLDocument
        {
            get { return _XmlDocument; }
        }

        public void SaveData(List<User> list)
        {
            _XmlDocument.Elements("Videos").Elements("Video").ToList().Remove();

            foreach (User item in list)
            {
                XElement elemento = new XElement("Video",
                new XAttribute("Id", item.Id),
                new XElement("Name", item.Name),
                new XElement("Url", item.Url),
                new XElement("Title", item.Title),
                new XElement("Comment", item.Comment));

                var videos = (from vid in _XmlDocument.Elements("Videos")
                              select vid).SingleOrDefault();
                videos.Add(elemento);
            }

            try
            {
                _XmlDocument.Save(_DataPath);
            }
            catch
            {
              
            }
        }

        public List<User> UserList()
        {
            List<User> list = new List<User>();
            foreach (var item in _XmlDocument.Elements("Videos").Elements("Video"))
            {
                list.Add(new User() {
                    Id = Int32.Parse(item.Attribute("Id").Value),
                    Name = item.Element("Name").Value,
                    Url = item.Element("Url").Value,
                    Title = item.Element("Title").Value,
                    Comment = item.Element("Comment").Value
                });
            }        
            return list;
        }

        public string MpvPath()
        {  
               string path = (from item in _XmlDocument.Elements("Videos").Elements("App")
                             select item.Attribute("MpvPath").Value).SingleOrDefault();
            return path;
        }

        public void SaveMpv(string newPath)
        {
            _XmlDocument.Elements("Videos").Elements("App").ToList().Remove();

            XElement elemento = new XElement("App",
                new XAttribute("MpvPath", newPath.ToString()));

            var config = (from conf in _XmlDocument.Elements("Videos")
                            select conf).SingleOrDefault();
            config.Add(elemento);

            try
            {
                _XmlDocument.Save(_DataPath);
            }
            catch
            {
              
            }
        
        }
    }   
}
