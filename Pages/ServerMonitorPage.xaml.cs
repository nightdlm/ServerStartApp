using System.Windows;
using System.Windows.Controls;
using WpfAppAi.Common;
using WpfAppAi.Model;
using WpfAppAi.Source;

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
                    Port = item.Port
                });
            }

            DataContext = ServerOperationUtil.Instance;

        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取按钮
            Button button = sender as Button;

            // 获取该行绑定的数据对象
            var serverInfo = button.Tag as ServerInfoItem;  // 假设每行绑定的是 ServerInfo 类型对象

            // 处理启动逻辑
            if (serverInfo != null)
            {

                if (ProcessManager.IsJobRunning(serverInfo.ServerName))
                {
                    MessageBox.Show($"已存在({serverInfo.ServerName})服务运行中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }


                // 在这里执行启动操作,先通过key去查询ItemList对应的配置项
                var item = ConfigOperationUtil.Instance.ItemList.FirstOrDefault(x => x.Key == serverInfo.ServerName);
                if (item != null)
                {
                    try
                    {
                        if (item.Port != 0)
                        {
                            bool isUse = ProcessManager.IsPortInUse(item.Port);
                            if (isUse)
                            {
                                throw new Exception($"({item.Port})端口被占用，请手动释放此端口，或更换端口");
                            }
                        }
                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"({item.Key})服务启动中..."
                        });
                        MessageBox.Show($"启动中，具体信息查询控制台");
                        ProcessManager.StartProcessWithJobObject(item.Key, item.FilePath, " " + item.Arg);

                    }
                    catch (Exception ex)
                    {
                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"({item.Key})服务启动失败：" + ex.Message
                        });
                    }
                    ;
                }
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取按钮
            Button button = sender as Button;

            // 获取该行绑定的数据对象
            var serverInfo = button.Tag as ServerInfoItem;

            // 处理停止逻辑
            if (serverInfo != null)
            {
                // 停止执行二次确认
                var result = MessageBox.Show($"确定要停止服务: {serverInfo.ServerName}吗？", "确认停止", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    if (!ProcessManager.IsJobRunning(serverInfo.ServerName))
                    {
                        MessageBox.Show($"未发现({serverInfo.ServerName}运行中", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // 在这里执行停止操作
                        ProcessManager.TerminateJobObject(serverInfo.ServerName);
                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"({serverInfo.ServerName})服务已停止"
                        });
                    }
                }
            }
        }


    }
}
