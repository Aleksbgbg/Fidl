namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

    using Fidl.EventArgs.RegistryEditor;
    using Fidl.Factories.Interfaces;
    using Fidl.Helpers;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueDisplayViewModel : ViewModelBase, IValueDisplayViewModel, IHandle<IKeyNodeViewModel>
    {
        private readonly IRegistryFactory _registryFactory;

        public ValueDisplayViewModel(IEventAggregator eventAggregator, IRegistryFactory registryFactory)
        {
            _registryFactory = registryFactory;

            eventAggregator.Subscribe(this);

            ((ListCollectionView)CollectionViewSource.GetDefaultView(Values)).CustomSort = ValueComparer.Default;

            Values.CollectionChanged += (sender, e) =>
            {
                void SubscribeNewItems()
                {
                    foreach (IValueViewModel item in e.NewItems)
                    {
                        item.Deleted += ValueDeleted;
                    }
                }

                void UnsubscribeOldItems()
                {
                    foreach (IValueViewModel item in e.OldItems)
                    {
                        item.Deleted -= ValueDeleted;
                    }
                }

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        SubscribeNewItems();
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        UnsubscribeOldItems();
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        UnsubscribeOldItems();
                        SubscribeNewItems();
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        Values.Apply(value => value.Deleted += ValueDeleted);
                        break;
                }
            };
        }

        public IObservableCollection<IValueViewModel> Values { get; } = new BindableCollection<IValueViewModel>();

        private Key _selectedKey;
        public Key SelectedKey
        {
            get => _selectedKey;

            set
            {
                if (_selectedKey == value) return;

                _selectedKey = value;
                NotifyOfPropertyChange(() => SelectedKey);
            }
        }

        public void Handle(IKeyNodeViewModel message)
        {
            if (!message.IsSelected) return;

            SelectedKey = message.Key;

            RefreshValues();
        }

        public void RefreshValues()
        {
            // Necessary as CollectionChanged event does not provide old items on Reset
            // Consider alternative solutions
            Values.Apply(value => value.Deleted -= ValueDeleted);

            Values.Clear();

            if (SelectedKey.RegistryKey == null) return;

            string[] keyValues = SelectedKey.RegistryKey.GetValueNames();

            if (!keyValues.Contains(string.Empty))
            {
                Values.Add(_registryFactory.MakeValue(new Value(SelectedKey.RegistryKey)));
            }

            Values.AddRange(keyValues.Select(valueName => new Value(SelectedKey.RegistryKey, valueName)).Select(_registryFactory.MakeValue));
        }

        private void ValueDeleted(object sender, ValueDeletedEventArgs e)
        {
            Values.Remove(e.Deleted);
        }

        private class ValueComparer : IComparer, IComparer<IValueViewModel>
        {
            internal static ValueComparer Default { get; } = new ValueComparer();

            public int Compare(object first, object second)
            {
                Debug.Assert(first is IValueViewModel, $"{nameof(first)} is {nameof(IValueViewModel)}");
                Debug.Assert(second is IValueViewModel, $"{nameof(second)} is {nameof(IValueViewModel)}");

                return Compare((IValueViewModel)first, (IValueViewModel)second);
            }

            public int Compare(IValueViewModel first, IValueViewModel second)
            {
                Debug.Assert(first != null, $"{nameof(first)} != null");
                Debug.Assert(second != null, $"{nameof(second)} != null");

                if (first.Value.Name == string.Empty)
                {
                    return -1;
                }

                if (second.Value.Name == string.Empty)
                {
                    return 1;
                }

                return LogicalStringComparer.Default.Compare(first.Value.Name, second.Value.Name);
            }
        }
    }
}