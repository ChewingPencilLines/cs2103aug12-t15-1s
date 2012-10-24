namespace ToDo
{
    partial class PreferencesPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NewOptions = new System.Windows.Forms.GroupBox();
            this.startingOptions = new ToDo.StartingOptions();
            this.flexiCommandsControl = new ToDo.FlexiCommandsControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.startingOptions);
            this.groupBox1.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox1.Location = new System.Drawing.Point(4, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 85);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.flexiCommandsControl);
            this.groupBox2.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox2.Location = new System.Drawing.Point(4, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 208);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FlexiCommands";
            // 
            // NewOptions
            // 
            this.NewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewOptions.ForeColor = System.Drawing.Color.Aqua;
            this.NewOptions.Location = new System.Drawing.Point(4, 299);
            this.NewOptions.Name = "NewOptions";
            this.NewOptions.Size = new System.Drawing.Size(365, 65);
            this.NewOptions.TabIndex = 6;
            this.NewOptions.TabStop = false;
            this.NewOptions.Text = "New Options";
            // 
            // startingOptions
            // 
            this.startingOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.startingOptions.Location = new System.Drawing.Point(6, 14);
            this.startingOptions.Name = "startingOptions";
            this.startingOptions.Size = new System.Drawing.Size(248, 66);
            this.startingOptions.TabIndex = 0;
            // 
            // flexiCommandsControl
            // 
            this.flexiCommandsControl.BackColor = System.Drawing.Color.Transparent;
            this.flexiCommandsControl.Location = new System.Drawing.Point(-9, 7);
            this.flexiCommandsControl.Name = "flexiCommandsControl";
            this.flexiCommandsControl.Size = new System.Drawing.Size(369, 200);
            this.flexiCommandsControl.TabIndex = 0;
            // 
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.NewOptions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(371, 373);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FlexiCommandsControl flexiCommandsControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox NewOptions;
        private StartingOptions startingOptions;
    }
}
