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
            this.label2 = new System.Windows.Forms.Label();
            this.joyToKeyComboBox = new System.Windows.Forms.ComboBox();
            this.hideMouseCheckBox = new System.Windows.Forms.CheckBox();
            this.fullScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.browseGameButton = new System.Windows.Forms.Button();
            this.runJoyToKeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game";
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
            this.playButton.Location = new System.Drawing.Point(116, 161);
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
            this.arcadeModeCheckBox.Location = new System.Drawing.Point(72, 69);
            this.arcadeModeCheckBox.Name = "arcadeModeCheckBox";
            this.arcadeModeCheckBox.Size = new System.Drawing.Size(207, 17);
            this.arcadeModeCheckBox.TabIndex = 3;
            this.arcadeModeCheckBox.Text = "Arcade mode (F2 to restart, F4 to stop)";
            this.arcadeModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // autostartCheckBox
            // 
            this.autostartCheckBox.AutoSize = true;
            this.autostartCheckBox.Location = new System.Drawing.Point(72, 138);
            this.autostartCheckBox.Name = "autostartCheckBox";
            this.autostartCheckBox.Size = new System.Drawing.Size(154, 17);
            this.autostartCheckBox.TabIndex = 4;
            this.autostartCheckBox.Text = "Autostart using last settings";
            this.autostartCheckBox.UseVisualStyleBackColor = true;
            this.autostartCheckBox.CheckedChanged += new System.EventHandler(this.autostartCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "JoyToKey";
            // 
            // joyToKeyComboBox
            // 
            this.joyToKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.joyToKeyComboBox.Location = new System.Drawing.Point(95, 42);
            this.joyToKeyComboBox.Name = "joyToKeyComboBox";
            this.joyToKeyComboBox.Size = new System.Drawing.Size(121, 21);
            this.joyToKeyComboBox.TabIndex = 0;
            // 
            // hideMouseCheckBox
            // 
            this.hideMouseCheckBox.AutoSize = true;
            this.hideMouseCheckBox.Location = new System.Drawing.Point(72, 115);
            this.hideMouseCheckBox.Name = "hideMouseCheckBox";
            this.hideMouseCheckBox.Size = new System.Drawing.Size(82, 17);
            this.hideMouseCheckBox.TabIndex = 6;
            this.hideMouseCheckBox.Text = "Hide mouse";
            this.hideMouseCheckBox.UseVisualStyleBackColor = true;
            // 
            // fullScreenCheckBox
            // 
            this.fullScreenCheckBox.AutoSize = true;
            this.fullScreenCheckBox.Location = new System.Drawing.Point(72, 92);
            this.fullScreenCheckBox.Name = "fullScreenCheckBox";
            this.fullScreenCheckBox.Size = new System.Drawing.Size(77, 17);
            this.fullScreenCheckBox.TabIndex = 7;
            this.fullScreenCheckBox.Text = "Full screen";
            this.fullScreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // browseGameButton
            // 
            this.browseGameButton.Location = new System.Drawing.Point(222, 10);
            this.browseGameButton.Name = "browseGameButton";
            this.browseGameButton.Size = new System.Drawing.Size(75, 23);
            this.browseGameButton.TabIndex = 8;
            this.browseGameButton.Text = "Browse...";
            this.browseGameButton.UseVisualStyleBackColor = true;
            this.browseGameButton.Click += new System.EventHandler(this.browseGameButton_Click);
            // 
            // runJoyToKeyButton
            // 
            this.runJoyToKeyButton.Location = new System.Drawing.Point(222, 40);
            this.runJoyToKeyButton.Name = "runJoyToKeyButton";
            this.runJoyToKeyButton.Size = new System.Drawing.Size(75, 23);
            this.runJoyToKeyButton.TabIndex = 9;
            this.runJoyToKeyButton.Text = "Run";
            this.runJoyToKeyButton.UseVisualStyleBackColor = true;
            this.runJoyToKeyButton.Click += new System.EventHandler(this.runJoyToKeyButton_Click);
            // 
            // UserGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 194);
            this.Controls.Add(this.runJoyToKeyButton);
            this.Controls.Add(this.browseGameButton);
            this.Controls.Add(this.fullScreenCheckBox);
            this.Controls.Add(this.hideMouseCheckBox);
            this.Controls.Add(this.joyToKeyComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.autostartCheckBox);
            this.Controls.Add(this.arcadeModeCheckBox);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.gameComboBox);
            this.Controls.Add(this.label1);
            this.Name = "UserGUI";
            this.Text = "Trollkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox joyToKeyComboBox;
        private System.Windows.Forms.CheckBox hideMouseCheckBox;
        private System.Windows.Forms.CheckBox fullScreenCheckBox;
        private System.Windows.Forms.Button browseGameButton;
        private System.Windows.Forms.Button runJoyToKeyButton;
    }
}