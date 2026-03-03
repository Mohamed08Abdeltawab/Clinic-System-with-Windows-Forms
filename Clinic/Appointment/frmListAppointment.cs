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

namespace Clinic.Appointment
{
    public partial class frmListAppointment : Form
    {
        private DataTable _dtAppointments;
        public frmListAppointment()
        {
            InitializeComponent();
        }

        private void frmListAppointment_Load(object sender, EventArgs e)
        {
            _dtAppointments = clsAppointment.GetAllAppointments();
            dgvAppointment.DataSource = _dtAppointments;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvAppointment.Rows.Count.ToString();

            if (dgvAppointment.Rows.Count > 0)
            {
                dgvAppointment.Columns[0].HeaderText = "Appointment ID";
                dgvAppointment.Columns[0].Width = 110;

                dgvAppointment.Columns[1].HeaderText = "Patient Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[1].Width = 220;

                dgvAppointment.Columns[2].HeaderText = "Doctor Name"; // تعديل من ID لـ Name
                dgvAppointment.Columns[2].Width = 220;

                dgvAppointment.Columns[3].HeaderText = "Type";
                dgvAppointment.Columns[3].Width = 170;

                dgvAppointment.Columns[4].HeaderText = "Appointment Date";
                dgvAppointment.Columns[4].Width = 220;

                dgvAppointment.Columns[5].HeaderText = "Status";
                dgvAppointment.Columns[5].Width = 170;

                dgvAppointment.Columns[6].HeaderText = "Created By UserID";
                dgvAppointment.Columns[6].Width = 110;
            }
        }
    }
}
