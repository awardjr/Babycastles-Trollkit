using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trollkit.Old {
    class Old {
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
