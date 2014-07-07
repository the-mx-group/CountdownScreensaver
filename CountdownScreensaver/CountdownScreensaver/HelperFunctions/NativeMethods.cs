using System;
using System.Runtime.InteropServices;

namespace CountdownScreensaver.HelperFunctions
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern bool LockWorkStation();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            UInt32 msg,
            IntPtr wParam,
            IntPtr lParam
            );

        internal static void DisplayOff()
        {
            SendMessage(
                (IntPtr)0xffff, // HWND_BROADCAST
                0x0112, // WM_SYSCOMMAND
                (IntPtr)0xf170, // SC_MONITORPOWER
                (IntPtr)0x0002 // POWER_OFF
                );
        }
    }
}