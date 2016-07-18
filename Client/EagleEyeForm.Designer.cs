namespace JXDL.Client
{
    partial class EagleEyeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EagleEyeForm));
            this.axMapControl_EagleEye = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl_EagleEye)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl_EagleEye
            // 
            this.axMapControl_EagleEye.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl_EagleEye.Location = new System.Drawing.Point(0, 0);
            this.axMapControl_EagleEye.Name = "axMapControl_EagleEye";
            this.axMapControl_EagleEye.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl_EagleEye.OcxState")));
            this.axMapControl_EagleEye.Size = new System.Drawing.Size(299, 245);
            this.axMapControl_EagleEye.TabIndex = 0;
            this.axMapControl_EagleEye.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl_EagleEye_OnMouseDown);
            // 
            // EagleEyeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 245);
            this.Controls.Add(this.axMapControl_EagleEye);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EagleEyeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "鹰眼";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EagleEyeForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EagleEyeForm_FormClosed);
            this.Load += new System.EventHandler(this.EagleEyeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl_EagleEye)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl_EagleEye;
    }
}