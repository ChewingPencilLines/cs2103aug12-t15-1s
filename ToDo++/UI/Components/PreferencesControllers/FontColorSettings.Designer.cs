namespace ToDo
{
    partial class FontColorSettings
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
            this.textSizeButton = new System.Windows.Forms.Button();
            this.taskDoneColorButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textSizeButton
            // 
            this.textSizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSizeButton.BackColor = System.Drawing.Color.DimGray;
            this.textSizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textSizeButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.textSizeButton.Location = new System.Drawing.Point(3, 3);
            this.textSizeButton.Name = "textSizeButton";
            this.textSizeButton.Size = new System.Drawing.Size(320, 23);
            this.textSizeButton.TabIndex = 1;
            this.textSizeButton.Text = "Font and Size";
            this.textSizeButton.UseVisualStyleBackColor = false;
            this.textSizeButton.Click += new System.EventHandler(this.textSizeButton_Click);
            // 
            // taskDoneColorButton
            // 
            this.taskDoneColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDoneColorButton.BackColor = System.Drawing.Color.DimGray;
            this.taskDoneColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskDoneColorButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskDoneColorButton.Location = new System.Drawing.Point(3, 32);
            this.taskDoneColorButton.Name = "taskDoneColorButton";
            this.taskDoneColorButton.Size = new System.Drawing.Size(320, 23);
            this.taskDoneColorButton.TabIndex = 2;
            this.taskDoneColorButton.Text = "Task Done Color";
            this.taskDoneColorButton.UseVisualStyleBackColor = false;
            this.taskDoneColorButton.Click += new System.EventHandler(this.taskDoneColorButton_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.DimGray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(3, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(320, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Text Size";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.DimGray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(3, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(320, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Text Size";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // FontColorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.taskDoneColorButton);
            this.Controls.Add(this.textSizeButton);
            this.Name = "FontColorSettings";
            this.Size = new System.Drawing.Size(326, 248);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button textSizeButton;
        private System.Windows.Forms.Button taskDoneColorButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

    }
}
