
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
    }
}
