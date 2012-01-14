//TODO: main list
//add more error handling! Especially with Game Handler
//delete c:\portable games folder when uninstalling, or store games in ../portable games?
//change start menu troll kit support shortcut to the github repository


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
