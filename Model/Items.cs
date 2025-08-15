using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfAppAi.Model
{
    [XmlRoot("Items")]
    public class Items
    {
        [XmlElement("Item")]
        public List<Item> ItemList { get; set; } = new List<Item>();

    }

    public class Item {

        [XmlElement("Key")]
        public string Key { get; set; } = string.Empty;

        [XmlElement("FilePath")]
        public string FilePath { get; set; } = string.Empty;

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("WaitForExit")]
        public bool WaitForExit { get; set; }

        [XmlArray("Args")]
        [XmlArrayItem("Arg")]
        public List<string> Args { get; set; } = new List<string>();
    }

}
