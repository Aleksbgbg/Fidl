namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using System.Linq;

    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueDisplayViewModel : ViewModelBase, IValueDisplayViewModel, IHandle<IKeyNodeViewModel>
    {
        private readonly IRegistryFactory _registryFactory;

        public ValueDisplayViewModel(IEventAggregator eventAggregator, IRegistryFactory registryFactory)
        {
            _registryFactory = registryFactory;

            eventAggregator.Subscribe(this);
        }

        public IObservableCollection<IValueViewModel> Values { get; } = new BindableCollection<IValueViewModel>();

        public void Handle(IKeyNodeViewModel message)
        {
            if (!message.IsSelected) return;

            Values.Clear();

            if (message.Key.RegistryKey == null) return;

            string[] keyValues = message.Key.RegistryKey.GetValueNames();

            if (!keyValues.Contains(string.Empty))
            {
                Values.Add(_registryFactory.MakeValue(new Value(message.Key.RegistryKey, string.Empty, false)));
            }

            Values.AddRange(keyValues.Select(valueName => new Value(message.Key.RegistryKey, valueName))
                                     .Select(_registryFactory.MakeValue));
        }
    }
}