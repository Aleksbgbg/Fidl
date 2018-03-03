namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal abstract class ValueEditViewModel : ViewModelBase, IValueEditViewModel
    {
        public abstract object StoredValue { get; set; }

        public void Initialise(object value)
        {
            StoredValue = value;
        }
    }
}