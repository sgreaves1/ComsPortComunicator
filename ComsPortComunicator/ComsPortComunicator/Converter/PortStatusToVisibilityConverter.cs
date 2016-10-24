using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ComsPortComunicator.Enum;

namespace ComsPortComunicator.Converter
{
    public class PortStatusToVisibilityConverter : IValueConverter
    {
        enum Direction
        {
            Normal, Inverted
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert value to State.
            ComOpenState state = (ComOpenState) value;

            // Get if the converter is to be inverted.
            Direction direction = Direction.Normal;
            if (!string.IsNullOrEmpty(parameter.ToString()))
                direction = (Direction) System.Enum.Parse(typeof (Direction), (string) parameter);
            
            // Convert state to visible Boolean.
            bool visible = state == ComOpenState.Open;

            // Invert if needed.
            if (direction == Direction.Inverted)
            {
                visible = !visible;
            }

            // Return visibility value.
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
