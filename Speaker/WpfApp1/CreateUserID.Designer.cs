namespace WpfApp1
{
    partial class CreateUserID
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateUserID));
            this.createnewprofile = new System.Windows.Forms.Button();
            this.getallprofiles = new System.Windows.Forms.Button();
            this.enrollprofile = new System.Windows.Forms.Button();
            this.selectfile = new System.Windows.Forms.Button();
            this.deleteprofile = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identifyspeaker = new System.Windows.Forms.Button();
            this.namebox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // createnewprofile
            // 
            this.createnewprofile.BackColor = System.Drawing.SystemColors.Control;
            this.createnewprofile.Location = new System.Drawing.Point(527, 43);
            this.createnewprofile.Name = "createnewprofile";
            this.createnewprofile.Size = new System.Drawing.Size(147, 48);
            this.createnewprofile.TabIndex = 0;
            this.createnewprofile.Text = "Create new profile";
            this.createnewprofile.UseVisualStyleBackColor = false;
            this.createnewprofile.Click += new System.EventHandler(this.CreateProfile_Clicked);
            // 
            // getallprofiles
            // 
            this.getallprofiles.BackColor = System.Drawing.SystemColors.Control;
            this.getallprofiles.Location = new System.Drawing.Point(527, 116);
            this.getallprofiles.Name = "getallprofiles";
            this.getallprofiles.Size = new System.Drawing.Size(147, 48);
            this.getallprofiles.TabIndex = 1;
            this.getallprofiles.Text = "Get all profles";
            this.getallprofiles.UseVisualStyleBackColor = false;
            this.getallprofiles.Click += new System.EventHandler(this.GetAllProfiles_Clicked);
            // 
            // enrollprofile
            // 
            this.enrollprofile.BackColor = System.Drawing.SystemColors.Control;
            this.enrollprofile.Location = new System.Drawing.Point(527, 184);
            this.enrollprofile.Name = "enrollprofile";
            this.enrollprofile.Size = new System.Drawing.Size(147, 48);
            this.enrollprofile.TabIndex = 3;
            this.enrollprofile.Text = "Enroll profile";
            this.enrollprofile.UseVisualStyleBackColor = false;
            this.enrollprofile.Click += new System.EventHandler(this.EnrollProfile_Clicked);
            // 
            // selectfile
            // 
            this.selectfile.BackColor = System.Drawing.SystemColors.Control;
            this.selectfile.Location = new System.Drawing.Point(582, 425);
            this.selectfile.Name = "selectfile";
            this.selectfile.Size = new System.Drawing.Size(92, 23);
            this.selectfile.TabIndex = 4;
            this.selectfile.Text = "SelectFile";
            this.selectfile.UseVisualStyleBackColor = false;
            this.selectfile.Click += new System.EventHandler(this.SelectFile_Clicked);
            // 
            // deleteprofile
            // 
            this.deleteprofile.BackColor = System.Drawing.SystemColors.Control;
            this.deleteprofile.Location = new System.Drawing.Point(582, 386);
            this.deleteprofile.Name = "deleteprofile";
            this.deleteprofile.Size = new System.Drawing.Size(92, 23);
            this.deleteprofile.TabIndex = 5;
            this.deleteprofile.Text = "Delete profile";
            this.deleteprofile.UseVisualStyleBackColor = false;
            this.deleteprofile.Click += new System.EventHandler(this.DeleteProfile_Clicked);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Status,
            this.Name});
            this.dataGridView1.Location = new System.Drawing.Point(21, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(339, 189);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ID
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle1;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // identifyspeaker
            // 
            this.identifyspeaker.BackColor = System.Drawing.SystemColors.Control;
            this.identifyspeaker.Location = new System.Drawing.Point(527, 256);
            this.identifyspeaker.Name = "identifyspeaker";
            this.identifyspeaker.Size = new System.Drawing.Size(147, 48);
            this.identifyspeaker.TabIndex = 7;
            this.identifyspeaker.Text = "Identify speaker";
            this.identifyspeaker.UseVisualStyleBackColor = false;
            this.identifyspeaker.Click += new System.EventHandler(this.IdentifySpeaker_Clicked);
            // 
            // namebox
            // 
            this.namebox.Location = new System.Drawing.Point(411, 58);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(100, 22);
            this.namebox.TabIndex = 8;
            this.namebox.Text = "\'Name here\'";
            this.namebox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.namebox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // CreateUserID
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 475);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.identifyspeaker);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.deleteprofile);
            this.Controls.Add(this.selectfile);
            this.Controls.Add(this.enrollprofile);
            this.Controls.Add(this.getallprofiles);
            this.Controls.Add(this.createnewprofile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            
            this.Text = "Profiles";
            this.Load += new System.EventHandler(this.CreateUserID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
            
        private System.Windows.Forms.Button createnewprofile;
        private System.Windows.Forms.Button getallprofiles;
        private System.Windows.Forms.Button enrollprofile;
        private System.Windows.Forms.Button selectfile;
        private System.Windows.Forms.Button deleteprofile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button identifyspeaker;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
    }
}