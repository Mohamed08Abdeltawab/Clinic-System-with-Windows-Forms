using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinicbusiness;

namespace Clinic.Doctor
{
    public partial class ctrlDoctorCardWithFilter : UserControl
    {
        // define event 
        public event Action<int> OnDoctorSelected;

        public virtual void DoctorSelected(int DoctorID)
        {
            Action<int> handler = OnDoctorSelected;
            if (handler != null)
            {
                handler.Invoke(DoctorID);
            }
        }

        private bool _ShowAddDoctor = true;
        public bool ShowAddDoctor
        {
            get { return _ShowAddDoctor; }
            set
            {
                _ShowAddDoctor = value;
                btnAddNewDoctor.Visible = _ShowAddDoctor;
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


        public string DoctorWorkingDays
        {
            get { return ctrlDoctorInfo1.DoctorWorkingDays; }
        }

        public ctrlDoctorCardWithFilter()
        {
            InitializeComponent();
        }

        private int _DoctorID;
        public int DoctorID
        {
            get { return ctrlDoctorInfo1.DoctorID; }
        }

        public clsDoctor SelectedDoctorInfo
        {
            get { return ctrlDoctorInfo1.SelectedDoctorInfo; }
        }

        public void LoadDoctorData(int DoctorID)
        {
            txtFilterValue.Text = DoctorID.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            ctrlDoctorInfo1.LoadDoctorInfo(int.Parse(txtFilterValue.Text));

            if (OnDoctorSelected != null && FilterEnabled)
                OnDoctorSelected.Invoke(ctrlDoctorInfo1.DoctorID);
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

        private void btnAddNewDoctor_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int DoctorID)
        {
            txtFilterValue.Text = DoctorID.ToString();
            ctrlDoctorInfo1.LoadDoctorInfo(DoctorID);
        }

    }
}
