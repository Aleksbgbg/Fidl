namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System;
    using System.IO;
    using System.Linq;

    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class KeyNodeViewModel : ViewModelBase, IKeyNodeViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly IRegistryFactory _registryFactory;

        public KeyNodeViewModel(IEventAggregator eventAggregator, IRegistryFactory registryFactory)
        {
            _eventAggregator = eventAggregator;
            _registryFactory = registryFactory;
        }

        public IObservableCollection<IKeyNodeViewModel> Keys { get; } = new BindableCollection<IKeyNodeViewModel>();

        private Key _key;
        public Key Key
        {
            get => _key;

            private set
            {
                if (_key == value) return;

                _key = value;
                NotifyOfPropertyChange(() => Key);
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;

            set
            {
                if (_isExpanded == value || Key != null && !Key.HasItems) return;

                _isExpanded = value;
                NotifyOfPropertyChange(() => IsExpanded);

                if (!IsValidKey()) return;

                if (_isExpanded)
                {
                    Keys.Clear();
                    Keys.AddRange(Key.GetSubKeys().Select(_registryFactory.MakeKey));
                }
                else
                {
                    Keys.Clear();
                    BalanceKeys();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                if (_isSelected == value) return;

                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);

                _eventAggregator.BeginPublishOnUIThread(this);
            }
        }

        public void Initialise(Key key)
        {
            Key = key;

            if (!IsValidKey()) return;

            DisplayName = Path.GetFileName(key.Name);
            BalanceKeys();
        }

        public IKeyNodeViewModel Find(string path)
        {
            if (Key == null)
            {
                return Keys.Single().Find(path);
            }

            if (Key.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return this;
            }

            string nextName = path.Split('\\').Skip(Key.Path.Count(character => character == '\\')).FirstOrDefault();

            IKeyNodeViewModel nextKey = Keys.SingleOrDefault(keyNodeViewModel => keyNodeViewModel.Key.Name.Equals(nextName, StringComparison.OrdinalIgnoreCase));

            if (nextKey == null)
            {
                return this;
            }

            nextKey.IsExpanded = true;

            return nextKey.Find(path);
        }

        public bool CanToggleExpansion()
        {
            return Key.HasItems;
        }

        public void ToggleExpansion()
        {
            IsExpanded = !IsExpanded;
        }

        private void BalanceKeys()
        {
            if (Key.IsAccessible && Key.HasItems)
            {
                Keys.Add(null);
            }
        }

        private bool IsValidKey()
        {
            return Key != null && Key.Path != "Computer";
        }
    }
}