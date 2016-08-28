﻿namespace JXDL.Client
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Data = new System.Windows.Forms.DataGridView();
            this.imageList_Tree = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).BeginInit();
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
            this.treeView_Layer.Size = new System.Drawing.Size(251, 540);
            this.treeView_Layer.TabIndex = 0;
            this.treeView_Layer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Layer_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView_Layer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 560);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_Data);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(257, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 560);
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
            this.dataGridView_Data.Size = new System.Drawing.Size(746, 540);
            this.dataGridView_Data.TabIndex = 0;
            // 
            // imageList_Tree
            // 
            this.imageList_Tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Tree.ImageStream")));
            this.imageList_Tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Tree.Images.SetKeyName(0, "point.png");
            this.imageList_Tree.Images.SetKeyName(1, "line.png");
            this.imageList_Tree.Images.SetKeyName(2, "area.png");
            // 
            // MapQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 560);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapQueryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图查询";
            this.Load += new System.EventHandler(this.MapQueryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_Layer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView_Data;
        private System.Windows.Forms.ImageList imageList_Tree;
    }
}