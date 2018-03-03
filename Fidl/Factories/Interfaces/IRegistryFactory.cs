namespace Fidl.Factories.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Dialogs.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal interface IRegistryFactory
    {
        IKeyNodeViewModel MakeKey(Key key);

        IValueViewModel MakeValue(Value value);

        IEditValueDialogViewModel MakeEditValueViewModel(Value value);

        TValueEditViewModel MakeValueEditViewModel<TValueEditViewModel>(Value value) where TValueEditViewModel : IValueEditViewModel;
    }
}