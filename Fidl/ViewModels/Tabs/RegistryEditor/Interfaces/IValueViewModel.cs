namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using System;

    using Fidl.EventArgs;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IValueViewModel : IViewModelBase
    {
        event EventHandler<ValueDeletedEventArgs> Deleted;

        Value Value { get; }

        void Initialise(Value value);
    }
}