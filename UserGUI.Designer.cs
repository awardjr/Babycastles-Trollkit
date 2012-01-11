namespace Trollkit
{
    partial class UserGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.gameComboBox = new System.Windows.Forms.ComboBox();
            this.playButton = new System.Windows.Forms.Button();
            this.arcadeModeCheckBox = new System.Windows.Forms.CheckBox();
            this.autostartCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a game:";
            // 
            // gameComboBox
            // 
            this.gameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameComboBox.FormattingEnabled = true;
            this.gameComboBox.Location = new System.Drawing.Point(95, 12);
            this.gameComboBox.Name = "gameComboBox";
            this.gameComboBox.Size = new System.Drawing.Size(121, 21);
            this.gameComboBox.TabIndex = 1;
            this.gameComboBox.SelectedIndexChanged += new System.EventHandler(this.gameComboBox_SelectedIndexChanged);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(222, 10);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // arcadeModeCheckBox
            // 
            this.arcadeModeCheckBox.AutoSize = true;
            this.arcadeModeCheckBox.Location = new System.Drawing.Point(87, 39);
            this.arcadeModeCheckBox.Name = "arcadeModeCheckBox";
            this.arcadeModeCheckBox.Size = new System.Drawing.Size(208, 17);
            this.arcadeModeCheckBox.TabIndex = 3;
            this.arcadeModeCheckBox.Text = "Arcade Mode (F2 to restart, F4 to stop)";
            this.arcadeModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // autostartCheckBox
            // 
            this.autostartCheckBox.AutoSize = true;
            this.autostartCheckBox.Location = new System.Drawing.Point(87, 62);
            this.autostartCheckBox.Name = "autostartCheckBox";
            this.autostartCheckBox.Size = new System.Drawing.Size(172, 17);
            this.autostartCheckBox.TabIndex = 4;
            this.autostartCheckBox.Text = "Autostart with last game played";
            this.autostartCheckBox.UseVisualStyleBackColor = true;
            this.autostartCheckBox.CheckedChanged += new System.EventHandler(this.autostartCheckBox_CheckedChanged);
            // 
            // UserGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 82);
            this.Controls.Add(this.autostartCheckBox);
            this.Controls.Add(this.arcadeModeCheckBox);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.gameComboBox);
            this.Controls.Add(this.label1);
            this.Name = "UserGUI";
            this.Text = "Troll Kit";
            this.Load += new System.EventHandler(this.UserGUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox gameComboBox;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.CheckBox arcadeModeCheckBox;
        private System.Windows.Forms.CheckBox autostartCheckBox;
    }
}