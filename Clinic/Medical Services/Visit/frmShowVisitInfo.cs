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
        public frmShowVisitInfo(int VisitID)
        {
            ctrlVisitInfoWithFilter1.LoadVisitInfo(VisitID);  
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
