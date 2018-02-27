namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System;

    using Fidl.EventArgs;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueViewModel : ViewModelBase, IValueViewModel
    {
        public event EventHandler<ValueDeletedEventArgs> Deleted;

        public Value Value { get; private set; }

        public void Initialise(Value value)
        {
            DisplayName = value.Name;
            Value = value;
        }

        public void Delete()
        {
            Value.Delete();
            Deleted?.Invoke(this, new ValueDeletedEventArgs(this));
        }
    }
}