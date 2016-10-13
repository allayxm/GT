namespace JXDL.Client
{
    partial class LayerManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerManageForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_Layers = new System.Windows.Forms.TreeView();
            this.imageList_Tree = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_Layer = new System.Windows.Forms.TabControl();
            this.tabPage_LayerProperty = new System.Windows.Forms.TabPage();
            this.button_MapColor = new System.Windows.Forms.Button();
            this.label_Color = new System.Windows.Forms.Label();
            this.textBox_Type = new System.Windows.Forms.TextBox();
            this.textBox_Expository = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_Annotation = new System.Windows.Forms.TabPage();
            this.button_Add = new System.Windows.Forms.Button();
            this.textBox_Express = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_Annotation = new System.Windows.Forms.CheckBox();
            this.comboBox_FontSize = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label_AnnotationColor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_Label = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage_Data = new System.Windows.Forms.TabPage();
            this.dataGridView_Data = new System.Windows.Forms.DataGridView();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Apply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl_Layer.SuspendLayout();
            this.tabPage_LayerProperty.SuspendLayout();
            this.tabPage_Annotation.SuspendLayout();
            this.tabPage_Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 357);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层管理";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_Layers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_Layer);
            this.splitContainer1.Size = new System.Drawing.Size(541, 334);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView_Layers
            // 
            this.treeView_Layers.AllowDrop = true;
            this.treeView_Layers.CheckBoxes = true;
            this.treeView_Layers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Layers.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView_Layers.HideSelection = false;
            this.treeView_Layers.ImageIndex = 0;
            this.treeView_Layers.ImageList = this.imageList_Tree;
            this.treeView_Layers.Location = new System.Drawing.Point(0, 0);
            this.treeView_Layers.Name = "treeView_Layers";
            this.treeView_Layers.SelectedImageIndex = 0;
            this.treeView_Layers.Size = new System.Drawing.Size(208, 334);
            this.treeView_Layers.TabIndex = 2;
            this.treeView_Layers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Layers_AfterCheck);
            this.treeView_Layers.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_Layers_DrawNode);
            this.treeView_Layers.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_Layers_ItemDrag);
            this.treeView_Layers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Layers_AfterSelect);
            this.treeView_Layers.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_Layers_DragDrop);
            this.treeView_Layers.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_Layers_DragEnter);
            // 
            // imageList_Tree
            // 
            this.imageList_Tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Tree.ImageStream")));
            this.imageList_Tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Tree.Images.SetKeyName(0, "point.png");
            this.imageList_Tree.Images.SetKeyName(1, "line.png");
            this.imageList_Tree.Images.SetKeyName(2, "area.png");
            // 
            // tabControl_Layer
            // 
            this.tabControl_Layer.Controls.Add(this.tabPage_LayerProperty);
            this.tabControl_Layer.Controls.Add(this.tabPage_Annotation);
            this.tabControl_Layer.Controls.Add(this.tabPage_Data);
            this.tabControl_Layer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Layer.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Layer.Name = "tabControl_Layer";
            this.tabControl_Layer.SelectedIndex = 0;
            this.tabControl_Layer.Size = new System.Drawing.Size(329, 334);
            this.tabControl_Layer.TabIndex = 12;
            // 
            // tabPage_LayerProperty
            // 
            this.tabPage_LayerProperty.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_LayerProperty.Controls.Add(this.button_MapColor);
            this.tabPage_LayerProperty.Controls.Add(this.label_Color);
            this.tabPage_LayerProperty.Controls.Add(this.textBox_Type);
            this.tabPage_LayerProperty.Controls.Add(this.textBox_Expository);
            this.tabPage_LayerProperty.Controls.Add(this.textBox_Name);
            this.tabPage_LayerProperty.Controls.Add(this.label4);
            this.tabPage_LayerProperty.Controls.Add(this.label3);
            this.tabPage_LayerProperty.Controls.Add(this.label2);
            this.tabPage_LayerProperty.Controls.Add(this.label1);
            this.tabPage_LayerProperty.Location = new System.Drawing.Point(4, 25);
            this.tabPage_LayerProperty.Name = "tabPage_LayerProperty";
            this.tabPage_LayerProperty.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_LayerProperty.Size = new System.Drawing.Size(321, 305);
            this.tabPage_LayerProperty.TabIndex = 0;
            this.tabPage_LayerProperty.Text = "属性";
            // 
            // button_MapColor
            // 
            this.button_MapColor.Image = ((System.Drawing.Image)(resources.GetObject("button_MapColor.Image")));
            this.button_MapColor.Location = new System.Drawing.Point(200, 170);
            this.button_MapColor.Name = "button_MapColor";
            this.button_MapColor.Size = new System.Drawing.Size(26, 26);
            this.button_MapColor.TabIndex = 20;
            this.button_MapColor.UseVisualStyleBackColor = true;
            this.button_MapColor.Click += new System.EventHandler(this.label_Color_DoubleClick);
            // 
            // label_Color
            // 
            this.label_Color.AutoSize = true;
            this.label_Color.BackColor = System.Drawing.Color.White;
            this.label_Color.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label_Color.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Color.Location = new System.Drawing.Point(96, 174);
            this.label_Color.Name = "label_Color";
            this.label_Color.Size = new System.Drawing.Size(98, 18);
            this.label_Color.TabIndex = 19;
            this.label_Color.Text = "          ";
            this.label_Color.DoubleClick += new System.EventHandler(this.label_Color_DoubleClick);
            // 
            // textBox_Type
            // 
            this.textBox_Type.Location = new System.Drawing.Point(94, 134);
            this.textBox_Type.Name = "textBox_Type";
            this.textBox_Type.ReadOnly = true;
            this.textBox_Type.Size = new System.Drawing.Size(204, 24);
            this.textBox_Type.TabIndex = 18;
            // 
            // textBox_Expository
            // 
            this.textBox_Expository.Location = new System.Drawing.Point(94, 97);
            this.textBox_Expository.Name = "textBox_Expository";
            this.textBox_Expository.ReadOnly = true;
            this.textBox_Expository.Size = new System.Drawing.Size(204, 24);
            this.textBox_Expository.TabIndex = 17;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(94, 57);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.ReadOnly = true;
            this.textBox_Name.Size = new System.Drawing.Size(204, 24);
            this.textBox_Name.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "图层颜色:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "图层表名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "图层类型:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "图层名称:";
            // 
            // tabPage_Annotation
            // 
            this.tabPage_Annotation.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Annotation.Controls.Add(this.button_Add);
            this.tabPage_Annotation.Controls.Add(this.textBox_Express);
            this.tabPage_Annotation.Controls.Add(this.label7);
            this.tabPage_Annotation.Controls.Add(this.button1);
            this.tabPage_Annotation.Controls.Add(this.checkBox_Annotation);
            this.tabPage_Annotation.Controls.Add(this.comboBox_FontSize);
            this.tabPage_Annotation.Controls.Add(this.label8);
            this.tabPage_Annotation.Controls.Add(this.label_AnnotationColor);
            this.tabPage_Annotation.Controls.Add(this.label6);
            this.tabPage_Annotation.Controls.Add(this.comboBox_Label);
            this.tabPage_Annotation.Controls.Add(this.label5);
            this.tabPage_Annotation.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Annotation.Name = "tabPage_Annotation";
            this.tabPage_Annotation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Annotation.Size = new System.Drawing.Size(321, 305);
            this.tabPage_Annotation.TabIndex = 1;
            this.tabPage_Annotation.Text = "标注";
            // 
            // button_Add
            // 
            this.button_Add.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Add.Location = new System.Drawing.Point(253, 56);
            this.button_Add.Margin = new System.Windows.Forms.Padding(0);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(26, 26);
            this.button_Add.TabIndex = 27;
            this.button_Add.Text = "+";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // textBox_Express
            // 
            this.textBox_Express.Location = new System.Drawing.Point(94, 182);
            this.textBox_Express.Multiline = true;
            this.textBox_Express.Name = "textBox_Express";
            this.textBox_Express.Size = new System.Drawing.Size(191, 117);
            this.textBox_Express.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "表达式:";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(200, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 24;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.label_AnnotationColor_DoubleClick);
            // 
            // checkBox_Annotation
            // 
            this.checkBox_Annotation.AutoSize = true;
            this.checkBox_Annotation.Location = new System.Drawing.Point(20, 24);
            this.checkBox_Annotation.Name = "checkBox_Annotation";
            this.checkBox_Annotation.Size = new System.Drawing.Size(191, 19);
            this.checkBox_Annotation.TabIndex = 23;
            this.checkBox_Annotation.Text = "标注些图层中的要素字段";
            this.checkBox_Annotation.UseVisualStyleBackColor = true;
            this.checkBox_Annotation.CheckedChanged += new System.EventHandler(this.checkBox_Annotation_CheckedChanged);
            // 
            // comboBox_FontSize
            // 
            this.comboBox_FontSize.FormattingEnabled = true;
            this.comboBox_FontSize.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.comboBox_FontSize.Location = new System.Drawing.Point(94, 138);
            this.comboBox_FontSize.Name = "comboBox_FontSize";
            this.comboBox_FontSize.Size = new System.Drawing.Size(121, 23);
            this.comboBox_FontSize.TabIndex = 22;
            this.comboBox_FontSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_FontSize_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "字体大小:";
            // 
            // label_AnnotationColor
            // 
            this.label_AnnotationColor.AutoSize = true;
            this.label_AnnotationColor.BackColor = System.Drawing.Color.White;
            this.label_AnnotationColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label_AnnotationColor.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_AnnotationColor.Location = new System.Drawing.Point(96, 98);
            this.label_AnnotationColor.Name = "label_AnnotationColor";
            this.label_AnnotationColor.Size = new System.Drawing.Size(98, 18);
            this.label_AnnotationColor.TabIndex = 20;
            this.label_AnnotationColor.Text = "          ";
            this.label_AnnotationColor.DoubleClick += new System.EventHandler(this.label_AnnotationColor_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "标签颜色:";
            // 
            // comboBox_Label
            // 
            this.comboBox_Label.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Label.FormattingEnabled = true;
            this.comboBox_Label.Location = new System.Drawing.Point(94, 58);
            this.comboBox_Label.Name = "comboBox_Label";
            this.comboBox_Label.Size = new System.Drawing.Size(152, 23);
            this.comboBox_Label.TabIndex = 12;
            this.comboBox_Label.SelectedIndexChanged += new System.EventHandler(this.comboBox_Label_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "标注字段:";
            // 
            // tabPage_Data
            // 
            this.tabPage_Data.Controls.Add(this.dataGridView_Data);
            this.tabPage_Data.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Data.Name = "tabPage_Data";
            this.tabPage_Data.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Data.Size = new System.Drawing.Size(321, 305);
            this.tabPage_Data.TabIndex = 2;
            this.tabPage_Data.Text = "数据";
            this.tabPage_Data.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Data
            // 
            this.dataGridView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Data.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Data.Name = "dataGridView_Data";
            this.dataGridView_Data.RowTemplate.Height = 23;
            this.dataGridView_Data.Size = new System.Drawing.Size(315, 299);
            this.dataGridView_Data.TabIndex = 0;
            // 
            // button_Exit
            // 
            this.button_Exit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(329, 363);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 6;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_Save
            // 
            this.button_Save.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Save.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Save.Image = ((System.Drawing.Image)(resources.GetObject("button_Save.Image")));
            this.button_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Save.Location = new System.Drawing.Point(212, 363);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(96, 43);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "保　存";
            this.button_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Apply.Enabled = false;
            this.button_Apply.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Apply.Image = ((System.Drawing.Image)(resources.GetObject("button_Apply.Image")));
            this.button_Apply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Apply.Location = new System.Drawing.Point(439, 363);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(96, 43);
            this.button_Apply.TabIndex = 12;
            this.button_Apply.Text = "应　用";
            this.button_Apply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // LayerManageForm
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(547, 413);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "LayerManageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层管理";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LayerManageForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl_Layer.ResumeLayout(false);
            this.tabPage_LayerProperty.ResumeLayout(false);
            this.tabPage_LayerProperty.PerformLayout();
            this.tabPage_Annotation.ResumeLayout(false);
            this.tabPage_Annotation.PerformLayout();
            this.tabPage_Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_Layers;
        private System.Windows.Forms.ImageList imageList_Tree;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.TabControl tabControl_Layer;
        private System.Windows.Forms.TabPage tabPage_LayerProperty;
        private System.Windows.Forms.Label label_Color;
        private System.Windows.Forms.TextBox textBox_Type;
        private System.Windows.Forms.TextBox textBox_Expository;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage_Annotation;
        private System.Windows.Forms.ComboBox comboBox_Label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_AnnotationColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_FontSize;
        private System.Windows.Forms.CheckBox checkBox_Annotation;
        private System.Windows.Forms.Button button_MapColor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage_Data;
        private System.Windows.Forms.DataGridView dataGridView_Data;
        private System.Windows.Forms.TextBox textBox_Express;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_Add;
    }
}