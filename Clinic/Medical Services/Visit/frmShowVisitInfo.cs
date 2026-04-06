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
    public partial class frmShowVisitInfo : Form
    {
        int _Mode = 1;//1:Visit details, 2:Appointment details
        public frmShowVisitInfo(int mode,int VisitAppointmentID)
        {
            InitializeComponent();
            if(mode == 1)
            {
                _LoadVisitInfoByVisitID(VisitAppointmentID);
            }
            else if(mode == 2)
            {
                _LoadVisitInfoByAppointmentID(VisitAppointmentID);
            }
            else
            {
                MessageBox.Show("Invalid mode specified for frmShowVisitInfo. Mode should be either 1 (Visit details) or 2 (Appointment details).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _LoadVisitInfoByVisitID(int VisitID)
        {
            ctrlVisitInfoWithFilter1.LoadVisitInfo(VisitID);
        }
        private void _LoadVisitInfoByAppointmentID(int AppointmentID)
        {
            ctrlVisitInfoWithFilter1.LoadVisitInfoByAppointment(AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowVisitInfo_Load(object sender, EventArgs e)
        {
            ctrlVisitInfoWithFilter1.FilterEnabled = false;
            ctrlVisitInfoWithFilter1.Mode = 1;//Read-Only Mode
        }
    }
}
