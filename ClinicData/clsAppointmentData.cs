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
            ref int AppointmentTypeID, ref decimal AppointmentFees, ref DateTime AppointmentDate,
            ref byte Status, ref int CreatedByUserID)
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
                    isFound = true;
                    PatientID = (int)reader["PatientID"];
                    DoctorID = (int)reader["DoctorID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];

                    // التحديث الجديد: استخدام ID النوع والسعر
                    AppointmentTypeID = (int)reader["AppointmentTypeID"];
                    AppointmentFees = Convert.ToDecimal(reader["AppointmentFees"]);

                    Status = (byte)reader["Status"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { isFound = false; }
            finally { connection.Close(); }

            return isFound;
        }

        public static int AddNewAppointment(int PatientID, int DoctorID, int AppointmentTypeID,
            decimal AppointmentFees, DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // تم تحديث الاستعلام ليشمل الحقول الجديدة
            string query = @"INSERT INTO Appointments (PatientID, DoctorID, AppointmentTypeID, AppointmentFees, AppointmentDate, Status, CreatedByUserID)
                             VALUES (@PatientID, @DoctorID, @AppointmentTypeID, @AppointmentFees, @AppointmentDate, @Status, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientID", PatientID);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);
            command.Parameters.AddWithValue("@AppointmentTypeID", AppointmentTypeID);
            command.Parameters.AddWithValue("@AppointmentFees", AppointmentFees);
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
            catch (Exception ex) { }
            finally { connection.Close(); }

            return AppointmentID;
        }

        public static bool UpdateAppointment(int AppointmentID, int PatientID, int DoctorID, int AppointmentTypeID,
            decimal AppointmentFees, DateTime AppointmentDate, byte Status, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Appointments  
                             set PatientID = @PatientID,
                                 DoctorID = @DoctorID,
                                 AppointmentTypeID = @AppointmentTypeID,
                                 AppointmentFees = @AppointmentFees,
                                 AppointmentDate = @AppointmentDate,
                                 Status = @Status,
                                 CreatedByUserID = @CreatedByUserID
                             where AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientID", PatientID);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);
            command.Parameters.AddWithValue("@AppointmentTypeID", AppointmentTypeID);
            command.Parameters.AddWithValue("@AppointmentFees", AppointmentFees);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // التحديث الاحترافي: الربط مع جدول AppointmentType الجديد لجلب الاسم
            string query = @"SELECT 
                 Apps.AppointmentID, 
                 Apps.PatientID, 
                 Apps.DoctorID,
                 PatientName = PeoplePat.FullName, 
                 DoctorName = PeopleDoc.FullName,
                 Types.AppointmentTypeName,
                 Apps.AppointmentFees,
                 Apps.AppointmentDate,
                 Status = CASE 
                    WHEN Apps.Status = 1 THEN 'Scheduled'
                    WHEN Apps.Status = 2 THEN 'Cancelled'
                    WHEN Apps.Status = 3 THEN 'Completed'
                    ELSE 'Unknown'
                 END,
                 Apps.CreatedByUserID
                 FROM Appointments Apps
                 INNER JOIN Patients ON Apps.PatientID = Patients.PatientID
                 INNER JOIN People PeoplePat ON Patients.PersonID = PeoplePat.PersonID
                 INNER JOIN Doctors ON Apps.DoctorID = Doctors.DoctorID
                 INNER JOIN People PeopleDoc ON Doctors.PersonID = PeopleDoc.PersonID
                 INNER JOIN AppointmentType Types ON Apps.AppointmentTypeID = Types.AppointmentTypeID
                 ORDER BY Apps.AppointmentDate DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

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

        public static bool IsAppointmentExistByPatientIDandDoctorID(int PatientID, int DoctorID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Appointments WHERE PatientID = @PatientID and DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientID", PatientID);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
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