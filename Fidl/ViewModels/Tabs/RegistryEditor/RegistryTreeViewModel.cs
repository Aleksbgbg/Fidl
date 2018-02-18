namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System.IO;
    using System.Linq;

    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    using Microsoft.Win32;

    internal class RegistryTreeViewModel : ViewModelBase, IRegistryTreeViewModel, IHandle<IKeyNodeViewModel>
    {
        public RegistryTreeViewModel(IEventAggregator eventAggregator, IRegistryFactory registryFactory)
        {
            eventAggregator.Subscribe(this);

            IKeyNodeViewModel computerKey = registryFactory.MakeKey(new Key());
            computerKey.Keys.AddRange(new RegistryKey[]
            {
                    Registry.ClassesRoot,
                    Registry.CurrentUser,
                    Registry.LocalMachine,
                    Registry.Users,
                    Registry.CurrentConfig
            }.Select(registryKey => new Key(registryKey)).Select(registryFactory.MakeKey));
            computerKey.IsExpanded = true;

            RootKey = registryFactory.MakeKey(null);
            RootKey.Keys.Add(computerKey);
            RootKey.IsExpanded = true;
        }

        private string _selectedPath = string.Empty;
        public string SelectedPath
        {
            get => _selectedPath;

            set
            {
                if (_selectedPath == value) return;

                _selectedPath = value;
                NotifyOfPropertyChange(() => SelectedPath);
            }
        }

        public IKeyNodeViewModel RootKey { get; }

        public void NavigateToSelectedPath()
        {
            NavigateTo(SelectedPath);
        }

        public void NavigateTo(string path)
        {
            RootKey.Find(path).IsSelected = true;
        }

        public void Handle(IKeyNodeViewModel message)
        {
            if (message.IsSelected)
            {
                SelectedPath = message.Key.Path;
            }
        }
    }
}