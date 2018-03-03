namespace Fidl.Factories
{
    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Dialogs.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal class RegistryFactory : IRegistryFactory
    {
        public IKeyNodeViewModel MakeKey(Key key)
        {
            IKeyNodeViewModel keyNodeViewModel = IoC.Get<IKeyNodeViewModel>();
            keyNodeViewModel.Initialise(key);

            return keyNodeViewModel;
        }

        public IValueViewModel MakeValue(Value value)
        {
            IValueViewModel valueViewModel = IoC.Get<IValueViewModel>();
            valueViewModel.Initialise(value);

            return valueViewModel;
        }

        public IEditValueDialogViewModel MakeEditValueViewModel(Value value)
        {
            IEditValueDialogViewModel editValueDialogViewModel = IoC.Get<IEditValueDialogViewModel>();
            editValueDialogViewModel.Initialise(value);

            return editValueDialogViewModel;
        }

        public TValueEditViewModel MakeValueEditViewModel<TValueEditViewModel>(Value value) where TValueEditViewModel : IValueEditViewModel
        {
            TValueEditViewModel valueEditViewModel = IoC.Get<TValueEditViewModel>();
            valueEditViewModel.Initialise(value);

            return valueEditViewModel;
        }
    }
}