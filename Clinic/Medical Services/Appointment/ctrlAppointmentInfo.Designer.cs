namespace Clinic.Appointment
{
    partial class ctrlAppointmentInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlPatientInfo1 = new Clinic.Patient.ctrlPatientInfo();
            this.ctrlDoctorInfo1 = new Clinic.Doctor.ctrlDoctorInfo();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlPatientInfo1
            // 
            this.ctrlPatientInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPatientInfo1.Location = new System.Drawing.Point(4, 45);
            this.ctrlPatientInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlPatientInfo1.Name = "ctrlPatientInfo1";
            this.ctrlPatientInfo1.Size = new System.Drawing.Size(842, 453);
            this.ctrlPatientInfo1.TabIndex = 0;
            // 
            // ctrlDoctorInfo1
            // 
            this.ctrlDoctorInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDoctorInfo1.Location = new System.Drawing.Point(4, 548);
            this.ctrlDoctorInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlDoctorInfo1.Name = "ctrlDoctorInfo1";
            this.ctrlDoctorInfo1.Size = new System.Drawing.Size(842, 453);
            this.ctrlDoctorInfo1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patient Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(17, 517);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 37);
            this.label2.TabIndex = 3;
            this.label2.Text = "Doctor Details";
            // 
            // ctrlAppointmentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlDoctorInfo1);
            this.Controls.Add(this.ctrlPatientInfo1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlAppointmentInfo";
            this.Size = new System.Drawing.Size(850, 1033);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Patient.ctrlPatientInfo ctrlPatientInfo1;
        private Doctor.ctrlDoctorInfo ctrlDoctorInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
