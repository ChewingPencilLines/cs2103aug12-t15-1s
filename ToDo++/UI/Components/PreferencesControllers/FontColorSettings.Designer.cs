namespace ToDo
{
    partial class FontColorSettings
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
            this.previewBox = new ToDo.OutputBox();
            this.textSizeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBox.BackColor = System.Drawing.Color.LightSkyBlue;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewBox.Location = new System.Drawing.Point(3, 163);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(320, 82);
            this.previewBox.TabIndex = 0;
            this.previewBox.Text = "";
            // 
            // textSizeButton
            // 
            this.textSizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSizeButton.BackColor = System.Drawing.Color.PaleTurquoise;
            this.textSizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textSizeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.textSizeButton.Location = new System.Drawing.Point(3, 3);
            this.textSizeButton.Name = "textSizeButton";
            this.textSizeButton.Size = new System.Drawing.Size(320, 23);
            this.textSizeButton.TabIndex = 1;
            this.textSizeButton.Text = "Text Size";
            this.textSizeButton.UseVisualStyleBackColor = false;
            this.textSizeButton.Click += new System.EventHandler(this.textSizeButton_Click);
            // 
            // FontColorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.textSizeButton);
            this.Controls.Add(this.previewBox);
            this.Name = "FontColorSettings";
            this.Size = new System.Drawing.Size(326, 248);
            this.ResumeLayout(false);

        }

        #endregion

        private OutputBox previewBox;
        private System.Windows.Forms.Button textSizeButton;

    }
}
