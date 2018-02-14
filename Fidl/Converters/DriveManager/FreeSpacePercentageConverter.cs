namespace Fidl.Converters.DriveManager
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(double))]
    internal class FreeSpacePercentageConverter : IValueConverter
    {
        public static FreeSpacePercentageConverter Instance { get; } = new FreeSpacePercentageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null", "Conversion value should not be null.");
            return 100 - (double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}