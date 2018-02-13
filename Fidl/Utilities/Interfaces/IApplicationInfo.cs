namespace Fidl.Utilities.Interfaces
{
    using System;

    internal interface IApplicationInfo
    {
        bool LaunchedAsAdministrator { get; }

        Version Version { get; }
    }
}