﻿namespace SteamDlcShopping.Updater;

partial class FrmMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblProgress = new Label();
        SuspendLayout();
        // 
        // lblProgress
        // 
        lblProgress.Location = new Point(12, 9);
        lblProgress.Name = "lblProgress";
        lblProgress.Size = new Size(260, 93);
        lblProgress.TabIndex = 0;
        lblProgress.Text = "lblProgress";
        lblProgress.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // FrmMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 111);
        Controls.Add(lblProgress);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Auto Update";
        Shown += FrmMain_Shown;
        ResumeLayout(false);
    }

    #endregion

    private Label lblProgress;
}
