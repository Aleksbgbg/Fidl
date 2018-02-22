namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System.Windows.Input;

    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class RegistryEditorViewModel : TabViewModel, IRegistryEditorViewModel
    {
        public RegistryEditorViewModel(IRegistryTreeViewModel registryTreeViewModel, IValueDisplayViewModel valueDisplayViewModel) : base("Regedit", "Browse and modify the Windows registry.")
        {
            RegistryTreeViewModel = registryTreeViewModel;
            ValueDisplayViewModel = valueDisplayViewModel;
        }

        public IRegistryTreeViewModel RegistryTreeViewModel { get; }

        public IValueDisplayViewModel ValueDisplayViewModel { get; }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RegistryTreeViewModel.NavigateToSelectedPath();
            }
        }
    }
}