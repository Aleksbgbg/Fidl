namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(string))]
    internal class RegistryValueNameConverter : IValueConverter
    {
        public static RegistryValueNameConverter Instance { get; } = new RegistryValueNameConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == string.Empty ? "(default)" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}