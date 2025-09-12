using System.Windows;
using System.Windows.Controls;

namespace WpfAppAi.Pages
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            InitConfig();
        }


        private void InitConfig()
        {
            DataContext = ConfigOperationUtil.Instance;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ConfigOperationUtil.AddItem();
        }

        private void SaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            ConfigOperationUtil.SaveConfig();
        }
    }
}
