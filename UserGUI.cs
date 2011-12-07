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
            //String path = Debugger.IsAttached ? @"..\..\Game Configurations\" : @"Game Configurations\"; //#IF DEBUG seems more correct

            string[] filePaths = Directory.GetFiles(@"Game Configurations\", "*.xml");
            
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

        private void playButton_Click(object sender, EventArgs e)
        {
            GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);
            
            //check if game exists
            if (!File.Exists(gameConfig.GamePath))
            {                
                //download the game
                using (WebClient webClient = new WebClient())
                {
                    //String portableFolderPath = General.ProgramFilesx86Path() + @"\TrollKit\Portable Games\"; //permissions error :(
                    String portableFolderPath = @"C:\Portable Games\";
                    String filename = Path.GetFileName(gameConfig.DownloadUrl);
                    String savePath = portableFolderPath + filename;

                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                        
                    byte[] data = webClient.DownloadData(gameConfig.DownloadUrl);
                    System.IO.File.WriteAllBytes(savePath, data);
                }

                //if the game requires installation, place it in the downloads folder and run it when it's complete
                //installer path

                //should provide a clickable link to the game, temp solution
                //MessageBox.Show("This game is not installed! Go download it from " + gameConfig.DownloadUrl, "Whoa");

                return;
            }

            //run the selected game
            this.Hide(); //should lock the window instead?
            GameHandler gameHandler = new GameHandler(gameConfig);
            this.Show();
            
            //TODO: if game does not exist, download the game, store download URL in game config
            //but also provide a download all option, for a full setup
            //is storing the games on our server illegal?
        }

        private void gameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: if game does not exist, change text to download, else, play
            //GameConfiguration gameConfig = GameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);
            //playButton.Text = File.Exists(gameConfig.GamePath) ? "Play" : "Download";
        }
    }
}
