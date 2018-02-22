namespace Fidl.Models.RegistryEditor
{
    using Microsoft.Win32;

    internal class Value
    {
        internal Value(RegistryKey registryKey, string name)
        {
            Name = name;
            StoredValue = registryKey.GetValue(name);
            Kind = registryKey.GetValueKind(name);
        }

        public string Name { get; }

        public object StoredValue { get; }

        public RegistryValueKind Kind { get; }
    }
}