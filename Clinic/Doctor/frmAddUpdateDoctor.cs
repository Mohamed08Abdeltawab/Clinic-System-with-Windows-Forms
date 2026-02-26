using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Global_Classes;
using Clinicbusiness;

namespace Clinic.Doctor
{
    public partial class frmAddUpdateDoctor : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _DoctorID = -1;
        clsDoctor _Doctor;

        public frmAddUpdateDoctor()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateDoctor(int DoctorID)
        {
            InitializeComponent();
            _DoctorID = DoctorID;
            _Mode = enMode.Update;
        }

       

        private void _ResetDefualtValues()
        {


            if (_Mode == enMode.AddNew)
            {

                lblTitle.Text = "New Doctor";
                this.Text = "New Doctor";
                _Doctor = new clsDoctor();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpDoctorInfo.Enabled = false;
                
            }
            else
            {
                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";

                tpDoctorInfo.Enabled = true;
                btnSave.Enabled = true;

            }

        }

        public void ReadSelectedDays()
        {
            // 1. حماية من الـ Null عشان البرنامج ميعملش كراش
            if (string.IsNullOrEmpty(_Doctor.WorkingDays) || _Doctor.WorkingDays == "Not Avilable")
                return;

            // شلنا ToString لأننا تأكدنا إنه مش Null
            string[] SelectedDays = _Doctor.WorkingDays.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < chkWorkingDays.Items.Count; i++)
            {
                // 2. هنا صلحنا البج: بناخد أول 3 حروف عشان المقارنة تنجح
                string shortDayName = chkWorkingDays.Items[i].ToString().Substring(0, 3);

                if (SelectedDays.Contains(shortDayName))
                {
                    chkWorkingDays.SetItemChecked(i, true);
                }
            }
        }

        private void _LoadData()
        {

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _Doctor = clsDoctor.Find(_DoctorID);

            if (_Doctor == null)
            {
                MessageBox.Show("No Doctor with ID = " + _DoctorID, "Doctor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonData(_Doctor.PersonID);
            txtSpecialization.Text = _Doctor.Specialization.ToString();
            txtConsultationFees.Text = _Doctor.ConsultationFees.ToString();
            //handle working days
            ReadSelectedDays();

        }

        

        private void frmAddUpdateDoctor_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }


        

        private void btnDoctorNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpDoctorInfo.Enabled = true;
                tcDoctorInfo.SelectedTab = tcDoctorInfo.TabPages["tpDoctorInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsDoctor.IsDoctorExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already a Doctor, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpDoctorInfo.Enabled = true;
                    tcDoctorInfo.SelectedTab = tcDoctorInfo.TabPages["tpDoctorInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }
        }

        public string SelectedDays()
        {
            List<string> shortDays = new List<string>();

            foreach (var item in chkWorkingDays.CheckedItems)
            {
                //take first 3 char
                string shortName = item.ToString().Substring(0, 3);
                shortDays.Add(shortName);
            }
            if(chkWorkingDays.CheckedItems.Count > 0)
            {
                return string.Join(", ", shortDays);
            }
            else
                { return "Not Avilable"; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _Doctor.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Doctor.Specialization = txtSpecialization.Text.Trim();
            _Doctor.ConsultationFees = Convert.ToDecimal(txtConsultationFees.Text.Trim());

            if (chkWorkingDays.CheckedItems.Count < 1)
            {
                chkWorkingDays.Focus();
                if (MessageBox.Show("Do you want to complete without giving a Working Days for this Doctor!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return; 
                }
            }
            _Doctor.WorkingDays = SelectedDays();


            if (_Doctor.Save())
            {
                lblDoctorID.Text = _Doctor.DoctorID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Doctor";
                this.Text = "Update Doctor";
                btnSave.Enabled = false;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkWorkingDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkWorkingDays.ClearSelected();
        }

        private void txtSpecialization_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSpecialization.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSpecialization, "Specialization can't be blank!");
            }
            else
            {
                errorProvider1.SetError(txtSpecialization, "");
            }
        }

        private void txtConsultationFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConsultationFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConsultationFees, "Consultation Fees can't be blank!");
            }
            else
            {
                errorProvider1.SetError(txtConsultationFees, "");
            }

        }

        private void txtConsultationFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح بالأرقام، وأزرار التحكم (زي الباك سبيس)، والعلامة العشرية
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // منع كتابة العلامة العشرية أكتر من مرة في نفس المربع
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
