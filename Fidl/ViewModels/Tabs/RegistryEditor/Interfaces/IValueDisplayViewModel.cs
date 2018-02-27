namespace Fidl.ViewModels.Tabs.RegistryEditor.Interfaces
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Interfaces;

    internal interface IValueDisplayViewModel : IViewModelBase
    {
        IObservableCollection<IValueViewModel> Values { get; }

        void RefreshValues();
    }
}