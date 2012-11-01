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
            this.preferencesTitle = new System.Windows.Forms.GroupBox();
            this.preferencesSelector = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.startingOptionsControl = new ToDo.StartingOptions();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flexiCommandsControl = new ToDo.FlexiCommandsControl();
            this.fontPage = new System.Windows.Forms.TabPage();
            this.fontColorSettingsControl = new ToDo.FontColorSettings();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.preferencesTree = new System.Windows.Forms.TreeView();
            this.preferencesTitle.SuspendLayout();
            this.preferencesSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fontPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // preferencesTitle
            // 
            this.preferencesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesTitle.Controls.Add(this.preferencesSelector);
            this.preferencesTitle.ForeColor = System.Drawing.Color.Aqua;
            this.preferencesTitle.Location = new System.Drawing.Point(123, 1);
            this.preferencesTitle.Name = "preferencesTitle";
            this.preferencesTitle.Size = new System.Drawing.Size(352, 307);
            this.preferencesTitle.TabIndex = 4;
            this.preferencesTitle.TabStop = false;
            this.preferencesTitle.Text = "Starting Options";
            // 
            // preferencesSelector
            // 
            this.preferencesSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesSelector.Controls.Add(this.tabPage1);
            this.preferencesSelector.Controls.Add(this.tabPage2);
            this.preferencesSelector.Controls.Add(this.fontPage);
            this.preferencesSelector.Location = new System.Drawing.Point(6, 17);
            this.preferencesSelector.Name = "preferencesSelector";
            this.preferencesSelector.SelectedIndex = 0;
            this.preferencesSelector.Size = new System.Drawing.Size(340, 280);
            this.preferencesSelector.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage1.Controls.Add(this.startingOptionsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(332, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Starting Options";
            // 
            // startingOptionsControl
            // 
            this.startingOptionsControl.BackColor = System.Drawing.Color.SteelBlue;
            this.startingOptionsControl.Location = new System.Drawing.Point(6, 6);
            this.startingOptionsControl.Name = "startingOptionsControl";
            this.startingOptionsControl.Size = new System.Drawing.Size(273, 66);
            this.startingOptionsControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage2.Controls.Add(this.flexiCommandsControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FlexiCommands";
            // 
            // flexiCommandsControl
            // 
            this.flexiCommandsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flexiCommandsControl.BackColor = System.Drawing.Color.SteelBlue;
            this.flexiCommandsControl.Location = new System.Drawing.Point(0, 0);
            this.flexiCommandsControl.Name = "flexiCommandsControl";
            this.flexiCommandsControl.Size = new System.Drawing.Size(329, 258);
            this.flexiCommandsControl.TabIndex = 0;
            // 
            // fontPage
            // 
            this.fontPage.BackColor = System.Drawing.Color.SteelBlue;
            this.fontPage.Controls.Add(this.fontColorSettingsControl);
            this.fontPage.Location = new System.Drawing.Point(4, 22);
            this.fontPage.Name = "fontPage";
            this.fontPage.Padding = new System.Windows.Forms.Padding(3);
            this.fontPage.Size = new System.Drawing.Size(332, 254);
            this.fontPage.TabIndex = 2;
            this.fontPage.Text = "Font";
            // 
            // fontColorSettingsControl
            // 
            this.fontColorSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fontColorSettingsControl.BackColor = System.Drawing.Color.SteelBlue;
            this.fontColorSettingsControl.Location = new System.Drawing.Point(3, 3);
            this.fontColorSettingsControl.Name = "fontColorSettingsControl";
            this.fontColorSettingsControl.Size = new System.Drawing.Size(326, 248);
            this.fontColorSettingsControl.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.preferencesTree);
            this.groupBox2.ForeColor = System.Drawing.Color.Aqua;
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 308);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // preferencesTree
            // 
            this.preferencesTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesTree.BackColor = System.Drawing.Color.SteelBlue;
            this.preferencesTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.preferencesTree.ForeColor = System.Drawing.SystemColors.Info;
            this.preferencesTree.LineColor = System.Drawing.Color.White;
            this.preferencesTree.Location = new System.Drawing.Point(3, 15);
            this.preferencesTree.Name = "preferencesTree";
            this.preferencesTree.Size = new System.Drawing.Size(102, 283);
            this.preferencesTree.TabIndex = 0;
            this.preferencesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.preferencesTree_AfterSelect);
            // 
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.preferencesTitle);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(478, 314);
            this.preferencesTitle.ResumeLayout(false);
            this.preferencesSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.fontPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox preferencesTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView preferencesTree;
        private CustomPanelControl preferencesSelector;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private StartingOptions startingOptionsControl;
        private FlexiCommandsControl flexiCommandsControl;
        private System.Windows.Forms.TabPage fontPage;
        private FontColorSettings fontColorSettingsControl;
    }
}
