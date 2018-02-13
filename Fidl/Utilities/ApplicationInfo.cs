namespace Fidl.Utilities
{
    using System.Security.Principal;

    using Fidl.Utilities.Interfaces;

    internal class ApplicationInfo : IApplicationInfo
    {
        public ApplicationInfo()
        {
            LaunchedAsAdministrator = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public bool LaunchedAsAdministrator { get; }
    }
}