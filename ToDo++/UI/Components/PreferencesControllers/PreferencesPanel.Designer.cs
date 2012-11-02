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
            this.preferencesTitle = new ToDo.GroupBoxMOD();
            this.preferencesSelector = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.startingOptionsControl = new ToDo.StartingOptions();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flexiCommandsControl = new ToDo.FlexiCommandsControl();
            this.fontPage = new System.Windows.Forms.TabPage();
            this.fontColorSettingsControl = new ToDo.FontColorSettings();
            this.groupBoxMOD1 = new ToDo.GroupBoxMOD();
            this.preferencesTree = new System.Windows.Forms.TreeView();
            this.preferencesTitle.SuspendLayout();
            this.preferencesSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fontPage.SuspendLayout();
            this.groupBoxMOD1.SuspendLayout();
            this.SuspendLayout();
            // 
            // preferencesTitle
            // 
            this.preferencesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesTitle.BorderColor = System.Drawing.Color.Black;
            this.preferencesTitle.BorderColorLight = System.Drawing.Color.Transparent;
            this.preferencesTitle.BorderRadius = 9;
            this.preferencesTitle.Controls.Add(this.preferencesSelector);
            this.preferencesTitle.Location = new System.Drawing.Point(123, 0);
            this.preferencesTitle.Name = "preferencesTitle";
            this.preferencesTitle.Size = new System.Drawing.Size(352, 308);
            this.preferencesTitle.TabIndex = 1;
            this.preferencesTitle.TabStop = false;
            this.preferencesTitle.Text = "Preferences";
            // 
            // preferencesSelector
            // 
            this.preferencesSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesSelector.Controls.Add(this.tabPage1);
            this.preferencesSelector.Controls.Add(this.tabPage2);
            this.preferencesSelector.Controls.Add(this.fontPage);
            this.preferencesSelector.Location = new System.Drawing.Point(6, 12);
            this.preferencesSelector.Name = "preferencesSelector";
            this.preferencesSelector.SelectedIndex = 0;
            this.preferencesSelector.Size = new System.Drawing.Size(340, 290);
            this.preferencesSelector.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.startingOptionsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(332, 264);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Starting Options";
            // 
            // startingOptionsControl
            // 
            this.startingOptionsControl.BackColor = System.Drawing.Color.White;
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
            this.tabPage2.Size = new System.Drawing.Size(332, 261);
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
            this.flexiCommandsControl.Size = new System.Drawing.Size(329, 265);
            this.flexiCommandsControl.TabIndex = 0;
            // 
            // fontPage
            // 
            this.fontPage.BackColor = System.Drawing.Color.SteelBlue;
            this.fontPage.Controls.Add(this.fontColorSettingsControl);
            this.fontPage.Location = new System.Drawing.Point(4, 22);
            this.fontPage.Name = "fontPage";
            this.fontPage.Padding = new System.Windows.Forms.Padding(3);
            this.fontPage.Size = new System.Drawing.Size(332, 261);
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
            this.fontColorSettingsControl.Size = new System.Drawing.Size(326, 255);
            this.fontColorSettingsControl.TabIndex = 0;
            // 
            // groupBoxMOD1
            // 
            this.groupBoxMOD1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxMOD1.BorderColor = System.Drawing.Color.Black;
            this.groupBoxMOD1.BorderColorLight = System.Drawing.Color.Transparent;
            this.groupBoxMOD1.BorderRadius = 9;
            this.groupBoxMOD1.Controls.Add(this.preferencesTree);
            this.groupBoxMOD1.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMOD1.Name = "groupBoxMOD1";
            this.groupBoxMOD1.Size = new System.Drawing.Size(114, 305);
            this.groupBoxMOD1.TabIndex = 5;
            this.groupBoxMOD1.TabStop = false;
            // 
            // preferencesTree
            // 
            this.preferencesTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.preferencesTree.BackColor = System.Drawing.Color.White;
            this.preferencesTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.preferencesTree.ForeColor = System.Drawing.Color.Black;
            this.preferencesTree.Location = new System.Drawing.Point(2, 12);
            this.preferencesTree.Name = "preferencesTree";
            this.preferencesTree.Size = new System.Drawing.Size(102, 287);
            this.preferencesTree.TabIndex = 0;
            this.preferencesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.preferencesTree_AfterSelect);
            // 
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.preferencesTitle);
            this.Controls.Add(this.groupBoxMOD1);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(478, 314);
            this.preferencesTitle.ResumeLayout(false);
            this.preferencesSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.fontPage.ResumeLayout(false);
            this.groupBoxMOD1.ResumeLayout(false);
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
        private GroupBoxMOD groupBoxMOD1;
        private GroupBoxMOD preferencesTitle;
    }
}
