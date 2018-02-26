namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Data;

    using Caliburn.Micro;

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

            Values.Clear();

            SelectedKey = message.Key;

            if (SelectedKey.RegistryKey == null) return;

            string[] keyValues = SelectedKey.RegistryKey.GetValueNames();

            if (!keyValues.Contains(string.Empty))
            {
                Values.Add(_registryFactory.MakeValue(new Value()));
            }

            Values.AddRange(keyValues.Select(valueName => new Value(SelectedKey.RegistryKey, valueName))
                                     .Select(_registryFactory.MakeValue));
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