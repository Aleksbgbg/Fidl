namespace Fidl.ViewModels.Tabs.RegistryEditor
{
    using Caliburn.Micro;

    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;

    internal class ValueDisplayViewModel : ViewModelBase, IValueDisplayViewModel
    {
        public IObservableCollection<IValueViewModel> Values { get; } = new BindableCollection<IValueViewModel>();
    }
}