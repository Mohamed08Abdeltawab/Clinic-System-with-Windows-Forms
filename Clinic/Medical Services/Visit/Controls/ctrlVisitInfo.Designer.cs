namespace Clinic.Medical_Services.Visit
{
    partial class ctrlVisitInfo
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
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.llDoctorInfo = new System.Windows.Forms.LinkLabel();
            this.llPatientInfo = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDoctorID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPatientID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAppointmentID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVisitID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llEditVisitInfo = new System.Windows.Forms.LinkLabel();
            this.txtDiagnosis = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.CustomFormat = "dd/M/yyyy";
            this.dtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTime.Location = new System.Drawing.Point(215, 146);
            this.dtpDateTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(167, 26);
            this.dtpDateTime.TabIndex = 236;
            this.dtpDateTime.Value = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(26, 273);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 20);
            this.label14.TabIndex = 232;
            this.label14.Text = "Notes";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(26, 200);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 20);
            this.label12.TabIndex = 230;
            this.label12.Text = "Diagnosis:";
            // 
            // llDoctorInfo
            // 
            this.llDoctorInfo.AutoSize = true;
            this.llDoctorInfo.Location = new System.Drawing.Point(683, 146);
            this.llDoctorInfo.Name = "llDoctorInfo";
            this.llDoctorInfo.Size = new System.Drawing.Size(89, 20);
            this.llDoctorInfo.TabIndex = 229;
            this.llDoctorInfo.TabStop = true;
            this.llDoctorInfo.Text = "Doctor Info";
            this.llDoctorInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDoctorInfo_LinkClicked_1);
            // 
            // llPatientInfo
            // 
            this.llPatientInfo.AutoSize = true;
            this.llPatientInfo.Location = new System.Drawing.Point(683, 92);
            this.llPatientInfo.Name = "llPatientInfo";
            this.llPatientInfo.Size = new System.Drawing.Size(91, 20);
            this.llPatientInfo.TabIndex = 228;
            this.llPatientInfo.TabStop = true;
            this.llPatientInfo.Text = "Patient Info";
            this.llPatientInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llPatientInfo_LinkClicked_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(26, 151);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 20);
            this.label10.TabIndex = 227;
            this.label10.Text = "Visit Date:";
            // 
            // lblDoctorID
            // 
            this.lblDoctorID.AutoSize = true;
            this.lblDoctorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctorID.Location = new System.Drawing.Point(574, 148);
            this.lblDoctorID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctorID.Name = "lblDoctorID";
            this.lblDoctorID.Size = new System.Drawing.Size(49, 20);
            this.lblDoctorID.TabIndex = 225;
            this.lblDoctorID.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(389, 148);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 224;
            this.label7.Text = "Doctor ID:";
            // 
            // lblPatientID
            // 
            this.lblPatientID.AutoSize = true;
            this.lblPatientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientID.Location = new System.Drawing.Point(574, 92);
            this.lblPatientID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatientID.Name = "lblPatientID";
            this.lblPatientID.Size = new System.Drawing.Size(49, 20);
            this.lblPatientID.TabIndex = 222;
            this.lblPatientID.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(389, 92);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 221;
            this.label5.Text = "Patient ID:";
            // 
            // lblAppointmentID
            // 
            this.lblAppointmentID.AutoSize = true;
            this.lblAppointmentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentID.Location = new System.Drawing.Point(211, 92);
            this.lblAppointmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppointmentID.Name = "lblAppointmentID";
            this.lblAppointmentID.Size = new System.Drawing.Size(49, 20);
            this.lblAppointmentID.TabIndex = 219;
            this.lblAppointmentID.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 218;
            this.label3.Text = "Apponitment ID:";
            // 
            // lblVisitID
            // 
            this.lblVisitID.AutoSize = true;
            this.lblVisitID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitID.Location = new System.Drawing.Point(211, 38);
            this.lblVisitID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVisitID.Name = "lblVisitID";
            this.lblVisitID.Size = new System.Drawing.Size(49, 20);
            this.lblVisitID.TabIndex = 216;
            this.lblVisitID.Text = "[???]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(26, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 215;
            this.label8.Text = "Visit ID:";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::Clinic.Properties.Resources.Calendar_32;
            this.pictureBox8.Location = new System.Drawing.Point(173, 146);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 237;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Clinic.Properties.Resources.Notes_32;
            this.pictureBox7.Location = new System.Drawing.Point(173, 273);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(31, 26);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 233;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Clinic.Properties.Resources.Diagnosis_32;
            this.pictureBox6.Location = new System.Drawing.Point(173, 200);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 231;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox3.Location = new System.Drawing.Point(536, 148);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 226;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox2.Location = new System.Drawing.Point(536, 92);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 223;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox1.Location = new System.Drawing.Point(173, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 220;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox5.Location = new System.Drawing.Point(173, 38);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 217;
            this.pictureBox5.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNotes);
            this.groupBox1.Controls.Add(this.txtDiagnosis);
            this.groupBox1.Controls.Add(this.llEditVisitInfo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.pictureBox8);
            this.groupBox1.Controls.Add(this.lblVisitID);
            this.groupBox1.Controls.Add(this.dtpDateTime);
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblAppointmentID);
            this.groupBox1.Controls.Add(this.pictureBox7);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox6);
            this.groupBox1.Controls.Add(this.lblPatientID);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.llDoctorInfo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.llPatientInfo);
            this.groupBox1.Controls.Add(this.lblDoctorID);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 361);
            this.groupBox1.TabIndex = 238;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visit Information";
            // 
            // llEditVisitInfo
            // 
            this.llEditVisitInfo.AutoSize = true;
            this.llEditVisitInfo.Location = new System.Drawing.Point(618, 38);
            this.llEditVisitInfo.Name = "llEditVisitInfo";
            this.llEditVisitInfo.Size = new System.Drawing.Size(156, 20);
            this.llEditVisitInfo.TabIndex = 238;
            this.llEditVisitInfo.TabStop = true;
            this.llEditVisitInfo.Text = "Edit Visit Information";
            this.llEditVisitInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llEditVisitInfo_LinkClicked);
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.AutoSize = true;
            this.txtDiagnosis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosis.Location = new System.Drawing.Point(211, 206);
            this.txtDiagnosis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(49, 20);
            this.txtDiagnosis.TabIndex = 239;
            this.txtDiagnosis.Text = "[???]";
            // 
            // txtNotes
            // 
            this.txtNotes.AutoSize = true;
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(211, 279);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(49, 20);
            this.txtNotes.TabIndex = 240;
            this.txtNotes.Text = "[???]";
            // 
            // ctrlVisitInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlVisitInfo";
            this.Size = new System.Drawing.Size(807, 371);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel llDoctorInfo;
        private System.Windows.Forms.LinkLabel llPatientInfo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblDoctorID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblPatientID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAppointmentID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblVisitID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llEditVisitInfo;
        private System.Windows.Forms.Label txtNotes;
        private System.Windows.Forms.Label txtDiagnosis;
    }
}
