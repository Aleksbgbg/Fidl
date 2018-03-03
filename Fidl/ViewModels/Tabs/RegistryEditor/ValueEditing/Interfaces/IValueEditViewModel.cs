namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IValueEditViewModel : IViewModelBase
    {
        Value Value { get; }

        void Initialise(Value value);
    }
}