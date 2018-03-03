namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IEditValueViewModel : IViewModelBase
    {
        IValueEditViewModel ValueEditViewModel { get; }

        void Initialise(Value value);
    }
}