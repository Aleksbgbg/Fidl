namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using System;

    using Fidl.EventArgs.RegistryEditor;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IValueViewModel : IViewModelBase
    {
        event EventHandler<ValueDeletedEventArgs> Deleted;

        Value Value { get; }

        void Initialise(Value value);

        bool IsRenaming { get; }

        void Delete();
    }
}