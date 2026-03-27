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

namespace Clinic.Doctor
{
    public partial class frmDoctorInfo : Form
    {
        private int _DoctorID;
        public bool IsReadOnly { get; set; } = false;
        public frmDoctorInfo(int DoctorID)
        {
            InitializeComponent();
            _DoctorID = DoctorID;
        }

        private void frmFindDoctor_Load(object sender, EventArgs e)
        {
            ctrlDoctorInfo1.LoadDoctorInfo(_DoctorID);
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
