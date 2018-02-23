﻿namespace Fidl.Models.RegistryEditor
{
    using Microsoft.Win32;

    internal class Value
    {
        internal Value()
        {
            Name = string.Empty;
            Kind = RegistryValueKind.String;
            StoredValue = null;
        }

        internal Value(RegistryKey registryKey, string name)
        {
            Name = name;

            if (registryKey == null)
            {
                Kind = RegistryValueKind.String;
                StoredValue = null;
                return;
            }

            Kind = registryKey.GetValueKind(name);
            StoredValue = registryKey.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
        }

        public string Name { get; }

        public RegistryValueKind Kind { get; }

        public object StoredValue { get; }
    }
}