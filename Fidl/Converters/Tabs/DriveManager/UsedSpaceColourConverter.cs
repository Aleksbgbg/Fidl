namespace Fidl.Converters.Tabs.DriveManager
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    internal class UsedSpaceColourConverter : IValueConverter
    {
        private static readonly SolidColorBrush BlueBrush = new SolidColorBrush(Colors.DodgerBlue);

        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Crimson);

        public static UsedSpaceColourConverter Instance { get; } = new UsedSpaceColourConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null", "Conversion value should not be null.");
            return (double)value > 80 ? RedBrush : BlueBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}