namespace Fidl.Factories.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal interface IRegistryFactory
    {
        IKeyNodeViewModel MakeKey(Key key);

        IValueViewModel MakeValue(Value value);
    }
}