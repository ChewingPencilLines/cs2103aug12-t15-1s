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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxMOD1 = new ToDo.GroupBoxMOD();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeControl1 = new ToDo.DateTimeControl();
            this.consoleButon = new ToDo.CustomButton();
            this.loadButton = new ToDo.CustomButton();
            this.preferencesButton = new ToDo.CustomButton();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.taskListViewControl = new ToDo.TaskListViewControl();
            this.Task = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.preferencesPanel = new ToDo.PreferencesPanel();
            this.taskDisplay = new System.Windows.Forms.TabPage();
            this.outputBox = new ToDo.OutputBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.inputBox2 = new ToDo.InputBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inputBox1 = new ToDo.InputBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.textInput = new ToDo.InputBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxMOD1.SuspendLayout();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.taskDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(522, 355);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxMOD1
            // 
            this.groupBoxMOD1.BackColor = System.Drawing.Color.White;
            this.groupBoxMOD1.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBoxMOD1.BorderColorLight = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxMOD1.BorderRadius = 7;
            this.groupBoxMOD1.Controls.Add(this.label1);
            this.groupBoxMOD1.Controls.Add(this.dateTimeControl1);
            this.groupBoxMOD1.Enabled = false;
            this.groupBoxMOD1.Location = new System.Drawing.Point(6, 46);
            this.groupBoxMOD1.Name = "groupBoxMOD1";
            this.groupBoxMOD1.Size = new System.Drawing.Size(506, 25);
            this.groupBoxMOD1.TabIndex = 20;
            this.groupBoxMOD1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "ToDo++";
            // 
            // dateTimeControl1
            // 
            this.dateTimeControl1.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeControl1.Location = new System.Drawing.Point(351, -8);
            this.dateTimeControl1.Name = "dateTimeControl1";
            this.dateTimeControl1.Size = new System.Drawing.Size(150, 36);
            this.dateTimeControl1.TabIndex = 0;
            // 
            // consoleButon
            // 
            this.consoleButon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleButon.BackColor = System.Drawing.Color.White;
            this.consoleButon.ButtonText = "Console";
            this.consoleButon.Location = new System.Drawing.Point(156, 3);
            this.consoleButon.Name = "consoleButon";
            this.consoleButon.Size = new System.Drawing.Size(214, 45);
            this.consoleButon.TabIndex = 19;
            this.consoleButon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.consoleButon_MouseDown);
            this.consoleButon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.consoleButon_MouseUp);
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.White;
            this.loadButton.ButtonText = "Load";
            this.loadButton.Location = new System.Drawing.Point(6, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(146, 45);
            this.loadButton.TabIndex = 18;
            this.loadButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loadButton_MouseDown);
            this.loadButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.loadButton_MouseUp);
            // 
            // preferencesButton
            // 
            this.preferencesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesButton.BackColor = System.Drawing.Color.White;
            this.preferencesButton.ButtonText = "Settings";
            this.preferencesButton.Location = new System.Drawing.Point(375, 3);
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(142, 45);
            this.preferencesButton.TabIndex = 16;
            this.preferencesButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.preferencesButton_MouseDown);
            this.preferencesButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.preferencesButton_MouseUp);
            // 
            // customPanelControl
            // 
            this.customPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanelControl.Controls.Add(this.tabPage1);
            this.customPanelControl.Controls.Add(this.tabPage2);
            this.customPanelControl.Controls.Add(this.taskDisplay);
            this.customPanelControl.Location = new System.Drawing.Point(7, 77);
            this.customPanelControl.Name = "customPanelControl";
            this.customPanelControl.SelectedIndex = 0;
            this.customPanelControl.Size = new System.Drawing.Size(509, 278);
            this.customPanelControl.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.taskListViewControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 252);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // taskListViewControl
            // 
            this.taskListViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskListViewControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskListViewControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Task,
            this.StartDate,
            this.EndDate});
            this.taskListViewControl.Location = new System.Drawing.Point(3, 3);
            this.taskListViewControl.Name = "taskListViewControl";
            this.taskListViewControl.Size = new System.Drawing.Size(495, 249);
            this.taskListViewControl.TabIndex = 17;
            this.taskListViewControl.UseCompatibleStateImageBehavior = false;
            this.taskListViewControl.View = System.Windows.Forms.View.Details;
            // 
            // Task
            // 
            this.Task.Width = 136;
            // 
            // StartDate
            // 
            this.StartDate.Width = 201;
            // 
            // EndDate
            // 
            this.EndDate.Width = 155;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.preferencesPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 252);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // preferencesPanel
            // 
            this.preferencesPanel.BackColor = System.Drawing.Color.White;
            this.preferencesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesPanel.Location = new System.Drawing.Point(3, 3);
            this.preferencesPanel.Name = "preferencesPanel";
            this.preferencesPanel.Size = new System.Drawing.Size(186, 68);
            this.preferencesPanel.TabIndex = 0;
            // 
            // taskDisplay
            // 
            this.taskDisplay.BackColor = System.Drawing.Color.White;
            this.taskDisplay.Controls.Add(this.outputBox);
            this.taskDisplay.Controls.Add(this.textBox3);
            this.taskDisplay.Controls.Add(this.inputBox2);
            this.taskDisplay.Controls.Add(this.textBox2);
            this.taskDisplay.Controls.Add(this.textBox1);
            this.taskDisplay.Controls.Add(this.inputBox1);
            this.taskDisplay.Controls.Add(this.pictureBox7);
            this.taskDisplay.Controls.Add(this.pictureBox6);
            this.taskDisplay.Controls.Add(this.pictureBox5);
            this.taskDisplay.Controls.Add(this.pictureBox4);
            this.taskDisplay.Controls.Add(this.pictureBox3);
            this.taskDisplay.Location = new System.Drawing.Point(4, 22);
            this.taskDisplay.Name = "taskDisplay";
            this.taskDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.taskDisplay.Size = new System.Drawing.Size(501, 252);
            this.taskDisplay.TabIndex = 2;
            this.taskDisplay.Text = "Task Display";
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.White;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputBox.Location = new System.Drawing.Point(3, 3);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(186, 275);
            this.outputBox.TabIndex = 28;
            this.outputBox.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(200, -20238);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(0, 20);
            this.textBox3.TabIndex = 24;
            // 
            // inputBox2
            // 
            this.inputBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox2.BackColor = System.Drawing.Color.Black;
            this.inputBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox2.ForeColor = System.Drawing.Color.White;
            this.inputBox2.Location = new System.Drawing.Point(0, -20552);
            this.inputBox2.Name = "inputBox2";
            this.inputBox2.Size = new System.Drawing.Size(0, 19);
            this.inputBox2.TabIndex = 22;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(0, -21009);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0, 19);
            this.textBox2.TabIndex = 21;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, -21464);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 19);
            this.textBox1.TabIndex = 19;
            // 
            // inputBox1
            // 
            this.inputBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox1.BackColor = System.Drawing.Color.Black;
            this.inputBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox1.ForeColor = System.Drawing.Color.White;
            this.inputBox1.Location = new System.Drawing.Point(3, -23063);
            this.inputBox1.Name = "inputBox1";
            this.inputBox1.Size = new System.Drawing.Size(0, 19);
            this.inputBox1.TabIndex = 17;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.BackColor = System.Drawing.Color.Black;
            this.pictureBox7.Location = new System.Drawing.Point(391, -19087);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(0, 50);
            this.pictureBox7.TabIndex = 27;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackColor = System.Drawing.Color.Black;
            this.pictureBox6.Location = new System.Drawing.Point(224, -19071);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(0, 50);
            this.pictureBox6.TabIndex = 26;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Enabled = false;
            this.pictureBox5.Location = new System.Drawing.Point(-3, -20556);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(0, 5);
            this.pictureBox5.TabIndex = 23;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Enabled = false;
            this.pictureBox4.Location = new System.Drawing.Point(0, -21697);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(0, 5);
            this.pictureBox4.TabIndex = 20;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Enabled = false;
            this.pictureBox3.Location = new System.Drawing.Point(-3, -24203);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(0, 5);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // textInput
            // 
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.BackColor = System.Drawing.Color.Black;
            this.textInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInput.ForeColor = System.Drawing.Color.White;
            this.textInput.Location = new System.Drawing.Point(11, 360);
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(501, 19);
            this.textInput.TabIndex = 0;
            this.textInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            this.textInput.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textInput_PreviewKeyDown);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(522, 385);
            this.Controls.Add(this.groupBoxMOD1);
            this.Controls.Add(this.consoleButon);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.preferencesButton);
            this.Controls.Add(this.customPanelControl);
            this.Controls.Add(this.textInput);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(522, 385);
            this.Name = "UI";
            this.Text = "ToDo++";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_MouseDown);
            this.Resize += new System.EventHandler(this.UI_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxMOD1.ResumeLayout(false);
            this.groupBoxMOD1.PerformLayout();
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.taskDisplay.ResumeLayout(false);
            this.taskDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private CustomPanelControl customPanelControl;
        private PreferencesPanel preferencesPanel;
        private InputBox textInput;
        private System.Windows.Forms.TabPage taskDisplay;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private InputBox inputBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private InputBox inputBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private TaskListViewControl taskListViewControl;
        private System.Windows.Forms.ColumnHeader Task;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader EndDate;
        private OutputBox outputBox;
        private CustomButton preferencesButton;
        private CustomButton loadButton;
        private CustomButton consoleButon;
        private GroupBoxMOD groupBoxMOD1;
        private DateTimeControl dateTimeControl1;
        private System.Windows.Forms.Label label1;
    }
}

