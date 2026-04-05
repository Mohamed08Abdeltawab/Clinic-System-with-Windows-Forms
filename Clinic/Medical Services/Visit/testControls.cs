using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic.Medical_Services.Visit
{
    public partial class testControls : Form
    {
        public testControls()
        {
            InitializeComponent();
        }

        private void testControls_Load(object sender, EventArgs e)
        {
            ctrlVisitInfo1.LoadVisitInfo(1);
        }
    }
}
