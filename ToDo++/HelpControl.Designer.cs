namespace ToDo
{
    partial class HelpControl
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
            this.components = new System.ComponentModel.Container();
            this.fadeInTimer = new System.Windows.Forms.Timer(this.components);
            this.fadeOutTimer = new System.Windows.Forms.Timer(this.components);
            this.customPanelControl1 = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.transpControl = new TranspControl.TranspControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.customPanelControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fadeInTimer
            // 
            this.fadeInTimer.Interval = 1;
            this.fadeInTimer.Tick += new System.EventHandler(this.fadeInTimer_Tick);
            // 
            // fadeOutTimer
            // 
            this.fadeOutTimer.Interval = 1;
            this.fadeOutTimer.Tick += new System.EventHandler(this.fadeOutTimer_Tick);
            // 
            // customPanelControl1
            // 
            this.customPanelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanelControl1.Controls.Add(this.tabPage1);
            this.customPanelControl1.Controls.Add(this.tabPage2);
            this.customPanelControl1.Location = new System.Drawing.Point(3, 3);
            this.customPanelControl1.Name = "customPanelControl1";
            this.customPanelControl1.SelectedIndex = 0;
            this.customPanelControl1.Size = new System.Drawing.Size(489, 309);
            this.customPanelControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.pictureBox);
            this.tabPage1.Controls.Add(this.transpControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(481, 283);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox.Image = global::ToDo.Properties.Resources.helpFrame1;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(475, 274);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // transpControl
            // 
            this.transpControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transpControl.BackColor = System.Drawing.Color.Transparent;
            this.transpControl.BackImage = null;
            this.transpControl.FillColor = System.Drawing.Color.Gainsboro;
            this.transpControl.ForeColor = System.Drawing.Color.Gainsboro;
            this.transpControl.GlassColor = System.Drawing.Color.Transparent;
            this.transpControl.GlassMode = true;
            this.transpControl.LineWidth = 2;
            this.transpControl.Location = new System.Drawing.Point(3, 3);
            this.transpControl.Name = "transpControl";
            this.transpControl.Opacity = 100;
            this.transpControl.Size = new System.Drawing.Size(475, 274);
            this.transpControl.TabIndex = 1;
            this.transpControl.TranspKey = System.Drawing.Color.White;
            this.transpControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transpControl_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(481, 283);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // HelpControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.customPanelControl1);
            this.Name = "HelpControl";
            this.Size = new System.Drawing.Size(495, 315);
            this.customPanelControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanelControl customPanelControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private TranspControl.TranspControl transpControl;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer fadeInTimer;
        private System.Windows.Forms.Timer fadeOutTimer;
    }
}
