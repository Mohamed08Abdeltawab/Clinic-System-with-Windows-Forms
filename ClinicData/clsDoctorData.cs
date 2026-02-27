using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsDoctorData
    {
        public static bool GetDoctorInfoByID(int DoctorID, ref int PersonID, ref string Specialization,
            ref decimal ConsultationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    Specialization = (string)reader["Specialization"];
                    ConsultationFees = (decimal)reader["ConsultationFees"];
                    // تم إزالة WorkingDays من هنا
                }
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

        public static bool GetDoctorInfoByPersonID(int PersonID, ref int DoctorID, ref string Specialization,
            ref decimal ConsultationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Doctors WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    DoctorID = (int)reader["DoctorID"];
                    Specialization = (string)reader["Specialization"];
                    ConsultationFees = (decimal)reader["ConsultationFees"];
                    // تم إزالة WorkingDays من هنا
                }
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

        public static int AddNewDoctor(int PersonID, string Specialization, decimal ConsultationFees)
        {
            int DoctorID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Doctors (PersonID, Specialization, ConsultationFees)
                             VALUES (@PersonID, @Specialization, @ConsultationFees);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Specialization", Specialization);
            command.Parameters.AddWithValue("@ConsultationFees", ConsultationFees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DoctorID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return DoctorID;
        }

        public static bool UpdateDoctor(int DoctorID, int PersonID, string Specialization, decimal ConsultationFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Doctors  
                             set PersonID = @PersonID,
                                 Specialization = @Specialization,
                                 ConsultationFees = @ConsultationFees
                             where DoctorID = @DoctorID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Specialization", Specialization);
            command.Parameters.AddWithValue("@ConsultationFees", ConsultationFees);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllDoctors()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // تم إزالة عمود WorkingDays من الاستعلام
            string query = @"SELECT Doctors.DoctorID, Doctors.PersonID,
                             People.FullName,
                             Doctors.Specialization,
                             Doctors.ConsultationFees
                             FROM Doctors INNER JOIN
                                  People ON Doctors.PersonID = People.PersonID";

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
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool DeleteDoctor(int DoctorID)
        {
            int rowsAffected = 0;

            // 🌟 خطوة هامة جداً: مسح أيام عمل الطبيب أولاً من الجدول الوسيط لمنع خطأ الـ Foreign Key
            DeleteDoctorWorkingDays(DoctorID);

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete Doctors where DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsDoctorExist(int DoctorID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Doctors WHERE DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);
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

        public static bool IsDoctorExistByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Doctors WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        // ===================================================================
        // 🌟 الدوال الجديدة الخاصة بجدول أيام عمل الأطباء (DoctorWorkingDays)
        // ===================================================================

        public static bool AddDoctorWorkingDay(int DoctorID, byte DayID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO DoctorWorkingDays (DoctorID, DayID) VALUES (@DoctorID, @DayID)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorID", DoctorID);
            command.Parameters.AddWithValue("@DayID", DayID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static bool DeleteDoctorWorkingDays(int DoctorID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM DoctorWorkingDays WHERE DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { return false; }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static DataTable GetDoctorWorkingDays(int DoctorID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // نجلب بيانات الأيام المرتبطة بهذا الطبيب
            string query = @"SELECT DaysOfWeek.DayID, DaysOfWeek.DayName 
                             FROM DoctorWorkingDays 
                             INNER JOIN DaysOfWeek ON DoctorWorkingDays.DayID = DaysOfWeek.DayID
                             WHERE DoctorID = @DoctorID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorID", DoctorID);

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
            catch (Exception ex) { }
            finally { connection.Close(); }

            return dt;
        }
    }
}