using System;
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
        private int _pid = 0;
        private bool _isRunning = false;

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

        // PID
        public int Pid
        {
            get => _pid;
            set
            {
                _pid = value;
                OnPropertyChanged(nameof(Pid));
            }
        }

        // 运行状态
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
                OnPropertyChanged(nameof(Status)); // 同时通知Status属性变更
            }
        }

        // 只读属性，用于显示状态文本
        public string Status => IsRunning ? "运行中" : "已停止";

        // 实现INotifyPropertyChanged接口
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}