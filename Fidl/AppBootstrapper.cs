﻿namespace Fidl
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Caliburn.Micro;

    using Fidl.ViewModels;
    using Fidl.ViewModels.Interfaces;
    using Fidl.ViewModels.Tabs.DriveManager;
    using Fidl.ViewModels.Tabs.DriveManager.Interfaces;
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

            // Register ViewModels
            _container.Singleton<IShellViewModel, ShellViewModel>();
            _container.Singleton<IMainViewModel, MainViewModel>();
            _container.Singleton<ITabConductorViewModel, TabConductorViewModel>();

            // Tab ViewModels
            _container.Singleton<IStartViewModel, StartViewModel>();
            _container.Singleton<IDriveManagerViewModel, DriveManagerViewModel>();
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