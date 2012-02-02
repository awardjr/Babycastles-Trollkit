using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Trollkit {
    class AutoStart {
        public static void add() {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            // Check to see the current state (running at startup or not)
            if (rkApp.GetValue("MyApp") == null) {
                // The value doesn't exist, the application is not set to run at startup
                chkRun.Checked = false;
            }
            else {
                // The value exists, the application is set to run at startup
                chkRun.Checked = true;
            }
        }

        private static void remove() {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkRun.Checked) {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("MyApp", Application.ExecutablePath.ToString());
            }
            else {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue("MyApp", false);
            }
        }
    }
}
