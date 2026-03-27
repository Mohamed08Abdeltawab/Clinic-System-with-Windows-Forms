using Clinic.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Patient
{
    public partial class frmPatientInfo : Form
    {
        private int _PatientID;
        public bool IsReadOnly { get; set; } = false;

        public frmPatientInfo(int PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;


        }

        private void frmPatientInfo_Load(object sender, EventArgs e)
        {
            ctrlPatientInfo1.LoadPatientInfo(_PatientID);
            if (IsReadOnly)
            {
                clsGlobal.SetControlsReadOnly(this, true);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
