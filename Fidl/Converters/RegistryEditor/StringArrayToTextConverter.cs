namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    [ValueConversion(typeof(string[]), typeof(string))]
    internal class StringArrayToTextConverter : IValueConverter
    {
        public static StringArrayToTextConverter Default { get; } = new StringArrayToTextConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null");

            return string.Join("\n", (string[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null");

            return ((string)value).Trim('\r', '\n')
                                  .Trim()
                                  .Replace("\r", string.Empty)
                                  .Split('\n')
                                  .Where(line => !string.IsNullOrWhiteSpace(line))
                                  .ToArray();
        }
    }
}