//TODO: main list
//add more error handling! Especially with Game Handler
//restart application or operating system if problems occur (memory leak, unrecoverable error, etc.)
//delete c:\portable games folder when uninstalling, or store games in ../portable games?
//change start menu troll kit support shortcut to the github repository
//try to hide title bar or at least the minimize and maximize buttons
//rename to "Troll Kit", remember to update the readme and wiki on Arthur's version
//change from ClickOnce to Windows Installer, it's becoming a nuissance
//add a download bar
//send error log to a server to detect the status of the program. Send computer ID too.
//remove minimize/maxmize butons
//re-center window for non full screen games
//add start menu shortcut for JoyToKey
//Kunal says Syed should get a demoscene program to show trippy graphics, keygen style, when you launch trollkit

//TOTAL REDESIGN
//if JoyToKey config file name is same as game name (or executable name?), choose that in the drop down menu when the game is selected
//seperate game list and download list

//later:
//be able to manually add games and JoyToKey configs from other locations, important for installed games in different locations
//still have game configurations for autoselecting options?

//TODO: not code related
//test on the next exhibition
//find other volunteers or friendly open source game programmers

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Trollkit {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            //start the GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UserGUI());
        }
    }
}
