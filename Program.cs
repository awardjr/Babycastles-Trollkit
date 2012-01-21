//TODO: main list
//add more error handling! Especially with Game Handler
//restart application or operating system if problems occur (memory leak, unrecoverable error, etc.)
//delete c:\portable games folder when uninstalling, or store games in ../portable games?
//change start menu troll kit support shortcut to the github repository
//try to hide title bar or at least the minimize and maximize buttons
//rename to "Troll Kit", remember to update the readme and wiki on Arthur's version
//change from ClickOnce to Windows Installer, it's becoming a nuissance
//move hide mouse, full screen, use joy to key from XML to the GUI as checkboxes
//add a download bar
//passworded download for retail games?
//send error log to a server to detect the status of the program. Send computer ID too.
//remove minimize/maxmize butons
//re-center window for non full screen games
//add start menu shortcut for JoyToKey

//TOTAL REDESIGN
//have two drop down menus: games, JoyToKey configs
//upon startup, check for games and JoyToKey configs and bind the drop down menus
//be able to manually add games and JoyToKey configs from other locations, important for installed games in different locations
//should be an option for no JoyToKey (checkbox to enable/disable JoyToKey)
//if JoyToKey config file name is same as game name, choose that in the drop down menu when the game is selected

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
