namespace CardClient
{
    partial class MainMenu
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lobbiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLobbyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrServerTick = new System.Windows.Forms.Timer(this.components);
            this.ListLobbies = new System.Windows.Forms.ListView();
            this.ListGames = new System.Windows.Forms.ListView();
            this.tmrLobbyCheck = new System.Windows.Forms.Timer(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.lobbiesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lobbiesToolStripMenuItem
            // 
            this.lobbiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLobbyToolStripMenuItem});
            this.lobbiesToolStripMenuItem.Name = "lobbiesToolStripMenuItem";
            this.lobbiesToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.lobbiesToolStripMenuItem.Text = "Lobbies";
            // 
            // newLobbyToolStripMenuItem
            // 
            this.newLobbyToolStripMenuItem.Name = "newLobbyToolStripMenuItem";
            this.newLobbyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.newLobbyToolStripMenuItem.Text = "New Hearts Lobby";
            this.newLobbyToolStripMenuItem.Click += new System.EventHandler(this.newLobbyToolStripMenuItem_Click);
            // 
            // tmrServerTick
            // 
            this.tmrServerTick.Tick += new System.EventHandler(this.tmrServerTick_Tick);
            // 
            // ListLobbies
            // 
            this.ListLobbies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListLobbies.HideSelection = false;
            this.ListLobbies.Location = new System.Drawing.Point(13, 28);
            this.ListLobbies.MultiSelect = false;
            this.ListLobbies.Name = "ListLobbies";
            this.ListLobbies.Size = new System.Drawing.Size(311, 410);
            this.ListLobbies.TabIndex = 1;
            this.ListLobbies.UseCompatibleStateImageBehavior = false;
            this.ListLobbies.DoubleClick += new System.EventHandler(this.ListLobbies_DoubleClick);
            // 
            // ListGames
            // 
            this.ListGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListGames.HideSelection = false;
            this.ListGames.Location = new System.Drawing.Point(330, 27);
            this.ListGames.MultiSelect = false;
            this.ListGames.Name = "ListGames";
            this.ListGames.Size = new System.Drawing.Size(458, 410);
            this.ListGames.TabIndex = 2;
            this.ListGames.UseCompatibleStateImageBehavior = false;
            this.ListGames.DoubleClick += new System.EventHandler(this.ListGames_DoubleClick);
            // 
            // tmrLobbyCheck
            // 
            this.tmrLobbyCheck.Enabled = true;
            this.tmrLobbyCheck.Interval = 10000;
            this.tmrLobbyCheck.Tick += new System.EventHandler(this.tmrLobbyCheck_Tick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ListGames);
            this.Controls.Add(this.ListLobbies);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer tmrServerTick;
        private System.Windows.Forms.ToolStripMenuItem lobbiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLobbyToolStripMenuItem;
        private System.Windows.Forms.ListView ListLobbies;
        private System.Windows.Forms.ListView ListGames;
        private System.Windows.Forms.Timer tmrLobbyCheck;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

