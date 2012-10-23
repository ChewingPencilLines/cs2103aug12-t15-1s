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
            this.startingOptions1 = new ToDo.StartingOptions();
            this.flexiCommandsControl1 = new ToDo.FlexiCommandsControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.startingOptions1);
            this.groupBox1.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox1.Location = new System.Drawing.Point(4, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 61);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.flexiCommandsControl1);
            this.groupBox2.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox2.Location = new System.Drawing.Point(4, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 109);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FlexiCommands";
            // 
            // NewOptions
            // 
            this.NewOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewOptions.ForeColor = System.Drawing.Color.Aqua;
            this.NewOptions.Location = new System.Drawing.Point(3, 181);
            this.NewOptions.Name = "NewOptions";
            this.NewOptions.Size = new System.Drawing.Size(338, 78);
            this.NewOptions.TabIndex = 6;
            this.NewOptions.TabStop = false;
            this.NewOptions.Text = "New Options";
            // 
            // startingOptions1
            // 
            this.startingOptions1.BackColor = System.Drawing.Color.Transparent;
            this.startingOptions1.Location = new System.Drawing.Point(6, 7);
            this.startingOptions1.Name = "startingOptions1";
            this.startingOptions1.Size = new System.Drawing.Size(248, 49);
            this.startingOptions1.TabIndex = 0;
            // 
            // flexiCommandsControl1
            // 
            this.flexiCommandsControl1.BackColor = System.Drawing.Color.Transparent;
            this.flexiCommandsControl1.Location = new System.Drawing.Point(-9, 11);
            this.flexiCommandsControl1.Name = "flexiCommandsControl1";
            this.flexiCommandsControl1.Size = new System.Drawing.Size(369, 92);
            this.flexiCommandsControl1.TabIndex = 0;
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
            this.Size = new System.Drawing.Size(361, 265);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private StartingOptions startingOptions1;
        private System.Windows.Forms.GroupBox groupBox1;
        private FlexiCommandsControl flexiCommandsControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox NewOptions;
    }
}
