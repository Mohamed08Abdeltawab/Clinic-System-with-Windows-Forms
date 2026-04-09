using Clinic.Medical_Services.Visit;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Manage_Prescriptions
{
    public partial class ctrlPrescriptionInfoWithFilter : UserControl
    {
        public event Action<int> OnPrescriptionSeleted;

        public virtual void PrescriptionSelected(int PrescriptionID)
        {
            Action<int> handler = OnPrescriptionSeleted;
            if (handler != null)
            {
                handler.Invoke(PrescriptionID);
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

        private int _PrescriptionID;
        public int PrescriptionID
        {
            get { return ctrlPrescriptionInfo1.PrescriptionID; }
        }

        public clsPrescription SelectedPrescriptionInfo
        {
            get { return ctrlPrescriptionInfo1.SelectedPrescription; }
        }

        private int _Mode = 0;//0:update, 1:read
        public int Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;
                ctrlPrescriptionInfo1.Mode = _Mode;
            }
        }

        public ctrlPrescriptionInfoWithFilter()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }

        public void LoadPrescriptionInfo(int PrescriptionID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PrescriptionID.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Prescription ID":
                    ctrlPrescriptionInfo1.LoadPrescriptionInfoByPrescriptionID(int.Parse(txtFilterValue.Text.Trim()));

                    break;

                case "Visit ID":
                    ctrlPrescriptionInfo1.LoadPrescriptionInfoByVisitID(int.Parse(txtFilterValue.Text.Trim()));
                    break;

                default:
                    break;
            }
            if (OnPrescriptionSeleted != null && FilterEnabled)
                // Raise the event with a parameter
                OnPrescriptionSeleted(ctrlPrescriptionInfo1.PrescriptionID);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
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

        //will use if add button is visible and user added new visit and want to show it in the filter
        private void DataBackEvent(object sender, int PrescriptionID)
        {
            txtFilterValue.Text = PrescriptionID.ToString();
            ctrlPrescriptionInfo1.LoadPrescriptionInfoByPrescriptionID(PrescriptionID);
        }
    }
}
