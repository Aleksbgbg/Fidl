namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal abstract class ValueEditViewModel : ViewModelBase, IValueEditViewModel
    {
        public abstract object Value { get; }
    }
}