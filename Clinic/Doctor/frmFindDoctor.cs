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
    public partial class frmFindDoctor : Form
    {
        private int _DoctorID;
        public frmFindDoctor(int DoctorID)
        {
            InitializeComponent();
            _DoctorID = DoctorID;
        }

        private void frmFindDoctor_Load(object sender, EventArgs e)
        {
            ctrlDoctorInfo1.LoadDoctorInfo(_DoctorID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
