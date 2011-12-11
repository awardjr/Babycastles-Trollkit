//TODO: main list
//need more error handling! Especially with Game Handler
//need to delete c:\portable games folder when uninstalling, or store games in ../portable games?


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
