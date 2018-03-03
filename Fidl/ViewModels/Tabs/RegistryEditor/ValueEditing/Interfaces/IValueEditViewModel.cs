namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces
{
    using Fidl.ViewModels.Interfaces;

    internal interface IValueEditViewModel : IViewModelBase
    {
        object StoredValue { get; }
    }
}