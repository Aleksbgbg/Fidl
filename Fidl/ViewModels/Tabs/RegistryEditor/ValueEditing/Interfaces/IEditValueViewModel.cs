namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces
{
    using Fidl.ViewModels.Interfaces;

    internal interface IEditValueViewModel : IViewModelBase
    {
        IValueEditViewModel ValueEditViewModel { get; }
    }
}