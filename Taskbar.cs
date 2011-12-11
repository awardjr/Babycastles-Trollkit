using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BabycastlesRunner
{

    class Taskbar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className,
                                                  string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        protected static IntPtr Handle
        {
            get
            {
                return (IntPtr)FindWindow("Shell_TrayWnd", "");
            }
        }

        protected static IntPtr hwndOrb
        {
            get
            {
                return (IntPtr)FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
            }
        }

        private Taskbar()
        {
            // hide ctor
        }

        public static void Show()
        {
            ShowWindow(Handle, SW_SHOW);
            ShowWindow(hwndOrb, SW_SHOW);
        }

        public static void Hide()
        {
            ShowWindow(Handle, SW_HIDE);
            ShowWindow(hwndOrb, SW_HIDE);
        }
    }
}
