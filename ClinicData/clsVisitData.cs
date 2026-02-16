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

            string query = "SELECT * FROM Visits WHERE VisitID = @VisitID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                AppointmentID = (int)reader["AppointmentID"];
                                VisitDate = (DateTime)reader["VisitDate"];
                                Diagnosis = (string)reader["Diagnosis"];

                                // Handle Nullable Notes
                                if (reader["Notes"] != DBNull.Value)
                                    Notes = (string)reader["Notes"];
                                else
                                    Notes = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewVisit(int AppointmentID, DateTime VisitDate,
            string Diagnosis, string Notes)
        {
            int VisitID = -1;

            string query = @"INSERT INTO Visits (AppointmentID, VisitDate, Diagnosis, Notes)
                             VALUES (@AppointmentID, @VisitDate, @Diagnosis, @Notes);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@VisitDate", VisitDate);
                        command.Parameters.AddWithValue("@Diagnosis", Diagnosis);

                        if (!string.IsNullOrEmpty(Notes))
                            command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            VisitID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log Error
            }

            return VisitID;
        }

        public static bool UpdateVisit(int VisitID, int AppointmentID, DateTime VisitDate,
            string Diagnosis, string Notes)
        {
            int rowsAffected = 0;

            string query = @"Update Visits  
                             set AppointmentID = @AppointmentID,
                                 VisitDate = @VisitDate,
                                 Diagnosis = @Diagnosis,
                                 Notes = @Notes
                             where VisitID = @VisitID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                        command.Parameters.AddWithValue("@VisitDate", VisitDate);
                        command.Parameters.AddWithValue("@Diagnosis", Diagnosis);

                        if (!string.IsNullOrEmpty(Notes))
                            command.Parameters.AddWithValue("@Notes", Notes);
                        else
                            command.Parameters.AddWithValue("@Notes", DBNull.Value);

                        command.Parameters.AddWithValue("@VisitID", VisitID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllVisits()
        {
            DataTable dt = new DataTable();

            // تم تصحيح PatientID هنا بافتراض أنك قمت بتعديل الاسم في قاعدة البيانات
            // إذا كان لا يزال PetientID، يرجى تعديل الكلمة أدناه
            string query = @"SELECT Visits.VisitID, Visits.VisitDate, 
                             PatientName = PeoplePat.FullName,
                             DoctorName = PeopleDoc.FullName,
                             Visits.Diagnosis, Visits.Notes,
                             Visits.AppointmentID
                             FROM Visits 
                             INNER JOIN Appointments ON Visits.AppointmentID = Appointments.AppointmentID
                             INNER JOIN Patients ON Appointments.PatientID = Patients.PatientID
                             INNER JOIN People PeoplePat ON Patients.PersonID = PeoplePat.PersonID
                             INNER JOIN Doctors ON Appointments.DoctorID = Doctors.DoctorID
                             INNER JOIN People PeopleDoc ON Doctors.PersonID = PeopleDoc.PersonID
                             ORDER BY Visits.VisitDate DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
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
                // Log Error
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
                                isFound = true;
                                VisitID = (int)reader["VisitID"];
                                VisitDate = (DateTime)reader["VisitDate"];
                                Diagnosis = (string)reader["Diagnosis"];

                                if (reader["Notes"] != DBNull.Value)
                                    Notes = (string)reader["Notes"];
                                else
                                    Notes = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool DeleteVisit(int VisitID)
        {
            int rowsAffected = 0;

            string query = @"Delete Visits where VisitID = @VisitID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Error
            }

            return (rowsAffected > 0);
        }

        public static bool IsVisitExist(int VisitID)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Visits WHERE VisitID = @VisitID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool IsVisitExistByAppointmentID(int AppointmentID)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Visits WHERE AppointmentID = @AppointmentID";

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
                            isFound = reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
    }
}