namespace Fidl.ViewModels.Tabs.Start
{
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class StartViewModel : TabViewModel, IStartViewModel
    {
        public StartViewModel() : base("Logo", "Welcome to Fidl!")
        {
        }
    }
}