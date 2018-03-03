namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal abstract class ValueEditViewModel : ViewModelBase, IValueEditViewModel
    {
        public Value Value { get; private set; }

        public void Initialise(Value value)
        {
            Value = value;
        }
    }
}