using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using Ionic.Zip;
using Microsoft.Win32;

namespace Trollkit {
    /// <summary>
    /// The main window
    /// </summary>
    public partial class UserGUI : Form {
        private List<GameConfiguration> gameConfigs = new List<GameConfiguration>();
        //private List<General.ListItemData<String>> games = new List<General.ListItemData<String>>();
        private List<General.ListItemData<String>> joyToKeyConfigs = new List<General.ListItemData<String>>();
        private String portableGamesFolderPath = General.ApplicationFolderPath + @"Portable Games\";

        public UserGUI() {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e) {
            //install JoyToKey if it does not exist
            if (!File.Exists(General.ApplicationFolderPath + @"JoyToKey\JoyToKey.exe")) {
                #if (DEBUG)
                String archivePath = @"..\..\JoyToKey.zip";
                #else
                String archivePath = @"JoyToKey.zip";
                #endif

                using (ZipFile zip = ZipFile.Read(archivePath)) {
                    zip.ExtractAll(General.ApplicationFolderPath, ExtractExistingFileAction.DoNotOverwrite); //the archive contains the folder JoyToKey
                }
            }

            #region old code, load game config and bind game combo box
            /*
            //load game configurations from folder
            #if (DEBUG)
                String path = @"..\..\Game Configurations\";
            #else
                String path = @"Game Configurations\";
            #endif

            string[] filePaths = Directory.GetFiles(path, "*.xml");

            foreach (string filePath in filePaths)
                gameConfigs.Add(new GameConfiguration(filePath));
            */

            //bind game combo box
            /*
            gameComboBox.ValueMember = "GameName"; //must be a property, probably should use int id
            //gameComboBox.DisplayMember = "GameName"; //optional, also must be a property, TODO: format to GameName by Author
            gameComboBox.DataSource = gameConfigs; //must set DataSource last!
            */
            #endregion

            //find games
            string[] filePaths = Directory.GetFiles(portableGamesFolderPath, "*.exe"); //TODO: add more extensions here

            foreach (String filePath in filePaths)
                gameConfigs.Add(new GameConfiguration { Path = filePath, Title = Path.GetFileNameWithoutExtension(filePath) });
            //games.Add(General.ListItemData.Create<String>(filePath, Path.GetFileNameWithoutExtension(filePath)));

            //bind game combo box
            gameComboBox.ValueMember = "Title";
            //joyToKeyComboBox.DisplayMember = "Title";
            gameComboBox.DataSource = gameConfigs;

            //load JoyToKey configurations from the default folder //TODO: should have an upload button, copies to default folder
            String joyToKeyFolderPath = General.ApplicationFolderPath + @"JoyToKey\";
            string[] joyToKeyConfigFilePaths = Directory.GetFiles(joyToKeyFolderPath, "*.cfg");

            //add None as a default
            joyToKeyConfigs.Add(General.ListItemData.Create<String>(String.Empty, "None")); //TODO: String.Empty is kinda confusing

            foreach (string filePath in joyToKeyConfigFilePaths)
                joyToKeyConfigs.Add(General.ListItemData.Create<String>(filePath, Path.GetFileNameWithoutExtension(filePath))); //TODO: learn this
            
            //bind JoyToKey combo box
            joyToKeyComboBox.ValueMember = "Value";
            joyToKeyComboBox.DisplayMember = "Text";
            joyToKeyComboBox.DataSource = joyToKeyConfigs;
            joyToKeyComboBox.SelectedItem = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "JoyToKeyConfigPath", String.Empty);

            //bind checkboxes
            autostartCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", "False") == "True"; //doesn't cast to bool
            fullScreenCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "FullScreen", "False") == "True";
            hideMouseCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "HideMouse", "False") == "True";

            //autostart last game played
            if (autostartCheckBox.Checked
                && Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", null) != null) {
                //select last game
                String lastGamePlayed = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", null);
                gameComboBox.SelectedItem = gameConfigs.Single(g => g.Path == lastGamePlayed); //TODO: use path or title?
                
                //play in arcade mode
                arcadeModeCheckBox.Checked = true;
                playButton_Click(null, null);
            }
        }

        private void playButton_Click(object sender, EventArgs e) //rename button to something else?
        {
            GameConfiguration gameConfig = gameConfigs.Single(g => g.Title == (String)gameComboBox.SelectedValue); //TODO: should store path into value

            if (File.Exists(gameConfig.Path)) {
                //set registry key for last game played
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", gameConfig.Path); //TODO: should just use a XML file instead, application config
                //for x64 adds to HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Troll Kit
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "JoyToKeyConfigPath", (String)joyToKeyComboBox.SelectedValue);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "FullScreen", fullScreenCheckBox.Checked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "HideMouse", hideMouseCheckBox.Checked);

                //run the selected game
                this.Hide(); //should lock the window instead?
                GameHandler gameHandler = new GameHandler(ref gameConfig, (String)joyToKeyComboBox.SelectedValue, arcadeModeCheckBox.Checked, fullScreenCheckBox.Checked, hideMouseCheckBox.Checked);
                this.Show();
                return;
            }

            #region extract/download game
            /*
            if (gameConfig.IsPortable)
            {
                if (downloadGame(ref gameConfig) && gameConfig.IsArchived) //TODO: or if archived file exists
                    extractGame(ref gameConfig);
                gameComboBox_SelectedIndexChanged(null, null); //to set the button text to play
                return;
            }

            //TODO: if the game requires installation, download it, direct the user to install it, run in the installer, delete the installer

            //if the game requires installation, direct the user to install the game
            DialogResult dialogResult = MessageBox.Show("This game requires installation. Download the game, then install it in the default location. If it's an extractor, extract it to " + portableGamesFolderPath + gameConfig.GameName + @"\." + "\n\nDo you want to download the game now?", "Whoa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(gameConfig.DownloadUrl);
            }

            //TODO: provide a download all option, for a full setup
            */
            #endregion
        }


        private void gameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            #region more extract/download game code
            /*
            //if game does not exist, change text to download, else, play
            GameConfiguration gameConfig = gameConfigs.Single(g => g.Title == (string)gameComboBox.SelectedValue);
            playButton.Text = File.Exists(gameConfig.Title) ? "Play" : "Download";
            */
            #endregion
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (autostartCheckBox.Checked) {
                //add application shortcut to startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(true);
                #endif

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", true);
            }
            else {
                //remove application shortcut from startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(false);
                #endif

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", false);
            }
        }

        #region downloadGame
        /*
        /// <returns>true upon success</returns>
        private Boolean downloadGame(ref GameConfiguration gameConfig)
        {
            String filename = Path.GetFileName(gameConfig.DownloadUrl);
            String savePath = portableGamesFolderPath + filename;

            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            //TODO: do this asynchronously and display a progress bar and a cancel button
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(gameConfig.DownloadUrl);
                    System.IO.File.WriteAllBytes(savePath, data);
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n\nTry downloading it yourself from " + gameConfig.DownloadUrl,
                    "Failed to download the game");
                //TODO: later, should log failed downloads and send them to to our server, to update broken links
                return false;
            }
        }
        */
        #endregion

        #region extractGame
        /*
        private void extractGame(ref GameConfiguration gameConfig)
        {
            String filename = Path.GetFileName(gameConfig.DownloadUrl);
            String archivePath = portableGamesFolderPath + filename;
            String extractionPath = portableGamesFolderPath + @"\" + gameConfig.Title + @"\"; //add a folder in case the archive does not have a top level folder
            //TODO: later, a better solution would be to check if the archive has one top level folder, then extract depending on that

            //extract the archive
            using (ZipFile zip = ZipFile.Read(archivePath))
            {
                zip.ExtractAll(extractionPath, ExtractExistingFileAction.DoNotOverwrite);
            }

            //delete the archive file
            File.Delete(archivePath);
        }
        */
        #endregion

        private void onFormClosing(object sender, FormClosingEventArgs e) {
            General.tryKillProcess("JoyToKey"); //TODO: eh, not needed
        }
    }
}
