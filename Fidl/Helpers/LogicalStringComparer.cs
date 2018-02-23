namespace Fidl.Helpers
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class LogicalStringComparer : IComparer<string>
    {
        internal static LogicalStringComparer Default { get; } = new LogicalStringComparer();

        public int Compare(string first, string second)
        {
            return StrCmpLogicalW(first, second);
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string first, string second);
    }
}