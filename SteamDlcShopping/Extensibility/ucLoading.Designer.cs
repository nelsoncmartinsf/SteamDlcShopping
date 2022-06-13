namespace SteamDlcShopping.Extensibility
{
    partial class ucLoading
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
            this.pgbLoading = new System.Windows.Forms.ProgressBar();
            this.ptbLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pgbLoading
            // 
            this.pgbLoading.Location = new System.Drawing.Point(83, 400);
            this.pgbLoading.Name = "pgbLoading";
            this.pgbLoading.Size = new System.Drawing.Size(600, 30);
            this.pgbLoading.TabIndex = 0;
            // 
            // ptbLoading
            // 
            this.ptbLoading.Location = new System.Drawing.Point(258, 212);
            this.ptbLoading.Name = "ptbLoading";
            this.ptbLoading.Size = new System.Drawing.Size(250, 182);
            this.ptbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbLoading.TabIndex = 0;
            this.ptbLoading.TabStop = false;
            // 
            // ucLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgbLoading);
            this.Controls.Add(this.ptbLoading);
            this.Name = "ucLoading";
            this.Size = new System.Drawing.Size(767, 642);
            this.Load += new System.EventHandler(this.ucLoading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ProgressBar pgbLoading;
        private PictureBox ptbLoading;
    }
}
