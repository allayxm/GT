namespace JXDL.Client
{
    partial class MapQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapQueryForm));
            this.treeView_Layer = new System.Windows.Forms.TreeView();
            this.imageList_Tree = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Data = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Location = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.textBox_KeyWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Query = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_Layer
            // 
            this.treeView_Layer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Layer.ImageIndex = 0;
            this.treeView_Layer.ImageList = this.imageList_Tree;
            this.treeView_Layer.Location = new System.Drawing.Point(3, 17);
            this.treeView_Layer.Name = "treeView_Layer";
            this.treeView_Layer.SelectedImageIndex = 0;
            this.treeView_Layer.Size = new System.Drawing.Size(251, 597);
            this.treeView_Layer.TabIndex = 0;
            this.treeView_Layer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Layer_AfterSelect);
            // 
            // imageList_Tree
            // 
            this.imageList_Tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Tree.ImageStream")));
            this.imageList_Tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Tree.Images.SetKeyName(0, "point.png");
            this.imageList_Tree.Images.SetKeyName(1, "line.png");
            this.imageList_Tree.Images.SetKeyName(2, "area.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView_Layer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 617);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_Data);
            this.groupBox2.Location = new System.Drawing.Point(257, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 561);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要素信息";
            // 
            // dataGridView_Data
            // 
            this.dataGridView_Data.AllowUserToAddRows = false;
            this.dataGridView_Data.AllowUserToDeleteRows = false;
            this.dataGridView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Data.Location = new System.Drawing.Point(3, 17);
            this.dataGridView_Data.Name = "dataGridView_Data";
            this.dataGridView_Data.RowTemplate.Height = 23;
            this.dataGridView_Data.Size = new System.Drawing.Size(746, 541);
            this.dataGridView_Data.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_Location);
            this.groupBox3.Controls.Add(this.button_Exit);
            this.groupBox3.Controls.Add(this.textBox_KeyWord);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button_Query);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(257, 555);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(752, 62);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // button_Location
            // 
            this.button_Location.Enabled = false;
            this.button_Location.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Location.Image = ((System.Drawing.Image)(resources.GetObject("button_Location.Image")));
            this.button_Location.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Location.Location = new System.Drawing.Point(540, 12);
            this.button_Location.Name = "button_Location";
            this.button_Location.Size = new System.Drawing.Size(96, 43);
            this.button_Location.TabIndex = 10;
            this.button_Location.Text = "定　位";
            this.button_Location.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Location.UseVisualStyleBackColor = true;
            this.button_Location.Click += new System.EventHandler(this.button_Location_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(644, 13);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 9;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // textBox_KeyWord
            // 
            this.textBox_KeyWord.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_KeyWord.Location = new System.Drawing.Point(69, 22);
            this.textBox_KeyWord.Name = "textBox_KeyWord";
            this.textBox_KeyWord.Size = new System.Drawing.Size(334, 24);
            this.textBox_KeyWord.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "关键字:";
            // 
            // button_Query
            // 
            this.button_Query.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Query.Image = ((System.Drawing.Image)(resources.GetObject("button_Query.Image")));
            this.button_Query.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Query.Location = new System.Drawing.Point(430, 13);
            this.button_Query.Name = "button_Query";
            this.button_Query.Size = new System.Drawing.Size(96, 43);
            this.button_Query.TabIndex = 6;
            this.button_Query.Text = "查　询";
            this.button_Query.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Query.UseVisualStyleBackColor = true;
            this.button_Query.Click += new System.EventHandler(this.button_Query_Click);
            // 
            // MapQueryForm
            // 
            this.AcceptButton = this.button_Query;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(1009, 617);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapQueryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图查询";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MapQueryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_Layer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView_Data;
        private System.Windows.Forms.ImageList imageList_Tree;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Query;
        private System.Windows.Forms.TextBox textBox_KeyWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Location;
    }
}