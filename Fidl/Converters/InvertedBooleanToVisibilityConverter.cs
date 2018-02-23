namespace Fidl.Converters
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public static InvertedBooleanToVisibilityConverter Default { get; } = new InvertedBooleanToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null", "Conversion value should not be null.");

            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null", "Conversion value should not be null.");

            return (Visibility)value == Visibility.Collapsed;
        }
    }
}