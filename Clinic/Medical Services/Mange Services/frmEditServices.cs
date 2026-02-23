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

namespace Clinic.Medical_Services.Mange_Services
{
    public partial class frmEditServices : Form
    {
        private int _ServiceID = -1;
        clsService _Service;
        public frmEditServices(int SerivceID)
        {
            InitializeComponent();
            _ServiceID = SerivceID;
        }

        private void frmEditServices_Load(object sender, EventArgs e)
        {
            _Service = clsService.Find(_ServiceID);
            if( _Service == null)
            {
                MessageBox.Show($"Error, there is no Service with ID: {_ServiceID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtServiceName.Text = _Service.ServiceName.ToString();
            txtServicePrice.Text = _Service.ServicePrice.ToString();
            txtServiceDescription.Text = _Service.ServiceDescription.ToString();
        }

        private void txtServiceDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtServiceDescription, "Service Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtServiceDescription, null);
            }
        }

        private void txtServiceName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtServiceName, "Service Name cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtServiceName, null);
            }
        }

        private void txtServicePrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtServicePrice.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtServicePrice, "Service Price cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtServicePrice, null);
            }


            if (!clsValidation.IsNumber(txtServicePrice.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtServicePrice, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtServicePrice, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Service.ServiceName = txtServiceName.Text.Trim();
            _Service.ServiceDescription = txtServiceDescription.Text.Trim();
            _Service.ServicePrice = Convert.ToDecimal(txtServicePrice.Text.Trim());

            if (_Service.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
