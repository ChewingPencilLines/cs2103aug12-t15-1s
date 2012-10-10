﻿namespace ToDo
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
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.button_go = new System.Windows.Forms.Button();
            this.notifyIcon_taskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.decreaseSizeButton = new System.Windows.Forms.Button();
            this.increaseSizeButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip = new ToDo.Menu();
            this.outputBox = new ToDo.OutputBox();
            this.dateTimeControl1 = new ToDo.DateTimeControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            // decreaseSizeButton
            // 
            this.decreaseSizeButton.BackColor = System.Drawing.Color.Transparent;
            this.decreaseSizeButton.Location = new System.Drawing.Point(329, 29);
            this.decreaseSizeButton.Name = "decreaseSizeButton";
            this.decreaseSizeButton.Size = new System.Drawing.Size(28, 23);
            this.decreaseSizeButton.TabIndex = 5;
            this.decreaseSizeButton.Text = "A-";
            this.decreaseSizeButton.UseVisualStyleBackColor = false;
            this.decreaseSizeButton.Click += new System.EventHandler(this.decreaseSizeButton_Click);
            // 
            // increaseSizeButton
            // 
            this.increaseSizeButton.BackColor = System.Drawing.Color.Transparent;
            this.increaseSizeButton.Location = new System.Drawing.Point(359, 29);
            this.increaseSizeButton.Name = "increaseSizeButton";
            this.increaseSizeButton.Size = new System.Drawing.Size(28, 23);
            this.increaseSizeButton.TabIndex = 6;
            this.increaseSizeButton.Text = "A+";
            this.increaseSizeButton.UseVisualStyleBackColor = false;
            this.increaseSizeButton.Click += new System.EventHandler(this.increaseSizeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::ToDo.Properties.Resources.tempImage;
            this.pictureBox1.Location = new System.Drawing.Point(13, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 29);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(403, 24);
            this.menuStrip.TabIndex = 9;
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(13, 62);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(378, 162);
            this.outputBox.TabIndex = 7;
            this.outputBox.Text = "";
            // 
            // dateTimeControl1
            // 
            this.dateTimeControl1.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeControl1.Location = new System.Drawing.Point(147, 22);
            this.dateTimeControl1.Name = "dateTimeControl1";
            this.dateTimeControl1.Size = new System.Drawing.Size(150, 36);
            this.dateTimeControl1.TabIndex = 10;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 262);
            this.Controls.Add(this.dateTimeControl1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.increaseSizeButton);
            this.Controls.Add(this.decreaseSizeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_go);
            this.Controls.Add(this.textBox_input);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI";
            this.Text = "ToDo++";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button decreaseSizeButton;
        private System.Windows.Forms.Button increaseSizeButton;
        private OutputBox outputBox;
        private Menu menuStrip;
        private DateTimeControl dateTimeControl1;
    }
}

