namespace Fidl.Factories
{
    using Caliburn.Micro;

    using Fidl.Factories.Interfaces;
    using Fidl.Models.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class RegistryFactory : IRegistryFactory
    {
        public IKeyNodeViewModel MakeKey(Key key)
        {
            IKeyNodeViewModel keyNodeViewModel = IoC.Get<IKeyNodeViewModel>();
            keyNodeViewModel.Initialise(key);

            return keyNodeViewModel;
        }

        public IValueViewModel MakeValue(Value value)
        {
            IValueViewModel valueViewModel = IoC.Get<IValueViewModel>();
            valueViewModel.Initialise(value);

            return valueViewModel;
        }
    }
}