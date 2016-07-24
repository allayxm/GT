namespace JXDL.Client
{
    partial class StatisticsReportForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsReportForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Statistics = new System.Windows.Forms.Button();
            this.comboBox_VillageCommittee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Township = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart_Statistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button_Print = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Statistics)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Print);
            this.groupBox1.Controls.Add(this.button_Statistics);
            this.groupBox1.Controls.Add(this.comboBox_VillageCommittee);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox_Township);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(826, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计条件";
            // 
            // button_Statistics
            // 
            this.button_Statistics.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Statistics.Image = ((System.Drawing.Image)(resources.GetObject("button_Statistics.Image")));
            this.button_Statistics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Statistics.Location = new System.Drawing.Point(535, 15);
            this.button_Statistics.Name = "button_Statistics";
            this.button_Statistics.Size = new System.Drawing.Size(90, 46);
            this.button_Statistics.TabIndex = 14;
            this.button_Statistics.Text = "统　计";
            this.button_Statistics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Statistics.UseVisualStyleBackColor = true;
            this.button_Statistics.Click += new System.EventHandler(this.button_Statistics_Click);
            // 
            // comboBox_VillageCommittee
            // 
            this.comboBox_VillageCommittee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_VillageCommittee.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_VillageCommittee.FormattingEnabled = true;
            this.comboBox_VillageCommittee.Location = new System.Drawing.Point(323, 28);
            this.comboBox_VillageCommittee.Name = "comboBox_VillageCommittee";
            this.comboBox_VillageCommittee.Size = new System.Drawing.Size(161, 23);
            this.comboBox_VillageCommittee.TabIndex = 11;
            this.comboBox_VillageCommittee.SelectedIndexChanged += new System.EventHandler(this.comboBox_VillageCommittee_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(256, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "村委会:";
            // 
            // comboBox_Township
            // 
            this.comboBox_Township.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Township.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_Township.FormattingEnabled = true;
            this.comboBox_Township.Location = new System.Drawing.Point(88, 28);
            this.comboBox_Township.Name = "comboBox_Township";
            this.comboBox_Township.Size = new System.Drawing.Size(161, 23);
            this.comboBox_Township.TabIndex = 9;
            this.comboBox_Township.SelectedIndexChanged += new System.EventHandler(this.comboBox_Township_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "乡镇街道:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chart_Statistics);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(826, 383);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计图表";
            // 
            // chart_Statistics
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Angle = 90;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart_Statistics.ChartAreas.Add(chartArea1);
            this.chart_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart_Statistics.Legends.Add(legend1);
            this.chart_Statistics.Location = new System.Drawing.Point(3, 17);
            this.chart_Statistics.Name = "chart_Statistics";
            this.chart_Statistics.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("仿宋", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Label = "#VAL";
            series1.LabelForeColor = System.Drawing.Color.Red;
            series1.LabelToolTip = "#VAL";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart_Statistics.Series.Add(series1);
            this.chart_Statistics.Size = new System.Drawing.Size(820, 363);
            this.chart_Statistics.TabIndex = 0;
            this.chart_Statistics.Text = "chart1";
            title1.Font = new System.Drawing.Font("仿宋", 18F);
            title1.Name = "Title1";
            this.chart_Statistics.Titles.Add(title1);
            // 
            // button_Print
            // 
            this.button_Print.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Print.Image = ((System.Drawing.Image)(resources.GetObject("button_Print.Image")));
            this.button_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Print.Location = new System.Drawing.Point(675, 15);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(90, 46);
            this.button_Print.TabIndex = 15;
            this.button_Print.Text = "打　印";
            this.button_Print.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Print.UseVisualStyleBackColor = true;
            this.button_Print.Click += new System.EventHandler(this.button_Print_Click);
            // 
            // StatisticsReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatisticsReportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计报表";
            this.Load += new System.EventHandler(this.StatisticsReportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Statistics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_VillageCommittee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Township;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Statistics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Statistics;
        private System.Windows.Forms.Button button_Print;
    }
}