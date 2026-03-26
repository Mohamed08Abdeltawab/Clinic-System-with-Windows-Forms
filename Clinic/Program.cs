using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic.Appointment;
using Clinic.Appointment.AppointmentType;
using Clinic.Doctor;
using Clinic.Login;
using Clinic.Medical_Services.Manage_Prescriptions;
using Clinic.Medical_Services.Mange_Services;
using Clinic.Medical_Services.Visit;
using Clinic.Patient;
using Clinic.People;
using Clinic.User;

namespace Clinic
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmListPrescriptions());
        }
    }
}
