using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Trollkit
{
    //this failed
    class Titlebar
    {
        //Finds a window by class name
        //[DllImport("USER32.DLL")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //Sets a window to be a child window of another window
        //[DllImport("USER32.DLL")]
        //public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //Sets window attributes
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Gets window attributes
        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        //assorted constants needed
        public static int GWL_STYLE = -16;
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar

        public static void Hide(IntPtr handle)
        {
            int style = GetWindowLong(handle, GWL_STYLE);
            SetWindowLong(handle, GWL_STYLE, (style & ~WS_CAPTION));
        }
    }
}
