namespace Fidl.ViewModels.Dialogs
{
    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Dialogs.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    using Microsoft.Win32;

    internal class EditValueDialogViewModel : ViewModelBase, IEditValueDialogViewModel
    {
        private readonly IRegistryFactory _registryFactory;

        public EditValueDialogViewModel(IRegistryFactory registryFactory)
        {
            _registryFactory = registryFactory;
        }

        public IValueEditViewModel ValueEditViewModel { get; private set; }

        public void Initialise(Value value)
        {
            switch (value.Kind)
            {
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    ValueEditViewModel = _registryFactory.MakeValueEditViewModel<IStringEditViewModel>(value);
                    break;

                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    break;

                case RegistryValueKind.MultiString:
                    ValueEditViewModel = _registryFactory.MakeValueEditViewModel<IMultiStringEditViewModel>(value);
                    break;

                default:
                    break;
            }

            DisplayName = $"Edit {value.Kind}";
        }

        public void Ok()
        {
            ValueEditViewModel.Value.FlushStoredValue();
            TryClose();
        }

        public void Cancel()
        {
            TryClose();
        }

        protected override void OnDeactivate(bool close)
        {
            ValueEditViewModel.Value.ResetStoredValue();
        }
    }
}