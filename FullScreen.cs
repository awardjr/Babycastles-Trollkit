using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace BabycastlesRunner
{
    class FullScreen
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)] //is charset needed?
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,int x,int y,int cx,int cy, UInt32 uFlags);

        public static IntPtr HWND_TOPMOST = (IntPtr)(-1);
        public const int SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static void test(Process process)
        {
            //TODO: may need to store process name and find handle by that later on, GetWindowModuleFileName (function may have deprecated!)
            //int isSuccessful = SetWindowPos(process.MainWindowHandle, HWND_TOPMOST, 0, 0, Screen.PrimaryScreen.Bounds.Right,
            //                                  Screen.PrimaryScreen.Bounds.Bottom, SWP_SHOWWINDOW);

            bool isSuccessful = MoveWindow(process.MainWindowHandle, 0, 0, Screen.PrimaryScreen.Bounds.Right-400,
                                            Screen.PrimaryScreen.Bounds.Bottom -400, true);

            Debug.WriteLine(isSuccessful);
            Debug.WriteLine(Marshal.GetLastWin32Error());
            if (!isSuccessful)
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error()); //not working? =/
        }
    }
}
