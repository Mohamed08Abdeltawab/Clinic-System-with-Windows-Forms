namespace Clinic.Medical_Services.Visit
{
    partial class frmAddUpdatelVisitDetails
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tcVisitInfo = new System.Windows.Forms.TabControl();
            this.tpVisitInfo = new System.Windows.Forms.TabPage();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.llDoctorInfo = new System.Windows.Forms.LinkLabel();
            this.llPatientInfo = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblDoctorID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblPatientID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAppointmentID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblVisitID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSaveandNext = new System.Windows.Forms.Button();
            this.tpPrescriptionInfo = new System.Windows.Forms.TabPage();
            this.dgvMedicines = new System.Windows.Forms.DataGridView();
            this.cmsMedicines = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowMedicineInfotoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMedicinetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMedicineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPrescriptionNotes = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpPrescriptionDate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPrescriptionID = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAddNewMedicine = new System.Windows.Forms.Button();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tcVisitInfo.SuspendLayout();
            this.tpVisitInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.tpPrescriptionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).BeginInit();
            this.cmsMedicines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(356, 37);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(278, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Set Visit Details";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tcVisitInfo
            // 
            this.tcVisitInfo.Controls.Add(this.tpVisitInfo);
            this.tcVisitInfo.Controls.Add(this.tpPrescriptionInfo);
            this.tcVisitInfo.Location = new System.Drawing.Point(21, 96);
            this.tcVisitInfo.Name = "tcVisitInfo";
            this.tcVisitInfo.SelectedIndex = 0;
            this.tcVisitInfo.Size = new System.Drawing.Size(904, 582);
            this.tcVisitInfo.TabIndex = 194;
            // 
            // tpVisitInfo
            // 
            this.tpVisitInfo.Controls.Add(this.pictureBox8);
            this.tpVisitInfo.Controls.Add(this.dtpDateTime);
            this.tpVisitInfo.Controls.Add(this.txtDiagnosis);
            this.tpVisitInfo.Controls.Add(this.txtNotes);
            this.tpVisitInfo.Controls.Add(this.pictureBox7);
            this.tpVisitInfo.Controls.Add(this.label14);
            this.tpVisitInfo.Controls.Add(this.pictureBox6);
            this.tpVisitInfo.Controls.Add(this.label12);
            this.tpVisitInfo.Controls.Add(this.llDoctorInfo);
            this.tpVisitInfo.Controls.Add(this.llPatientInfo);
            this.tpVisitInfo.Controls.Add(this.label10);
            this.tpVisitInfo.Controls.Add(this.pictureBox3);
            this.tpVisitInfo.Controls.Add(this.lblDoctorID);
            this.tpVisitInfo.Controls.Add(this.label7);
            this.tpVisitInfo.Controls.Add(this.pictureBox2);
            this.tpVisitInfo.Controls.Add(this.lblPatientID);
            this.tpVisitInfo.Controls.Add(this.label5);
            this.tpVisitInfo.Controls.Add(this.pictureBox1);
            this.tpVisitInfo.Controls.Add(this.lblAppointmentID);
            this.tpVisitInfo.Controls.Add(this.label3);
            this.tpVisitInfo.Controls.Add(this.pictureBox5);
            this.tpVisitInfo.Controls.Add(this.lblVisitID);
            this.tpVisitInfo.Controls.Add(this.label8);
            this.tpVisitInfo.Controls.Add(this.btnSaveandNext);
            this.tpVisitInfo.Location = new System.Drawing.Point(4, 29);
            this.tpVisitInfo.Name = "tpVisitInfo";
            this.tpVisitInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpVisitInfo.Size = new System.Drawing.Size(896, 549);
            this.tpVisitInfo.TabIndex = 0;
            this.tpVisitInfo.Text = "Visit Info";
            this.tpVisitInfo.UseVisualStyleBackColor = true;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::Clinic.Properties.Resources.Calendar_32;
            this.pictureBox8.Location = new System.Drawing.Point(183, 165);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 214;
            this.pictureBox8.TabStop = false;
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.CustomFormat = "dd/M/yyyy";
            this.dtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTime.Location = new System.Drawing.Point(225, 165);
            this.dtpDateTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(167, 26);
            this.dtpDateTime.TabIndex = 213;
            this.dtpDateTime.Value = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(183, 275);
            this.txtDiagnosis.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDiagnosis.MaxLength = 50;
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(539, 75);
            this.txtDiagnosis.TabIndex = 212;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(183, 402);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNotes.MaxLength = 50;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(539, 116);
            this.txtNotes.TabIndex = 211;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Clinic.Properties.Resources.Notes_32;
            this.pictureBox7.Location = new System.Drawing.Point(183, 368);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(31, 26);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 210;
            this.pictureBox7.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(36, 368);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 20);
            this.label14.TabIndex = 209;
            this.label14.Text = "Notes";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Clinic.Properties.Resources.Diagnosis_32;
            this.pictureBox6.Location = new System.Drawing.Point(183, 241);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 208;
            this.pictureBox6.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(36, 241);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 20);
            this.label12.TabIndex = 207;
            this.label12.Text = "Diagnosis:";
            // 
            // llDoctorInfo
            // 
            this.llDoctorInfo.AutoSize = true;
            this.llDoctorInfo.Location = new System.Drawing.Point(765, 91);
            this.llDoctorInfo.Name = "llDoctorInfo";
            this.llDoctorInfo.Size = new System.Drawing.Size(89, 20);
            this.llDoctorInfo.TabIndex = 206;
            this.llDoctorInfo.TabStop = true;
            this.llDoctorInfo.Text = "Doctor Info";
            this.llDoctorInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDoctorInfo_LinkClicked);
            // 
            // llPatientInfo
            // 
            this.llPatientInfo.AutoSize = true;
            this.llPatientInfo.Location = new System.Drawing.Point(765, 24);
            this.llPatientInfo.Name = "llPatientInfo";
            this.llPatientInfo.Size = new System.Drawing.Size(91, 20);
            this.llPatientInfo.TabIndex = 205;
            this.llPatientInfo.TabStop = true;
            this.llPatientInfo.Text = "Patient Info";
            this.llPatientInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llPatientInfo_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(36, 170);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 20);
            this.label10.TabIndex = 204;
            this.label10.Text = "Visit Date:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox3.Location = new System.Drawing.Point(618, 93);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 203;
            this.pictureBox3.TabStop = false;
            // 
            // lblDoctorID
            // 
            this.lblDoctorID.AutoSize = true;
            this.lblDoctorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctorID.Location = new System.Drawing.Point(656, 93);
            this.lblDoctorID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctorID.Name = "lblDoctorID";
            this.lblDoctorID.Size = new System.Drawing.Size(49, 20);
            this.lblDoctorID.TabIndex = 202;
            this.lblDoctorID.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(471, 93);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 201;
            this.label7.Text = "Doctor ID:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox2.Location = new System.Drawing.Point(618, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 200;
            this.pictureBox2.TabStop = false;
            // 
            // lblPatientID
            // 
            this.lblPatientID.AutoSize = true;
            this.lblPatientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientID.Location = new System.Drawing.Point(656, 24);
            this.lblPatientID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatientID.Name = "lblPatientID";
            this.lblPatientID.Size = new System.Drawing.Size(49, 20);
            this.lblPatientID.TabIndex = 199;
            this.lblPatientID.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(471, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 198;
            this.label5.Text = "Patient ID:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox1.Location = new System.Drawing.Point(183, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 197;
            this.pictureBox1.TabStop = false;
            // 
            // lblAppointmentID
            // 
            this.lblAppointmentID.AutoSize = true;
            this.lblAppointmentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentID.Location = new System.Drawing.Point(221, 91);
            this.lblAppointmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppointmentID.Name = "lblAppointmentID";
            this.lblAppointmentID.Size = new System.Drawing.Size(49, 20);
            this.lblAppointmentID.TabIndex = 196;
            this.lblAppointmentID.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 195;
            this.label3.Text = "Apponitment ID:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox5.Location = new System.Drawing.Point(183, 24);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 194;
            this.pictureBox5.TabStop = false;
            // 
            // lblVisitID
            // 
            this.lblVisitID.AutoSize = true;
            this.lblVisitID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitID.Location = new System.Drawing.Point(221, 24);
            this.lblVisitID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVisitID.Name = "lblVisitID";
            this.lblVisitID.Size = new System.Drawing.Size(49, 20);
            this.lblVisitID.TabIndex = 193;
            this.lblVisitID.Text = "[???]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(36, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 192;
            this.label8.Text = "Visit ID:";
            // 
            // btnSaveandNext
            // 
            this.btnSaveandNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveandNext.Image = global::Clinic.Properties.Resources.Next_32;
            this.btnSaveandNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveandNext.Location = new System.Drawing.Point(730, 481);
            this.btnSaveandNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveandNext.Name = "btnSaveandNext";
            this.btnSaveandNext.Size = new System.Drawing.Size(151, 37);
            this.btnSaveandNext.TabIndex = 119;
            this.btnSaveandNext.Text = "Save and Next";
            this.btnSaveandNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveandNext.UseVisualStyleBackColor = true;
            this.btnSaveandNext.Click += new System.EventHandler(this.btnSaveandNext_Click);
            // 
            // tpPrescriptionInfo
            // 
            this.tpPrescriptionInfo.Controls.Add(this.dgvMedicines);
            this.tpPrescriptionInfo.Controls.Add(this.txtPrescriptionNotes);
            this.tpPrescriptionInfo.Controls.Add(this.label15);
            this.tpPrescriptionInfo.Controls.Add(this.dtpPrescriptionDate);
            this.tpPrescriptionInfo.Controls.Add(this.label13);
            this.tpPrescriptionInfo.Controls.Add(this.lblPrescriptionID);
            this.tpPrescriptionInfo.Controls.Add(this.label11);
            this.tpPrescriptionInfo.Controls.Add(this.btnAddNewMedicine);
            this.tpPrescriptionInfo.Controls.Add(this.pictureBox13);
            this.tpPrescriptionInfo.Controls.Add(this.pictureBox12);
            this.tpPrescriptionInfo.Controls.Add(this.pictureBox11);
            this.tpPrescriptionInfo.Location = new System.Drawing.Point(4, 29);
            this.tpPrescriptionInfo.Name = "tpPrescriptionInfo";
            this.tpPrescriptionInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPrescriptionInfo.Size = new System.Drawing.Size(896, 549);
            this.tpPrescriptionInfo.TabIndex = 1;
            this.tpPrescriptionInfo.Text = "Prescription Info";
            this.tpPrescriptionInfo.UseVisualStyleBackColor = true;
            // 
            // dgvMedicines
            // 
            this.dgvMedicines.AllowUserToAddRows = false;
            this.dgvMedicines.AllowUserToDeleteRows = false;
            this.dgvMedicines.AllowUserToResizeRows = false;
            this.dgvMedicines.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicines.ContextMenuStrip = this.cmsMedicines;
            this.dgvMedicines.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMedicines.Location = new System.Drawing.Point(20, 146);
            this.dgvMedicines.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMedicines.MultiSelect = false;
            this.dgvMedicines.Name = "dgvMedicines";
            this.dgvMedicines.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMedicines.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMedicines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicines.Size = new System.Drawing.Size(827, 248);
            this.dgvMedicines.TabIndex = 223;
            this.dgvMedicines.TabStop = false;
            // 
            // cmsMedicines
            // 
            this.cmsMedicines.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMedicineInfotoolStripMenuItem,
            this.EditMedicinetoolStripMenuItem,
            this.removeMedicineToolStripMenuItem});
            this.cmsMedicines.Name = "contextMenuStrip1";
            this.cmsMedicines.Size = new System.Drawing.Size(196, 118);
            // 
            // ShowMedicineInfotoolStripMenuItem
            // 
            this.ShowMedicineInfotoolStripMenuItem.Image = global::Clinic.Properties.Resources.show_medicine_32;
            this.ShowMedicineInfotoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowMedicineInfotoolStripMenuItem.Name = "ShowMedicineInfotoolStripMenuItem";
            this.ShowMedicineInfotoolStripMenuItem.Size = new System.Drawing.Size(195, 38);
            this.ShowMedicineInfotoolStripMenuItem.Text = "Show Medicine Info";
            this.ShowMedicineInfotoolStripMenuItem.Click += new System.EventHandler(this.ShowMedicineInfotoolStripMenuItem_Click);
            // 
            // EditMedicinetoolStripMenuItem
            // 
            this.EditMedicinetoolStripMenuItem.Image = global::Clinic.Properties.Resources.edit_medicinek_32;
            this.EditMedicinetoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditMedicinetoolStripMenuItem.Name = "EditMedicinetoolStripMenuItem";
            this.EditMedicinetoolStripMenuItem.Size = new System.Drawing.Size(195, 38);
            this.EditMedicinetoolStripMenuItem.Text = "Edit Medicine";
            this.EditMedicinetoolStripMenuItem.Click += new System.EventHandler(this.EditMedicinetoolStripMenuItem_Click);
            // 
            // removeMedicineToolStripMenuItem
            // 
            this.removeMedicineToolStripMenuItem.Image = global::Clinic.Properties.Resources.remove_32;
            this.removeMedicineToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.removeMedicineToolStripMenuItem.Name = "removeMedicineToolStripMenuItem";
            this.removeMedicineToolStripMenuItem.Size = new System.Drawing.Size(195, 38);
            this.removeMedicineToolStripMenuItem.Text = "Remove Medicine";
            this.removeMedicineToolStripMenuItem.Click += new System.EventHandler(this.removeMedicineToolStripMenuItem_Click);
            // 
            // txtPrescriptionNotes
            // 
            this.txtPrescriptionNotes.Location = new System.Drawing.Point(217, 435);
            this.txtPrescriptionNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPrescriptionNotes.MaxLength = 50;
            this.txtPrescriptionNotes.Multiline = true;
            this.txtPrescriptionNotes.Name = "txtPrescriptionNotes";
            this.txtPrescriptionNotes.Size = new System.Drawing.Size(429, 81);
            this.txtPrescriptionNotes.TabIndex = 214;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 435);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 20);
            this.label15.TabIndex = 212;
            this.label15.Text = "Prescription Notes:";
            // 
            // dtpPrescriptionDate
            // 
            this.dtpPrescriptionDate.CustomFormat = "";
            this.dtpPrescriptionDate.Location = new System.Drawing.Point(568, 44);
            this.dtpPrescriptionDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpPrescriptionDate.Name = "dtpPrescriptionDate";
            this.dtpPrescriptionDate.Size = new System.Drawing.Size(279, 26);
            this.dtpPrescriptionDate.TabIndex = 180;
            this.dtpPrescriptionDate.Value = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(366, 44);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(153, 20);
            this.label13.TabIndex = 181;
            this.label13.Text = "Prescription Date:";
            // 
            // lblPrescriptionID
            // 
            this.lblPrescriptionID.AutoSize = true;
            this.lblPrescriptionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrescriptionID.Location = new System.Drawing.Point(208, 44);
            this.lblPrescriptionID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrescriptionID.Name = "lblPrescriptionID";
            this.lblPrescriptionID.Size = new System.Drawing.Size(49, 20);
            this.lblPrescriptionID.TabIndex = 169;
            this.lblPrescriptionID.Text = "[???]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 44);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 20);
            this.label11.TabIndex = 168;
            this.label11.Text = "Prescription ID:";
            // 
            // btnAddNewMedicine
            // 
            this.btnAddNewMedicine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNewMedicine.Image = global::Clinic.Properties.Resources.add_32;
            this.btnAddNewMedicine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewMedicine.Location = new System.Drawing.Point(20, 98);
            this.btnAddNewMedicine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddNewMedicine.Name = "btnAddNewMedicine";
            this.btnAddNewMedicine.Size = new System.Drawing.Size(204, 38);
            this.btnAddNewMedicine.TabIndex = 222;
            this.btnAddNewMedicine.Text = "Add New Medicine";
            this.btnAddNewMedicine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNewMedicine.UseVisualStyleBackColor = true;
            this.btnAddNewMedicine.Click += new System.EventHandler(this.btnAddNewMedicine_Click);
            // 
            // pictureBox13
            // 
            this.pictureBox13.Image = global::Clinic.Properties.Resources.Notes_32;
            this.pictureBox13.Location = new System.Drawing.Point(179, 435);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(31, 26);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox13.TabIndex = 213;
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = global::Clinic.Properties.Resources.appoint_date_32;
            this.pictureBox12.Location = new System.Drawing.Point(526, 44);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(31, 26);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 182;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::Clinic.Properties.Resources.ID_32;
            this.pictureBox11.Location = new System.Drawing.Point(170, 44);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(31, 26);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 170;
            this.pictureBox11.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Clinic.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(639, 690);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 193;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Image = global::Clinic.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(773, 690);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 37);
            this.btnSave.TabIndex = 192;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmFillVisitDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 741);
            this.Controls.Add(this.tcVisitInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmFillVisitDetails";
            this.Text = "frmFillVisitDetails";
            this.Load += new System.EventHandler(this.frmFillVisitDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tcVisitInfo.ResumeLayout(false);
            this.tpVisitInfo.ResumeLayout(false);
            this.tpVisitInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.tpPrescriptionInfo.ResumeLayout(false);
            this.tpPrescriptionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicines)).EndInit();
            this.cmsMedicines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabControl tcVisitInfo;
        private System.Windows.Forms.TabPage tpVisitInfo;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.TextBox txtNotes;
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
        private System.Windows.Forms.Button btnSaveandNext;
        private System.Windows.Forms.TabPage tpPrescriptionInfo;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label lblPrescriptionID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.DateTimePicker dtpPrescriptionDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrescriptionNotes;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnAddNewMedicine;
        private System.Windows.Forms.DataGridView dgvMedicines;
        private System.Windows.Forms.ContextMenuStrip cmsMedicines;
        private System.Windows.Forms.ToolStripMenuItem EditMedicinetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowMedicineInfotoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMedicineToolStripMenuItem;
    }
}