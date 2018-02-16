namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class RegistryEditorViewModel : TabViewModel, IRegistryEditorViewModel
    {
        public RegistryEditorViewModel() : base("Regedit", "Browse and modify the Windows registry.")
        {
        }
    }
}