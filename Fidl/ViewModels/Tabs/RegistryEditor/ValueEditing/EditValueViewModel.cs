namespace Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing
{
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal class EditValueViewModel : ViewModelBase, IEditValueViewModel
    {
        public EditValueViewModel(IValueEditViewModel valueEditViewModel)
        {
            ValueEditViewModel = valueEditViewModel;
        }

        public IValueEditViewModel ValueEditViewModel { get; }
    }
}