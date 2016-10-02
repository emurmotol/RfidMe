namespace RfidMe
{
    partial class MainForm
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
            this.tagLabel = new System.Windows.Forms.Label();
            this.tagLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tagLabel
            // 
            this.tagLabel.AutoSize = true;
            this.tagLabel.Location = new System.Drawing.Point(17, 9);
            this.tagLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tagLabel.Name = "tagLabel";
            this.tagLabel.Size = new System.Drawing.Size(70, 39);
            this.tagLabel.TabIndex = 0;
            this.tagLabel.Text = "Tag:";
            // 
            // tagLinkLabel
            // 
            this.tagLinkLabel.AutoSize = true;
            this.tagLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tagLinkLabel.Location = new System.Drawing.Point(103, 9);
            this.tagLinkLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tagLinkLabel.Name = "tagLinkLabel";
            this.tagLinkLabel.Size = new System.Drawing.Size(0, 39);
            this.tagLinkLabel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 59);
            this.Controls.Add(this.tagLinkLabel);
            this.Controls.Add(this.tagLabel);
            this.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RfidMe";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tagLabel;
        public System.Windows.Forms.LinkLabel tagLinkLabel;
    }
}

