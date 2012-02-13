using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Trollkit {
    /// <summary>
    /// Has one property--isOn.
    /// http://www.geekpedia.com/tutorial151_Run-the-application-at-Windows-startup.html
    /// </summary>
    class AutoStart {
        public static Boolean isOn {
            get {
                // The path to the key where Windows looks for startup applications
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                // Check to see the current state (running at startup or not)
                if (rkApp.GetValue("Trollkit") == null) {
                    // The value doesn't exist, the application is not set to run at startup
                    return false;
                }
                else {
                    // The value exists, the application is set to run at startup
                    return true;
                }
            }
            set {
                // The path to the key where Windows looks for startup applications
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (value) {
                    // Add the value in the registry so that the application runs at startup
                    rkApp.SetValue("Trollkit", Application.ExecutablePath.ToString());
                }
                else {
                    // Remove the value from the registry so that the application doesn't start
                    rkApp.DeleteValue("Trollkit", false);
                }
            }
        }
    }
}
