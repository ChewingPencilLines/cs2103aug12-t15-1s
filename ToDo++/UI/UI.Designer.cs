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
            this.preferencesButton = new System.Windows.Forms.Button();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textInput = new ToDo.InputBox();
            this.outputBox = new ToDo.OutputBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.preferencesPanel = new ToDo.PreferencesPanel();
            this.taskDisplay = new System.Windows.Forms.TabPage();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.taskListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.taskDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // preferencesButton
            // 
            this.preferencesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesButton.BackColor = System.Drawing.Color.Silver;
            this.preferencesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.preferencesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesButton.ForeColor = System.Drawing.Color.DimGray;
            this.preferencesButton.Location = new System.Drawing.Point(372, 5);
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(88, 37);
            this.preferencesButton.TabIndex = 14;
            this.preferencesButton.Text = "Preferences";
            this.preferencesButton.UseVisualStyleBackColor = false;
            this.preferencesButton.Click += new System.EventHandler(this.preferencesButton_Click);
            // 
            // customPanelControl
            // 
            this.customPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanelControl.Controls.Add(this.tabPage1);
            this.customPanelControl.Controls.Add(this.tabPage2);
            this.customPanelControl.Controls.Add(this.taskDisplay);
            this.customPanelControl.Location = new System.Drawing.Point(5, 48);
            this.customPanelControl.Name = "customPanelControl";
            this.customPanelControl.SelectedIndex = 0;
            this.customPanelControl.Size = new System.Drawing.Size(509, 333);
            this.customPanelControl.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DimGray;
            this.tabPage1.Controls.Add(this.textInput);
            this.tabPage1.Controls.Add(this.outputBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // textInput
            // 
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.BackColor = System.Drawing.Color.DarkGray;
            this.textInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInput.Location = new System.Drawing.Point(3, 278);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(507, 19);
            this.textInput.TabIndex = 0;
            this.textInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            this.textInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textInput_PreviewKeyDown);
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.Color.DarkGray;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.Location = new System.Drawing.Point(3, 0);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(498, 272);
            this.outputBox.TabIndex = 7;
            this.outputBox.Text = "";
            this.outputBox.MouseHover += new System.EventHandler(this.outputBox_MouseHover);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage2.Controls.Add(this.preferencesPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 307);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // preferencesPanel
            // 
            this.preferencesPanel.BackColor = System.Drawing.Color.SteelBlue;
            this.preferencesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesPanel.Location = new System.Drawing.Point(3, 3);
            this.preferencesPanel.Name = "preferencesPanel";
            this.preferencesPanel.Size = new System.Drawing.Size(186, 68);
            this.preferencesPanel.TabIndex = 0;
            // 
            // taskDisplay
            // 
            this.taskDisplay.BackColor = System.Drawing.Color.DarkGray;
            this.taskDisplay.Controls.Add(this.splitContainerMain);
            this.taskDisplay.Location = new System.Drawing.Point(4, 22);
            this.taskDisplay.Name = "taskDisplay";
            this.taskDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.taskDisplay.Size = new System.Drawing.Size(501, 307);
            this.taskDisplay.TabIndex = 2;
            this.taskDisplay.Text = "TaskDisplay";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.taskListView);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.textBox1);
            this.splitContainerMain.Size = new System.Drawing.Size(495, 301);
            this.splitContainerMain.SplitterDistance = 260;
            this.splitContainerMain.TabIndex = 0;
            // 
            // taskListView
            // 
            this.taskListView.BackColor = System.Drawing.Color.DarkGray;
            this.taskListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.Location = new System.Drawing.Point(0, 0);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(495, 260);
            this.taskListView.TabIndex = 0;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Task Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 2;
            this.columnHeader2.Text = "Start Date";
            this.columnHeader2.Width = 143;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 1;
            this.columnHeader3.Text = "End Date";
            this.columnHeader3.Width = 125;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.DimGray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(495, 16);
            this.textBox1.TabIndex = 0;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(522, 385);
            this.Controls.Add(this.preferencesButton);
            this.Controls.Add(this.customPanelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(522, 385);
            this.Name = "UI";
            this.Text = "ToDo++";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_MouseDown);
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.taskDisplay.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.Button preferencesButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private OutputBox outputBox;
        private CustomPanelControl customPanelControl;
        private PreferencesPanel preferencesPanel;
        private System.Windows.Forms.TabPage taskDisplay;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ListView taskListView;
        private InputBox textInput;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

