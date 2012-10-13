namespace ToDo
{
    partial class Settings
    {
        /// <summary>
        /// Req
        /// 
        /// 
        /// 
        /// 
        /// red designer variable.
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.minimisedCheckbox = new System.Windows.Forms.CheckBox();
            this.loadOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.commandDescription = new System.Windows.Forms.RichTextBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.addUserCommandButton = new System.Windows.Forms.Button();
            this.userCommand = new System.Windows.Forms.TextBox();
            this.listOfCommands = new System.Windows.Forms.ListBox();
            this.commandPreview = new System.Windows.Forms.Label();
            this.CommandTree = new System.Windows.Forms.TreeView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 208);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 182);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Personal Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.minimisedCheckbox);
            this.groupBox1.Controls.Add(this.loadOnStartupCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 171);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // minimisedCheckbox
            // 
            this.minimisedCheckbox.AutoSize = true;
            this.minimisedCheckbox.Location = new System.Drawing.Point(6, 19);
            this.minimisedCheckbox.Name = "minimisedCheckbox";
            this.minimisedCheckbox.Size = new System.Drawing.Size(95, 17);
            this.minimisedCheckbox.TabIndex = 0;
            this.minimisedCheckbox.Text = "Start Minimzed";
            this.minimisedCheckbox.UseVisualStyleBackColor = true;
            this.minimisedCheckbox.CheckStateChanged += new System.EventHandler(this.minimisedCheckbox_CheckStateChanged);
            // 
            // loadOnStartupCheckbox
            // 
            this.loadOnStartupCheckbox.AutoSize = true;
            this.loadOnStartupCheckbox.Location = new System.Drawing.Point(6, 42);
            this.loadOnStartupCheckbox.Name = "loadOnStartupCheckbox";
            this.loadOnStartupCheckbox.Size = new System.Drawing.Size(102, 17);
            this.loadOnStartupCheckbox.TabIndex = 1;
            this.loadOnStartupCheckbox.Text = "Load on Startup";
            this.loadOnStartupCheckbox.UseVisualStyleBackColor = true;
            this.loadOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckStateChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.removeButton);
            this.tabPage3.Controls.Add(this.addUserCommandButton);
            this.tabPage3.Controls.Add(this.userCommand);
            this.tabPage3.Controls.Add(this.listOfCommands);
            this.tabPage3.Controls.Add(this.commandPreview);
            this.tabPage3.Controls.Add(this.CommandTree);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(436, 182);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FlexiCommands";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.commandDescription);
            this.groupBox2.Location = new System.Drawing.Point(264, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // commandDescription
            // 
            this.commandDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandDescription.Location = new System.Drawing.Point(7, 19);
            this.commandDescription.Name = "commandDescription";
            this.commandDescription.ReadOnly = true;
            this.commandDescription.Size = new System.Drawing.Size(153, 90);
            this.commandDescription.TabIndex = 8;
            this.commandDescription.Text = "";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(378, 157);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(55, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addUserCommandButton
            // 
            this.addUserCommandButton.Location = new System.Drawing.Point(333, 157);
            this.addUserCommandButton.Name = "addUserCommandButton";
            this.addUserCommandButton.Size = new System.Drawing.Size(42, 23);
            this.addUserCommandButton.TabIndex = 4;
            this.addUserCommandButton.Text = "Add";
            this.addUserCommandButton.UseVisualStyleBackColor = true;
            this.addUserCommandButton.Click += new System.EventHandler(this.addUserCommandButton_Click);
            // 
            // userCommand
            // 
            this.userCommand.Location = new System.Drawing.Point(264, 160);
            this.userCommand.Name = "userCommand";
            this.userCommand.Size = new System.Drawing.Size(63, 20);
            this.userCommand.TabIndex = 3;
            // 
            // listOfCommands
            // 
            this.listOfCommands.FormattingEnabled = true;
            this.listOfCommands.Location = new System.Drawing.Point(138, 33);
            this.listOfCommands.Name = "listOfCommands";
            this.listOfCommands.Size = new System.Drawing.Size(120, 147);
            this.listOfCommands.TabIndex = 2;
            // 
            // commandPreview
            // 
            this.commandPreview.AutoSize = true;
            this.commandPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandPreview.Location = new System.Drawing.Point(133, 6);
            this.commandPreview.Name = "commandPreview";
            this.commandPreview.Size = new System.Drawing.Size(56, 25);
            this.commandPreview.TabIndex = 1;
            this.commandPreview.Text = "ADD";
            // 
            // CommandTree
            // 
            this.CommandTree.Location = new System.Drawing.Point(6, 6);
            this.CommandTree.Name = "CommandTree";
            this.CommandTree.Size = new System.Drawing.Size(121, 174);
            this.CommandTree.TabIndex = 0;
            this.CommandTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CommandTree_AfterSelect);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(381, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(300, 227);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(219, 227);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 262);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView CommandTree;
        private System.Windows.Forms.Label commandPreview;
        private System.Windows.Forms.ListBox listOfCommands;
        private System.Windows.Forms.Button addUserCommandButton;
        private System.Windows.Forms.TextBox userCommand;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.CheckBox loadOnStartupCheckbox;
        private System.Windows.Forms.CheckBox minimisedCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox commandDescription;

    }
}