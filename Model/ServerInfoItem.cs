using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfAppAi.Model
{
    public class ServerInfoItems
    {
        public ObservableCollection<ServerInfoItem> ServerInfoItemList { get; set; } = new ObservableCollection<ServerInfoItem>();

    }

    public class ServerInfoItem : INotifyPropertyChanged
    {
        private string _serverName = string.Empty;
        private int _port = 0;

        // 服务器名称
        public string ServerName
        {
            get => _serverName;
            set
            {
                _serverName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }

        // 端口
        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }

        // 实现INotifyPropertyChanged接口
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}