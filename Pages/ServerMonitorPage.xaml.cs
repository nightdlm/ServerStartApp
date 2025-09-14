using System.Windows.Controls;
using WpfAppAi.Common;
using WpfAppAi.Model;

namespace WpfAppAi.Pages
{
    /// <summary>
    /// ServerMonitorPage.xaml 的交互逻辑
    /// </summary>
    public partial class ServerMonitorPage : Page
    {
        public ServerMonitorPage()
        {
            InitializeComponent();

            ServerOperationUtil.ClearAllData();

            // 初始化数据
            foreach (var item in ConfigOperationUtil.Instance.ItemList)
            {
                ServerOperationUtil.AddServerInfoItem(new ServerInfoItem()
                {
                    ServerName = item.Key,
                    Port = item.Port,
                    IsRunning = ProcessManager.IsPortInUse(item.Port)
                });
            }

            DataContext = ServerOperationUtil.Instance;

        }
    }
}
