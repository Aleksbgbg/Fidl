namespace Fidl.Models.RegistryEditor
{
    using System;

    using Microsoft.Win32;

    internal class Value
    {
        private readonly RegistryKey _registryKey;

        internal Value(RegistryKey registryKey)
        {
            _registryKey = registryKey;

            Name = string.Empty;
            Kind = RegistryValueKind.String;
            StoredValue = null;
        }

        internal Value(RegistryKey registryKey, string name)
        {
            _registryKey = registryKey;

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

        internal void Delete()
        {
            int firstSeparatorIndex = _registryKey.Name.IndexOf('\\');

            RegistryKey GetRootKey()
            {
                switch (_registryKey.Name.Substring(0, firstSeparatorIndex))
                {
                    case "HKEY_CLASSES_ROOT":
                        return Registry.ClassesRoot;

                    case "HKEY_CURRENT_USER":
                        return Registry.CurrentUser;

                    case "HKEY_LOCAL_MACHINE":
                        return Registry.LocalMachine;

                    case "HKEY_USERS":
                        return Registry.Users;

                    case "HKEY_CURRENT_CONFIG":
                        return Registry.CurrentConfig;

                    default:
                        throw new InvalidOperationException("Non-existant root key received.");
                }
            }

            using (RegistryKey writeableKey = GetRootKey().OpenSubKey(_registryKey.Name.Substring(firstSeparatorIndex + 1), true))
            {
                writeableKey.DeleteValue(Name == string.Empty ? null : Name);
            }
        }
    }
}