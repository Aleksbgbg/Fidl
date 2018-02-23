namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;

    using Microsoft.Win32;

    [ValueConversion(typeof(RegistryValueKind), typeof(string))]
    internal class RegistryValueKindToStringConverter : IValueConverter
    {
        public static RegistryValueKindToStringConverter Instance { get; } = new RegistryValueKindToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, $"{nameof(value)} != null");

            switch ((RegistryValueKind)value)
            {
                case RegistryValueKind.String:
                    return "REG_SZ (String)";

                case RegistryValueKind.ExpandString:
                    return "REG_EXPAND_SZ (ExpandString)";

                case RegistryValueKind.Binary:
                    return "REG_BINARY (Binary)";

                case RegistryValueKind.DWord:
                    return "REG_DWORD (DoubleWord)";

                case RegistryValueKind.MultiString:
                    return "REG_MULTI_SZ (Multi-String)";

                case RegistryValueKind.QWord:
                    return "REG_QWORD (QuadWord)";

                case RegistryValueKind.Unknown:
                    return "REG_UNKNOWN (Unknown)";

                case RegistryValueKind.None:
                    return "REG_NONE (None)";

                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}