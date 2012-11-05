namespace ToDo
{
    partial class FlexiCommandsControl
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
            this.commandTree = new System.Windows.Forms.TreeView();
            this.contextTree = new System.Windows.Forms.TreeView();
            this.descriptionLabel = new System.Windows.Forms.RichTextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.listedFlexiCommands = new System.Windows.Forms.ListBox();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.timeRangeKeywordTree = new System.Windows.Forms.TreeView();
            this.flatTabControl1 = new FlatTabControl.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rangeController = new Zzzz.ZzzzRangeBar();
            this.timeRangeTree = new System.Windows.Forms.TreeView();
            this.removeButton = new ToDo.RoundButton();
            this.addButton = new ToDo.RoundButton();
            this.grouper1.SuspendLayout();
            this.flatTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandTree
            // 
            this.commandTree.BackColor = System.Drawing.Color.Gainsboro;
            this.commandTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTree.ForeColor = System.Drawing.Color.Black;
            this.commandTree.LineColor = System.Drawing.Color.Gainsboro;
            this.commandTree.Location = new System.Drawing.Point(-14, 3);
            this.commandTree.Name = "commandTree";
            this.commandTree.Size = new System.Drawing.Size(111, 188);
            this.commandTree.TabIndex = 1;
            this.commandTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.commandTree_AfterSelect);
            this.commandTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.commandTree_NodeMouseDoubleClick);
            // 
            // contextTree
            // 
            this.contextTree.BackColor = System.Drawing.Color.Gainsboro;
            this.contextTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contextTree.ForeColor = System.Drawing.Color.Black;
            this.contextTree.LineColor = System.Drawing.Color.Gainsboro;
            this.contextTree.Location = new System.Drawing.Point(-14, 3);
            this.contextTree.Name = "contextTree";
            this.contextTree.Size = new System.Drawing.Size(111, 188);
            this.contextTree.TabIndex = 10;
            this.contextTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contextTree_AfterSelect);
            this.contextTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contextTree_NodeMouseDoubleClick);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionLabel.ForeColor = System.Drawing.Color.Black;
            this.descriptionLabel.Location = new System.Drawing.Point(159, 58);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(243, 71);
            this.descriptionLabel.TabIndex = 13;
            this.descriptionLabel.Text = "Please go ahead and select a command to see it\'s description";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(154, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(176, 25);
            this.titleLabel.TabIndex = 12;
            this.titleLabel.Text = "Nothing Selected";
            // 
            // listedFlexiCommands
            // 
            this.listedFlexiCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listedFlexiCommands.BackColor = System.Drawing.Color.Gainsboro;
            this.listedFlexiCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listedFlexiCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listedFlexiCommands.ForeColor = System.Drawing.Color.Black;
            this.listedFlexiCommands.FormattingEnabled = true;
            this.listedFlexiCommands.ItemHeight = 16;
            this.listedFlexiCommands.Location = new System.Drawing.Point(5, 13);
            this.listedFlexiCommands.Name = "listedFlexiCommands";
            this.listedFlexiCommands.Size = new System.Drawing.Size(396, 48);
            this.listedFlexiCommands.TabIndex = 2;
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouper1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.listedFlexiCommands);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(3, 157);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(408, 64);
            this.grouper1.TabIndex = 14;
            // 
            // timeRangeKeywordTree
            // 
            this.timeRangeKeywordTree.BackColor = System.Drawing.Color.Gainsboro;
            this.timeRangeKeywordTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeRangeKeywordTree.ForeColor = System.Drawing.Color.Black;
            this.timeRangeKeywordTree.LineColor = System.Drawing.Color.Gainsboro;
            this.timeRangeKeywordTree.Location = new System.Drawing.Point(-14, 3);
            this.timeRangeKeywordTree.Name = "timeRangeKeywordTree";
            this.timeRangeKeywordTree.Size = new System.Drawing.Size(111, 68);
            this.timeRangeKeywordTree.TabIndex = 17;
            this.timeRangeKeywordTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.timeRangeKeywordTree_AfterSelect);
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flatTabControl1.Controls.Add(this.tabPage1);
            this.flatTabControl1.Controls.Add(this.tabPage2);
            this.flatTabControl1.Controls.Add(this.tabPage3);
            this.flatTabControl1.Location = new System.Drawing.Point(3, 3);
            this.flatTabControl1.myBackColor = System.Drawing.Color.Gainsboro;
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(408, 161);
            this.flatTabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.commandTree);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(400, 132);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Commands";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.contextTree);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(400, 132);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Context";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage3.Controls.Add(this.rangeController);
            this.tabPage3.Controls.Add(this.timeRangeTree);
            this.tabPage3.Controls.Add(this.timeRangeKeywordTree);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(400, 132);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Timing";
            // 
            // rangeController
            // 
            this.rangeController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rangeController.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.rangeController.DivisionNum = 24;
            this.rangeController.ForeColor = System.Drawing.Color.Black;
            this.rangeController.HeightOfBar = 5;
            this.rangeController.HeightOfMark = 24;
            this.rangeController.HeightOfTick = 5;
            this.rangeController.InnerColor = System.Drawing.Color.Black;
            this.rangeController.Location = new System.Drawing.Point(8, 80);
            this.rangeController.Name = "rangeController";
            this.rangeController.Orientation = Zzzz.ZzzzRangeBar.RangeBarOrientation.horizontal;
            this.rangeController.RangeMaximum = 4;
            this.rangeController.RangeMinimum = 3;
            this.rangeController.ScaleOrientation = Zzzz.ZzzzRangeBar.TopBottomOrientation.bottom;
            this.rangeController.Size = new System.Drawing.Size(321, 51);
            this.rangeController.TabIndex = 19;
            this.rangeController.TotalMaximum = 24;
            this.rangeController.TotalMinimum = 0;
            // 
            // timeRangeTree
            // 
            this.timeRangeTree.BackColor = System.Drawing.Color.Gainsboro;
            this.timeRangeTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeRangeTree.ForeColor = System.Drawing.Color.Black;
            this.timeRangeTree.LineColor = System.Drawing.Color.Gainsboro;
            this.timeRangeTree.Location = new System.Drawing.Point(-14, 67);
            this.timeRangeTree.Name = "timeRangeTree";
            this.timeRangeTree.Size = new System.Drawing.Size(111, 68);
            this.timeRangeTree.TabIndex = 18;
            this.timeRangeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.timeRangeTree_AfterSelect);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.ButtonText = "Remove";
            this.removeButton.Location = new System.Drawing.Point(233, 223);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(183, 29);
            this.removeButton.TabIndex = 16;
            this.removeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.removeButton_MouseDown);
            this.removeButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.removeButton_MouseUp);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.ButtonText = "Add";
            this.addButton.Location = new System.Drawing.Point(2, 223);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(177, 29);
            this.addButton.TabIndex = 15;
            this.addButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseDown);
            this.addButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseUp);
            // 
            // FlexiCommandsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.flatTabControl1);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.grouper1);
            this.Name = "FlexiCommandsControl";
            this.Size = new System.Drawing.Size(414, 253);
            this.grouper1.ResumeLayout(false);
            this.flatTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView commandTree;
        private System.Windows.Forms.TreeView contextTree;
        private System.Windows.Forms.RichTextBox descriptionLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ListBox listedFlexiCommands;
        private CodeVendor.Controls.Grouper grouper1;
        private RoundButton addButton;
        private RoundButton removeButton;
        private System.Windows.Forms.TreeView timeRangeKeywordTree;
        private FlatTabControl.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView timeRangeTree;
        private Zzzz.ZzzzRangeBar rangeController;
    }
}
