using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo()
        {
            InitializeComponent();
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadInfo(1);
        }
    }
}
