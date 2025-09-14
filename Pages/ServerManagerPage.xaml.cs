using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfAppAi.Common;
using WpfAppAi.Model;
using WpfAppAi.Source;

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
            ServerLogLIst.DataContext = LogListSource.GetInstance();
            if(DynamicConfig.IsAutoScorll){
                InitializeAutoScrollTimer();
            }
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

                    if (ProcessManager.IsJobRunning(item.Key))
                    {
                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"({item.Key})服务已运行中..."
                        });
                        continue;
                    }

                    string args = " " + item.Arg;

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
                    ProcessManager.StartProcessWithJobObject(item.Key, item.FilePath, args);

                    if (item.Port != 0)
                    {
                        bool isOccupied = await ProcessManager.WaitForPortOccupiedAsync(item.Port);
                        if (!isOccupied)
                        {
                            throw new Exception($"({item.Key})未能成功启动，请查询对应服务配置信息");
                        }
                    }

                    if (item.WaitForExit)
                    {
                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"等待({item.Key})进程任务完成..."
                        });
                        await ProcessManager.WaitForJobExitAsync(item.Key);

                        LogListSource.AddLog(new LogEntry
                        {
                            ServerName = "SYSTEM",
                            Message = $"({item.Key})进程已结束："
                        });
                    }
                }

                LogListSource.AddLog(new LogEntry
                {
                    ServerName = "SYSTEM",
                    Message = $"所有服务启动成功"
                });
            }
            catch (Exception e)
            {
                LogListSource.AddLog(new LogEntry
                {
                    ServerName = "SYSTEM",
                    Message = e.Message
                });
                ProcessManager.CloseAllJobObjects();
                LogListSource.AddLog(new LogEntry
                {
                    ServerName = "SYSTEM",
                    Message = "已终止并关闭所有已启动进程"
                });
                StopServer.IsEnabled = false;
                StartServer.IsEnabled = true;
            }
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

            LogListSource.AddLog(new LogEntry
            {
                ServerName = "SYSTEM",
                Message = "已终止并关闭所有已启动进程"
            });

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
            LogListSource.ClearLog();
        }


        private void ToggleButtonComponent_Loaded(object sender, RoutedEventArgs e)
        {
            if (DynamicConfig.IsAutoScorll)
            {
                InitializeAutoScrollTimer();
            } else
            {
                StopAutoScrollTimer();
            }
        }

        private DispatcherTimer autoScrollTimer;

        // 初始化自动滚动定时器
        private void InitializeAutoScrollTimer()
        {
            autoScrollTimer = new DispatcherTimer();
            autoScrollTimer.Interval = TimeSpan.FromSeconds(1);
            autoScrollTimer.Tick += AutoScrollTimer_Tick;
            autoScrollTimer.Start();
        }

        // 定时器事件处理函数
        private void AutoScrollTimer_Tick(object sender, EventArgs e)
        {
            if (DynamicConfig.IsAutoScorll && ServerLogLIst.Items.Count > 0)
            {
                ServerLogLIst.ScrollIntoView(ServerLogLIst.Items[ServerLogLIst.Items.Count - 1]);

            }
        }

        //在适当的地方（如页面关闭时）停止定时器
        private void StopAutoScrollTimer()
        {
            if (autoScrollTimer != null)
            {
                autoScrollTimer.Stop();
                autoScrollTimer = null;
            }
        }

    }
}
