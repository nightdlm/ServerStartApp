
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfAppAi.Model;


namespace WpfAppAi.Components
{
    /// <summary>
    /// UserComponentItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserComponentItem : UserControl
    {
        public UserComponentItem()
        {
            InitializeComponent();
        }

        private void Remove_Click(object sender,MouseButtonEventArgs e) {
            if (DataContext is Item item)
            {
                string name = item.Key;
                if (name == string.Empty) {
                    name = "未定义";
                }
                // 显示确认对话框
                var result = MessageBox.Show($"确定要删除({name})项吗？", "确认删除", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                // 如果用户点击了确定按钮
                if (result == MessageBoxResult.OK)
                {
                    ConfigOperationUtil.RemoveItem(item);
                    MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DownMove_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Item item)
            {
                ConfigOperationUtil.MoveItemDown(item);
            }
        }

        private void UpMove_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Item item) {
                ConfigOperationUtil.MoveItemUp(item);
            }
        }

        private void TextBox_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            // 创建 WPF 专用的 OpenFileDialog（注意：不是 WinForms 的！）
            OpenFileDialog openFileDialog = new()
            {
                DefaultDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Server"),
                Title = "选择启动文件", // 对话框标题
                Filter = "可执行文件 (*.exe;*.bat)|*.exe;*.bat|所有文件 (*.*)|*.*", // 同时支持exe和bat文件
                Multiselect = false // 不允许多选
            };

            // 显示对话框，若用户点击“打开”
            if (openFileDialog.ShowDialog() == true)
            {
                if (this.DataContext is Item CurrentItem)
                {
                    CurrentItem.FilePath = openFileDialog.FileName.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                }
            }
        }

        private void UserConfigYes_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Item item)
            {
                item.IsEnabled = true;
            }
        }

        private void UserConfigNo_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Item item)
            {
                item.IsEnabled = false;
            }
        }

    }
}
