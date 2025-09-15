using System.Diagnostics;
using System.Windows;
using WpfAppAi.Pages;


namespace WpfAppAi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ServerManagerPage());
        }


        protected override void OnInitialized(EventArgs e)
        {
            // 获取应用启动的进程名
            string processName = Process.GetCurrentProcess().ProcessName;
            int count = Process.GetProcessesByName(processName).Length;

            if (count > 1)
            {
                Environment.Exit(0); // 强制退出
            }

            //MessageBox.Show("请插入加密狗");
            // 直接退出应用程序
            //Environment.Exit(0); // 强制退出
            base.OnInitialized(e);
        }

        private void ServiceManagement_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServerManagerPage());
        }

        private void SystemSettings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new SettingPage());
        }

        private void ServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServerMonitorPage());
        }
    }

}