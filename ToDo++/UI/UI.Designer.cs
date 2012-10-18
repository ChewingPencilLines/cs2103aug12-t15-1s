namespace ToDo
{
    partial class UI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.notifyIcon_taskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.preferencesButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textInput = new System.Windows.Forms.TextBox();
            this.outputBox = new ToDo.OutputBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flexiCommandsControl1 = new ToDo.FlexiCommandsControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadOnStartup = new System.Windows.Forms.CheckBox();
            this.minimisedCheckbox = new System.Windows.Forms.CheckBox();
            this.dateTimeControl1 = new ToDo.DateTimeControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::ToDo.Properties.Resources.CoverPhoto;
            this.pictureBox1.Location = new System.Drawing.Point(8, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(377, 29);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.PowderBlue;
            this.loadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.loadButton.Location = new System.Drawing.Point(9, 4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(122, 37);
            this.loadButton.TabIndex = 13;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = false;
            // 
            // preferencesButton
            // 
            this.preferencesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesButton.BackColor = System.Drawing.Color.PowderBlue;
            this.preferencesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.preferencesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.preferencesButton.Location = new System.Drawing.Point(135, 4);
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(122, 37);
            this.preferencesButton.TabIndex = 14;
            this.preferencesButton.Text = "Preferences";
            this.preferencesButton.UseVisualStyleBackColor = false;
            this.preferencesButton.Click += new System.EventHandler(this.preferencesButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.BackColor = System.Drawing.Color.PowderBlue;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.helpButton.Location = new System.Drawing.Point(263, 4);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(122, 37);
            this.helpButton.TabIndex = 15;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            // 
            // customPanelControl
            // 
            this.customPanelControl.Controls.Add(this.tabPage1);
            this.customPanelControl.Controls.Add(this.tabPage2);
            this.customPanelControl.Location = new System.Drawing.Point(5, 84);
            this.customPanelControl.Name = "customPanelControl";
            this.customPanelControl.SelectedIndex = 0;
            this.customPanelControl.Size = new System.Drawing.Size(380, 194);
            this.customPanelControl.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage1.Controls.Add(this.textInput);
            this.tabPage1.Controls.Add(this.outputBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(372, 168);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // textInput
            // 
            this.textInput.BackColor = System.Drawing.Color.PaleTurquoise;
            this.textInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInput.Location = new System.Drawing.Point(3, 164);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(378, 19);
            this.textInput.TabIndex = 0;
            this.textInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.PaleTurquoise;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.Location = new System.Drawing.Point(3, 0);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(378, 158);
            this.outputBox.TabIndex = 7;
            this.outputBox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(372, 168);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flexiCommandsControl1);
            this.groupBox2.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox2.Location = new System.Drawing.Point(6, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 119);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FlexiCommands";
            // 
            // flexiCommandsControl1
            // 
            this.flexiCommandsControl1.BackColor = System.Drawing.Color.Transparent;
            this.flexiCommandsControl1.Location = new System.Drawing.Point(-9, 11);
            this.flexiCommandsControl1.Name = "flexiCommandsControl1";
            this.flexiCommandsControl1.Size = new System.Drawing.Size(369, 111);
            this.flexiCommandsControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loadOnStartup);
            this.groupBox1.Controls.Add(this.minimisedCheckbox);
            this.groupBox1.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 61);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Options";
            // 
            // loadOnStartup
            // 
            this.loadOnStartup.AutoSize = true;
            this.loadOnStartup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadOnStartup.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.loadOnStartup.Location = new System.Drawing.Point(8, 37);
            this.loadOnStartup.Name = "loadOnStartup";
            this.loadOnStartup.Size = new System.Drawing.Size(101, 17);
            this.loadOnStartup.TabIndex = 1;
            this.loadOnStartup.Text = "Load On Startup";
            this.loadOnStartup.UseVisualStyleBackColor = true;
            // 
            // minimisedCheckbox
            // 
            this.minimisedCheckbox.AutoSize = true;
            this.minimisedCheckbox.BackColor = System.Drawing.Color.SteelBlue;
            this.minimisedCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimisedCheckbox.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.minimisedCheckbox.Location = new System.Drawing.Point(8, 17);
            this.minimisedCheckbox.Name = "minimisedCheckbox";
            this.minimisedCheckbox.Size = new System.Drawing.Size(94, 17);
            this.minimisedCheckbox.TabIndex = 0;
            this.minimisedCheckbox.Text = "Start Minimized";
            this.minimisedCheckbox.UseVisualStyleBackColor = false;
            // 
            // dateTimeControl1
            // 
            this.dateTimeControl1.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeControl1.Location = new System.Drawing.Point(229, 42);
            this.dateTimeControl1.Name = "dateTimeControl1";
            this.dateTimeControl1.Size = new System.Drawing.Size(150, 36);
            this.dateTimeControl1.TabIndex = 10;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(391, 282);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.preferencesButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.customPanelControl);
            this.Controls.Add(this.dateTimeControl1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI";
            this.Text = "ToDo++";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textInput;
        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private OutputBox outputBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DateTimeControl dateTimeControl1;
        private CustomPanelControl customPanelControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox minimisedCheckbox;
        private System.Windows.Forms.CheckBox loadOnStartup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FlexiCommandsControl flexiCommandsControl1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button preferencesButton;
        private System.Windows.Forms.Button helpButton;
    }
}

