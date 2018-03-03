namespace Fidl.ViewModels.Dialogs.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal interface IEditValueDialogViewModel : IViewModelBase
    {
        IValueEditViewModel ValueEditViewModel { get; }

        void Initialise(Value value);
    }
}