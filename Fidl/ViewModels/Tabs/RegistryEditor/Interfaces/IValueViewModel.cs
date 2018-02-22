namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IValueViewModel : IViewModelBase
    {
        Value Value { get; }

        void Initialise(Value value);
    }
}