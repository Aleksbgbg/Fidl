namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueViewModel : ViewModelBase, IValueViewModel
    {
        public Value Value { get; private set; }

        public void Initialise(Value value)
        {
            DisplayName = value.Name;
            Value = value;
        }
    }
}