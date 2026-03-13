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

namespace Clinic.Appointment.AppointmentType
{
    public partial class frmListAppointmentType : Form
    {
        private DataTable _dtAllAppointmentType;
        public frmListAppointmentType()
        {
            InitializeComponent();
        }

        private void frmListAppointmentType_Load(object sender, EventArgs e)
        {
            _dtAllAppointmentType = clsAppointmentType.GetAllAppointmentTypes();
            dgvAppointmentType.DataSource = _dtAllAppointmentType;
            lblRecordsCount.Text = dgvAppointmentType.Rows.Count.ToString();

            if(dgvAppointmentType.Rows.Count > 0 )
            {
                dgvAppointmentType.Columns[0].HeaderText = "ID";
                dgvAppointmentType.Columns[0].Width = 110;

                dgvAppointmentType.Columns[1].HeaderText = "Appointment Type Name";
                dgvAppointmentType.Columns[1].Width = 250;

                dgvAppointmentType.Columns[2].HeaderText = "Appointment Type Fees";
                dgvAppointmentType.Columns[2].Width = 200;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditAppointmentType frm = new frmEditAppointmentType((int)dgvAppointmentType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListAppointmentType_Load(null,null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
