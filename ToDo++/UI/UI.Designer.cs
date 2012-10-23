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
            this.timeDateControl1 = new CustomControls.TimeDateControl();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textInput = new ToDo.InputBox();
            this.outputBox = new ToDo.OutputBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.customScrollbar = new CustomControls.CustomScrollbar();
            this.coverPanel = new System.Windows.Forms.Panel();
            this.shiftPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.coverPanel.SuspendLayout();
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
            this.loadButton.Location = new System.Drawing.Point(7, 5);
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
            this.preferencesButton.Location = new System.Drawing.Point(134, 5);
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(124, 37);
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
            this.helpButton.Location = new System.Drawing.Point(263, 5);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(122, 37);
            this.helpButton.TabIndex = 15;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Enabled = false;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::ToDo.Properties.Resources.CoverPhoto;
            this.pictureBox1.Location = new System.Drawing.Point(7, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(377, 29);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // timeDateControl1
            // 
            this.timeDateControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeDateControl1.BackColor = System.Drawing.Color.Aqua;
            this.timeDateControl1.Location = new System.Drawing.Point(252, 47);
            this.timeDateControl1.Name = "timeDateControl1";
            this.timeDateControl1.Size = new System.Drawing.Size(129, 29);
            this.timeDateControl1.TabIndex = 16;
            // 
            // customPanelControl
            // 
            this.customPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.BackColor = System.Drawing.Color.PaleTurquoise;
            this.textInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInput.Location = new System.Drawing.Point(3, 138);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(378, 19);
            this.textInput.TabIndex = 1;
            this.textInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            this.textInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textInput_PreviewKeyDown);
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.Color.PaleTurquoise;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.Location = new System.Drawing.Point(3, 0);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(378, 132);
            this.outputBox.TabIndex = 7;
            this.outputBox.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.pictureBox3);
            this.tabPage2.Controls.Add(this.customScrollbar);
            this.tabPage2.Controls.Add(this.coverPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(372, 168);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(355, -12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(3, 200);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Location = new System.Drawing.Point(371, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 145);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // customScrollbar
            // 
            this.customScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customScrollbar.ChannelColor = System.Drawing.Color.DodgerBlue;
            this.customScrollbar.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.DownArrowImage")));
            this.customScrollbar.LargeChange = 10;
            this.customScrollbar.Location = new System.Drawing.Point(357, -1);
            this.customScrollbar.Maximum = 100;
            this.customScrollbar.Minimum = 0;
            this.customScrollbar.MinimumSize = new System.Drawing.Size(15, 92);
            this.customScrollbar.Name = "customScrollbar";
            this.customScrollbar.Size = new System.Drawing.Size(15, 165);
            this.customScrollbar.SmallChange = 1;
            this.customScrollbar.TabIndex = 11;
            this.customScrollbar.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbBottomImage")));
            this.customScrollbar.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbBottomSpanImage")));
            this.customScrollbar.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbMiddleImage")));
            this.customScrollbar.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbTopImage")));
            this.customScrollbar.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbTopSpanImage")));
            this.customScrollbar.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.UpArrowImage")));
            this.customScrollbar.Value = 0;
            this.customScrollbar.Scroll += new System.EventHandler(this.customScrollbar_Scroll);
            // 
            // coverPanel
            // 
            this.coverPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.coverPanel.Controls.Add(this.shiftPanel);
            this.coverPanel.Location = new System.Drawing.Point(-9, -10);
            this.coverPanel.Name = "coverPanel";
            this.coverPanel.Size = new System.Drawing.Size(387, 177);
            this.coverPanel.TabIndex = 4;
            // 
            // shiftPanel
            // 
            this.shiftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftPanel.AutoScroll = true;
            this.shiftPanel.Location = new System.Drawing.Point(5, 3);
            this.shiftPanel.Name = "shiftPanel";
            this.shiftPanel.Size = new System.Drawing.Size(379, 168);
            this.shiftPanel.TabIndex = 3;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(393, 282);
            this.Controls.Add(this.timeDateControl1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.preferencesButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.customPanelControl);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(393, 282);
            this.Name = "UI";
            this.Text = "ToDo++";
            this.ResizeBegin += new System.EventHandler(this.UI_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.UI_ResizeEnd);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_MouseDown);
            this.Resize += new System.EventHandler(this.UI_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.coverPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button preferencesButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private InputBox textInput;
        private OutputBox outputBox;
        private CustomPanelControl customPanelControl;
        private CustomControls.CustomScrollbar customScrollbar;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel coverPanel;
        private System.Windows.Forms.Panel shiftPanel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private CustomControls.TimeDateControl timeDateControl1;
    }
}

