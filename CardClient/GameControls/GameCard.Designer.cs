namespace CardClient.GameControls
{
    partial class GameCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PicCard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicCard)).BeginInit();
            this.SuspendLayout();
            // 
            // PicCard
            // 
            this.PicCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicCard.Image = global::CardClient.Properties.Resources.card_back;
            this.PicCard.Location = new System.Drawing.Point(0, 0);
            this.PicCard.Name = "PicCard";
            this.PicCard.Size = new System.Drawing.Size(389, 422);
            this.PicCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicCard.TabIndex = 0;
            this.PicCard.TabStop = false;
            // 
            // GameCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PicCard);
            this.Name = "GameCard";
            this.Size = new System.Drawing.Size(389, 422);
            ((System.ComponentModel.ISupportInitialize)(this.PicCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicCard;
    }
}
