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
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.contextTree = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listedFlexiCommands = new System.Windows.Forms.ListBox();
            this.descriptionLabel = new System.Windows.Forms.RichTextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandTree
            // 
            this.commandTree.BackColor = System.Drawing.Color.SteelBlue;
            this.commandTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTree.ForeColor = System.Drawing.Color.PowderBlue;
            this.commandTree.LineColor = System.Drawing.Color.PaleTurquoise;
            this.commandTree.Location = new System.Drawing.Point(3, 3);
            this.commandTree.Name = "commandTree";
            this.commandTree.Size = new System.Drawing.Size(111, 188);
            this.commandTree.TabIndex = 1;
            this.commandTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.commandTree_AfterSelect);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.BackColor = System.Drawing.Color.PowderBlue;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.addButton.Location = new System.Drawing.Point(3, 223);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(166, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.BackColor = System.Drawing.Color.PowderBlue;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.removeButton.Location = new System.Drawing.Point(201, 223);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(181, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = false;
            // 
            // contextTree
            // 
            this.contextTree.BackColor = System.Drawing.Color.SteelBlue;
            this.contextTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contextTree.ForeColor = System.Drawing.Color.PowderBlue;
            this.contextTree.LineColor = System.Drawing.Color.PaleTurquoise;
            this.contextTree.Location = new System.Drawing.Point(93, 3);
            this.contextTree.Name = "contextTree";
            this.contextTree.Size = new System.Drawing.Size(111, 188);
            this.contextTree.TabIndex = 10;
            this.contextTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contextTree_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.listedFlexiCommands);
            this.groupBox1.Location = new System.Drawing.Point(3, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 61);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // listedFlexiCommands
            // 
            this.listedFlexiCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listedFlexiCommands.BackColor = System.Drawing.Color.SteelBlue;
            this.listedFlexiCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listedFlexiCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listedFlexiCommands.ForeColor = System.Drawing.Color.PowderBlue;
            this.listedFlexiCommands.FormattingEnabled = true;
            this.listedFlexiCommands.ItemHeight = 16;
            this.listedFlexiCommands.Items.AddRange(new object[] {
            "add",
            "++",
            "remind"});
            this.listedFlexiCommands.Location = new System.Drawing.Point(6, 10);
            this.listedFlexiCommands.Name = "listedFlexiCommands";
            this.listedFlexiCommands.Size = new System.Drawing.Size(367, 48);
            this.listedFlexiCommands.TabIndex = 2;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionLabel.ForeColor = System.Drawing.SystemColors.Info;
            this.descriptionLabel.Location = new System.Drawing.Point(201, 33);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(181, 107);
            this.descriptionLabel.TabIndex = 13;
            this.descriptionLabel.Text = "Lting and typesetting industry. Lorem Ipsum has been the industry\'s standard dumm" +
    "y text ever since the 1500s, when an unknown printer took a galley of type ";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titleLabel.Location = new System.Drawing.Point(195, 3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(53, 25);
            this.titleLabel.TabIndex = 12;
            this.titleLabel.Text = "Title";
            // 
            // FlexiCommandsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.contextTree);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.commandTree);
            this.Name = "FlexiCommandsControl";
            this.Size = new System.Drawing.Size(385, 253);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView commandTree;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TreeView contextTree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listedFlexiCommands;
        private System.Windows.Forms.RichTextBox descriptionLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}
