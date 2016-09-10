namespace JXDL.Client
{
    partial class BufferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BufferForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Layers = new System.Windows.Forms.DataGridView();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Analysis = new System.Windows.Forms.Button();
            this.Column_CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Layers)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_Layers);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 256);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // dataGridView_Layers
            // 
            this.dataGridView_Layers.AllowUserToAddRows = false;
            this.dataGridView_Layers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Layers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_CheckBox,
            this.Column_Name,
            this.Column_Alias,
            this.Column_Type,
            this.Column_Distance});
            this.dataGridView_Layers.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView_Layers.Location = new System.Drawing.Point(3, 20);
            this.dataGridView_Layers.Name = "dataGridView_Layers";
            this.dataGridView_Layers.RowTemplate.Height = 23;
            this.dataGridView_Layers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Layers.Size = new System.Drawing.Size(434, 230);
            this.dataGridView_Layers.TabIndex = 7;
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(240, 262);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 6;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_Analysis
            // 
            this.button_Analysis.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Analysis.Image = ((System.Drawing.Image)(resources.GetObject("button_Analysis.Image")));
            this.button_Analysis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Analysis.Location = new System.Drawing.Point(99, 262);
            this.button_Analysis.Name = "button_Analysis";
            this.button_Analysis.Size = new System.Drawing.Size(96, 43);
            this.button_Analysis.TabIndex = 5;
            this.button_Analysis.Text = "分　析";
            this.button_Analysis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Analysis.UseVisualStyleBackColor = true;
            this.button_Analysis.Click += new System.EventHandler(this.button_Analysis_Click);
            // 
            // Column_CheckBox
            // 
            this.Column_CheckBox.DataPropertyName = "选择";
            this.Column_CheckBox.HeaderText = "选择";
            this.Column_CheckBox.Name = "Column_CheckBox";
            this.Column_CheckBox.Width = 60;
            // 
            // Column_Name
            // 
            this.Column_Name.DataPropertyName = "图层名称";
            this.Column_Name.HeaderText = "图层名称";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.Visible = false;
            // 
            // Column_Alias
            // 
            this.Column_Alias.DataPropertyName = "图层别名";
            this.Column_Alias.HeaderText = "图层名称";
            this.Column_Alias.Name = "Column_Alias";
            this.Column_Alias.ReadOnly = true;
            // 
            // Column_Type
            // 
            this.Column_Type.DataPropertyName = "图层类型";
            this.Column_Type.HeaderText = "类型";
            this.Column_Type.Name = "Column_Type";
            this.Column_Type.ReadOnly = true;
            // 
            // Column_Distance
            // 
            this.Column_Distance.DataPropertyName = "缓冲距离";
            this.Column_Distance.HeaderText = "缓冲距离/米";
            this.Column_Distance.Name = "Column_Distance";
            this.Column_Distance.Width = 120;
            // 
            // BufferForm
            // 
            this.AcceptButton = this.button_Analysis;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(440, 317);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Analysis);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BufferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缓冲区分析";
            this.Load += new System.EventHandler(this.BufferForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Layers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Analysis;
        private System.Windows.Forms.DataGridView dataGridView_Layers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Alias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Distance;
    }
}