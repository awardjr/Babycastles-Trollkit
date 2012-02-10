using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trollkit.Old {
    class Old {
        /*
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
        */

        /*
        //save settings for last game played
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "LastGamePlayed", gameConfig.Path); //TODO: should just use a XML file instead, application config
        //for x64 adds to HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Trollkit
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "PathOfLastJoyToKeyConfigUsed", (String)joyToKeyComboBox.SelectedValue);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "FullScreen", fullScreenCheckBox.Checked);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "HideMouse", hideMouseCheckBox.Checked);
        */

        #region ClickOnce autostart
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

        /*
            //example of writing to xml file
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

            //fullScreenCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "FullScreen", "False") == "True"; //save last settings vs only when autostart is on
            //hideMouseCheckBox.Checked = (String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "HideMouse", "False") == "True";
        //(String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", "False") == "True"; //doesn't cast to bool
        //(String)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "PathOfLastJoyToKeyConfigUsed", String.Empty);


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



        #region more extract/download game code
        /*
            //if game does not exist, change text to download, else, play
            GameConfiguration gameConfig = gameConfigs.Single(g => g.Title == (string)gameComboBox.SelectedValue);
            playButton.Text = File.Exists(gameConfig.Title) ? "Play" : "Download";
            */
        #endregion



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



    }
}
