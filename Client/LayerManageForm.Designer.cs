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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerManageForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_Layer = new System.Windows.Forms.ListView();
            this.columnHeader_Visible = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView_Layer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 295);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层管理";
            // 
            // listView_Layer
            // 
            this.listView_Layer.CheckBoxes = true;
            this.listView_Layer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Visible,
            this.columnHeader_ID,
            this.columnHeader_Name,
            this.columnHeader_Type,
            this.columnHeader_Color});
            this.listView_Layer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Layer.FullRowSelect = true;
            this.listView_Layer.LabelEdit = true;
            this.listView_Layer.Location = new System.Drawing.Point(3, 17);
            this.listView_Layer.MultiSelect = false;
            this.listView_Layer.Name = "listView_Layer";
            this.listView_Layer.Size = new System.Drawing.Size(360, 275);
            this.listView_Layer.TabIndex = 0;
            this.listView_Layer.UseCompatibleStateImageBehavior = false;
            this.listView_Layer.View = System.Windows.Forms.View.Details;
            this.listView_Layer.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_Layer_ItemSelectionChanged);
            this.listView_Layer.DoubleClick += new System.EventHandler(this.listView_Layer_DoubleClick);
            // 
            // columnHeader_Visible
            // 
            this.columnHeader_Visible.Text = "可见";
            this.columnHeader_Visible.Width = 40;
            // 
            // columnHeader_ID
            // 
            this.columnHeader_ID.Text = "序号";
            this.columnHeader_ID.Width = 40;
            // 
            // columnHeader_Name
            // 
            this.columnHeader_Name.Text = "图层名称";
            this.columnHeader_Name.Width = 133;
            // 
            // columnHeader_Type
            // 
            this.columnHeader_Type.Text = "类型";
            // 
            // columnHeader_Color
            // 
            this.columnHeader_Color.Text = "颜色";
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(215, 312);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 6;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Save.Image = ((System.Drawing.Image)(resources.GetObject("button_Save.Image")));
            this.button_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Save.Location = new System.Drawing.Point(51, 312);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(96, 43);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "保　存";
            this.button_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // LayerManageForm
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(366, 380);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LayerManageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层管理";
            this.Load += new System.EventHandler(this.LayerManageForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView_Layer;
        private System.Windows.Forms.ColumnHeader columnHeader_Name;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.ColumnHeader columnHeader_ID;
        private System.Windows.Forms.ColumnHeader columnHeader_Type;
        private System.Windows.Forms.ColumnHeader columnHeader_Visible;
        private System.Windows.Forms.ColumnHeader columnHeader_Color;
    }
}