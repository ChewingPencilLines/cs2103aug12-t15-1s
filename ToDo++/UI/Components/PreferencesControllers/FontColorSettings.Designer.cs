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
            this.taskMissedDeadlineColorButton = new System.Windows.Forms.Button();
            this.taskDeadlineDayColor = new System.Windows.Forms.Button();
            this.taskEventColor = new System.Windows.Forms.Button();
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
            // taskMissedDeadlineColorButton
            // 
            this.taskMissedDeadlineColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskMissedDeadlineColorButton.BackColor = System.Drawing.Color.DimGray;
            this.taskMissedDeadlineColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskMissedDeadlineColorButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskMissedDeadlineColorButton.Location = new System.Drawing.Point(3, 61);
            this.taskMissedDeadlineColorButton.Name = "taskMissedDeadlineColorButton";
            this.taskMissedDeadlineColorButton.Size = new System.Drawing.Size(320, 23);
            this.taskMissedDeadlineColorButton.TabIndex = 3;
            this.taskMissedDeadlineColorButton.Text = "Task Missed Deadline Color";
            this.taskMissedDeadlineColorButton.UseVisualStyleBackColor = false;
            this.taskMissedDeadlineColorButton.Click += new System.EventHandler(this.taskDeadlineColorButton_Click);
            // 
            // taskDeadlineDayColor
            // 
            this.taskDeadlineDayColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDeadlineDayColor.BackColor = System.Drawing.Color.DimGray;
            this.taskDeadlineDayColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskDeadlineDayColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskDeadlineDayColor.Location = new System.Drawing.Point(3, 90);
            this.taskDeadlineDayColor.Name = "taskDeadlineDayColor";
            this.taskDeadlineDayColor.Size = new System.Drawing.Size(320, 23);
            this.taskDeadlineDayColor.TabIndex = 4;
            this.taskDeadlineDayColor.Text = "Task Nearing Deadline Color";
            this.taskDeadlineDayColor.UseVisualStyleBackColor = false;
            this.taskDeadlineDayColor.Click += new System.EventHandler(this.taskDeadlineDayColor_Click);
            // 
            // taskEventColor
            // 
            this.taskEventColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskEventColor.BackColor = System.Drawing.Color.DimGray;
            this.taskEventColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskEventColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskEventColor.Location = new System.Drawing.Point(3, 119);
            this.taskEventColor.Name = "taskEventColor";
            this.taskEventColor.Size = new System.Drawing.Size(320, 23);
            this.taskEventColor.TabIndex = 5;
            this.taskEventColor.Text = "Task Missed Color";
            this.taskEventColor.UseVisualStyleBackColor = false;
            this.taskEventColor.Click += new System.EventHandler(this.taskEventColor_Click);
            // 
            // FontColorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.taskEventColor);
            this.Controls.Add(this.taskDeadlineDayColor);
            this.Controls.Add(this.taskMissedDeadlineColorButton);
            this.Controls.Add(this.taskDoneColorButton);
            this.Controls.Add(this.textSizeButton);
            this.Name = "FontColorSettings";
            this.Size = new System.Drawing.Size(326, 248);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button textSizeButton;
        private System.Windows.Forms.Button taskDoneColorButton;
        private System.Windows.Forms.Button taskMissedDeadlineColorButton;
        private System.Windows.Forms.Button taskDeadlineDayColor;
        private System.Windows.Forms.Button taskEventColor;

    }
}
