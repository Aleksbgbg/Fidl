namespace Fidl.Models.RegistryEditor
{
    using Microsoft.Win32;

    internal class Value
    {
        internal Value()
        {
            Name = "(default)";
            Kind = RegistryValueKind.String;
            StoredValue = "(value not set)";
        }

        internal Value(RegistryKey registryKey, string name)
        {
            Name = name == string.Empty ? "(default)" : name;

            if (registryKey == null)
            {
                Kind = RegistryValueKind.String;
                StoredValue = "(value not set)";
                return;
            }

            Kind = registryKey.GetValueKind(name);
            StoredValue = registryKey.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames) ?? "(value not set)";
        }

        public string Name { get; }

        public RegistryValueKind Kind { get; }

        public object StoredValue { get; }
    }
}