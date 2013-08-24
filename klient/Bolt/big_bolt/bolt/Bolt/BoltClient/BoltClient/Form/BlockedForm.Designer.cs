namespace BoltClient
{
    partial class BlockedForm
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
            this.mainTextLabel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainTextLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTextLabel
            // 
            this.mainTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTextLabel.ErrorImage = null;
            this.mainTextLabel.Image = global::BoltClient.Properties.Resources.logo;
            this.mainTextLabel.ImageLocation = "";
            this.mainTextLabel.InitialImage = null;
            this.mainTextLabel.Location = new System.Drawing.Point(0, 0);
            this.mainTextLabel.Name = "mainTextLabel";
            this.mainTextLabel.Size = new System.Drawing.Size(900, 500);
            this.mainTextLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainTextLabel.TabIndex = 1;
            this.mainTextLabel.TabStop = false;
            // 
            // BlockedForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.mainTextLabel);
            this.Font = new System.Drawing.Font("Tahoma", 150F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockedForm";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientBolt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.mainTextLabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainTextLabel;



    }
}