using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.People.Controls;

namespace Clinic.Patient
{
    public partial class frmFindPatient : Form
    {
        // declare a delegate
        public delegate void DataBackEventHandler(object sender, int PatientID);
        public event DataBackEventHandler DataBack;

        public frmFindPatient()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, ctrlPatientCardWithFilter1.PatientID);
            this.Close();
        }
    }
}
