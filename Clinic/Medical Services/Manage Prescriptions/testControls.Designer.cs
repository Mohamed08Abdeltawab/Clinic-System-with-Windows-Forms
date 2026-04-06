namespace Clinic.Medical_Services.Manage_Prescriptions
{
    partial class testControls
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
            this.ctrlPrescriptionInfo1 = new Clinic.Medical_Services.Manage_Prescriptions.ctrlPrescriptionInfo();
            this.SuspendLayout();
            // 
            // ctrlPrescriptionInfo1
            // 
            this.ctrlPrescriptionInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPrescriptionInfo1.Location = new System.Drawing.Point(27, 34);
            this.ctrlPrescriptionInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlPrescriptionInfo1.Name = "ctrlPrescriptionInfo1";
            this.ctrlPrescriptionInfo1.Size = new System.Drawing.Size(855, 469);
            this.ctrlPrescriptionInfo1.TabIndex = 0;
            // 
            // testControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 529);
            this.Controls.Add(this.ctrlPrescriptionInfo1);
            this.Name = "testControls";
            this.Text = "testControls";
            this.Load += new System.EventHandler(this.testControls_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPrescriptionInfo ctrlPrescriptionInfo1;
    }
}