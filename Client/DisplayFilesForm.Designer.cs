namespace JXDL.Client
{
    partial class DisplayFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayFilesForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_FileList = new System.Windows.Forms.DataGridView();
            this.Column_FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button_Exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FileList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_FileList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件档案";
            // 
            // dataGridView_FileList
            // 
            this.dataGridView_FileList.AllowUserToAddRows = false;
            this.dataGridView_FileList.AllowUserToDeleteRows = false;
            this.dataGridView_FileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_FileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_FileName,
            this.Column_Unit,
            this.Column_Author,
            this.Column_Date,
            this.Column_Download});
            this.dataGridView_FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_FileList.Location = new System.Drawing.Point(3, 17);
            this.dataGridView_FileList.MultiSelect = false;
            this.dataGridView_FileList.Name = "dataGridView_FileList";
            this.dataGridView_FileList.ReadOnly = true;
            this.dataGridView_FileList.RowTemplate.Height = 23;
            this.dataGridView_FileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_FileList.Size = new System.Drawing.Size(735, 330);
            this.dataGridView_FileList.TabIndex = 0;
            this.dataGridView_FileList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_FileList_CellClick);
            // 
            // Column_FileName
            // 
            this.Column_FileName.DataPropertyName = "FileName";
            this.Column_FileName.HeaderText = "文件名";
            this.Column_FileName.Name = "Column_FileName";
            this.Column_FileName.ReadOnly = true;
            this.Column_FileName.Width = 260;
            // 
            // Column_Unit
            // 
            this.Column_Unit.DataPropertyName = "UnitName";
            this.Column_Unit.HeaderText = "所属单位";
            this.Column_Unit.Name = "Column_Unit";
            this.Column_Unit.ReadOnly = true;
            this.Column_Unit.Width = 130;
            // 
            // Column_Author
            // 
            this.Column_Author.DataPropertyName = "Author";
            this.Column_Author.HeaderText = "作者";
            this.Column_Author.Name = "Column_Author";
            this.Column_Author.ReadOnly = true;
            // 
            // Column_Date
            // 
            this.Column_Date.DataPropertyName = "UploadTime";
            this.Column_Date.HeaderText = "上传日期";
            this.Column_Date.Name = "Column_Date";
            this.Column_Date.ReadOnly = true;
            // 
            // Column_Download
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "下　　载";
            this.Column_Download.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column_Download.HeaderText = "下载";
            this.Column_Download.Name = "Column_Download";
            this.Column_Download.ReadOnly = true;
            this.Column_Download.Text = "下载";
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(311, 359);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 5;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // DisplayFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(741, 415);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DisplayFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件档案";
            this.Load += new System.EventHandler(this.DisplayFilesForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FileList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.DataGridView dataGridView_FileList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Date;
        private System.Windows.Forms.DataGridViewButtonColumn Column_Download;
    }
}