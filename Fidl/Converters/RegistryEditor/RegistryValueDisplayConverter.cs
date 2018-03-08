namespace Fidl.Converters.RegistryEditor
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    using Microsoft.Win32;

    [ValueConversion(typeof(object), typeof(string))]
    internal class RegistryValueDisplayConverter : IMultiValueConverter
    {
        public static RegistryValueDisplayConverter Default { get; } = new RegistryValueDisplayConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(values.Length == 2);
            Debug.Assert(values[1] is RegistryValueKind);

            switch ((RegistryValueKind)values[1])
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                case RegistryValueKind.Unknown:
                case RegistryValueKind.None:
                    return values[0];

                case RegistryValueKind.Binary:
                {
                    byte[] bytes = (byte[])values[0];

                    return bytes.Length == 0 ? "(zero-length binary value)" : string.Join(" ", bytes.Select(number => number.ToString("X2")));
                }

                case RegistryValueKind.DWord:
                    return $"0x{values[0]:X8} ({(uint)(int)values[0]:N0})";

                case RegistryValueKind.QWord:
                    return $"0x{values[0]:X16} ({(ulong)(long)values[0]:N0})";

                case RegistryValueKind.MultiString:
                    return string.Join(" ", (string[])values[0]).Replace("\r", string.Empty);

                default:
                    throw new ArgumentOutOfRangeException(nameof(values), values, "RegistryValueKind is invalid.");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported.");
        }
    }
}