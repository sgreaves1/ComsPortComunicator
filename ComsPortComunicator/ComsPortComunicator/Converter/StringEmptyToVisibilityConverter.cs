using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ComsPortComunicator.Converter
{
    public class StringEmptyToVisibilityConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert value to string
            string text = (string) value;

            // Convert string null or empty into Boolean. 
            bool visible = string.IsNullOrEmpty(text);

            // Return visibility value.
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
