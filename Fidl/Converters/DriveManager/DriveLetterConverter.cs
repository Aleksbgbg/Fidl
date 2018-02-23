namespace Fidl.Converters.DriveManager
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(string))]
    internal class DriveLetterConverter : IValueConverter
    {
        public static DriveLetterConverter Default { get; } = new DriveLetterConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null", "Conversion value should not be null.");

            return ((string)value).TrimEnd('\\');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}