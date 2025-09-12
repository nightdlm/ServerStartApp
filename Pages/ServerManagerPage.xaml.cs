using System.Windows;
using System.Windows.Controls;
using WpfAppAi.Model;

namespace WpfAppAi.Pages
{
    /// <summary>
    /// ServerManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class ServerManagerPage : Page
    {
        public ServerManagerPage()
        {
            InitializeComponent();
        }

        // 一键唤醒所有服务
        private async Task FullAutoStartServer()
        {
            try
            {
                StopServer.IsEnabled = true;
                StartServer.IsEnabled = false;
                foreach (Item item in ConfigOperationUtil.Instance.ItemList)
                {
                    if (!item.IsEnabled) continue;

                    string args = " " + item.Arg;

                    if (item.Port != 0)
                    {
                        bool isUse = ProcessManager.IsPortInUse(item.Port);
                        if (isUse)
                        {
                            throw new Exception(item.Port + "端口被占用，请手动释放此端口，或更换端口\n");
                        }
                    }

                    ProcessManager.StartProcessWithJobObject(item.Key, item.FilePath, args, ServerLog);
                    ServerLog.AppendText("服务启动中...：\n");
                    if (item.Port != 0)
                    {
                        bool isOccupied = await ProcessManager.WaitForPortOccupiedAsync(item.Port);
                        if (!isOccupied)
                        {
                            throw new Exception(item.Key + "未能成功启动，请查询对应服务配置信息\n");
                        }
                    }

                    if (item.WaitForExit)
                    {
                        ServerLog.AppendText(item.Key + "等待进程任务完成...：\n");
                        await ProcessManager.WaitForJobExitAsync(item.Key);
                        ServerLog.AppendText(item.Key + "进程已结束：\n");
                    }
                }
                ServerLog.AppendText("服务启动成功：\n");
            }
            catch (Exception e)
            {
                ServerLog.AppendText("启动过程发生异常：\n");
                ServerLog.AppendText(e.Message);
               ProcessManager.CloseAllJobObjects();
                ServerLog.AppendText("已终止并关闭所有已启动进程");
                StopServer.IsEnabled = false;
                StartServer.IsEnabled = true;
            }
        }

        private void ServerLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            LogViewer.ScrollToEnd();
        }

        private async void StartServer_Click(object sender, RoutedEventArgs e)
        {
            StopServer.IsEnabled = true;
            StartServer.IsEnabled = false;
            await FullAutoStartServer();
        }

        private void StopServer_Click(object sender, RoutedEventArgs e)
        {
            ProcessManager.CloseAllJobObjects();
            ServerLog.AppendText("所有服务已停止\n");
            StopServer.IsEnabled = false;
            StartServer.IsEnabled = true;
        }


        private void Setting_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ConfigOperationUtil.SaveConfig();
            NavigationService.Navigate(new SettingPage());
        }


        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ServerLog.Text = "";
        }

    }
}
