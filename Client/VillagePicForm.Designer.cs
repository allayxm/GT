namespace JXDL.Client
{
    partial class VillagePicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VillagePicForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_Village = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_VillageCommittee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Township = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_OutPic = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_Village);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox_VillageCommittee);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox_Township);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // comboBox_Village
            // 
            this.comboBox_Village.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Village.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Village.FormattingEnabled = true;
            this.comboBox_Village.Location = new System.Drawing.Point(122, 108);
            this.comboBox_Village.Name = "comboBox_Village";
            this.comboBox_Village.Size = new System.Drawing.Size(161, 23);
            this.comboBox_Village.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(55, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "自然村:";
            // 
            // comboBox_VillageCommittee
            // 
            this.comboBox_VillageCommittee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_VillageCommittee.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_VillageCommittee.FormattingEnabled = true;
            this.comboBox_VillageCommittee.Location = new System.Drawing.Point(122, 65);
            this.comboBox_VillageCommittee.Name = "comboBox_VillageCommittee";
            this.comboBox_VillageCommittee.Size = new System.Drawing.Size(161, 23);
            this.comboBox_VillageCommittee.TabIndex = 7;
            this.comboBox_VillageCommittee.SelectedIndexChanged += new System.EventHandler(this.comboBox_VillageCommittee_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(55, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "村委会:";
            // 
            // comboBox_Township
            // 
            this.comboBox_Township.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Township.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Township.FormattingEnabled = true;
            this.comboBox_Township.Location = new System.Drawing.Point(122, 20);
            this.comboBox_Township.Name = "comboBox_Township";
            this.comboBox_Township.Size = new System.Drawing.Size(161, 23);
            this.comboBox_Township.TabIndex = 4;
            this.comboBox_Township.SelectedIndexChanged += new System.EventHandler(this.comboBox_Township_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "乡镇街道:";
            // 
            // button_OutPic
            // 
            this.button_OutPic.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_OutPic.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OutPic.Image = ((System.Drawing.Image)(resources.GetObject("button_OutPic.Image")));
            this.button_OutPic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_OutPic.Location = new System.Drawing.Point(207, 166);
            this.button_OutPic.Name = "button_OutPic";
            this.button_OutPic.Size = new System.Drawing.Size(96, 43);
            this.button_OutPic.TabIndex = 8;
            this.button_OutPic.Text = "出　图";
            this.button_OutPic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_OutPic.UseVisualStyleBackColor = true;
            this.button_OutPic.Click += new System.EventHandler(this.button_OutPic_Click);
            // 
            // button_Search
            // 
            this.button_Search.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Search.Image = ((System.Drawing.Image)(resources.GetObject("button_Search.Image")));
            this.button_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Search.Location = new System.Drawing.Point(43, 166);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(96, 43);
            this.button_Search.TabIndex = 7;
            this.button_Search.Text = "定　位";
            this.button_Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // VillagePicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 221);
            this.Controls.Add(this.button_OutPic);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VillagePicForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "一村一图";
            this.Load += new System.EventHandler(this.VillagePicForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_OutPic;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.ComboBox comboBox_Township;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_VillageCommittee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Village;
        private System.Windows.Forms.Label label3;
    }
}