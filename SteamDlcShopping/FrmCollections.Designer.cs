namespace SteamDlcShopping
{
    partial class FrmCollections
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
            this.lsvCollections = new System.Windows.Forms.ListView();
            this.colCollection = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lsvCollections
            // 
            this.lsvCollections.CheckBoxes = true;
            this.lsvCollections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCollection});
            this.lsvCollections.FullRowSelect = true;
            this.lsvCollections.GridLines = true;
            this.lsvCollections.Location = new System.Drawing.Point(0, 0);
            this.lsvCollections.MultiSelect = false;
            this.lsvCollections.Name = "lsvCollections";
            this.lsvCollections.Size = new System.Drawing.Size(221, 370);
            this.lsvCollections.TabIndex = 0;
            this.lsvCollections.UseCompatibleStateImageBehavior = false;
            this.lsvCollections.View = System.Windows.Forms.View.Details;
            // 
            // colCollection
            // 
            this.colCollection.Text = "Collection";
            this.colCollection.Width = 200;
            // 
            // FrmCollections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 370);
            this.Controls.Add(this.lsvCollections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCollections";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmCollections_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView lsvCollections;
        private ColumnHeader colCollection;
    }
}