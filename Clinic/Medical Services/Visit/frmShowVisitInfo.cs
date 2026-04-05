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
        public frmShowVisitInfo()
        {
            InitializeComponent();
        }

        public void LoadVisitInfoByVisitID(int VisitID)
        {
            ctrlVisitInfoWithFilter1.LoadVisitInfo(VisitID);
        }
        public void LoadVisitInfoByAppointmentID(int AppointmentID)
        {
            ctrlVisitInfoWithFilter1.LoadVisitInfoByAppointment(AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
