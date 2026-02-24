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

namespace Clinic.Medical_Services.Mange_Services
{
    public partial class frmListServices : Form
    {
        private DataTable _dtAllServices;
        public frmListServices()
        {
            InitializeComponent();
        }

        private void frmListServices_Load(object sender, EventArgs e)
        {
            _dtAllServices = clsService.GetAllServices();
            dgvServices.DataSource = _dtAllServices;
            lblRecordsCount.Text = dgvServices.Rows.Count.ToString();

            dgvServices.Columns[0].HeaderText = "ID";
            dgvServices.Columns[0].Width = 70;

            dgvServices.Columns[1].HeaderText = "Service Name";
            dgvServices.Columns[1].Width = 150;

            dgvServices.Columns[2].HeaderText = "Service Description";
            dgvServices.Columns[2].Width = 310;

            dgvServices.Columns[3].HeaderText = "Service Price";
            dgvServices.Columns[3].Width = 120;

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditServices frm = new frmEditServices((int)dgvServices.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListServices_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
