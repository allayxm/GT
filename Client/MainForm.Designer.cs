namespace JXDL.Client
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_System = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_System_Setup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_System_Logout = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_System_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc_Input = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc_Query = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Doc_FileQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Layer = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Browse = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Map = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Buffer = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Statistics = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_EagleEye = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_VillagePic = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pic_Raster = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.contextMenuStrip_Right = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_FeatureSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FileQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_MapQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SpaceAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.contextMenuStrip_Right.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_System,
            this.ToolStripMenuItem_Doc,
            this.ToolStripMenuItem_Pic,
            this.ToolStripMenuItem_About});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(678, 28);
            this.menuStrip_Main.TabIndex = 0;
            this.menuStrip_Main.Text = "menuStrip_Main";
            // 
            // ToolStripMenuItem_System
            // 
            this.ToolStripMenuItem_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_System_Setup,
            this.ToolStripMenuItem_System_Logout,
            this.ToolStripMenuItem_System_Exit});
            this.ToolStripMenuItem_System.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_System.Image")));
            this.ToolStripMenuItem_System.Name = "ToolStripMenuItem_System";
            this.ToolStripMenuItem_System.Size = new System.Drawing.Size(67, 24);
            this.ToolStripMenuItem_System.Text = "系统";
            // 
            // ToolStripMenuItem_System_Setup
            // 
            this.ToolStripMenuItem_System_Setup.Name = "ToolStripMenuItem_System_Setup";
            this.ToolStripMenuItem_System_Setup.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_System_Setup.Text = "系统设置";
            this.ToolStripMenuItem_System_Setup.Click += new System.EventHandler(this.ToolStripMenuItem_System_Setup_Click);
            // 
            // ToolStripMenuItem_System_Logout
            // 
            this.ToolStripMenuItem_System_Logout.Name = "ToolStripMenuItem_System_Logout";
            this.ToolStripMenuItem_System_Logout.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_System_Logout.Text = "注销";
            this.ToolStripMenuItem_System_Logout.Click += new System.EventHandler(this.ToolStripMenuItem_System_Logout_Click);
            // 
            // ToolStripMenuItem_System_Exit
            // 
            this.ToolStripMenuItem_System_Exit.Name = "ToolStripMenuItem_System_Exit";
            this.ToolStripMenuItem_System_Exit.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_System_Exit.Text = "退出";
            this.ToolStripMenuItem_System_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_System_Exit_Click);
            // 
            // ToolStripMenuItem_Doc
            // 
            this.ToolStripMenuItem_Doc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Doc_Input,
            this.ToolStripMenuItem_Doc_Edit,
            this.ToolStripMenuItem_Doc_Query,
            this.ToolStripMenuItem_Doc_Report,
            this.ToolStripMenuItem_Doc_FileQuery});
            this.ToolStripMenuItem_Doc.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_Doc.Image")));
            this.ToolStripMenuItem_Doc.Name = "ToolStripMenuItem_Doc";
            this.ToolStripMenuItem_Doc.Size = new System.Drawing.Size(97, 24);
            this.ToolStripMenuItem_Doc.Text = "文档资料";
            // 
            // ToolStripMenuItem_Doc_Input
            // 
            this.ToolStripMenuItem_Doc_Input.Name = "ToolStripMenuItem_Doc_Input";
            this.ToolStripMenuItem_Doc_Input.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_Doc_Input.Text = "信息输入";
            this.ToolStripMenuItem_Doc_Input.Click += new System.EventHandler(this.ToolStripMenuItem_Doc_Input_Click);
            // 
            // ToolStripMenuItem_Doc_Edit
            // 
            this.ToolStripMenuItem_Doc_Edit.Name = "ToolStripMenuItem_Doc_Edit";
            this.ToolStripMenuItem_Doc_Edit.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_Doc_Edit.Text = "管理编辑";
            this.ToolStripMenuItem_Doc_Edit.Click += new System.EventHandler(this.ToolStripMenuItem_Doc_Edit_Click);
            // 
            // ToolStripMenuItem_Doc_Query
            // 
            this.ToolStripMenuItem_Doc_Query.Name = "ToolStripMenuItem_Doc_Query";
            this.ToolStripMenuItem_Doc_Query.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_Doc_Query.Text = "信息查询";
            this.ToolStripMenuItem_Doc_Query.Click += new System.EventHandler(this.ToolStripMenuItem_Doc_Query_Click);
            // 
            // ToolStripMenuItem_Doc_Report
            // 
            this.ToolStripMenuItem_Doc_Report.Name = "ToolStripMenuItem_Doc_Report";
            this.ToolStripMenuItem_Doc_Report.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_Doc_Report.Text = "统计报表";
            this.ToolStripMenuItem_Doc_Report.Click += new System.EventHandler(this.ToolStripMenuItem_Doc_Report_Click);
            // 
            // ToolStripMenuItem_Doc_FileQuery
            // 
            this.ToolStripMenuItem_Doc_FileQuery.Name = "ToolStripMenuItem_Doc_FileQuery";
            this.ToolStripMenuItem_Doc_FileQuery.Size = new System.Drawing.Size(138, 24);
            this.ToolStripMenuItem_Doc_FileQuery.Text = "文件查询";
            this.ToolStripMenuItem_Doc_FileQuery.Click += new System.EventHandler(this.ToolStripMenuItem_Doc_FileQuery_Click);
            // 
            // ToolStripMenuItem_Pic
            // 
            this.ToolStripMenuItem_Pic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Pic_Layer,
            this.ToolStripMenuItem_Pic_Browse,
            this.ToolStripMenuItem_Pic_Map,
            this.ToolStripMenuItem_Pic_Buffer,
            this.ToolStripMenuItem_Pic_Statistics,
            this.ToolStripMenuItem_EagleEye,
            this.ToolStripMenuItem_VillagePic,
            this.ToolStripMenuItem_Pic_Import,
            this.ToolStripMenuItem_Pic_Raster});
            this.ToolStripMenuItem_Pic.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_Pic.Image")));
            this.ToolStripMenuItem_Pic.Name = "ToolStripMenuItem_Pic";
            this.ToolStripMenuItem_Pic.Size = new System.Drawing.Size(97, 24);
            this.ToolStripMenuItem_Pic.Text = "图形资料";
            // 
            // ToolStripMenuItem_Pic_Layer
            // 
            this.ToolStripMenuItem_Pic_Layer.Name = "ToolStripMenuItem_Pic_Layer";
            this.ToolStripMenuItem_Pic_Layer.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Layer.Text = "图层管理";
            this.ToolStripMenuItem_Pic_Layer.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Layer_Click);
            // 
            // ToolStripMenuItem_Pic_Browse
            // 
            this.ToolStripMenuItem_Pic_Browse.Name = "ToolStripMenuItem_Pic_Browse";
            this.ToolStripMenuItem_Pic_Browse.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Browse.Text = "要素编辑";
            this.ToolStripMenuItem_Pic_Browse.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Browse_Click);
            // 
            // ToolStripMenuItem_Pic_Map
            // 
            this.ToolStripMenuItem_Pic_Map.Name = "ToolStripMenuItem_Pic_Map";
            this.ToolStripMenuItem_Pic_Map.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Map.Text = "地图查询";
            this.ToolStripMenuItem_Pic_Map.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Map_Click);
            // 
            // ToolStripMenuItem_Pic_Buffer
            // 
            this.ToolStripMenuItem_Pic_Buffer.Name = "ToolStripMenuItem_Pic_Buffer";
            this.ToolStripMenuItem_Pic_Buffer.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Buffer.Text = "空间分析 ";
            this.ToolStripMenuItem_Pic_Buffer.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Anayle_Click);
            // 
            // ToolStripMenuItem_Pic_Statistics
            // 
            this.ToolStripMenuItem_Pic_Statistics.Name = "ToolStripMenuItem_Pic_Statistics";
            this.ToolStripMenuItem_Pic_Statistics.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Statistics.Text = "数据查询";
            this.ToolStripMenuItem_Pic_Statistics.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Statistics_Click);
            // 
            // ToolStripMenuItem_EagleEye
            // 
            this.ToolStripMenuItem_EagleEye.Name = "ToolStripMenuItem_EagleEye";
            this.ToolStripMenuItem_EagleEye.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_EagleEye.Text = "鹰眼地图";
            this.ToolStripMenuItem_EagleEye.Click += new System.EventHandler(this.ToolStripMenuItem_EagleEye_Click);
            // 
            // ToolStripMenuItem_VillagePic
            // 
            this.ToolStripMenuItem_VillagePic.Name = "ToolStripMenuItem_VillagePic";
            this.ToolStripMenuItem_VillagePic.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_VillagePic.Text = "一村一图";
            this.ToolStripMenuItem_VillagePic.Click += new System.EventHandler(this.ToolStripMenuItem_VillagePic_Click);
            // 
            // ToolStripMenuItem_Pic_Import
            // 
            this.ToolStripMenuItem_Pic_Import.Name = "ToolStripMenuItem_Pic_Import";
            this.ToolStripMenuItem_Pic_Import.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Import.Text = "导入地图";
            this.ToolStripMenuItem_Pic_Import.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Import_Click);
            // 
            // ToolStripMenuItem_Pic_Raster
            // 
            this.ToolStripMenuItem_Pic_Raster.Name = "ToolStripMenuItem_Pic_Raster";
            this.ToolStripMenuItem_Pic_Raster.Size = new System.Drawing.Size(152, 24);
            this.ToolStripMenuItem_Pic_Raster.Text = "导入栅格";
            this.ToolStripMenuItem_Pic_Raster.Click += new System.EventHandler(this.ToolStripMenuItem_Pic_Raster_Click);
            // 
            // ToolStripMenuItem_About
            // 
            this.ToolStripMenuItem_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.ToolStripMenuItem_About.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_About.Image")));
            this.ToolStripMenuItem_About.Name = "ToolStripMenuItem_About";
            this.ToolStripMenuItem_About.Size = new System.Drawing.Size(67, 24);
            this.ToolStripMenuItem_About.Text = "关于";
            this.ToolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(415, 328);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(678, 28);
            this.axToolbarControl1.TabIndex = 2;
            this.axToolbarControl1.OnItemClick += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnItemClickEventHandler(this.axToolbarControl1_OnItemClick);
            // 
            // axMapControl1
            // 
            this.axMapControl1.ContextMenuStrip = this.contextMenuStrip_Right;
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 56);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(678, 422);
            this.axMapControl1.TabIndex = 3;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnSelectionChanged += new System.EventHandler(this.AxMapControl1_OnSelectionChanged);
            this.axMapControl1.OnKeyDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyDownEventHandler(this.axMapControl1_OnKeyDown);
            // 
            // contextMenuStrip_Right
            // 
            this.contextMenuStrip_Right.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_FeatureSelect,
            this.toolStripMenuItem_FileQuery,
            this.toolStripMenuItem_MapQuery,
            this.toolStripMenuItem_SpaceAnalyze});
            this.contextMenuStrip_Right.Name = "contextMenuStrip_Right";
            this.contextMenuStrip_Right.Size = new System.Drawing.Size(125, 92);
            // 
            // toolStripMenuItem_FeatureSelect
            // 
            this.toolStripMenuItem_FeatureSelect.Checked = true;
            this.toolStripMenuItem_FeatureSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_FeatureSelect.Name = "toolStripMenuItem_FeatureSelect";
            this.toolStripMenuItem_FeatureSelect.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_FeatureSelect.Text = "要素选择";
            this.toolStripMenuItem_FeatureSelect.Click += new System.EventHandler(this.ToolStripMenuItem_FeatureSelect_Click);
            // 
            // toolStripMenuItem_FileQuery
            // 
            this.toolStripMenuItem_FileQuery.Name = "toolStripMenuItem_FileQuery";
            this.toolStripMenuItem_FileQuery.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_FileQuery.Text = "文件查询";
            this.toolStripMenuItem_FileQuery.Click += new System.EventHandler(this.toolStripMenuItem_FileQuery_Click);
            // 
            // toolStripMenuItem_MapQuery
            // 
            this.toolStripMenuItem_MapQuery.Name = "toolStripMenuItem_MapQuery";
            this.toolStripMenuItem_MapQuery.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_MapQuery.Text = "地图查询";
            this.toolStripMenuItem_MapQuery.Click += new System.EventHandler(this.toolStripMenuItem_MapQuery_Click);
            // 
            // toolStripMenuItem_SpaceAnalyze
            // 
            this.toolStripMenuItem_SpaceAnalyze.Name = "toolStripMenuItem_SpaceAnalyze";
            this.toolStripMenuItem_SpaceAnalyze.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_SpaceAnalyze.Text = "空间分析";
            this.toolStripMenuItem_SpaceAnalyze.Click += new System.EventHandler(this.toolStripMenuItem_SpaceAnalyze_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 478);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Main;
            this.Name = "MainForm";
            this.Text = "主窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.contextMenuStrip_Right.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_System;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_About;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_System_Setup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_System_Logout;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_System_Exit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc_Input;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc_Edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc_Query;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc_Report;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Layer;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Browse;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Map;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Buffer;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Statistics;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EagleEye;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Right;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FileQuery;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_MapQuery;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_VillagePic;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FeatureSelect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SpaceAnalyze;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Doc_FileQuery;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Import;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pic_Raster;
    }
}

