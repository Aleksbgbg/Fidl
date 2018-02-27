namespace Fidl.Extensions
{
    using System;

    using Microsoft.Win32;

    internal static class RegistryKeyExtensions
    {
        internal static RegistryKey GetWriteableKey(this RegistryKey key)
        {
            int firstSeparatorIndex = key.Name.IndexOf('\\');

            RegistryKey GetRootKey()
            {
                switch (key.Name.Substring(0, firstSeparatorIndex))
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

            return GetRootKey().OpenSubKey(key.Name.Substring(firstSeparatorIndex + 1), true);
        }
    }
}