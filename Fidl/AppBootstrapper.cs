namespace Fidl
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    using Fidl.Factories;
    using Fidl.Factories.Interfaces;
    using Fidl.Services;
    using Fidl.Services.Interfaces;
    using Fidl.Utilities;
    using Fidl.Utilities.Interfaces;
    using Fidl.ViewModels;
    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;
    using Fidl.ViewModels.Tabs.RegistryEditor;
    using Fidl.ViewModels.Tabs.RegistryEditor.Interfaces;
    using Fidl.ViewModels.Tabs.Start;
    using Fidl.ViewModels.Tabs.Start.Interfaces;

    internal class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        internal AppBootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            // Register Services
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<IDialogService, DialogService>();

            _container.Singleton<IDriveIconService, DriveIconService>();
            _container.Singleton<IIniService, IniService>();

            // Register Factories
            _container.Singleton<IDriveFactory, DriveFactory>();
            _container.Singleton<IRegistryFactory, RegistryFactory>();

            // Register Utilities
            _container.Singleton<IApplicationInfo, ApplicationInfo>();

            // Register ViewModels
            _container.Singleton<IShellViewModel, ShellViewModel>();
            _container.Singleton<IMainViewModel, MainViewModel>();
            _container.Singleton<ITabConductorViewModel, TabConductorViewModel>();

            _container.PerRequest<IDialogViewModel, DialogViewModel>();

            // Tab ViewModels
            // Start tab
            _container.Singleton<IStartViewModel, StartViewModel>();

            // Drive Manager tab
            _container.Singleton<IDriveManagerViewModel, DriveManagerViewModel>();

            _container.Singleton<IDriveConductorViewModel, DriveConductorViewModel>();
            _container.PerRequest<IDriveViewModel, DriveViewModel>();

            // Registry Editor tab
            _container.Singleton<IRegistryEditorViewModel, RegistryEditorViewModel>();

            _container.Singleton<IRegistryTreeViewModel, RegistryTreeViewModel>();
            _container.Singleton<IValueDisplayViewModel, ValueDisplayViewModel>();

            _container.PerRequest<IKeyNodeViewModel, KeyNodeViewModel>();
            _container.PerRequest<IValueViewModel, ValueViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        }
    }
}