using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Manage_Prescriptions
{
    public partial class frmFindPrescription : Form
    {
        public delegate void DataBackEventHandler(object sender, int PrescriptionID);
        public event DataBackEventHandler DataBack;
        public frmFindPrescription()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, ctrlPrescriptionInfoWithFilter1.PrescriptionID);
            this.Close();
        }
    }
}
