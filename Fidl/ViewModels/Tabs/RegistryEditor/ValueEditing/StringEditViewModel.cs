namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal class StringEditViewModel : ValueEditViewModel, IStringEditViewModel
    {
        private string _value;
        public string Value
        {
            get => _value;

            set
            {
                if (_value == value) return;

                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public override object StoredValue
        {
            get => Value;

            set => Value = (string)value;
        }
    }
}