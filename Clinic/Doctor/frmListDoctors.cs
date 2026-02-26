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
    public partial class frmListDoctors : Form
    {
        private DataTable _dtDoctors;
        public frmListDoctors()
        {
            InitializeComponent();
        }

        private void frmListDoctors_Load(object sender, EventArgs e)
        {
            _dtDoctors = clsDoctor.GetAllDoctors();
            dgvDoctors.DataSource = _dtDoctors;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();


            if(dgvDoctors.Rows.Count > 0 )
            {
                dgvDoctors.Columns[0].HeaderText = "Docotr ID";
                dgvDoctors.Columns[0].Width = 110;
                
                dgvDoctors.Columns[1].HeaderText = "Person ID";
                dgvDoctors.Columns[1].Width = 110;
                
                
                dgvDoctors.Columns[2].HeaderText = "Full Name";
                dgvDoctors.Columns[2].Width = 320;
                
                dgvDoctors.Columns[3].HeaderText = "Specialization";
                dgvDoctors.Columns[3].Width = 170;
                
                
                dgvDoctors.Columns[4].HeaderText = "Consultation Fees";
                dgvDoctors.Columns[4].Width = 130;
                
                dgvDoctors.Columns[5].HeaderText = "WorkingDays";
                dgvDoctors.Columns[5].Width = 200;
                
                
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Doctor ID":
                    FilterColumn = "DoctorID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Specialization":
                    FilterColumn = "Specialization";
                    break;

                case "Consultation Fees":
                    FilterColumn = "ConsultationFees";
                    break;

                case "Working Days":
                    FilterColumn = "WorkingDays";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDoctors.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID" || FilterColumn == "DoctorID")
                //in this case we deal with integer not string.
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDoctors.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvDoctors.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor();
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            frmDoctorInfo frm = new frmDoctorInfo(PersonID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor();
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            frmAddUpdateDoctor frm = new frmAddUpdateDoctor(PersonID);
            frm.ShowDialog();
            frmListDoctors_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            int DoctorID = (int)dgvDoctors.CurrentRow.Cells[0].Value;
            if(MessageBox.Show("Are you sure you want delete this Doctor!","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsDoctor.DeleteDoctor(DoctorID))
                {
                    MessageBox.Show("Doctor has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListDoctors_Load(null, null);
                }

                else
                    MessageBox.Show("Doctor is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.SelectedIndex == 1 ||  cbFilterBy.SelectedIndex == 2)//1 -> person id , 2 -> doctor id
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        
    }
}
