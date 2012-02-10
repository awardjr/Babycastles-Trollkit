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
        private String portableGamesFolderPath = Global.ApplicationFolderPath + @"\Portable Games\";

        public UserGUI() {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e) {
            //create a config file if it does not exist
            if (!File.Exists(Global.ConfigurationFilePath)) {
                XmlDocument configDoc = new XmlDocument();
                configDoc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><configuration></configuration>");
                Directory.CreateDirectory(Path.GetDirectoryName(Global.ConfigurationFilePath));
                configDoc.Save(Global.ConfigurationFilePath);
            }

            bindGameComboBox();

            bindJoyToKeyComboBox();

            //bind settings checkboxes from config file
            String pathOfLastGamePlayed;

            AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
            using (profile.Buffer()) {
                pathOfLastGamePlayed = profile.GetValue("Settings", "PathOfLastGamePlayed", String.Empty);
                joyToKeyComboBox.SelectedItem = profile.GetValue("Settings", "PathOfLastJoyToKeyConfigUsed", String.Empty);
                fullScreenCheckBox.Checked = profile.GetValue("Settings", "FullScreen", false);
                hideMouseCheckBox.Checked = profile.GetValue("Settings", "HideMouse", false);
            }

            autostartCheckBox.Checked = AutoStart.isOn;

            //autostart last game played
            if (autostartCheckBox.Checked && pathOfLastGamePlayed != String.Empty) {
                //select last game
                String lastGamePlayed = pathOfLastGamePlayed;
                gameComboBox.SelectedItem = gameConfigs.Single(g => g.Path == lastGamePlayed); //TODO: use path or title?

                //play in arcade mode
                arcadeModeCheckBox.Checked = true;
                playButton_Click(null, null);
            }
        }

        private void bindGameComboBox() {
            //add games from the Portable Games folder
            String[] filePaths = Directory.GetFiles(portableGamesFolderPath, "*.exe"); //TODO: add more extensions here

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
            gameComboBox.ValueMember = "Title";
            //joyToKeyComboBox.DisplayMember = "Title";
            gameComboBox.DataSource = gameConfigs;
        }

        private void bindJoyToKeyComboBox() {
            //load JoyToKey configurations from the default folder
            String joyToKeyFolderPath = Global.ApplicationFolderPath + @"\JoyToKey\";
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
            GameConfiguration gameConfig = gameConfigs.Single(g => g.Title == (String)gameComboBox.SelectedValue); //TODO: should store path into value

            if (File.Exists(gameConfig.Path)) {
                //save settings for last game played
                AMS.Profile.Xml profile = new AMS.Profile.Xml(Global.ConfigurationFilePath);
                using (profile.Buffer()) {
                    profile.SetValue("Settings", "PathOfLastGamePlayed", gameConfig.Path);
                    profile.SetValue("Settings", "PathOfLastJoyToKeyConfigUsed", (String)joyToKeyComboBox.SelectedValue);
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
            //if ((String)gameComboBox.SelectedValue == joyToKeyConfigs.SingleOrDefault(c => c.Text == (String)gameComboBox.SelectedValue).Text) //TODO: selectedText?
            //joyToKeyComboBox.SelectedItem = joyToKeyConfigs.Single(c => c.Text == (String)gameComboBox.SelectedValue);
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e) {
            AutoStart.isOn = autostartCheckBox.Checked;
        }


        private void onFormClosing(object sender, FormClosingEventArgs e) {
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
            }
        }

        private void runJoyToKeyButton_Click(object sender, EventArgs e) {
            String joyToKeyFolderPath = Global.ApplicationFolderPath + @"\JoyToKey";
            String joyToKeyFilePath = joyToKeyFolderPath + @"\JoyToKey.exe";

            ProcessStartInfo joyToKeyPsi = new ProcessStartInfo(joyToKeyFilePath);
            joyToKeyPsi.WorkingDirectory = joyToKeyFolderPath;
            Process.Start(joyToKeyPsi);
        }

    }
    /*
    public class UserGUISettings { //TODO: struct?
        public
        (String)joyToKeyComboBox.SelectedValue, arcadeModeCheckBox.Checked, fullScreenCheckBox.Checked, hideMouseCheckBox.Checked
    }
    */
}
