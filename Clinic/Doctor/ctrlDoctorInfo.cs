using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinicbusiness;

namespace Clinic.Doctor
{
    public partial class ctrlDoctorInfo : UserControl
    {
        private clsDoctor _Doctor;
        private int _DoctorID;

        //need public id
        public int DoctorID
        {
            get { return _DoctorID; }
        }
        public ctrlDoctorInfo()
        {
            InitializeComponent();
        }

        public void LoadDoctorInfo(int DoctorID)
        {
            _Doctor = clsDoctor.Find(DoctorID);
            if( _Doctor == null )
            {
                _ResetPersonInfo();
                MessageBox.Show($"No Doctor with ID: {DoctorID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillDoctorInfo();
        }

        private void _FillDoctorInfo()
        {

            ctrlPersonCard1.LoadInfo(_Doctor.PersonID);
            lblDoctorID.Text = _Doctor.DoctorID.ToString();
            lblConsultationFees.Text = _Doctor.ConsultationFees.ToString();
            lblSpecialization.Text = _Doctor.Specialization.ToString();
            lblWorkingDays.Text = _Doctor.WorkingDays.ToString();

        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblDoctorID.Text = "[???]";
            lblConsultationFees.Text = "[???]";
            lblSpecialization.Text = "[???]";
            lblWorkingDays.Text = "[???]";
        }
    }
}
