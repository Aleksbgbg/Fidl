namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(bool))]
    internal class RegistryValueIsNotDefaultConverter : IValueConverter
    {
        public static RegistryValueIsNotDefaultConverter Default { get; } = new RegistryValueIsNotDefaultConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}