namespace WpfApp1
{
    partial class Silence
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
            this.button1 = new System.Windows.Forms.Button();
            this.detectsilence = new System.Windows.Forms.Button();
            this.transcribe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // detectsilence
            // 
            this.detectsilence.Location = new System.Drawing.Point(566, 100);
            this.detectsilence.Name = "detectsilence";
            this.detectsilence.Size = new System.Drawing.Size(156, 57);
            this.detectsilence.TabIndex = 1;
            this.detectsilence.Text = "Detect silence";
            this.detectsilence.UseVisualStyleBackColor = true;
            this.detectsilence.Click += new System.EventHandler(this.DetectSilence_Clicked);
            // 
            // transcribe
            // 
            this.transcribe.Location = new System.Drawing.Point(178, 134);
            this.transcribe.Name = "transcribe";
            this.transcribe.Size = new System.Drawing.Size(191, 88);
            this.transcribe.TabIndex = 2;
            this.transcribe.Text = "Transcribe";
            this.transcribe.UseVisualStyleBackColor = true;
            this.transcribe.Click += new System.EventHandler(this.Transcribe_Clicked);
            // 
            // Silence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 435);
            this.Controls.Add(this.transcribe);
            this.Controls.Add(this.detectsilence);
            this.Controls.Add(this.button1);
            this.Name = "Silence";
            this.Text = "Silence";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button detectsilence;
        private System.Windows.Forms.Button transcribe;
    }
}