namespace Fidl.Models.RegistryEditor
{
    using Caliburn.Micro;

    using Fidl.Extensions;

    using Microsoft.Win32;

    internal class Value : PropertyChangedBase
    {
        internal Value(RegistryKey registryKey)
        {
            RegistryKey = registryKey;

            Name = string.Empty;
            Kind = RegistryValueKind.String;
            ActualValue = null;
        }

        internal Value(RegistryKey registryKey, string name)
        {
            RegistryKey = registryKey;

            Name = name;

            if (registryKey == null)
            {
                Kind = RegistryValueKind.String;
                ActualValue = null;
                return;
            }

            Kind = registryKey.GetValueKind(name);
            ActualValue = registryKey.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
        }

        private string _name;
        public string Name
        {
            get => _name;

            private set
            {
                if (_name == value) return;

                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public RegistryValueKind Kind { get; }

        private object _storedValue;
        public object StoredValue
        {
            get => _storedValue;

            set
            {
                if (_storedValue == value) return;

                _storedValue = value;
                // NotifyOfPropertyChange(() => StoredValue);
            }
        }

        private object _actualValue;
        public object ActualValue
        {
            get => _actualValue;

            private set
            {
                if (_actualValue == value) return;

                _actualValue = value;
                // NotifyOfPropertyChange(() => ActualValue);

                StoredValue = _actualValue;
                NotifyOfPropertyChange(() => StoredValue);
            }
        }

        internal RegistryKey RegistryKey { get; }

        internal void FlushStoredValue()
        {
            if (ActualValue == StoredValue) return;

            using (RegistryKey writeableKey = RegistryKey.GetWriteableKey())
            {
                writeableKey.SetValue(Name, StoredValue, Kind);
            }

            ActualValue = StoredValue;
        }

        internal void ResetStoredValue()
        {
            StoredValue = ActualValue;
        }

        internal void Delete()
        {
            using (RegistryKey writeableKey = RegistryKey.GetWriteableKey())
            {
                writeableKey.DeleteValue(Name == string.Empty ? null : Name);
            }
        }

        internal void Rename(string newName)
        {
            if (Name == newName) return;

            using (RegistryKey writeableKey = RegistryKey.GetWriteableKey())
            {
                writeableKey.DeleteValue(Name);
                writeableKey.SetValue(newName, ActualValue, Kind);
            }

            Name = newName;
        }
    }
}