namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using Fidl.ViewModels.Tabs.Interfaces;

    internal interface IRegistryEditorViewModel : ITabViewModel
    {
        IRegistryTreeViewModel RegistryTreeViewModel { get; }
    }
}