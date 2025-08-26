using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfAppAi.Converter
{
    internal class BooleanToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 检查源值是否为bool类型
            if (value is not bool boolValue)
            {
                return Visibility.Collapsed; // 非bool类型默认隐藏
            }

            // 检查是否需要反转逻辑
            bool isInverse = parameter is string param && param.Equals("inverse", StringComparison.OrdinalIgnoreCase);

            // 根据是否反转决定可见性
            if (isInverse)
            {
                // 反转逻辑：源值为true时隐藏，false时显示
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                // 正常逻辑：源值为true时显示，false时隐藏
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 检查目标值是否为Visibility类型
            if (value is not Visibility visibility)
            {
                return false; // 非Visibility类型默认返回false
            }

            // 检查是否需要反转逻辑
            bool isInverse = parameter is string param && param.Equals("inverse", StringComparison.OrdinalIgnoreCase);

            // 将可见性转换回bool值
            bool result = visibility == Visibility.Visible;

            // 根据是否反转返回结果
            return isInverse ? !result : result;
        }
    }
}
