using Aladdin.HASP;
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

        // 获取命令行参数
        string[] args = Environment.GetCommandLineArgs();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckDog() {
            
            if (args.Contains("--disabled"))
            {
                return;
            }


            string vendor_code = "0xxCXF0c6iULYhyUOA2hmVMjPNhcBE8NeqpC8znhnwKZ6FazEJKm+dtM6UAyMLOfK5PQoYtaOX3Exm" +
                "rfK+VB5bVGQbTHGEggoobgCYNC13yx8kjua87o9oTYSyPmQTexw0qB6tsK/Tq+fyBsuAJhkNLxraYUvEzgqLzPA3f9DIuxh" +
                "fx1X1c4JLg8kQObHkB5a8zzhDSw5ANEo0M2uRcvg34bNgcrxKutRsLXOMjPOzrocg4YEUQgtOSxvmJPSM1/nuGyL76mxKpO" +
                "6NEgJJ/fRY5ZK2+xY2EECUdIRSLicAS8APjhD4gq/8+M0G+vIRbROb0E5h2M7wlUz8MrGh4PCFkrPHGbqjPmlEa1L/ITyZa" +
                "K/1WPt58DGk6/y4xT7WAv0peqWMmbzpqQOelsvLKmnliqJKqosV9niW8KekUrBkZhLCGNOsAR988C25h/I5xUIs6UNetPWW" +
                "0bPER9y/9x+ujjEGJN29+vGqKYUaLqQZ5W5Pbd43wMUZcq84Qe2Nsr1SU2XwFQsMVFYBZ2VTBgNWr8G7sjBilL5/05DfPP4" +
                "7uq89hM0WQVDh995oCwLEXN/9yDhbF3B2wVZoqzPIx8GkYIELDYeZVuaH3HpwT16ruSCfO1HVvmcLeDbqAUQnPtftVraiZ4" +
                "qTBcw8bLwbNCaCDamI39cZ7NlQgEa+WiPPGtMnaTMeQ8id2BaGSNp22A1cYBEUfKRwL6TsqmNgBEeErHsCYGJbS00ENV2oM" +
                "rtU/U5SMJP8Jhc/sD77K9qfECPjpnINOJwGsygEoPEvUFRjhPG/ouGhmU+yNAtINPUzPOBs9zRSHGja9ciQa9+IEuJ6GYdL" +
                "9Jcj8I2OTruQKAN3mKHbo4wxAdiOyvVcsgr31oqY460ltiwBeo5fFZaKi9Z8u6ap/plqC9Ki+zJIoH/l90uiO/p70F3BbJn" +
                "71pmLnMeGPYGxoE4EyLytu8al+QYIb/SCeGyxoTGlM0DewKPw==";


            string fingerprint_format = "<haspformat format=\"host_fingerprint\"/>";

            string scope =
                                                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                                                "<haspscope>" +
                                                "    <license_manager hostname=\"localhost\" />" +
                                                "</haspscope>";

            string info = null;

            HaspStatus status = Hasp.GetInfo(scope, fingerprint_format, vendor_code, ref info);
            if (status != HaspStatus.StatusOk)
            {
                MessageBox.Show("错误：" + status, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取应用启动的进程名
            string processName = Process.GetCurrentProcess().ProcessName;
            int count = Process.GetProcessesByName(processName).Length;

            if (count > 1 )
            {
                Environment.Exit(0); // 强制退出
            }
            MainFrame.Navigate(new ServerManagerPage());

            
            if (args.Contains("--console"))
            {
                SystemSettingsMenu.Visibility = Visibility.Visible;
            }
            else
            {
                SystemSettingsMenu.Visibility = Visibility.Hidden;
            }

            CheckDog();
        }


        private void ServiceManagement_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServerManagerPage());
        }

        private void SystemSettings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingPage());
        }

        private void ServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ServerMonitorPage());
        }
    }

}