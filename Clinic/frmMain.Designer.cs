namespace Clinic
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblLoggedInUser = new System.Windows.Forms.Label();
            this.msMainMenue = new System.Windows.Forms.MenuStrip();
            this.servicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appointmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prescriptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FinancialstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageBillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PatientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DoctorsStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MangementStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageServicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mangeAppointmentTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserOptiontoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Password32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.msMainMenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoggedInUser
            // 
            this.lblLoggedInUser.AutoSize = true;
            this.lblLoggedInUser.BackColor = System.Drawing.SystemColors.Control;
            this.lblLoggedInUser.Location = new System.Drawing.Point(1201, 1035);
            this.lblLoggedInUser.Name = "lblLoggedInUser";
            this.lblLoggedInUser.Size = new System.Drawing.Size(93, 20);
            this.lblLoggedInUser.TabIndex = 8;
            this.lblLoggedInUser.Text = "[UserName]";
            // 
            // msMainMenue
            // 
            this.msMainMenue.BackColor = System.Drawing.Color.White;
            this.msMainMenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMainMenue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servicesToolStripMenuItem,
            this.FinancialstoolStripMenuItem,
            this.peopleToolStripMenuItem,
            this.PatientsToolStripMenuItem,
            this.DoctorsStripMenuItem1,
            this.UsersToolStripMenuItem,
            this.accountSettingsToolStripMenuItem});
            this.msMainMenue.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.msMainMenue.Location = new System.Drawing.Point(0, 0);
            this.msMainMenue.Name = "msMainMenue";
            this.msMainMenue.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msMainMenue.Size = new System.Drawing.Size(1924, 72);
            this.msMainMenue.TabIndex = 7;
            this.msMainMenue.Text = "menuStrip1";
            // 
            // servicesToolStripMenuItem
            // 
            this.servicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appointmentsToolStripMenuItem,
            this.visitsToolStripMenuItem,
            this.prescriptionsToolStripMenuItem});
            this.servicesToolStripMenuItem.Image = global::Clinic.Properties.Resources.MedicalServices;
            this.servicesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.servicesToolStripMenuItem.Name = "servicesToolStripMenuItem";
            this.servicesToolStripMenuItem.Size = new System.Drawing.Size(214, 68);
            this.servicesToolStripMenuItem.Text = "&Medical Services";
            // 
            // appointmentsToolStripMenuItem
            // 
            this.appointmentsToolStripMenuItem.Image = global::Clinic.Properties.Resources.appointment_64;
            this.appointmentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.appointmentsToolStripMenuItem.Name = "appointmentsToolStripMenuItem";
            this.appointmentsToolStripMenuItem.Size = new System.Drawing.Size(303, 70);
            this.appointmentsToolStripMenuItem.Text = "Manage Appointments";
            this.appointmentsToolStripMenuItem.Click += new System.EventHandler(this.appointmentsToolStripMenuItem_Click_1);
            // 
            // visitsToolStripMenuItem
            // 
            this.visitsToolStripMenuItem.Image = global::Clinic.Properties.Resources.Visits_64;
            this.visitsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.visitsToolStripMenuItem.Name = "visitsToolStripMenuItem";
            this.visitsToolStripMenuItem.Size = new System.Drawing.Size(303, 70);
            this.visitsToolStripMenuItem.Text = "Manage Visits";
            this.visitsToolStripMenuItem.Click += new System.EventHandler(this.visitsToolStripMenuItem_Click);
            // 
            // prescriptionsToolStripMenuItem
            // 
            this.prescriptionsToolStripMenuItem.Image = global::Clinic.Properties.Resources.Prescription_64;
            this.prescriptionsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.prescriptionsToolStripMenuItem.Name = "prescriptionsToolStripMenuItem";
            this.prescriptionsToolStripMenuItem.Size = new System.Drawing.Size(303, 70);
            this.prescriptionsToolStripMenuItem.Text = "Manage Prescriptions";
            this.prescriptionsToolStripMenuItem.Click += new System.EventHandler(this.prescriptionsToolStripMenuItem_Click);
            // 
            // FinancialstoolStripMenuItem
            // 
            this.FinancialstoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paymentsToolStripMenuItem,
            this.manageBillsToolStripMenuItem});
            this.FinancialstoolStripMenuItem.Image = global::Clinic.Properties.Resources.Financials;
            this.FinancialstoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FinancialstoolStripMenuItem.Name = "FinancialstoolStripMenuItem";
            this.FinancialstoolStripMenuItem.Size = new System.Drawing.Size(162, 68);
            this.FinancialstoolStripMenuItem.Text = "Financials";
            // 
            // paymentsToolStripMenuItem
            // 
            this.paymentsToolStripMenuItem.Image = global::Clinic.Properties.Resources.Payment_64;
            this.paymentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.paymentsToolStripMenuItem.Name = "paymentsToolStripMenuItem";
            this.paymentsToolStripMenuItem.Size = new System.Drawing.Size(226, 70);
            this.paymentsToolStripMenuItem.Text = "Payments";
            // 
            // manageBillsToolStripMenuItem
            // 
            this.manageBillsToolStripMenuItem.Image = global::Clinic.Properties.Resources.Bill_64;
            this.manageBillsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageBillsToolStripMenuItem.Name = "manageBillsToolStripMenuItem";
            this.manageBillsToolStripMenuItem.Size = new System.Drawing.Size(226, 70);
            this.manageBillsToolStripMenuItem.Text = "Manage Bills";
            this.manageBillsToolStripMenuItem.Click += new System.EventHandler(this.manageBillsToolStripMenuItem_Click);
            // 
            // peopleToolStripMenuItem
            // 
            this.peopleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("peopleToolStripMenuItem.Image")));
            this.peopleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
            this.peopleToolStripMenuItem.Size = new System.Drawing.Size(139, 68);
            this.peopleToolStripMenuItem.Text = "People";
            this.peopleToolStripMenuItem.Click += new System.EventHandler(this.peopleToolStripMenuItem_Click);
            // 
            // PatientsToolStripMenuItem
            // 
            this.PatientsToolStripMenuItem.Image = global::Clinic.Properties.Resources.patient;
            this.PatientsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PatientsToolStripMenuItem.Name = "PatientsToolStripMenuItem";
            this.PatientsToolStripMenuItem.Size = new System.Drawing.Size(148, 68);
            this.PatientsToolStripMenuItem.Text = "Patients";
            this.PatientsToolStripMenuItem.Click += new System.EventHandler(this.PatientsToolStripMenuItem_Click);
            // 
            // DoctorsStripMenuItem1
            // 
            this.DoctorsStripMenuItem1.Image = global::Clinic.Properties.Resources.doctor;
            this.DoctorsStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DoctorsStripMenuItem1.Name = "DoctorsStripMenuItem1";
            this.DoctorsStripMenuItem1.Size = new System.Drawing.Size(145, 68);
            this.DoctorsStripMenuItem1.Text = "Doctors";
            this.DoctorsStripMenuItem1.Click += new System.EventHandler(this.DoctorsStripMenuItem1_Click);
            // 
            // UsersToolStripMenuItem
            // 
            this.UsersToolStripMenuItem.Image = global::Clinic.Properties.Resources.User_Options_64;
            this.UsersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            this.UsersToolStripMenuItem.Size = new System.Drawing.Size(127, 68);
            this.UsersToolStripMenuItem.Text = "Users";
            this.UsersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MangementStripMenuItem,
            this.UserOptiontoolStripMenuItem,
            this.toolStripSeparator4,
            this.signOutToolStripMenuItem});
            this.accountSettingsToolStripMenuItem.Image = global::Clinic.Properties.Resources.Account_Setttings_64;
            this.accountSettingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(215, 68);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            // 
            // MangementStripMenuItem
            // 
            this.MangementStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageServicesToolStripMenuItem,
            this.mangeAppointmentTypeToolStripMenuItem});
            this.MangementStripMenuItem.Image = global::Clinic.Properties.Resources.mange_32;
            this.MangementStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MangementStripMenuItem.Name = "MangementStripMenuItem";
            this.MangementStripMenuItem.Size = new System.Drawing.Size(189, 38);
            this.MangementStripMenuItem.Text = "Mangement";
            // 
            // manageServicesToolStripMenuItem
            // 
            this.manageServicesToolStripMenuItem.Image = global::Clinic.Properties.Resources.Mange_Services32;
            this.manageServicesToolStripMenuItem.Name = "manageServicesToolStripMenuItem";
            this.manageServicesToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.manageServicesToolStripMenuItem.Text = "Manage Services";
            this.manageServicesToolStripMenuItem.Click += new System.EventHandler(this.manageServicesToolStripMenuItem_Click);
            // 
            // mangeAppointmentTypeToolStripMenuItem
            // 
            this.mangeAppointmentTypeToolStripMenuItem.Image = global::Clinic.Properties.Resources.appointment_type_32;
            this.mangeAppointmentTypeToolStripMenuItem.Name = "mangeAppointmentTypeToolStripMenuItem";
            this.mangeAppointmentTypeToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.mangeAppointmentTypeToolStripMenuItem.Text = "Mange Appointment Type";
            this.mangeAppointmentTypeToolStripMenuItem.Click += new System.EventHandler(this.mangeAppointmentTypeToolStripMenuItem_Click);
            // 
            // UserOptiontoolStripMenuItem
            // 
            this.UserOptiontoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentUserInfoToolStripMenuItem1,
            this.Password32ToolStripMenuItem});
            this.UserOptiontoolStripMenuItem.Image = global::Clinic.Properties.Resources.User_Options_32;
            this.UserOptiontoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UserOptiontoolStripMenuItem.Name = "UserOptiontoolStripMenuItem";
            this.UserOptiontoolStripMenuItem.Size = new System.Drawing.Size(189, 38);
            this.UserOptiontoolStripMenuItem.Text = "UserOption";
            // 
            // currentUserInfoToolStripMenuItem1
            // 
            this.currentUserInfoToolStripMenuItem1.Image = global::Clinic.Properties.Resources.PersonDetails_32;
            this.currentUserInfoToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.currentUserInfoToolStripMenuItem1.Name = "currentUserInfoToolStripMenuItem1";
            this.currentUserInfoToolStripMenuItem1.Size = new System.Drawing.Size(230, 38);
            this.currentUserInfoToolStripMenuItem1.Text = "&Current User Info";
            this.currentUserInfoToolStripMenuItem1.Click += new System.EventHandler(this.currentUserInfoToolStripMenuItem_Click);
            // 
            // Password32ToolStripMenuItem
            // 
            this.Password32ToolStripMenuItem.Image = global::Clinic.Properties.Resources.Password_32;
            this.Password32ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Password32ToolStripMenuItem.Name = "Password32ToolStripMenuItem";
            this.Password32ToolStripMenuItem.Size = new System.Drawing.Size(230, 38);
            this.Password32ToolStripMenuItem.Text = "Change Password";
            this.Password32ToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(186, 6);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("signOutToolStripMenuItem.Image")));
            this.signOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(189, 38);
            this.signOutToolStripMenuItem.Text = "Sign &Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Clinic.Properties.Resources.ClinicWallpaper;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1924, 1061);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.lblLoggedInUser);
            this.Controls.Add(this.msMainMenue);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.msMainMenue.ResumeLayout(false);
            this.msMainMenue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoggedInUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PatientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip msMainMenue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem DoctorsStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem appointmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prescriptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FinancialstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageBillsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MangementStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserOptiontoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Password32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageServicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mangeAppointmentTypeToolStripMenuItem;
    }
}