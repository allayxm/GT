namespace JXDL.Client
{
    partial class FileManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManageForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox_Query = new System.Windows.Forms.GroupBox();
            this.button_Print = new System.Windows.Forms.Button();
            this.button_Query = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Author = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Village = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_VillageCommittee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Township = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_FileList = new System.Windows.Forms.DataGridView();
            this.Column_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column_Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox_Query.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FileList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Query
            // 
            this.groupBox_Query.Controls.Add(this.button_Print);
            this.groupBox_Query.Controls.Add(this.button_Query);
            this.groupBox_Query.Controls.Add(this.textBox_File);
            this.groupBox_Query.Controls.Add(this.label4);
            this.groupBox_Query.Controls.Add(this.textBox_Author);
            this.groupBox_Query.Controls.Add(this.label5);
            this.groupBox_Query.Controls.Add(this.comboBox_Village);
            this.groupBox_Query.Controls.Add(this.label3);
            this.groupBox_Query.Controls.Add(this.comboBox_VillageCommittee);
            this.groupBox_Query.Controls.Add(this.label2);
            this.groupBox_Query.Controls.Add(this.comboBox_Township);
            this.groupBox_Query.Controls.Add(this.label1);
            this.groupBox_Query.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Query.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_Query.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Query.Name = "groupBox_Query";
            this.groupBox_Query.Size = new System.Drawing.Size(802, 121);
            this.groupBox_Query.TabIndex = 0;
            this.groupBox_Query.TabStop = false;
            this.groupBox_Query.Text = "查询条件";
            // 
            // button_Print
            // 
            this.button_Print.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Print.Image = ((System.Drawing.Image)(resources.GetObject("button_Print.Image")));
            this.button_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Print.Location = new System.Drawing.Point(647, 63);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(96, 43);
            this.button_Print.TabIndex = 15;
            this.button_Print.Text = "打  印";
            this.button_Print.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Print.UseVisualStyleBackColor = true;
            this.button_Print.Click += new System.EventHandler(this.button_Print_Click);
            // 
            // button_Query
            // 
            this.button_Query.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Query.Image = ((System.Drawing.Image)(resources.GetObject("button_Query.Image")));
            this.button_Query.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Query.Location = new System.Drawing.Point(516, 63);
            this.button_Query.Name = "button_Query";
            this.button_Query.Size = new System.Drawing.Size(96, 43);
            this.button_Query.TabIndex = 14;
            this.button_Query.Text = "查　询";
            this.button_Query.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Query.UseVisualStyleBackColor = true;
            this.button_Query.Click += new System.EventHandler(this.button_Query_Click);
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(339, 76);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(161, 24);
            this.textBox_File.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(258, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "文档名称:";
            // 
            // textBox_Author
            // 
            this.textBox_Author.Location = new System.Drawing.Point(94, 75);
            this.textBox_Author.Name = "textBox_Author";
            this.textBox_Author.Size = new System.Drawing.Size(161, 24);
            this.textBox_Author.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "文档作者:";
            // 
            // comboBox_Village
            // 
            this.comboBox_Village.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Village.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Village.FormattingEnabled = true;
            this.comboBox_Village.Location = new System.Drawing.Point(582, 27);
            this.comboBox_Village.Name = "comboBox_Village";
            this.comboBox_Village.Size = new System.Drawing.Size(161, 23);
            this.comboBox_Village.TabIndex = 7;
            this.comboBox_Village.SelectedIndexChanged += new System.EventHandler(this.comboBox_Village_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(515, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "自然村:";
            // 
            // comboBox_VillageCommittee
            // 
            this.comboBox_VillageCommittee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_VillageCommittee.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_VillageCommittee.FormattingEnabled = true;
            this.comboBox_VillageCommittee.Location = new System.Drawing.Point(339, 27);
            this.comboBox_VillageCommittee.Name = "comboBox_VillageCommittee";
            this.comboBox_VillageCommittee.Size = new System.Drawing.Size(161, 23);
            this.comboBox_VillageCommittee.TabIndex = 5;
            this.comboBox_VillageCommittee.SelectedIndexChanged += new System.EventHandler(this.comboBox_VillageCommittee_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(272, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "村委会:";
            // 
            // comboBox_Township
            // 
            this.comboBox_Township.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Township.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Township.FormattingEnabled = true;
            this.comboBox_Township.Location = new System.Drawing.Point(94, 27);
            this.comboBox_Township.Name = "comboBox_Township";
            this.comboBox_Township.Size = new System.Drawing.Size(161, 23);
            this.comboBox_Township.TabIndex = 2;
            this.comboBox_Township.SelectedIndexChanged += new System.EventHandler(this.comboBox_Township_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "乡镇街道:";
            // 
            // dataGridView_FileList
            // 
            this.dataGridView_FileList.AllowUserToAddRows = false;
            this.dataGridView_FileList.AllowUserToDeleteRows = false;
            this.dataGridView_FileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_FileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_ID,
            this.Column_FileName,
            this.Column_Unit,
            this.Column_Author,
            this.Column_Date,
            this.Column_Download,
            this.Column_Delete});
            this.dataGridView_FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_FileList.Location = new System.Drawing.Point(0, 121);
            this.dataGridView_FileList.MultiSelect = false;
            this.dataGridView_FileList.Name = "dataGridView_FileList";
            this.dataGridView_FileList.ReadOnly = true;
            this.dataGridView_FileList.RowTemplate.Height = 23;
            this.dataGridView_FileList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_FileList.ShowEditingIcon = false;
            this.dataGridView_FileList.ShowRowErrors = false;
            this.dataGridView_FileList.Size = new System.Drawing.Size(802, 309);
            this.dataGridView_FileList.TabIndex = 1;
            this.dataGridView_FileList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_FileList_CellClick);
            // 
            // Column_ID
            // 
            this.Column_ID.DataPropertyName = "XH";
            this.Column_ID.HeaderText = "序号";
            this.Column_ID.Name = "Column_ID";
            this.Column_ID.ReadOnly = true;
            this.Column_ID.Width = 40;
            // 
            // Column_FileName
            // 
            this.Column_FileName.DataPropertyName = "FileName";
            this.Column_FileName.HeaderText = "文件名";
            this.Column_FileName.Name = "Column_FileName";
            this.Column_FileName.ReadOnly = true;
            this.Column_FileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_FileName.Width = 190;
            // 
            // Column_Unit
            // 
            this.Column_Unit.DataPropertyName = "UnitName";
            this.Column_Unit.HeaderText = "所属单位";
            this.Column_Unit.Name = "Column_Unit";
            this.Column_Unit.ReadOnly = true;
            this.Column_Unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_Unit.Width = 160;
            // 
            // Column_Author
            // 
            this.Column_Author.DataPropertyName = "Author";
            this.Column_Author.HeaderText = "作者";
            this.Column_Author.Name = "Column_Author";
            this.Column_Author.ReadOnly = true;
            this.Column_Author.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Date
            // 
            this.Column_Date.DataPropertyName = "UploadTime";
            this.Column_Date.HeaderText = "上传日期";
            this.Column_Date.Name = "Column_Date";
            this.Column_Date.ReadOnly = true;
            this.Column_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.Column_Download.Width = 80;
            // 
            // Column_Delete
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "删    除";
            this.Column_Delete.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column_Delete.HeaderText = "删除";
            this.Column_Delete.Name = "Column_Delete";
            this.Column_Delete.ReadOnly = true;
            this.Column_Delete.Text = "删除";
            this.Column_Delete.Width = 80;
            // 
            // FileManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 430);
            this.Controls.Add(this.dataGridView_FileList);
            this.Controls.Add(this.groupBox_Query);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FileManageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件管理";
            this.Load += new System.EventHandler(this.FileManageForm_Load);
            this.groupBox_Query.ResumeLayout(false);
            this.groupBox_Query.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FileList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Township;
        private System.Windows.Forms.ComboBox comboBox_VillageCommittee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Village;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Author;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Query;
        private System.Windows.Forms.DataGridView dataGridView_FileList;
        private System.Windows.Forms.Button button_Print;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Date;
        private System.Windows.Forms.DataGridViewButtonColumn Column_Download;
        private System.Windows.Forms.DataGridViewButtonColumn Column_Delete;
    }
}