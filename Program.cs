//TODO: main list
//add more error handling! Especially with Game Handler
//restart application or operating system if problems occur (memory leak, unrecoverable error, etc.)
//delete c:\portable games folder when uninstalling, or store games in ../portable games?
//change start menu troll kit support shortcut to the github repository
//try to hide title bar or at least the minimize and maximize buttons
//rename to "Troll Kit", remember to update the readme and wiki on Arthur's version
//change from ClickOnce to Windows Installer, it's becoming a nuissance

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Trollkit
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
