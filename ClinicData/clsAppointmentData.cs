using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsAppointmentData
    {

        public static bool GetAppointmentInfoByID(int AppointmentID, ref int PatientID, ref int DoctorID,
            ref byte AppointmentType, ref DateTime AppointmentDate, ref byte Status, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PatientID = (int)reader["PatientID"];
                    DoctorID = (int)reader["DoctorID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];

                    // tinyint in SQL maps to byte in C#
                    AppointmentType = (byte)reader["AppointmentType"]; // New Column
                    Status = (byte)reader["Status"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewAppointment(int PatientID, int DoctorID, byte AppointmentType,
            DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            //this function will return the new appointment id if succeeded and -1 if not.
            int AppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Appointments (PatientID, DoctorID, AppointmentType, AppointmentDate, Status, CreatedByUserID)
                             VALUES (@PatientID, @DoctorID, @AppointmentType, @AppointmentDate, @Status, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", PatientID);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);
            command.Parameters.AddWithValue("@AppointmentType", AppointmentType); // New Parameter
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    AppointmentID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return AppointmentID;
        }

        public static bool UpdateAppointment(int AppointmentID, int PatientID, int DoctorID, byte AppointmentType,
            DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Appointments  
                             set PatientID = @PatientID,
                                 DoctorID = @DoctorID,
                                 AppointmentType = @AppointmentType,
                                 AppointmentDate = @AppointmentDate,
                                 Status = @Status,
                                 CreatedByUserID = @CreatedByUserID
                             where AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", PatientID);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);
            command.Parameters.AddWithValue("@AppointmentType", AppointmentType); // New Parameter
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // تم تحديث الاستعلام ليشمل نوع الموعد (AppointmentType)
            string query = @"SELECT Appointments.AppointmentID, 
                             Appointments.AppointmentDate,
                             PatientName = PeoplePat.FullName,
                             DoctorName = PeopleDoc.FullName,
                             Appointments.AppointmentType,
                             CASE 
                                WHEN Appointments.AppointmentType = 1 THEN 'Checkup'
                                WHEN Appointments.AppointmentType = 2 THEN 'Consultation'
                                ELSE 'Unknown'
                             END as AppointmentTypeCaption,
                             Appointments.Status,
                             CASE 
                                WHEN Appointments.Status = 1 THEN 'Scheduled'
                                WHEN Appointments.Status = 2 THEN 'Cancelled'
                                WHEN Appointments.Status = 3 THEN 'Completed'
                                ELSE 'Unknown'
                             END as StatusCaption
                             FROM Appointments 
                             INNER JOIN Patients ON Appointments.PatientID = Patients.PetientID
                             INNER JOIN People PeoplePat ON Patients.PersonID = PeoplePat.PersonID
                             INNER JOIN Doctors ON Appointments.DoctorID = Doctors.DoctorID
                             INNER JOIN People PeopleDoc ON Doctors.PersonID = PeopleDoc.PersonID
                             ORDER BY Appointments.AppointmentDate DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static DataTable GetAppointmentsByPatientID(int PatientID)
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Appointments WHERE PatientID = @PatientID ORDER BY AppointmentDate DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientID", PatientID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }


        public static DataTable GetAppointmentsByDoctorID(int DoctorID)
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Appointments WHERE DoctorID = @DoctorID ORDER BY AppointmentDate DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", DoctorID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // يمكن تسجيل الخطأ هنا
            }

            return dt;
        }


        public static bool DeleteAppointment(int AppointmentID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Appointments 
                             where AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsAppointmentExist(int AppointmentID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Appointments WHERE AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}