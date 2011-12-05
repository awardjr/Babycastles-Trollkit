//main TODO:
//need an installer
//have a game folder with a sample game in it
//synchronize games and config files from a server
//add an autoupdater
//need error handling!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BabycastlesRunner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //start the GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UserGUI());
        }
    }
}
