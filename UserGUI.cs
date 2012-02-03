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
        private String portableGamesFolderPath = Global.ApplicationFolderPath + @"Portable Games\";

        public UserGUI() {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e) {
            //create a config file if it does not exist
            if (!File.Exists(Global.ConfigurationFilePath)) {
                XmlDocument configDoc = new XmlDocument();
                //configDoc.Load(configFilePath);
                configDoc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><configuration></configuration>");

                // Save xml document to the specified folder path.
                Directory.CreateDirectory(Path.GetDirectoryName(Global.ConfigurationFilePath));
                configDoc.Save(Global.ConfigurationFilePath);
            }

            /*
            XmlDocument configDoc = new XmlDocument();
            //configDoc.Load(configFilePath);
                
            //XmlNode xmlNode = configDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");

            //create <Root> Node
            //XmlElement rootElement = configDoc.CreateElement("configuration");
            //configDoc.AppendChild(rootElement);
                
            //create <InstallationId> Node
            XmlElement installationElement = configDoc.CreateElement("InstallationId");
            XmlText installationIdText = configDoc.CreateTextNode(Guid.Empty.ToString());
            installationElement.AppendChild(installationIdText);
            configDoc.ChildNodes.Item(0).AppendChild(installationElement);
            */

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
            String joyToKeyFolderPath = Global.ApplicationFolderPath + @"JoyToKey\";
            string[] joyToKeyConfigFilePaths = Directory.GetFiles(joyToKeyFolderPath, "*.cfg");

            //add None as a default
            joyToKeyConfigs.Add(Rahil.ListItemData.Create<String>(String.Empty, "None")); //TODO: String.Empty is kinda confusing

            foreach (string filePath in joyToKeyConfigFilePaths)
                joyToKeyConfigs.Add(Rahil.ListItemData.Create<String>(filePath, Path.GetFileNameWithoutExtension(filePath))); //TODO: learn this
            
            //bind JoyToKey combo box
            joyToKeyComboBox.ValueMember = "Value";
            joyToKeyComboBox.DisplayMember = "Text";
            joyToKeyComboBox.DataSource = joyToKeyConfigs;
            joyToKeyComboBox.SelectedItem = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "PathOfLastJoyToKeyConfigUsed", String.Empty);

            //bind checkboxes
            autostartCheckBox.Checked = AutoStart.isOn; //(String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", "False") == "True"; //doesn't cast to bool
            fullScreenCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "FullScreen", "False") == "True"; //save last settings vs only when autostart is on
            hideMouseCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "HideMouse", "False") == "True";

            //autostart last game played
            if (autostartCheckBox.Checked
                && Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "LastGamePlayed", null) != null) {
                //select last game
                String lastGamePlayed = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "LastGamePlayed", null);
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
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "LastGamePlayed", gameConfig.Path); //TODO: should just use a XML file instead, application config
                //for x64 adds to HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Trollkit
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "PathOfLastJoyToKeyConfigUsed", (String)joyToKeyComboBox.SelectedValue);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "FullScreen", fullScreenCheckBox.Checked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "HideMouse", hideMouseCheckBox.Checked);

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

            //if JoyToKey config file of selected game exists and has the same name, select that
            //if ((String)gameComboBox.SelectedValue == joyToKeyConfigs.SingleOrDefault(c => c.Text == (String)gameComboBox.SelectedValue).Text) //TODO: selectedText?
                //joyToKeyComboBox.SelectedItem = joyToKeyConfigs.Single(c => c.Text == (String)gameComboBox.SelectedValue);
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e) {
            AutoStart.isOn = autostartCheckBox.Checked;
            #region old code
            /*
            if (autostartCheckBox.Checked) {
                //add application shortcut to startup folder
                #region old ClickOnce code
                //#if (!DEBUG)
                //ClickOnce.AppShortcut.AutoStart(true);
                //#endif
                #endregion

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", true);
            }
            else {
                //remove application shortcut from startup folder
                #region old ClickOnce code
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(false);
                #endif
                #endregion

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", false);
            }
            */
            #endregion
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
            Rahil.Shared.tryKillProcess("JoyToKey"); //TODO: eh, not needed
        }

        private void browseGameButton_Click(object sender, EventArgs e) {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Game executables (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                //openFileDialog.FileName //test nothing selected and press ok

                //add file path to config file
                //TODO: STOPPED HERE, to create the config file
            }
        }

        private void browseJoyToKeyButton_Click(object sender, EventArgs e) {

        }
    }
}
