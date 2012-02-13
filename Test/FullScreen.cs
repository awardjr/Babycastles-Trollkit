using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace Trollkit {
    //this failed
    class FullScreen {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] //is charset needed?
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, UInt32 uFlags);

        public static IntPtr HWND_TOPMOST = (IntPtr)(-1);
        public const int SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static void set(IntPtr handle) {
            //TODO: don't know what's wrong. The handle seems fine. Maybe beccause the application doesn't have elevated priveledges?
            int isSuccessful = SetWindowPos(handle, HWND_TOPMOST, 0, 0, Screen.PrimaryScreen.Bounds.Right,
                                              Screen.PrimaryScreen.Bounds.Bottom, SWP_SHOWWINDOW);

            //bool isSuccessful = MoveWindow(process.MainWindowHandle, 0, 0, Screen.PrimaryScreen.Bounds.Right,
            //                                Screen.PrimaryScreen.Bounds.Bottom, true);

            Debug.WriteLine(isSuccessful);
            Debug.WriteLine(Marshal.GetLastWin32Error()); //1008
            Debug.WriteLine(Marshal.GetHRForLastWin32Error());
            if (isSuccessful == 0)
            //if (isSuccessful)
            {
                Debug.WriteLine("fullscreen failed");
                //throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()); //An attempt was made to reference a token that does not exist
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error()); //not working? =/
                //Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error()); //fail
            }
        }
    }
}
