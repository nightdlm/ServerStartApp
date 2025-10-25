using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace WpfAppAi.Model
{
    public class ServerInfoItems
    {
        public ObservableCollection<ServerInfoItem> ServerInfoItemList { get; set; } = new ObservableCollection<ServerInfoItem>();

    }

    public class ServerInfoItem : INotifyPropertyChanged
    {
        private System.Threading.Timer _statusCheckTimer;
        private string _serverName = string.Empty;
        private int _port = 0;
        private string _status = "Checking";

        public ServerInfoItem()
        {
            // 每5秒检查一次状态
            _statusCheckTimer = new System.Threading.Timer(CheckStatus, null, 0, 1000);
        }

        private void CheckStatus(object state)
        {
            // 这里实现实际的状态检查逻辑
            string newStatus = CheckServerStatus();
            if (_status != newStatus)
            {
                Status = newStatus;
            }
        }

        private string CheckServerStatus()
        {
            return ProcessManager.IsJobRunning(ServerName)? "Running" : "Stopped"; 
        }


        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


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