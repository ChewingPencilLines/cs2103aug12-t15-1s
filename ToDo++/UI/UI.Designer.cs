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
            this.loadButton = new System.Windows.Forms.Button();
            this.preferencesButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textInput = new System.Windows.Forms.TextBox();
            this.outputBox = new ToDo.OutputBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.coverPanel = new System.Windows.Forms.Panel();
            this.shiftPanel = new System.Windows.Forms.Panel();
            this.preferencesPanel1 = new ToDo.PreferencesPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.customScrollbar1 = new CustomControls.CustomScrollbar();
            this.dateTimeControl1 = new ToDo.DateTimeControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.coverPanel.SuspendLayout();
            this.shiftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
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
            this.tabPage1.Size = new System.Drawing.Size(440, 221);
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
            this.tabPage2.Controls.Add(this.coverPanel);
            this.tabPage2.Controls.Add(this.pictureBox3);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.customScrollbar1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(372, 168);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // coverPanel
            // 
            this.coverPanel.Controls.Add(this.shiftPanel);
            this.coverPanel.Location = new System.Drawing.Point(-9, -10);
            this.coverPanel.Name = "coverPanel";
            this.coverPanel.Size = new System.Drawing.Size(370, 194);
            this.coverPanel.TabIndex = 4;
            // 
            // shiftPanel
            // 
            this.shiftPanel.AutoScroll = true;
            this.shiftPanel.Controls.Add(this.preferencesPanel1);
            this.shiftPanel.Location = new System.Drawing.Point(5, 3);
            this.shiftPanel.Name = "shiftPanel";
            this.shiftPanel.Size = new System.Drawing.Size(383, 183);
            this.shiftPanel.TabIndex = 3;
            // 
            // preferencesPanel1
            // 
            this.preferencesPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.preferencesPanel1.Location = new System.Drawing.Point(5, 6);
            this.preferencesPanel1.Name = "preferencesPanel1";
            this.preferencesPanel1.Size = new System.Drawing.Size(360, 240);
            this.preferencesPanel1.TabIndex = 0;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(379, -1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 171);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(356, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 171);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // customScrollbar1
            // 
            this.customScrollbar1.ChannelColor = System.Drawing.Color.DodgerBlue;
            this.customScrollbar1.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.DownArrowImage")));
            this.customScrollbar1.LargeChange = 10;
            this.customScrollbar1.Location = new System.Drawing.Point(365, -1);
            this.customScrollbar1.Maximum = 100;
            this.customScrollbar1.Minimum = 0;
            this.customScrollbar1.MinimumSize = new System.Drawing.Size(15, 92);
            this.customScrollbar1.Name = "customScrollbar1";
            this.customScrollbar1.Size = new System.Drawing.Size(15, 180);
            this.customScrollbar1.SmallChange = 1;
            this.customScrollbar1.TabIndex = 0;
            this.customScrollbar1.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbBottomImage")));
            this.customScrollbar1.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbBottomSpanImage")));
            this.customScrollbar1.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbMiddleImage")));
            this.customScrollbar1.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbTopImage")));
            this.customScrollbar1.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbTopSpanImage")));
            this.customScrollbar1.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.UpArrowImage")));
            this.customScrollbar1.Value = 0;
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
            this.coverPanel.ResumeLayout(false);
            this.shiftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button preferencesButton;
        private System.Windows.Forms.Button helpButton;
        private CustomControls.CustomScrollbar customScrollbar1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel shiftPanel;
        private PreferencesPanel preferencesPanel1;
        private System.Windows.Forms.Panel coverPanel;
    }
}

