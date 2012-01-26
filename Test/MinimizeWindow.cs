using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Trollkit {
    class MinimizeWindow {
        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        private const Int32 SW_MINIMIZE = 2;

        public static void minimize(String processName) {
            Process[] myProcess = Process.GetProcessesByName(processName);
            foreach (Process p in myProcess) {
                IntPtr hwnd = p.MainWindowHandle;
                ShowWindow(hwnd, SW_MINIMIZE);
            }
        }
    }
}
