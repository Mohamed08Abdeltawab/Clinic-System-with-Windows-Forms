using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Doctor
{
    public partial class frmAddUpdateDoctor : Form
    {
        public frmAddUpdateDoctor()
        {
            InitializeComponent();
        }

        private void chkWorkingDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkWorkingDays.ClearSelected();
        }

        private void txtSpecialization_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtConsultationFees_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
