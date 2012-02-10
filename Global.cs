using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace Trollkit {
    /// <summary>
    /// Project specific globals.
    /// </summary>
    class Global {
        /// <summary>
        /// Returns the folder path of the application
        /// </summary>
        public static String ApplicationFolderPath {
            get { return Application.StartupPath; }
        }

        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\configuration.xml
        /// </summary>
        public static String ConfigurationFilePath {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Babycastles\Trollkit\configuration.xml"; }
        }
    }
}
