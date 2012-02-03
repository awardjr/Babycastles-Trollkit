using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Trollkit {
    /// <summary>
    /// Project specific globals.
    /// </summary>
    class Global {
        /// <summary>
        /// Returns Program Files\Trollkit\
        /// </summary>
        public static String ApplicationFolderPath {
            get { return Rahil.Shared.ProgramFilesx86Path + @"Trollkit\"; }
        }

        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\configuration.xml
        /// </summary>
        public static String ConfigurationFilePath {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Babycastles\Trollkit\configuration.xml"; }
        }
    }
}
