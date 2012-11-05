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
            this.preferencesTree = new System.Windows.Forms.TreeView();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.preferencesTitle = new CodeVendor.Controls.Grouper();
            this.preferencesSelector = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.startingOptionsControl = new ToDo.StartingOptions();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flexiCommandsControl = new ToDo.FlexiCommandsControl();
            this.fontPage = new System.Windows.Forms.TabPage();
            this.fontColorSettingsControl = new ToDo.FontColorSettings();
            this.grouper1.SuspendLayout();
            this.preferencesTitle.SuspendLayout();
            this.preferencesSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fontPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // preferencesTree
            // 
            this.preferencesTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesTree.BackColor = System.Drawing.Color.Gainsboro;
            this.preferencesTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.preferencesTree.ForeColor = System.Drawing.Color.Black;
            this.preferencesTree.Location = new System.Drawing.Point(3, 20);
            this.preferencesTree.Name = "preferencesTree";
            this.preferencesTree.Scrollable = false;
            this.preferencesTree.Size = new System.Drawing.Size(99, 275);
            this.preferencesTree.TabIndex = 0;
            this.preferencesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.preferencesTree_AfterSelect);
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouper1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.preferencesTree);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(4, -6);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.Gray;
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(110, 315);
            this.grouper1.TabIndex = 5;
            // 
            // preferencesTitle
            // 
            this.preferencesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesTitle.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.preferencesTitle.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.preferencesTitle.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.preferencesTitle.BorderColor = System.Drawing.Color.Black;
            this.preferencesTitle.BorderThickness = 1F;
            this.preferencesTitle.Controls.Add(this.preferencesSelector);
            this.preferencesTitle.CustomGroupBoxColor = System.Drawing.Color.White;
            this.preferencesTitle.GroupImage = null;
            this.preferencesTitle.GroupTitle = "";
            this.preferencesTitle.Location = new System.Drawing.Point(123, -6);
            this.preferencesTitle.Name = "preferencesTitle";
            this.preferencesTitle.Padding = new System.Windows.Forms.Padding(20);
            this.preferencesTitle.PaintGroupBox = false;
            this.preferencesTitle.RoundCorners = 10;
            this.preferencesTitle.ShadowColor = System.Drawing.Color.Gray;
            this.preferencesTitle.ShadowControl = true;
            this.preferencesTitle.ShadowThickness = 3;
            this.preferencesTitle.Size = new System.Drawing.Size(419, 315);
            this.preferencesTitle.TabIndex = 6;
            // 
            // preferencesSelector
            // 
            this.preferencesSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesSelector.Controls.Add(this.tabPage1);
            this.preferencesSelector.Controls.Add(this.tabPage2);
            this.preferencesSelector.Controls.Add(this.fontPage);
            this.preferencesSelector.Location = new System.Drawing.Point(5, 14);
            this.preferencesSelector.Name = "preferencesSelector";
            this.preferencesSelector.SelectedIndex = 0;
            this.preferencesSelector.Size = new System.Drawing.Size(407, 295);
            this.preferencesSelector.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.startingOptionsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(399, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Starting Options";
            // 
            // startingOptionsControl
            // 
            this.startingOptionsControl.BackColor = System.Drawing.Color.Gainsboro;
            this.startingOptionsControl.Location = new System.Drawing.Point(6, 6);
            this.startingOptionsControl.Name = "startingOptionsControl";
            this.startingOptionsControl.Size = new System.Drawing.Size(273, 66);
            this.startingOptionsControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.flexiCommandsControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(399, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FlexiCommands";
            // 
            // flexiCommandsControl
            // 
            this.flexiCommandsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flexiCommandsControl.BackColor = System.Drawing.Color.Gainsboro;
            this.flexiCommandsControl.Location = new System.Drawing.Point(0, 0);
            this.flexiCommandsControl.Name = "flexiCommandsControl";
            this.flexiCommandsControl.Size = new System.Drawing.Size(396, 269);
            this.flexiCommandsControl.TabIndex = 0;
            // 
            // fontPage
            // 
            this.fontPage.BackColor = System.Drawing.Color.SteelBlue;
            this.fontPage.Controls.Add(this.fontColorSettingsControl);
            this.fontPage.Location = new System.Drawing.Point(4, 22);
            this.fontPage.Name = "fontPage";
            this.fontPage.Padding = new System.Windows.Forms.Padding(3);
            this.fontPage.Size = new System.Drawing.Size(399, 269);
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
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.preferencesTitle);
            this.Controls.Add(this.grouper1);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(545, 314);
            this.grouper1.ResumeLayout(false);
            this.preferencesTitle.ResumeLayout(false);
            this.preferencesSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.fontPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView preferencesTree;
        private CustomPanelControl preferencesSelector;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private StartingOptions startingOptionsControl;
        private FlexiCommandsControl flexiCommandsControl;
        private System.Windows.Forms.TabPage fontPage;
        private FontColorSettings fontColorSettingsControl;
        private CodeVendor.Controls.Grouper grouper1;
        private CodeVendor.Controls.Grouper preferencesTitle;
    }
}
