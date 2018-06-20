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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Silence));
            this.button1 = new System.Windows.Forms.Button();
            this.transcribe = new System.Windows.Forms.Button();
            this.resultview = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 164);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // transcribe
            // 
            this.transcribe.Location = new System.Drawing.Point(322, 60);
            this.transcribe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.transcribe.Name = "transcribe";
            this.transcribe.Size = new System.Drawing.Size(143, 72);
            this.transcribe.TabIndex = 2;
            this.transcribe.Text = "Transcribe";
            this.transcribe.UseVisualStyleBackColor = true;
            this.transcribe.Click += new System.EventHandler(this.Transcribe_Clicked);
            // 
            // resultview
            // 
            this.resultview.Location = new System.Drawing.Point(25, 60);
            this.resultview.Name = "resultview";
            this.resultview.Size = new System.Drawing.Size(273, 271);
            this.resultview.TabIndex = 3;
            this.resultview.UseCompatibleStateImageBehavior = false;
            this.resultview.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Silence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(476, 353);
            this.Controls.Add(this.resultview);
            this.Controls.Add(this.transcribe);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Silence";
            this.Text = "Transcribe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button transcribe;
        private System.Windows.Forms.ListView resultview;
    }
}