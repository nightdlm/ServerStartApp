using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppAi
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            InitConfig();
        }

        private void InitConfig() {
            DataContext = ConfigOperationUtil.Instance;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(); ;
            if(mainWindow != null)
        {
                // 如果窗口已存在但被隐藏，显示它
                if (mainWindow.Visibility == Visibility.Hidden ||
                    mainWindow.WindowState == WindowState.Minimized)
                {
                    mainWindow.Visibility = Visibility.Visible;
                    mainWindow.WindowState = WindowState.Normal; // 恢复窗口状态（如果最小化）
                    mainWindow.Activate(); // 激活窗口使其获得焦点
                }
            }
        else
            {
                // 如果窗口不存在，创建并显示新窗口
                mainWindow = new MainWindow();
                mainWindow.Show();
            }

            // 配置强制保存
            ConfigOperationUtil.SaveConfig();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ConfigOperationUtil.AddItem();
        }
    }
}
