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
            //can do some of this in Program.cs

            //TODO: synchronize game config list with the server

            //create a folder if it does not exist

            //load game configurations from folder
            string[] filePaths = Directory.GetFiles(@"..\..\Game Configurations\", "*.xml");
            
            foreach (string filePath in filePaths)
            {
                GameConfiguration gameConfig = new GameConfiguration(filePath);
                gameConfigs.Add(gameConfig);
            }

            //bind combo box
            gameComboBox.DataSource = gameConfigs;
            //gameComboBox.DisplayMember = "GameName"; //optional, and almost must be a property
            gameComboBox.ValueMember = "GameName"; //must be a property, probably should use int id
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            //run the selected game
            GameConfiguration gameConfig = gameConfigs.Single(g => g.GameName == (string)gameComboBox.SelectedValue);
            GameHandler gameHandler = new GameHandler();
            gameHandler.begin(gameConfig);

            //TODO: if game does not exist, download the game, store download URL in game config
            //but also provide a download all option, for a full setup
        }
    }
}
