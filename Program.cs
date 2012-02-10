//TODO: main list
//add more error handling! Especially with Game Handler
//restart application or operating system if problems occur (memory leak, unrecoverable error, etc.)
//delete c:\portable games folder when uninstalling, or store games in ../portable games?
//change start menu troll kit support shortcut to the github repository
//try to hide title bar or at least the minimize and maximize buttons
//send error log to a server to detect the status of the program. Send computer ID too.
//remove minimize/maxmize butons
//re-center window for non full screen games
//Kunal says Syed should get a demoscene program to show trippy graphics, keygen style, when you launch trollkit
//rename to "Troll Kit"

//TOTAL REDESIGN
//add an auto-updater
//CLEAN THIS SHIT UP, KEEP IT SIMPLE
//test first run of program
//test test test!

//later:
//if JoyToKey config file name is same as game name (or executable name?), choose that in the drop down menu when the game is selected
//still have game configurations for autoselecting options?
//be able to manually add games and JoyToKey configs from other locations, important for installed games in different locations
//should have an upload button, copies JoyToKey config to default folder
//upon clicking a combobox, rebind
//use standard XML parsing code, likely XPATH, not the AMS.Profile library
//seperate game list and download list
//combine .exe and .msi into one installer

//TODO: not code related
//test on the next exhibition
//find other volunteers or friendly open source game programmers

//TODO: current
//cannot find file or assembly Rahil for the installed version. Try the release version
//code project XML vs LINQ to XML
//DESIGN: should have an add/edit/remove window for games and JoyToKey

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
