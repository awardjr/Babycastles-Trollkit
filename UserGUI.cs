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
        private List<GameConfiguration> gameConfigs = new List<GameConfiguration>();

        public UserGUI()
        {
            InitializeComponent();
        }

        private void UserGUI_Load(object sender, EventArgs e)
        {
            //TODO: synchronize game config list with the server
            //eh, should just update the entire program along with xml config files on startup

            //load game configurations from folder
            //String path = Debugger.IsAttached ? @"..\..\Game Configurations\" : @"Game Configurations\"; //#IF DEBUG seems more correct

            string[] filePaths = Directory.GetFiles(@"Game Configurations\", "*.xml");
            
            foreach (string filePath in filePaths)
            {
                GameConfiguration gameConfig = new GameConfiguration(filePath);
                gameConfigs.Add(gameConfig);
            }

            //bind combo box
            gameComboBox.DataSource = gameConfigs;
            gameComboBox.ValueMember = "GameName"; //must be a property, probably should use int id
            //gameComboBox.DisplayMember = "GameName"; //optional, also must be a property
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            //run the selected game
            GameConfiguration gameConfig = gameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);
            GameHandler gameHandler = new GameHandler(gameConfig);

            //TODO: if game does not exist, download the game, store download URL in game config
            //but also provide a download all option, for a full setup
            //is storing the games on our server illegal?
        }
    }
}
