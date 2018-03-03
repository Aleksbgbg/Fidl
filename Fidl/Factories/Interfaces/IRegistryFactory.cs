namespace Fidl.Factories.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor.ValueEditing.Interfaces;

    internal interface IRegistryFactory
    {
        IKeyNodeViewModel MakeKey(Key key);

        IValueViewModel MakeValue(Value value);

        TValueEditViewModel MakeValueEditViewModel<TValueEditViewModel>(object value) where TValueEditViewModel : IValueEditViewModel;
    }
}