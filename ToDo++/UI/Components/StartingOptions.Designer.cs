namespace ToDo
{
    partial class StartingOptions
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
            this.minimisedCheckbox = new System.Windows.Forms.CheckBox();
            this.loadOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // minimisedCheckbox
            // 
            this.minimisedCheckbox.AutoSize = true;
            this.minimisedCheckbox.BackColor = System.Drawing.Color.SteelBlue;
            this.minimisedCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimisedCheckbox.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.minimisedCheckbox.Location = new System.Drawing.Point(8, 9);
            this.minimisedCheckbox.Name = "minimisedCheckbox";
            this.minimisedCheckbox.Size = new System.Drawing.Size(94, 17);
            this.minimisedCheckbox.TabIndex = 2;
            this.minimisedCheckbox.Text = "Start Minimized";
            this.minimisedCheckbox.UseVisualStyleBackColor = false;
            this.minimisedCheckbox.CheckedChanged += new System.EventHandler(this.minimisedCheckbox_CheckedChanged);
            // 
            // loadOnStartupCheckbox
            // 
            this.loadOnStartupCheckbox.AutoSize = true;
            this.loadOnStartupCheckbox.BackColor = System.Drawing.Color.SteelBlue;
            this.loadOnStartupCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadOnStartupCheckbox.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.loadOnStartupCheckbox.Location = new System.Drawing.Point(8, 29);
            this.loadOnStartupCheckbox.Name = "loadOnStartupCheckbox";
            this.loadOnStartupCheckbox.Size = new System.Drawing.Size(101, 17);
            this.loadOnStartupCheckbox.TabIndex = 3;
            this.loadOnStartupCheckbox.Text = "Load On Startup";
            this.loadOnStartupCheckbox.UseVisualStyleBackColor = false;
            this.loadOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckedChanged);
            // 
            // StartingOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.loadOnStartupCheckbox);
            this.Controls.Add(this.minimisedCheckbox);
            this.Name = "StartingOptions";
            this.Size = new System.Drawing.Size(248, 54);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox minimisedCheckbox;
        private System.Windows.Forms.CheckBox loadOnStartupCheckbox;
    }
}
