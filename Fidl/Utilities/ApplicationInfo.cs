namespace Fidl.Utilities
{
    using System;
    using System.Reflection;
    using System.Security.Principal;

    using Fidl.Utilities.Interfaces;

    internal class ApplicationInfo : IApplicationInfo
    {
        public ApplicationInfo()
        {
            LaunchedAsAdministrator = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            Version = Assembly.GetEntryAssembly().GetName().Version;
        }

        public bool LaunchedAsAdministrator { get; }

        public Version Version { get; }
    }
}