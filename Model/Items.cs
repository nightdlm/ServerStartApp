
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WpfAppAi.Model
{
    [XmlRoot("Items")]
    public class Items
    {
        [XmlElement("Item")]
        public ObservableCollection<Item> ItemList { get; set; } = [];

    }

    public class Item : INotifyPropertyChanged
    {

        // 字段：存储属性的实际值（与属性分离，用于判断值是否变化）
        private bool _isEnabled = false;
        private string _key = string.Empty;
        private string _filePath = string.Empty;
        private int _port = 0;
        private bool _waitForExit = false;
        private string _arg = string.Empty;


        // 属性：对外暴露，setter中触发变更通知
        [XmlElement("IsEnabled")]
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value) // 仅当值变化时触发通知
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled"); // 通知UI属性变化
                }
            }
        }

        [XmlElement("Key")]
        public string Key
        {
            get => _key;
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        [XmlElement("FilePath")]
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }

        [XmlElement("Port")]
        public int Port
        {
            get => _port;
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        [XmlElement("WaitForExit")]
        public bool WaitForExit
        {
            get => _waitForExit;
            set
            {
                if (_waitForExit != value)
                {
                    _waitForExit = value;
                    OnPropertyChanged("WaitForExit");
                }
            }
        }

        [XmlElement("Arg")]
        public string Arg
        {
            get => _arg;
            set
            {
                if (_arg != value)
                {
                    _arg = value;
                    OnPropertyChanged("Arg");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //[XmlArray("Args")]
        //[XmlArrayItem("Arg")]
        //public List<string> Args { get; set; } = new List<string>();

    }

}
