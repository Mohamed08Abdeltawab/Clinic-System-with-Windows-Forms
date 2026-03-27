using Clinicbusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Medicine
{
    public partial class frmAddUpdateMedicineToPrescription : Form
    {

        public delegate void DataBackEventHandler(object sender, clsPrescriptionItem Item);
        public event DataBackEventHandler DataBack;

        private clsPrescriptionItem _Item;
        public frmAddUpdateMedicineToPrescription()
        {
            InitializeComponent();
            _Item = new clsPrescriptionItem();
        }

        public frmAddUpdateMedicineToPrescription(clsPrescriptionItem Item)
        {
            InitializeComponent();
            _Item = Item;
        }

        //fill combo box of Medicne
        private void _FillMedicinesComboBox()
        {
            DataTable dt = clsMedicine.GetAllMedicines();
            foreach(DataRow row in dt.Rows)
            {
                cbMedicines.Items.Add(row["MedicineName"]);
            }
        }

        private void _LoadData()
        {
            cbMedicines.SelectedIndex = cbMedicines.FindString(_Item.MedicineName);
            txtQuantity.Text = _Item.Quantity.ToString();
            txtDosage.Text = _Item.Dosage;
            txtInstructions.Text = _Item.Instructions;
        }

        private void frmAddUpdateMedicineToPrescription_Load(object sender, EventArgs e)
        {
            _FillMedicinesComboBox();

            if(_Item.ItemID == -1)
            {
                MessageBox.Show("")
            }
        }
    }
}
