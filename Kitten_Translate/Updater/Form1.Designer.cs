namespace Updater;

partial class Form1
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
        ProgressBar = new ProgressBar();
        StatusLabel = new Label();
        OpenAfterUpdate = new CheckBox();
        SuspendLayout();
        // 
        // ProgressBar
        // 
        ProgressBar.Location = new Point(12, 61);
        ProgressBar.Name = "ProgressBar";
        ProgressBar.Size = new Size(385, 23);
        ProgressBar.TabIndex = 0;
        // 
        // StatusLabel
        // 
        StatusLabel.AutoSize = true;
        StatusLabel.Location = new Point(12, 43);
        StatusLabel.Name = "StatusLabel";
        StatusLabel.Size = new Size(49, 15);
        StatusLabel.TabIndex = 1;
        StatusLabel.Text = "Check...";
        StatusLabel.TextAlign = ContentAlignment.BottomCenter;
        StatusLabel.Click += StatusLabel_Click;
        // 
        // OpenAfterUpdate
        // 
        OpenAfterUpdate.AutoSize = true;
        OpenAfterUpdate.Location = new Point(12, 21);
        OpenAfterUpdate.Name = "OpenAfterUpdate";
        OpenAfterUpdate.Size = new Size(142, 19);
        OpenAfterUpdate.TabIndex = 2;
        OpenAfterUpdate.Text = "Open after the update";
        OpenAfterUpdate.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(409, 96);
        Controls.Add(OpenAfterUpdate);
        Controls.Add(StatusLabel);
        Controls.Add(ProgressBar);
        Name = "Form1";
        Text = "Updater";
        Shown += Form1_Shown;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ProgressBar ProgressBar;
    private Label StatusLabel;
    private CheckBox OpenAfterUpdate;
}
