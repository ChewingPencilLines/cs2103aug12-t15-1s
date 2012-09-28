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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toDoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.button_go = new System.Windows.Forms.Button();
            this.notifyIcon_taskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toDoToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(403, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toDoToolStripMenuItem
            // 
            this.toDoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.openRecentToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exportToToolStripMenuItem,
            this.toolStripMenuItem2,
            this.startMinimizedToolStripMenuItem,
            this.loadOnStartupToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toDoToolStripMenuItem.Name = "toDoToolStripMenuItem";
            this.toDoToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.toDoToolStripMenuItem.Text = "&File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.fileToolStripMenuItem.Text = "Open";
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openRecentToolStripMenuItem.Text = "Open Recent";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // exportToToolStripMenuItem
            // 
            this.exportToToolStripMenuItem.Name = "exportToToolStripMenuItem";
            this.exportToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.exportToToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportToToolStripMenuItem.Text = "Export To";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 6);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            this.startMinimizedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.startMinimizedToolStripMenuItem.Text = "Start Minimized";
            // 
            // loadOnStartupToolStripMenuItem
            // 
            this.loadOnStartupToolStripMenuItem.Name = "loadOnStartupToolStripMenuItem";
            this.loadOnStartupToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadOnStartupToolStripMenuItem.Text = "Load on Startup";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.settingsToolStripMenuItem.Text = "&Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpToolStripMenuItem.Text = "&Settings";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.settingsClicked);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // richTextBox_output
            // 
            this.richTextBox_output.Location = new System.Drawing.Point(12, 29);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.Size = new System.Drawing.Size(379, 191);
            this.richTextBox_output.TabIndex = 2;
            this.richTextBox_output.Text = "";
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(13, 230);
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.Size = new System.Drawing.Size(314, 20);
            this.textBox_input.TabIndex = 0;
            this.textBox_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            // 
            // button_go
            // 
            this.button_go.Location = new System.Drawing.Point(333, 227);
            this.button_go.Name = "button_go";
            this.button_go.Size = new System.Drawing.Size(58, 23);
            this.button_go.TabIndex = 1;
            this.button_go.Text = "Go";
            this.button_go.UseVisualStyleBackColor = true;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 262);
            this.Controls.Add(this.button_go);
            this.Controls.Add(this.textBox_input);
            this.Controls.Add(this.richTextBox_output);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UI";
            this.Text = "ToDo++";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toDoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem startMinimizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadOnStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
    }
}

