using Clinic.People;
using Clinic.People.Controls;
using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Visit
{
    public partial class ctrlVisitInfoWithFilter : UserControl
    {
        //define event 
        public event Action<int> OnVisitSeleted;

        public virtual void VisitSelected(int VisitID)
        {
            Action<int> handler = OnVisitSeleted;
            if (handler != null)
            {
                handler.Invoke(VisitID);
            }
        }

        private bool _ShowAddVisit = true;
        public bool ShowAddVisit
        {
            get { return _ShowAddVisit; }
            set
            {
                _ShowAddVisit = value;
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

        private int _Mode = 0;//0:update, 1:read
        public int Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;
                ctrlVisitInfo1.Mode = _Mode;
            }
        }

        private int _VisitID;
        public int VisitID
        {
            get { return ctrlVisitInfo1.VisitID; }
        }

        public clsVisit SelectedVisitInfo
        {
            get { return ctrlVisitInfo1.SelectedVisitInfo; }
        }

        public ctrlVisitInfoWithFilter()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }

        public void LoadVisitInfo(int VisitID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = VisitID.ToString();
            _FindNow();
        }

        public void LoadVisitInfoByAppointment(int AppointmentID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = AppointmentID.ToString();
            _FindNow();
        }
        private void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Visit ID":
                    ctrlVisitInfo1.LoadVisitInfo(int.Parse(txtFilterValue.Text));
                   
                    break;

                case "Appointment ID":
                    ctrlVisitInfo1.LoadVisitInfoByAppointmentID(int.Parse(txtFilterValue.Text));
                    break;

                default:
                    break;
            }
           
            if ( OnVisitSeleted !=null && FilterEnabled)
                // Raise the event with a parameter
                OnVisitSeleted(ctrlVisitInfo1.VisitID);
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
        private void DataBackEvent(object sender, int VisitID)
        {
            txtFilterValue.Text = VisitID.ToString();
            ctrlVisitInfo1.LoadVisitInfo(VisitID);
        }
    }
}
