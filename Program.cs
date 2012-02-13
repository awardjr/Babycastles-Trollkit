//TODO: main list
//add more error handling!
//change start menu troll kit support shortcut to the github repository
//try to hide title bar or at least the minimize and maximize buttons
//send error log to a server to detect the status of the program. Send computer ID too.
//remove minimize/maxmize butons
//re-center window for non full screen games
//Kunal says Syed should get a demoscene program to show trippy graphics, keygen style, when you launch trollkit
//rename to "Troll Kit", just the title, not in the code?
//ask Arthur for original Trollkit github repository
//delete files upon uninstall
//test full screen/hide mouse/etc in a non ClickOnce application to determine if the problem is related to priveleges
//clickonce elevated privelege using app.manifest and publish manifest?
//use windows installer, write auto-update code, move everything to program files folder

//TOTAL REDESIGN
//CLEAN THIS SHIT UP, KEEP IT SIMPLE
//test first run of program
//test test test!

//if arcade mode, do not auto update

//later:
//if JoyToKey config file name is same as game name (or executable name?), choose that in the drop down menu when the game is selected
//be able to manually add games and JoyToKey configs from other locations, important for installed games in different locations
//should have an upload button, copies JoyToKey config to default folder
//upon clicking a combobox, rebind
//use standard XML parsing code, likely XPATH, not the AMS.Profile library, http://www.codeproject.com/Articles/9494/Manipulate-XML-data-with-XPath-and-XmlDocument-C
//add game configurations to automatically set settings (full screen, hide mouse) for each game?
//add download game functionality

//much later:
//certificate

//not code related
//test on the next exhibition
//find other volunteers or friendly open source game programmers

//current
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
