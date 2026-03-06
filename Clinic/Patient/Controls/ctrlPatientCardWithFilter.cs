using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.People.Controls;
using Clinicbusiness;

namespace Clinic.Patient
{
    public partial class ctrlPatientCardWithFilter : UserControl
    {
        public event Action<int> OnPatientSelected;

        public virtual void PersonSelected(int PatientID)
        {
            Action<int> handler = OnPatientSelected;
            if (handler != null)
            {
                handler.Invoke(PatientID);
            }
        }

        private bool _ShowAddPatient = true;
        public bool ShowAddPatient
        {
            get { return _ShowAddPatient; }
            set
            {
                _ShowAddPatient = value;
                btnAddNewPatient.Visible = _ShowAddPatient;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public ctrlPatientCardWithFilter()
        {
            InitializeComponent();
        }


        private int _PatientID = -1;
        public int PatientID
        {
            get { return ctrlPatientInfo1.PatientID; }
        }

        public clsPatient SelectedPatientInfo
        {
            get { return ctrlPatientInfo1.SelectedPatientInfo; }
        }

        public void LoadPatientData(int PatientID)
        {
            txtFilterValue.Text = PatientID.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            ctrlPatientInfo1.LoadPatientInfo(int.Parse(txtFilterValue.Text));

            if (OnPatientSelected != null && FilterEnabled)
                OnPatientSelected.Invoke(ctrlPatientInfo1.PatientID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please correct the errors and try again", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Please enter a value to search");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, string.Empty);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnFind.PerformClick();
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void btnAddNewPatient_Click(object sender, EventArgs e)
        {
            frmAddUpdatePatient frm = new frmAddUpdatePatient();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PatientID)
        {
            txtFilterValue.Text = PatientID.ToString();
            ctrlPatientInfo1.LoadPatientInfo(PatientID);
        }
    }
}
