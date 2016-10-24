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
            ComOpenState state = (ComOpenState) value;
            Direction direction = (Direction) System.Enum.Parse(typeof (Direction), (string) parameter);

            bool visible = false;

            switch (state)
            {
                case ComOpenState.Open:
                    visible = true;
                    break;

                case ComOpenState.Closed:
                    visible = false;
                    break;
            }

            if (direction == Direction.Inverted)
            {
                visible = !visible;
            }

            return visible? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
