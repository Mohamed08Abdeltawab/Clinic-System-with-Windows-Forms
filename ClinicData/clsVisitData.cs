using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsVisitData
    {

        public static bool GetVisitInfoByID(int VisitID, ref int AppointmentID, ref DateTime VisitDate,
            ref string Diagnosis, ref string Notes)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Visits WHERE VisitID = @VisitID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    AppointmentID = (int)reader["AppointmentID"];
                    VisitDate = (DateTime)reader["VisitDate"];
                    Diagnosis = (string)reader["Diagnosis"];

                    // Notes allows null in database, so we must handle it
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }
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

        public static int AddNewVisit(int AppointmentID, DateTime VisitDate,
            string Diagnosis, string Notes)
        {
            //this function will return the new visit id if succeeded and -1 if not.
            int VisitID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Visits (AppointmentID, VisitDate, Diagnosis, Notes)
                             VALUES (@AppointmentID, @VisitDate, @Diagnosis, @Notes);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            command.Parameters.AddWithValue("@VisitDate", VisitDate);
            command.Parameters.AddWithValue("@Diagnosis", Diagnosis);

            // Handle Null for Notes
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    VisitID = insertedID;
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

            return VisitID;
        }

        public static bool UpdateVisit(int VisitID, int AppointmentID, DateTime VisitDate,
            string Diagnosis, string Notes)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Visits  
                             set AppointmentID = @AppointmentID,
                                 VisitDate = @VisitDate,
                                 Diagnosis = @Diagnosis,
                                 Notes = @Notes
                             where VisitID = @VisitID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            command.Parameters.AddWithValue("@VisitDate", VisitDate);
            command.Parameters.AddWithValue("@Diagnosis", Diagnosis);

            // Handle Null for Notes
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@VisitID", VisitID);

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

        public static DataTable GetAllVisits()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // قمت بعمل Join مع جدول Appointments ومنه إلى Patients و Doctors
            // لعرض بيانات مفيدة (مثل اسم المريض والطبيب) بدلاً من عرض الأرقام فقط.
            string query = @"SELECT Visits.VisitID, Visits.VisitDate, 
                             PatientName = PeoplePat.FullName,
                             DoctorName = PeopleDoc.FullName,
                             Visits.Diagnosis, Visits.Notes,
                             Visits.AppointmentID
                             FROM Visits 
                             INNER JOIN Appointments ON Visits.AppointmentID = Appointments.AppointmentID
                             INNER JOIN Patients ON Appointments.PatientID = Patients.PetientID
                             INNER JOIN People PeoplePat ON Patients.PersonID = PeoplePat.PersonID
                             INNER JOIN Doctors ON Appointments.DoctorID = Doctors.DoctorID
                             INNER JOIN People PeopleDoc ON Doctors.PersonID = PeopleDoc.PersonID
                             ORDER BY Visits.VisitDate DESC";

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

        public static bool GetVisitInfoByAppointmentID(int AppointmentID, ref int VisitID,
                ref DateTime VisitDate, ref string Diagnosis, ref string Notes)
        {
            bool isFound = false;

            string query = "SELECT * FROM Visits WHERE AppointmentID = @AppointmentID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                VisitID = (int)reader["VisitID"];
                                VisitDate = (DateTime)reader["VisitDate"];
                                Diagnosis = (string)reader["Diagnosis"];

                                // معالجة القيمة Null في عمود الملاحظات
                                if (reader["Notes"] != DBNull.Value)
                                {
                                    Notes = (string)reader["Notes"];
                                }
                                else
                                {
                                    Notes = "";
                                }
                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }

            return isFound;
        }

        public static bool DeleteVisit(int VisitID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Visits 
                             where VisitID = @VisitID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);

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

        public static bool IsVisitExist(int VisitID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Visits WHERE VisitID = @VisitID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);

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

        public static bool IsVisitExistByAppointmentID(int AppointmentID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Visits WHERE AppointmentID = @AppointmentID";

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