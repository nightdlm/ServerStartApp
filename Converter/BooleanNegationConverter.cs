using System.Globalization;
using System.Windows.Data;

namespace WpfAppAi.Converter
{
    public class BooleanNegationConverter : IValueConverter
    {
        // 将值反转（true→false，false→true）
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        // 反转回来（用于双向绑定）
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false; // 默认值
        }
    }
}
