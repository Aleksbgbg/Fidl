namespace Fidl.EventArgs
{
    using System;

    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueDeletedEventArgs : EventArgs
    {
        internal ValueDeletedEventArgs(IValueViewModel deleted)
        {
            Deleted = deleted;
        }

        public IValueViewModel Deleted { get; }
    }
}