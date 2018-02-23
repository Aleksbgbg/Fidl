namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(string))]
    internal class RegistryValueConverter : IValueConverter
    {
        public static RegistryValueConverter Default { get; } = new RegistryValueConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? "(value not set)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}