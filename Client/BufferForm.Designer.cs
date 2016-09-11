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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BufferForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Layers = new System.Windows.Forms.DataGridView();
            this.Column_CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Analysis = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView_BufferLayers = new System.Windows.Forms.ListView();
            this.columnHeader_Visible = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox_Info = new System.Windows.Forms.GroupBox();
            this.textBox_Info = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip_Right = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Layers)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox_Info.SuspendLayout();
            this.contextMenuStrip_Right.SuspendLayout();
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
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(301, 638);
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
            this.button_Analysis.Location = new System.Drawing.Point(33, 638);
            this.button_Analysis.Name = "button_Analysis";
            this.button_Analysis.Size = new System.Drawing.Size(96, 43);
            this.button_Analysis.TabIndex = 5;
            this.button_Analysis.Text = "分　析";
            this.button_Analysis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Analysis.UseVisualStyleBackColor = true;
            this.button_Analysis.Click += new System.EventHandler(this.button_Analysis_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listView_BufferLayers);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 256);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缓冲图层";
            // 
            // listView_BufferLayers
            // 
            this.listView_BufferLayers.CheckBoxes = true;
            this.listView_BufferLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Visible,
            this.columnHeader_Name});
            this.listView_BufferLayers.ContextMenuStrip = this.contextMenuStrip_Right;
            this.listView_BufferLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_BufferLayers.FullRowSelect = true;
            this.listView_BufferLayers.LabelEdit = true;
            this.listView_BufferLayers.Location = new System.Drawing.Point(3, 20);
            this.listView_BufferLayers.MultiSelect = false;
            this.listView_BufferLayers.Name = "listView_BufferLayers";
            this.listView_BufferLayers.Size = new System.Drawing.Size(434, 233);
            this.listView_BufferLayers.TabIndex = 1;
            this.listView_BufferLayers.UseCompatibleStateImageBehavior = false;
            this.listView_BufferLayers.View = System.Windows.Forms.View.Details;
            this.listView_BufferLayers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_BufferLayers_ItemChecked);
            // 
            // columnHeader_Visible
            // 
            this.columnHeader_Visible.Text = "可见";
            this.columnHeader_Visible.Width = 58;
            // 
            // columnHeader_Name
            // 
            this.columnHeader_Name.Text = "图层名称";
            this.columnHeader_Name.Width = 263;
            // 
            // groupBox_Info
            // 
            this.groupBox_Info.Controls.Add(this.textBox_Info);
            this.groupBox_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Info.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_Info.Location = new System.Drawing.Point(0, 512);
            this.groupBox_Info.Name = "groupBox_Info";
            this.groupBox_Info.Size = new System.Drawing.Size(440, 120);
            this.groupBox_Info.TabIndex = 8;
            this.groupBox_Info.TabStop = false;
            this.groupBox_Info.Text = "信息";
            // 
            // textBox_Info
            // 
            this.textBox_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Info.Location = new System.Drawing.Point(3, 20);
            this.textBox_Info.Multiline = true;
            this.textBox_Info.Name = "textBox_Info";
            this.textBox_Info.Size = new System.Drawing.Size(434, 97);
            this.textBox_Info.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(178, 640);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip_Right
            // 
            this.contextMenuStrip_Right.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Delete});
            this.contextMenuStrip_Right.Name = "contextMenuStrip_Right";
            this.contextMenuStrip_Right.Size = new System.Drawing.Size(153, 48);
            // 
            // toolStripMenuItem_Delete
            // 
            this.toolStripMenuItem_Delete.Name = "toolStripMenuItem_Delete";
            this.toolStripMenuItem_Delete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_Delete.Text = "删除";
            // 
            // BufferForm
            // 
            this.AcceptButton = this.button_Analysis;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(440, 686);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox_Info);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Analysis);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BufferForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缓冲区分析";
            this.Load += new System.EventHandler(this.BufferForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Layers)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox_Info.ResumeLayout(false);
            this.groupBox_Info.PerformLayout();
            this.contextMenuStrip_Right.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox_Info;
        private System.Windows.Forms.TextBox textBox_Info;
        private System.Windows.Forms.ListView listView_BufferLayers;
        private System.Windows.Forms.ColumnHeader columnHeader_Visible;
        private System.Windows.Forms.ColumnHeader columnHeader_Name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Right;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Delete;
    }
}