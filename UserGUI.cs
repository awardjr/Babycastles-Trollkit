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
using System.Xml;

namespace Trollkit {
    /// <summary>
    /// The main window
    /// </summary>
    public partial class UserGUI : Form {
        private List<GameConfiguration> gameConfigs = new List<GameConfiguration>();
        private List<Rahil.ListItemData<String>> joyToKeyConfigs = new List<Rahil.ListItemData<String>>();

        public UserGUI() {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e) {
            //install JoyToKey if it does not exist
            if (!File.Exists(Global.JoyToKeyFolderPath + "JoyToKey.exe")) {
                #if (DEBUG)
                String archivePath = @"..\..\JoyToKey.zip";
                #else
                String archivePath = @"JoyToKey.zip";
                #endif

                using (ZipFile zip = ZipFile.Read(archivePath)) {
                    zip.ExtractAll(Global.CommonApplicationDataFolderPath, ExtractExistingFileAction.DoNotOverwrite); //the archive contains the folder JoyToKey
                }
            }

            //create a config file if it does not exist
            if (!File.Exists(Global.ConfigurationFilePath)) {
                XmlDocument configDoc = new XmlDocument();
                configDoc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><configuration><section name=\"GamePaths\"></section></configuration>");
                Directory.CreateDirectory(Path.GetDirectoryName(Global.ConfigurationFilePath));
                configDoc.Save(Global.ConfigurationFilePath);
            }

            //create a portable games folder if it does not exist
            if (!Directory.Exists(Global.PortableGamesFolderPath))
                Directory.CreateDirectory(Global.PortableGamesFolderPath);

            bindGameComboBox();

            bindJoyToKeyComboBox();

            //bind settings checkboxes from config file
            String pathOfLastGamePlayed;
            Boolean autostartCheckBoxValue;

            //see http://www.codeproject.com/Articles/5304/Read-Write-XML-files-Config-files-INI-files-or-the
            AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
            using (profile.Buffer()) {
                pathOfLastGamePlayed = profile.GetValue("Settings", "PathOfLastGamePlayed", String.Empty);
                joyToKeyComboBox.SelectedItem = profile.GetValue("Settings", "PathOfLastJoyToKeyConfigUsed", String.Empty);
                arcadeModeCheckBox.Checked = profile.GetValue("Settings", "ArcadeMode", false);
                fullScreenCheckBox.Checked = profile.GetValue("Settings", "FullScreen", false);
                hideMouseCheckBox.Checked = profile.GetValue("Settings", "HideMouse", false);
                autostartCheckBoxValue = profile.GetValue("Settings", "AutoStart", false);
            }

            autostartCheckBox.Checked = autostartCheckBoxValue; //TODO: hackish, save all data on closing instead?

            //autostart last game played
            if (autostartCheckBox.Checked && arcadeModeCheckBox.Checked == true && pathOfLastGamePlayed != String.Empty) {
                //select last game
                gameComboBox.SelectedItem = gameConfigs.Single(g => g.Path == pathOfLastGamePlayed);

                //play
                playButton_Click(null, null);
            }
        }

        private void bindGameComboBox() {
            gameConfigs.Clear();
            gameComboBox.DataSource = null;

            //add None as a default
            gameConfigs.Add(new GameConfiguration { Path = String.Empty, Title = "None" });

            //add games from the Portable Games folder
            String[] filePaths = Directory.GetFiles(Global.PortableGamesFolderPath, "*.*")
                .Where(file => file.EndsWith(".exe") || file.EndsWith(".swf"))
                .ToArray();

            foreach (String filePath in filePaths)
                gameConfigs.Add(new GameConfiguration { Path = filePath, Title = Path.GetFileNameWithoutExtension(filePath) });

            //add games from the config file
            AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
            using (profile.Buffer()) {
                String[] entries = profile.GetEntryNames("GamePaths");

                foreach (String entry in entries) {
                    String gamePath = (String)profile.GetValue("GamePaths", entry);
                    
                    //if game does not exist delete the entry from the config file
                    if (!File.Exists(gamePath)) {
                        profile.RemoveEntry("GamePaths", entry);
                        continue;
                    }

                    gameConfigs.Add(new GameConfiguration { Path = gamePath, Title = Path.GetFileNameWithoutExtension(gamePath) });
                }
            }

            //bind game combo box
            gameComboBox.ValueMember = "Path";
            gameComboBox.DisplayMember = "Title";
            gameComboBox.DataSource = gameConfigs;
        }

        private void bindJoyToKeyComboBox() {
            joyToKeyConfigs.Clear();
            joyToKeyComboBox.DataSource = null;

            //load JoyToKey configurations from the default folder
            String joyToKeyFolderPath = Global.CommonApplicationDataFolderPath + @"\JoyToKey\";
            string[] joyToKeyConfigFilePaths = Directory.GetFiles(joyToKeyFolderPath, "*.cfg");

            //add None as a default
            joyToKeyConfigs.Add(Rahil.ListItemData.Create<String>(String.Empty, "None")); //TODO: String.Empty is kinda confusing

            foreach (string filePath in joyToKeyConfigFilePaths)
                joyToKeyConfigs.Add(Rahil.ListItemData.Create<String>(filePath, Path.GetFileNameWithoutExtension(filePath)));

            //bind JoyToKey combo box
            joyToKeyComboBox.ValueMember = "Value";
            joyToKeyComboBox.DisplayMember = "Text";
            joyToKeyComboBox.DataSource = joyToKeyConfigs;
        }

        private void playButton_Click(object sender, EventArgs e) //rename button to something else?
        {
            GameConfiguration gameConfig = gameConfigs.Single(g => g.Path == (String)gameComboBox.SelectedValue);

            if (File.Exists(gameConfig.Path)) {
                //save settings for last game played
                AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
                using (profile.Buffer()) {
                    profile.SetValue("Settings", "PathOfLastGamePlayed", gameConfig.Path);
                    profile.SetValue("Settings", "PathOfLastJoyToKeyConfigUsed", (String)joyToKeyComboBox.SelectedValue);
                    profile.SetValue("Settings", "ArcadeMode", arcadeModeCheckBox.Checked);
                    profile.SetValue("Settings", "FullScreen", fullScreenCheckBox.Checked);
                    profile.SetValue("Settings", "HideMouse", hideMouseCheckBox.Checked);
                }

                //run the selected game
                this.Hide(); //should lock the window instead?
                GameHandler gameHandler = new GameHandler(ref gameConfig, (String)joyToKeyComboBox.SelectedValue, arcadeModeCheckBox.Checked, fullScreenCheckBox.Checked, hideMouseCheckBox.Checked);
                this.Show();
                return;
            }
        }

        private void gameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            //if JoyToKey config file of selected game exists and has the same name, select that
            String selectedGameTitle = ((GameConfiguration)gameComboBox.SelectedItem).Title;
            if (joyToKeyConfigs.Any(c => c.Text == selectedGameTitle))
                joyToKeyComboBox.SelectedItem = joyToKeyConfigs.First(c => c.Text == selectedGameTitle);
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (autostartCheckBox.Checked) {
                //add application shortcut to startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(true);
                #endif

                AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
                using (profile.Buffer()) {
                    profile.SetValue("Settings", "AutoStart", true);
                }
            }
            else {
                //remove application shortcut from startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(false);
                #endif

                AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
                using (profile.Buffer()) {
                    profile.SetValue("Settings", "AutoStart", false);
                }
            }
        }


        private void UserGUI_FormClosing(object sender, FormClosingEventArgs e) {
            Rahil.Shared.tryKillProcess("JoyToKey");
        }

        private void browseGameButton_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Game executables (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                //add file path to config file
                AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
                using (profile.Buffer()) {
                    profile.SetValue("GamePaths", Path.GetFileNameWithoutExtension(openFileDialog.FileName), openFileDialog.FileName);
                }

                bindGameComboBox();
                gameComboBox.SelectedItem = gameConfigs.Single(g => g.Path == openFileDialog.FileName);
            }
        }

        private void runJoyToKeyButton_Click(object sender, EventArgs e) {
            String joyToKeyFolderPath = Global.JoyToKeyFolderPath;
            String joyToKeyFilePath = joyToKeyFolderPath + @"\JoyToKey.exe";

            ProcessStartInfo joyToKeyPsi = new ProcessStartInfo(joyToKeyFilePath);
            joyToKeyPsi.WorkingDirectory = joyToKeyFolderPath;
            Process.Start(joyToKeyPsi);
        }

        private void joyToKeyComboBox_DropDown(object sender, EventArgs e) {
            bindJoyToKeyComboBox();
        }

        private void gameComboBox_DropDown(object sender, EventArgs e) {
            //bindGameComboBox();
        }

    }
    /*
    public class UserGUISettings { //TODO: struct?
        public
        (String)joyToKeyComboBox.SelectedValue, arcadeModeCheckBox.Checked, fullScreenCheckBox.Checked, hideMouseCheckBox.Checked
    }
    */
}
