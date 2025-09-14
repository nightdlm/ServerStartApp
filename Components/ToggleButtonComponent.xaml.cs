using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfAppAi.Common;

namespace WpfAppAi.Components
{
    /// <summary>
    /// ToggleButtonComponent.xaml 的交互逻辑
    /// </summary>
    public partial class ToggleButtonComponent : UserControl
    {
        public ToggleButtonComponent()
        {
            InitializeComponent();
        }

        // 完全加载后执行
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Animation_Start();
        }

        private void Animation_Start()
        {
            // 获取模板中的元素
            var backgroundBorder = toggleButton.Template.FindName("backgroundBorder", toggleButton) as Border;
            var sliderBorder = toggleButton.Template.FindName("sliderBorder", toggleButton) as Border;

            // 创建动画故事板
            Storyboard storyboard = new Storyboard();

            if (DynamicConfig.IsAutoScorll)
            {
                // 切换到开启状态：绿色背景，滑块在右侧
                // 背景颜色动画
                ColorAnimation backgroundAnimation = new ColorAnimation(Colors.LightGray, Colors.Green, TimeSpan.FromMilliseconds(200));
                Storyboard.SetTarget(backgroundAnimation, backgroundBorder);
                Storyboard.SetTargetProperty(backgroundAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
                storyboard.Children.Add(backgroundAnimation);

                // 滑块位置动画
                ThicknessAnimation marginAnimation = new ThicknessAnimation(sliderBorder.Margin, new Thickness(60, 10, 10, 10), TimeSpan.FromMilliseconds(200));
                Storyboard.SetTarget(marginAnimation, sliderBorder);
                Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(Border.MarginProperty));
                storyboard.Children.Add(marginAnimation);
            }
            else
            {
                // 切换到关闭状态：灰色背景，滑块在左侧
                // 背景颜色动画
                ColorAnimation backgroundAnimation = new ColorAnimation(Colors.Green, Colors.LightGray, TimeSpan.FromMilliseconds(200));
                Storyboard.SetTarget(backgroundAnimation, backgroundBorder);
                Storyboard.SetTargetProperty(backgroundAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
                storyboard.Children.Add(backgroundAnimation);

                // 滑块位置动画
                ThicknessAnimation marginAnimation = new ThicknessAnimation(sliderBorder.Margin, new Thickness(10, 10, 60, 10), TimeSpan.FromMilliseconds(200));
                Storyboard.SetTarget(marginAnimation, sliderBorder);
                Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(Border.MarginProperty));
                storyboard.Children.Add(marginAnimation);
            }

            // 启动动画
            storyboard.Begin();
        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DynamicConfig.IsAutoScorll = !DynamicConfig.IsAutoScorll;
            Animation_Start();
        }
    }
}
