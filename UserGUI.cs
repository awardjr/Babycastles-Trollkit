#define DEBUG

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

namespace BabycastlesRunner
{
    /// <summary>
    /// The main window
    /// </summary>
    public partial class UserGUI : Form
    {
        private List<GameConfiguration> GameConfigs = new List<GameConfiguration>();

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
            {
                GameConfiguration gameConfig = new GameConfiguration(filePath);
                GameConfigs.Add(gameConfig);
            }

            //bind combo box
            gameComboBox.DataSource = GameConfigs;
            gameComboBox.ValueMember = "GameName"; //must be a property, probably should use int id
            //gameComboBox.DisplayMember = "GameName"; //optional, also must be a property
        }

        private void playButton_Click(object sender, EventArgs e) //rename button to something else?
        {
            GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);

            if (File.Exists(gameConfig.GamePath))
            {
                //run the selected game
                this.Hide(); //should lock the window instead?
                GameHandler gameHandler = new GameHandler(gameConfig, arcadeModeCheckBox.Checked);
                this.Show();
                return;
            }

            if (gameConfig.IsPortable)
            {
                if (downloadGame(gameConfig) && gameConfig.IsArchived) //TODO: or if archived file exists
                    extractGame(gameConfig); //TODO: should use references to gameconfig every time
                gameComboBox_SelectedIndexChanged(null, null); //to set the button text to play
                return;
            }

            //if the game requires installation, place it in the downloads folder, run it, delete it
            //installer path

            //provide a clickable link to the game
            MessageBox.Show("This game requires installation! Download it from " + gameConfig.DownloadUrl + " and install it in the default location", "Whoa");
            
            //TODO: if game does not exist, download the game, store download URL in game config
            //but also provide a download all option, for a full setup
            //is storing the games on our server illegal?
        }

        private void gameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if game does not exist, change text to download, else, play
            //GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue); //why is it returning GameConfiguration here?
            //GameConfiguration gameConfig = (GameConfiguration)gameComboBox.SelectedValue;
            //playButton.Text = File.Exists(gameConfig.GamePath) ? "Play" : "Download";
        }

        /// <returns>true upon success</returns>
        private Boolean downloadGame(GameConfiguration gameConfig)
        {
            //TODO: do this asynchronously and display a progress bar
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //String portableFolderPath = General.ProgramFilesx86Path() + @"\TrollKit\Portable Games\"; //permissions error =(
                    String portableFolderPath = @"C:\Portable Games\";
                    String filename = Path.GetFileName(gameConfig.DownloadUrl);
                    String savePath = portableFolderPath + filename;

                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                    byte[] data = webClient.DownloadData(gameConfig.DownloadUrl);
                    System.IO.File.WriteAllBytes(savePath, data);
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n\nTry downloading it yourself from " + gameConfig.DownloadUrl,
                    "Failed to download the game");
                return false;
            }
        }

        private void extractGame(GameConfiguration gameConfig)
        {
            //TODO: duplicate code
            //String portableFolderPath = General.ProgramFilesx86Path() + @"\TrollKit\Portable Games\"; //permissions error =(
            String portableFolderPath = @"C:\Portable Games\";
            String filename = Path.GetFileName(gameConfig.DownloadUrl);
            String archivePath = portableFolderPath + filename;
            
            //extract the archive
            using (ZipFile zip = ZipFile.Read(archivePath))
            {
                zip.ExtractAll(portableFolderPath);
            }

            //delete the archive file
            File.Delete(archivePath);
        }
    }
}
