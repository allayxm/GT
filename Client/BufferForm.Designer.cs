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
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Analysis = new System.Windows.Forms.Button();
            this.contextMenuStrip_Right = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_FeatureLayers = new System.Windows.Forms.TreeView();
            this.imageList_Tree = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_Setup = new System.Windows.Forms.TabControl();
            this.tabPage_Parameter = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Layers = new System.Windows.Forms.ComboBox();
            this.button_Remove = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.listView_Layers = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_Distance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Type = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage_Buffer = new System.Windows.Forms.TabPage();
            this.dataGridView_Analyze = new System.Windows.Forms.DataGridView();
            this.listView_Buffer = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl_Setup.SuspendLayout();
            this.tabPage_Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Distance)).BeginInit();
            this.tabPage_Buffer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Analyze)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(539, 534);
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
            this.button_Analysis.Location = new System.Drawing.Point(418, 534);
            this.button_Analysis.Name = "button_Analysis";
            this.button_Analysis.Size = new System.Drawing.Size(96, 43);
            this.button_Analysis.TabIndex = 5;
            this.button_Analysis.Text = "分　析";
            this.button_Analysis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Analysis.UseVisualStyleBackColor = true;
            this.button_Analysis.Click += new System.EventHandler(this.button_Analysis_Click);
            // 
            // contextMenuStrip_Right
            // 
            this.contextMenuStrip_Right.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Delete});
            this.contextMenuStrip_Right.Name = "contextMenuStrip_Right";
            this.contextMenuStrip_Right.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem_Delete
            // 
            this.toolStripMenuItem_Delete.Name = "toolStripMenuItem_Delete";
            this.toolStripMenuItem_Delete.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem_Delete.Text = "删除";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_FeatureLayers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_Setup);
            this.splitContainer1.Size = new System.Drawing.Size(647, 514);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 10;
            // 
            // treeView_FeatureLayers
            // 
            this.treeView_FeatureLayers.CheckBoxes = true;
            this.treeView_FeatureLayers.ContextMenuStrip = this.contextMenuStrip_Right;
            this.treeView_FeatureLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FeatureLayers.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView_FeatureLayers.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_FeatureLayers.HideSelection = false;
            this.treeView_FeatureLayers.ImageIndex = 0;
            this.treeView_FeatureLayers.ImageList = this.imageList_Tree;
            this.treeView_FeatureLayers.Location = new System.Drawing.Point(0, 0);
            this.treeView_FeatureLayers.Name = "treeView_FeatureLayers";
            this.treeView_FeatureLayers.SelectedImageIndex = 0;
            this.treeView_FeatureLayers.Size = new System.Drawing.Size(215, 514);
            this.treeView_FeatureLayers.TabIndex = 0;
            this.treeView_FeatureLayers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_FeatureLayers_AfterCheck);
            this.treeView_FeatureLayers.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_FeatureLayers_DrawNode);
            this.treeView_FeatureLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_FeatureLayers_AfterSelect);
            // 
            // imageList_Tree
            // 
            this.imageList_Tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Tree.ImageStream")));
            this.imageList_Tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Tree.Images.SetKeyName(0, "point.png");
            this.imageList_Tree.Images.SetKeyName(1, "line.png");
            this.imageList_Tree.Images.SetKeyName(2, "area.png");
            // 
            // tabControl_Setup
            // 
            this.tabControl_Setup.Controls.Add(this.tabPage_Parameter);
            this.tabControl_Setup.Controls.Add(this.tabPage_Buffer);
            this.tabControl_Setup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Setup.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl_Setup.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Setup.Name = "tabControl_Setup";
            this.tabControl_Setup.SelectedIndex = 0;
            this.tabControl_Setup.Size = new System.Drawing.Size(428, 514);
            this.tabControl_Setup.TabIndex = 0;
            // 
            // tabPage_Parameter
            // 
            this.tabPage_Parameter.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Parameter.Controls.Add(this.label5);
            this.tabPage_Parameter.Controls.Add(this.comboBox_Layers);
            this.tabPage_Parameter.Controls.Add(this.button_Remove);
            this.tabPage_Parameter.Controls.Add(this.button_Add);
            this.tabPage_Parameter.Controls.Add(this.listView_Layers);
            this.tabPage_Parameter.Controls.Add(this.label4);
            this.tabPage_Parameter.Controls.Add(this.numericUpDown_Distance);
            this.tabPage_Parameter.Controls.Add(this.label1);
            this.tabPage_Parameter.Controls.Add(this.textBox_Type);
            this.tabPage_Parameter.Controls.Add(this.label2);
            this.tabPage_Parameter.Controls.Add(this.textBox_Name);
            this.tabPage_Parameter.Controls.Add(this.label3);
            this.tabPage_Parameter.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Parameter.Name = "tabPage_Parameter";
            this.tabPage_Parameter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Parameter.Size = new System.Drawing.Size(420, 485);
            this.tabPage_Parameter.TabIndex = 0;
            this.tabPage_Parameter.Text = "参数设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "米";
            // 
            // comboBox_Layers
            // 
            this.comboBox_Layers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Layers.FormattingEnabled = true;
            this.comboBox_Layers.Location = new System.Drawing.Point(102, 428);
            this.comboBox_Layers.Name = "comboBox_Layers";
            this.comboBox_Layers.Size = new System.Drawing.Size(126, 23);
            this.comboBox_Layers.TabIndex = 27;
            // 
            // button_Remove
            // 
            this.button_Remove.Location = new System.Drawing.Point(272, 424);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(32, 32);
            this.button_Remove.TabIndex = 26;
            this.button_Remove.Text = "-";
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // button_Add
            // 
            this.button_Add.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Add.Location = new System.Drawing.Point(234, 423);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(32, 32);
            this.button_Add.TabIndex = 25;
            this.button_Add.Text = "+";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // listView_Layers
            // 
            this.listView_Layers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_Layers.FullRowSelect = true;
            this.listView_Layers.Location = new System.Drawing.Point(101, 188);
            this.listView_Layers.MultiSelect = false;
            this.listView_Layers.Name = "listView_Layers";
            this.listView_Layers.Size = new System.Drawing.Size(198, 228);
            this.listView_Layers.TabIndex = 24;
            this.listView_Layers.UseCompatibleStateImageBehavior = false;
            this.listView_Layers.View = System.Windows.Forms.View.List;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "图层名称";
            this.columnHeader2.Width = 190;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "分析图层:";
            // 
            // numericUpDown_Distance
            // 
            this.numericUpDown_Distance.Location = new System.Drawing.Point(102, 149);
            this.numericUpDown_Distance.Name = "numericUpDown_Distance";
            this.numericUpDown_Distance.Size = new System.Drawing.Size(120, 24);
            this.numericUpDown_Distance.TabIndex = 22;
            this.numericUpDown_Distance.ValueChanged += new System.EventHandler(this.numericUpDown_Distance_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "缓冲距离:";
            // 
            // textBox_Type
            // 
            this.textBox_Type.Location = new System.Drawing.Point(100, 101);
            this.textBox_Type.Name = "textBox_Type";
            this.textBox_Type.ReadOnly = true;
            this.textBox_Type.Size = new System.Drawing.Size(204, 24);
            this.textBox_Type.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "图层类型:";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(100, 56);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.ReadOnly = true;
            this.textBox_Name.Size = new System.Drawing.Size(204, 24);
            this.textBox_Name.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "图层名称:";
            // 
            // tabPage_Buffer
            // 
            this.tabPage_Buffer.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Buffer.Controls.Add(this.dataGridView_Analyze);
            this.tabPage_Buffer.Controls.Add(this.listView_Buffer);
            this.tabPage_Buffer.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Buffer.Name = "tabPage_Buffer";
            this.tabPage_Buffer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Buffer.Size = new System.Drawing.Size(420, 485);
            this.tabPage_Buffer.TabIndex = 1;
            this.tabPage_Buffer.Text = "空间分析";
            // 
            // dataGridView_Analyze
            // 
            this.dataGridView_Analyze.AllowUserToAddRows = false;
            this.dataGridView_Analyze.AllowUserToDeleteRows = false;
            this.dataGridView_Analyze.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Analyze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Analyze.Location = new System.Drawing.Point(3, 109);
            this.dataGridView_Analyze.Name = "dataGridView_Analyze";
            this.dataGridView_Analyze.ReadOnly = true;
            this.dataGridView_Analyze.RowTemplate.Height = 23;
            this.dataGridView_Analyze.Size = new System.Drawing.Size(414, 373);
            this.dataGridView_Analyze.TabIndex = 26;
            // 
            // listView_Buffer
            // 
            this.listView_Buffer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_Buffer.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView_Buffer.FullRowSelect = true;
            this.listView_Buffer.Location = new System.Drawing.Point(3, 3);
            this.listView_Buffer.MultiSelect = false;
            this.listView_Buffer.Name = "listView_Buffer";
            this.listView_Buffer.Size = new System.Drawing.Size(414, 106);
            this.listView_Buffer.TabIndex = 25;
            this.listView_Buffer.UseCompatibleStateImageBehavior = false;
            this.listView_Buffer.View = System.Windows.Forms.View.List;
            this.listView_Buffer.SelectedIndexChanged += new System.EventHandler(this.listView_Buffer_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "图层名称";
            this.columnHeader1.Width = 400;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 596);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(647, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // BufferForm
            // 
            this.AcceptButton = this.button_Analysis;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(647, 618);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Analysis);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BufferForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "空间分析";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BufferForm_FormClosed);
            this.Load += new System.EventHandler(this.BufferForm_Load);
            this.contextMenuStrip_Right.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl_Setup.ResumeLayout(false);
            this.tabPage_Parameter.ResumeLayout(false);
            this.tabPage_Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Distance)).EndInit();
            this.tabPage_Buffer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Analyze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Analysis;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Right;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Delete;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_FeatureLayers;
        private System.Windows.Forms.TabControl tabControl_Setup;
        private System.Windows.Forms.TabPage tabPage_Parameter;
        private System.Windows.Forms.TabPage tabPage_Buffer;
        private System.Windows.Forms.ImageList imageList_Tree;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Distance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView_Layers;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox comboBox_Layers;
        private System.Windows.Forms.ListView listView_Buffer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.DataGridView dataGridView_Analyze;
        private System.Windows.Forms.Label label5;
    }
}