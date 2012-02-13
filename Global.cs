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
        /*
        /// <summary>
        /// Returns %localappdata%\Babycastles\Trollkit
        /// Windows XP: C:\Documents and Settings\userprofile\Local Settings\Apps
        /// Windows 7: C:\Users\Rahil\AppData\Local
        /// </summary>
        public static String ApplicationFolderPath {
            get { return Application.StartupPath; } //TODO: not used?
        }
        */
        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\
        /// This was used because ClickOnce cannot have elevated priveledges to access Program Files.
        /// </summary>
        public static String CommonApplicationDataFolderPath {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Babycastles\Trollkit"; }
        }

        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\configuration.xml
        /// </summary>
        public static String ConfigurationFilePath {
            get { return Global.CommonApplicationDataFolderPath + @"\configuration.xml"; }
        }
        
        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\JoyToKey
        /// </summary>
        public static String JoyToKeyFolderPath {
            get { return Global.CommonApplicationDataFolderPath + @"\JoyToKey"; }
        }

        /// <summary>
        /// Returns C:\ProgramData\Babycastles\Trollkit\Portable Games
        /// </summary>
        public static String PortableGamesFolderPath {
            get { return Global.CommonApplicationDataFolderPath + @"\Portable Games"; }
        }
    }
}
