namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using Caliburn.Micro;

    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Interfaces;

    internal interface IKeyNodeViewModel : IViewModelBase
    {
        IObservableCollection<IKeyNodeViewModel> Keys { get; }

        Key Key { get; }

        bool IsExpanded { get; set; }

        bool IsSelected { get; set; }

        void Initialise(Key key);

        IKeyNodeViewModel Find(string path);
    }
}