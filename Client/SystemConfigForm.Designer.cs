namespace JXDL.Client
{
    partial class SystemConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemConfigForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_MapColor = new System.Windows.Forms.Label();
            this.button_MapColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_DownloadPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_TownshipColor = new System.Windows.Forms.Label();
            this.button_TownshipColor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label_VillageCommitteeColor = new System.Windows.Forms.Label();
            this.button_VillageCommitteeColor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label_VillageColor = new System.Windows.Forms.Label();
            this.button_VillageColor = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_VillageColor);
            this.groupBox1.Controls.Add(this.label_VillageColor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button_VillageCommitteeColor);
            this.groupBox1.Controls.Add(this.label_VillageCommitteeColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button_TownshipColor);
            this.groupBox1.Controls.Add(this.label_TownshipColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label_MapColor);
            this.groupBox1.Controls.Add(this.button_MapColor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "地图设置";
            // 
            // label_MapColor
            // 
            this.label_MapColor.AutoSize = true;
            this.label_MapColor.BackColor = System.Drawing.Color.White;
            this.label_MapColor.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MapColor.Location = new System.Drawing.Point(154, 28);
            this.label_MapColor.Name = "label_MapColor";
            this.label_MapColor.Size = new System.Drawing.Size(95, 15);
            this.label_MapColor.TabIndex = 7;
            this.label_MapColor.Text = "           ";
            // 
            // button_MapColor
            // 
            this.button_MapColor.Image = ((System.Drawing.Image)(resources.GetObject("button_MapColor.Image")));
            this.button_MapColor.Location = new System.Drawing.Point(253, 22);
            this.button_MapColor.Name = "button_MapColor";
            this.button_MapColor.Size = new System.Drawing.Size(26, 26);
            this.button_MapColor.TabIndex = 6;
            this.button_MapColor.UseVisualStyleBackColor = true;
            this.button_MapColor.Click += new System.EventHandler(this.button_SelectColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(52, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "地图背景颜色:";
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Save.Image = ((System.Drawing.Image)(resources.GetObject("button_Save.Image")));
            this.button_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Save.Location = new System.Drawing.Point(28, 261);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(96, 43);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "保　存";
            this.button_Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Exit.Image = ((System.Drawing.Image)(resources.GetObject("button_Exit.Image")));
            this.button_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Exit.Location = new System.Drawing.Point(192, 261);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(96, 43);
            this.button_Exit.TabIndex = 4;
            this.button_Exit.Text = "退　出";
            this.button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Browse);
            this.groupBox2.Controls.Add(this.textBox_DownloadPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置";
            // 
            // button_Browse
            // 
            this.button_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Browse.Image = ((System.Drawing.Image)(resources.GetObject("button_Browse.Image")));
            this.button_Browse.Location = new System.Drawing.Point(286, 44);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(35, 30);
            this.button_Browse.TabIndex = 8;
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_DownloadPath
            // 
            this.textBox_DownloadPath.Location = new System.Drawing.Point(33, 50);
            this.textBox_DownloadPath.Name = "textBox_DownloadPath";
            this.textBox_DownloadPath.ReadOnly = true;
            this.textBox_DownloadPath.Size = new System.Drawing.Size(250, 21);
            this.textBox_DownloadPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "文档下载保存路径:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(52, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "街道要素颜色:";
            // 
            // label_TownshipColor
            // 
            this.label_TownshipColor.AutoSize = true;
            this.label_TownshipColor.BackColor = System.Drawing.Color.White;
            this.label_TownshipColor.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_TownshipColor.Location = new System.Drawing.Point(154, 61);
            this.label_TownshipColor.Name = "label_TownshipColor";
            this.label_TownshipColor.Size = new System.Drawing.Size(95, 15);
            this.label_TownshipColor.TabIndex = 9;
            this.label_TownshipColor.Text = "           ";
            // 
            // button_TownshipColor
            // 
            this.button_TownshipColor.Image = ((System.Drawing.Image)(resources.GetObject("button_TownshipColor.Image")));
            this.button_TownshipColor.Location = new System.Drawing.Point(253, 54);
            this.button_TownshipColor.Name = "button_TownshipColor";
            this.button_TownshipColor.Size = new System.Drawing.Size(26, 26);
            this.button_TownshipColor.TabIndex = 10;
            this.button_TownshipColor.UseVisualStyleBackColor = true;
            this.button_TownshipColor.Click += new System.EventHandler(this.button_TownshipColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(37, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "村委会要素颜色:";
            // 
            // label_VillageCommitteeColor
            // 
            this.label_VillageCommitteeColor.AutoSize = true;
            this.label_VillageCommitteeColor.BackColor = System.Drawing.Color.White;
            this.label_VillageCommitteeColor.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_VillageCommitteeColor.Location = new System.Drawing.Point(154, 90);
            this.label_VillageCommitteeColor.Name = "label_VillageCommitteeColor";
            this.label_VillageCommitteeColor.Size = new System.Drawing.Size(95, 15);
            this.label_VillageCommitteeColor.TabIndex = 12;
            this.label_VillageCommitteeColor.Text = "           ";
            // 
            // button_VillageCommitteeColor
            // 
            this.button_VillageCommitteeColor.Image = ((System.Drawing.Image)(resources.GetObject("button_VillageCommitteeColor.Image")));
            this.button_VillageCommitteeColor.Location = new System.Drawing.Point(253, 86);
            this.button_VillageCommitteeColor.Name = "button_VillageCommitteeColor";
            this.button_VillageCommitteeColor.Size = new System.Drawing.Size(26, 26);
            this.button_VillageCommitteeColor.TabIndex = 13;
            this.button_VillageCommitteeColor.UseVisualStyleBackColor = true;
            this.button_VillageCommitteeColor.Click += new System.EventHandler(this.button_VillageCommitteeColor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(37, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "自然村要素颜色:";
            // 
            // label_VillageColor
            // 
            this.label_VillageColor.AutoSize = true;
            this.label_VillageColor.BackColor = System.Drawing.Color.White;
            this.label_VillageColor.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_VillageColor.Location = new System.Drawing.Point(154, 122);
            this.label_VillageColor.Name = "label_VillageColor";
            this.label_VillageColor.Size = new System.Drawing.Size(95, 15);
            this.label_VillageColor.TabIndex = 15;
            this.label_VillageColor.Text = "           ";
            // 
            // button_VillageColor
            // 
            this.button_VillageColor.Image = ((System.Drawing.Image)(resources.GetObject("button_VillageColor.Image")));
            this.button_VillageColor.Location = new System.Drawing.Point(253, 118);
            this.button_VillageColor.Name = "button_VillageColor";
            this.button_VillageColor.Size = new System.Drawing.Size(26, 26);
            this.button_VillageColor.TabIndex = 16;
            this.button_VillageColor.UseVisualStyleBackColor = true;
            this.button_VillageColor.Click += new System.EventHandler(this.button_VillageColor_Click);
            // 
            // SystemConfigForm
            // 
            this.AcceptButton = this.button_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(327, 317);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SystemConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.SysteConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_MapColor;
        private System.Windows.Forms.Label label_MapColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_DownloadPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_TownshipColor;
        private System.Windows.Forms.Label label_TownshipColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_VillageColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_VillageCommitteeColor;
        private System.Windows.Forms.Label label_VillageCommitteeColor;
        private System.Windows.Forms.Button button_VillageColor;
    }
}