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
            this.ctrlPrescriptionInfoWithFilter1 = new Clinic.Medical_Services.Manage_Prescriptions.ctrlPrescriptionInfoWithFilter();
            this.SuspendLayout();
            // 
            // ctrlPrescriptionInfoWithFilter1
            // 
            this.ctrlPrescriptionInfoWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlPrescriptionInfoWithFilter1.FilterEnabled = true;
            this.ctrlPrescriptionInfoWithFilter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPrescriptionInfoWithFilter1.Location = new System.Drawing.Point(134, 49);
            this.ctrlPrescriptionInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlPrescriptionInfoWithFilter1.Name = "ctrlPrescriptionInfoWithFilter1";
            this.ctrlPrescriptionInfoWithFilter1.Size = new System.Drawing.Size(862, 582);
            this.ctrlPrescriptionInfoWithFilter1.TabIndex = 0;
            // 
            // testControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 701);
            this.Controls.Add(this.ctrlPrescriptionInfoWithFilter1);
            this.Name = "testControls";
            this.Text = "testControls";
            this.Load += new System.EventHandler(this.testControls_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPrescriptionInfoWithFilter ctrlPrescriptionInfoWithFilter1;
    }
}