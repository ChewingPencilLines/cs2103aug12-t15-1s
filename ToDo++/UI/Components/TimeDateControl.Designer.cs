namespace CustomControls
{
    partial class TimeDateControl
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
            this.dateObject = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateObject
            // 
            this.dateObject.AutoSize = true;
            this.dateObject.BackColor = System.Drawing.Color.Transparent;
            this.dateObject.ForeColor = System.Drawing.Color.White;
            this.dateObject.Location = new System.Drawing.Point(3, 6);
            this.dateObject.Name = "dateObject";
            this.dateObject.Size = new System.Drawing.Size(59, 13);
            this.dateObject.TabIndex = 0;
            this.dateObject.Text = "dateObject";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.ForeColor = System.Drawing.Color.White;
            this.timeLabel.Location = new System.Drawing.Point(68, 6);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(52, 13);
            this.timeLabel.TabIndex = 1;
            this.timeLabel.Text = "timeLabel";
            // 
            // TimeDateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.dateObject);
            this.Name = "TimeDateControl";
            this.Size = new System.Drawing.Size(150, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dateObject;
        private System.Windows.Forms.Label timeLabel;
    }
}
