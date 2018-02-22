namespace Fidl.Models.RegistryEditor
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security;

    using Microsoft.Win32;

    using IOPath = System.IO.Path;

    internal class Key
    {
        internal Key() : this(string.Empty, true, true)
        {
        }

        internal Key(RegistryKey registryKey) : this(registryKey.Name, true, registryKey.SubKeyCount > 0)
        {
            RegistryKey = registryKey;
        }

        private Key(string name) : this(name, false, false)
        {
        }

        private Key(string name, bool isAccessible, bool hasItems)
        {
            // This constructor must only be called by other constructors
            Debug.Assert(new StackFrame(1).GetMethod().Name == ".ctor");

            Path = IOPath.Combine("Computer", name);
            Name = IOPath.GetFileName(name);
            IsAccessible = isAccessible;
            HasItems = hasItems;
        }

        public string Path { get; }

        public string Name { get; }

        public bool IsAccessible { get; }

        public bool HasItems { get; }

        internal RegistryKey RegistryKey { get; }

        internal IEnumerable<Key> GetSubKeys()
        {
            return RegistryKey.GetSubKeyNames().Select(keyName =>
            {
                try
                {
                    return new Key(RegistryKey.OpenSubKey(keyName));
                }
                catch (SecurityException) // Lack of permissions to open key (still display it as hidden)
                {
                    return new Key(IOPath.Combine(RegistryKey.Name, keyName));
                }
            });
        }
    }
}