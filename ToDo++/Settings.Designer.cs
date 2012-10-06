﻿namespace ToDo
{
    partial class Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.minimisedCheckbox = new System.Windows.Forms.CheckBox();
            this.loadOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FontTree = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.removeButton = new System.Windows.Forms.Button();
            this.commandDescription = new System.Windows.Forms.Label();
            this.addUserCommandButton = new System.Windows.Forms.Button();
            this.userCommand = new System.Windows.Forms.TextBox();
            this.listOfCommands = new System.Windows.Forms.ListBox();
            this.commandPreview = new System.Windows.Forms.Label();
            this.CommandTree = new System.Windows.Forms.TreeView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 238);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(436, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Personal Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 42);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome User";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.minimisedCheckbox);
            this.groupBox1.Controls.Add(this.loadOnStartupCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(6, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 152);
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
            this.minimisedCheckbox.CheckedChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckedChanged);
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
            this.loadOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckedChanged);
            this.loadOnStartupCheckbox.CheckStateChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckStateChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.FontTree);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(436, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fonts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FontTree
            // 
            this.FontTree.Location = new System.Drawing.Point(6, 6);
            this.FontTree.Name = "FontTree";
            this.FontTree.Size = new System.Drawing.Size(121, 200);
            this.FontTree.TabIndex = 0;
            this.FontTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FontTree_MouseClick);
            this.FontTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FontTree_MouseDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.removeButton);
            this.tabPage3.Controls.Add(this.commandDescription);
            this.tabPage3.Controls.Add(this.addUserCommandButton);
            this.tabPage3.Controls.Add(this.userCommand);
            this.tabPage3.Controls.Add(this.listOfCommands);
            this.tabPage3.Controls.Add(this.commandPreview);
            this.tabPage3.Controls.Add(this.CommandTree);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(436, 212);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FlexiCommands";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(378, 184);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(55, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // commandDescription
            // 
            this.commandDescription.AutoSize = true;
            this.commandDescription.Location = new System.Drawing.Point(265, 33);
            this.commandDescription.Name = "commandDescription";
            this.commandDescription.Size = new System.Drawing.Size(110, 13);
            this.commandDescription.TabIndex = 5;
            this.commandDescription.Text = "Command Description";
            // 
            // addUserCommandButton
            // 
            this.addUserCommandButton.Location = new System.Drawing.Point(333, 184);
            this.addUserCommandButton.Name = "addUserCommandButton";
            this.addUserCommandButton.Size = new System.Drawing.Size(42, 23);
            this.addUserCommandButton.TabIndex = 4;
            this.addUserCommandButton.Text = "Add";
            this.addUserCommandButton.UseVisualStyleBackColor = true;
            this.addUserCommandButton.Click += new System.EventHandler(this.addUserCommandButton_Click);
            // 
            // userCommand
            // 
            this.userCommand.Location = new System.Drawing.Point(264, 186);
            this.userCommand.Name = "userCommand";
            this.userCommand.Size = new System.Drawing.Size(63, 20);
            this.userCommand.TabIndex = 3;
            // 
            // listOfCommands
            // 
            this.listOfCommands.FormattingEnabled = true;
            this.listOfCommands.Location = new System.Drawing.Point(138, 33);
            this.listOfCommands.Name = "listOfCommands";
            this.listOfCommands.Size = new System.Drawing.Size(120, 173);
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
            this.CommandTree.Size = new System.Drawing.Size(121, 200);
            this.CommandTree.TabIndex = 0;
            this.CommandTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CommandTree_AfterSelect);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 262);
            this.Controls.Add(this.tabControl1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView FontTree;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView CommandTree;
        private System.Windows.Forms.Label commandPreview;
        private System.Windows.Forms.ListBox listOfCommands;
        private System.Windows.Forms.Label commandDescription;
        private System.Windows.Forms.Button addUserCommandButton;
        private System.Windows.Forms.TextBox userCommand;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.CheckBox loadOnStartupCheckbox;
        private System.Windows.Forms.CheckBox minimisedCheckbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}