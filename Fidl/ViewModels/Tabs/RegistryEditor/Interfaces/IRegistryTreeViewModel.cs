namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using Fidl.ViewModels.Interfaces;

    internal interface IRegistryTreeViewModel : IViewModelBase
    {
        string SelectedPath { get; set; }

        IKeyNodeViewModel RootKey { get; }

        void NavigateToSelectedPath();

        void NavigateTo(string path);
    }
}