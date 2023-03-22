using System.Runtime.InteropServices;

namespace ScreenLocker;

internal class Locker
{
    [DllImport("user32")]
    internal static extern void LockWorkStation();
}