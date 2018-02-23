namespace Fidl.Models.RegistryEditor
{
    using Microsoft.Win32;

    internal class Value
    {
        internal Value(RegistryKey registryKey, string name, bool exists = true)
        {
            Name = name == string.Empty ? "(default)" : name;

            if (registryKey == null)
            {
                Kind = RegistryValueKind.String;
                StoredValue = "(value not set)";
                return;
            }

            Kind = exists ? registryKey.GetValueKind(name) : RegistryValueKind.String;
            StoredValue = registryKey.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames) ?? "(value not set)";
        }

        public string Name { get; }

        public RegistryValueKind Kind { get; }

        public object StoredValue { get; }
    }
}