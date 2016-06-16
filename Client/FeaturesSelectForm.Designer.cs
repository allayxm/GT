namespace JXDL.Client
{
    partial class FeaturesSelectForm
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
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(35, 456);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(95, 47);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "选　择";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 430);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择的图形要素";
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(187, 456);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(95, 47);
            this.button_Exit.TabIndex = 2;
            this.button_Exit.Text = "选　择";
            this.button_Exit.UseVisualStyleBackColor = true;
            // 
            // FeaturesSelectForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 532);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FeaturesSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "已选择元素";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Exit;
    }
}