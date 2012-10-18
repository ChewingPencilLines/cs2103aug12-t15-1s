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
            this.addedFlexiCommands = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandTree
            // 
            this.commandTree.BackColor = System.Drawing.Color.SteelBlue;
            this.commandTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTree.ForeColor = System.Drawing.Color.PowderBlue;
            this.commandTree.LineColor = System.Drawing.Color.PaleTurquoise;
            this.commandTree.Location = new System.Drawing.Point(11, 9);
            this.commandTree.Name = "commandTree";
            this.commandTree.Size = new System.Drawing.Size(85, 94);
            this.commandTree.TabIndex = 1;
            // 
            // addedFlexiCommands
            // 
            this.addedFlexiCommands.BackColor = System.Drawing.Color.SteelBlue;
            this.addedFlexiCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.addedFlexiCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addedFlexiCommands.ForeColor = System.Drawing.Color.PowderBlue;
            this.addedFlexiCommands.FormattingEnabled = true;
            this.addedFlexiCommands.ItemHeight = 16;
            this.addedFlexiCommands.Items.AddRange(new object[] {
            "add",
            "++",
            "remind"});
            this.addedFlexiCommands.Location = new System.Drawing.Point(7, 10);
            this.addedFlexiCommands.Name = "addedFlexiCommands";
            this.addedFlexiCommands.Size = new System.Drawing.Size(203, 48);
            this.addedFlexiCommands.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.addedFlexiCommands);
            this.groupBox1.Location = new System.Drawing.Point(102, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 61);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.PowderBlue;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.addButton.Location = new System.Drawing.Point(102, 68);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(104, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.PowderBlue;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.removeButton.Location = new System.Drawing.Point(212, 68);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(106, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = false;
            // 
            // FlexiCommandsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.commandTree);
            this.Name = "FlexiCommandsControl";
            this.Size = new System.Drawing.Size(321, 111);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView commandTree;
        private System.Windows.Forms.ListBox addedFlexiCommands;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
    }
}
