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

namespace Trollkit
{
    /// <summary>
    /// The main window
    /// </summary>
    public partial class UserGUI : Form
    {
        private List<GameConfiguration> GameConfigs = new List<GameConfiguration>();
        private String portableGamesFolderPath = General.ApplicationFolderPath + @"Portable Games\";

        public UserGUI()
        {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e)
        {
            //load game configurations from folder
            #if (DEBUG)
                String path = @"..\..\Game Configurations\";
            #else
                String path = @"Game Configurations\";
            #endif

            string[] filePaths = Directory.GetFiles(path, "*.xml");

            foreach (string filePath in filePaths)
                GameConfigs.Add(new GameConfiguration(filePath));

            //bind combo box
            gameComboBox.ValueMember = "GameName"; //must be a property, probably should use int id
            //gameComboBox.DisplayMember = "GameName"; //optional, also must be a property, TODO: format to GameName by Author
            gameComboBox.DataSource = GameConfigs; //must set DataSource last!

            //bind autostart checkbox
            autostartCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", false) == "True"; //doesn't cast to bool

            //install JoyToKey if it does not exist //TODO: need to test this for release
            if (!File.Exists(General.ApplicationFolderPath + @"JoyToKey\JoyToKey.exe"))
            {
                #if (DEBUG)
                String archivePath = @"..\..\JoyToKey.zip";
                #else
                String archivePath = @"JoyToKey.zip";
                #endif

                using (ZipFile zip = ZipFile.Read(archivePath))
                {
                    zip.ExtractAll(General.ApplicationFolderPath, ExtractExistingFileAction.DoNotOverwrite);
                }
            }

            //autostart last game played
            if (autostartCheckBox.Checked
                && Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", null) != null)
            {
                //select last game
                String lastGamePlayed = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", null);
                gameComboBox.SelectedItem = GameConfigs.Single(g => g.GameName == lastGamePlayed);

                //play it in arcade mode
                arcadeModeCheckBox.Checked = true;
                playButton_Click(null, null);
            }
        }

        private void playButton_Click(object sender, EventArgs e) //rename button to something else?
        {
            GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (String)gameComboBox.SelectedValue);

            if (File.Exists(gameConfig.GamePath))
            {
                //set registry key for last game played
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "LastGamePlayed", gameConfig.GameName);
                //for x64 adds to HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Troll Kit

                //run the selected game
                this.Hide(); //should lock the window instead?
                GameHandler gameHandler = new GameHandler(ref gameConfig, arcadeModeCheckBox.Checked);
                this.Show();
                return;
            }

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
        }

        private void gameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if game does not exist, change text to download, else, play
            GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);
            playButton.Text = File.Exists(gameConfig.GamePath) ? "Play" : "Download";
        }

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autostartCheckBox.Checked)
            {
                //add application shortcut to startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(true);
                #endif

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", true);
            }
            else
            {
                //remove application shortcut from startup folder
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(false);
                #endif

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Troll Kit", "AutostartLastGame", false);
            }
        }

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

        private void extractGame(ref GameConfiguration gameConfig)
        {
            String filename = Path.GetFileName(gameConfig.DownloadUrl);
            String archivePath = portableGamesFolderPath + filename;
            String extractionPath = portableGamesFolderPath + @"\" + gameConfig.GameName + @"\"; //add a folder in case the archive does not have a top level folder
            //TODO: later, a better solution would be to check if the archive has one top level folder, then extract depending on that

            //extract the archive
            using (ZipFile zip = ZipFile.Read(archivePath))
            {
                zip.ExtractAll(extractionPath, ExtractExistingFileAction.DoNotOverwrite);
            }

            //delete the archive file
            File.Delete(archivePath);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            General.tryKillProcess("JoyToKey"); //TODO: eh, not needed
        }
    }
}
