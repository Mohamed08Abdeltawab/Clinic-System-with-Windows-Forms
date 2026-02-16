using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicData;

namespace ClinicData
{
    public class clsPatientData
    {
        public static bool GetPatientInfoByID(int PatientID, ref int PersonID, ref string MedicalHistory,
            ref string BloodType, ref string EmergencyContact)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Assuming column name is corrected to 'PatientID', if not use 'PetientID'
            string query = "SELECT * FROM Patients WHERE PatientID = @PatientID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", PatientID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    
                    // Handling Nullable Strings
                    MedicalHistory = (reader["MedicalHistory"] != DBNull.Value) ? (string)reader["MedicalHistory"] : "";
                    BloodType = (reader["BloodType"] != DBNull.Value) ? (string)reader["BloodType"] : "";
                    
                    // EmergencyContact is varbinary in DB, but usually treated as string in logic if it's a phone number.
                    // If you store it as text, cast to string. If binary, you need byte[].
                    // Here I assume you changed it to nvarchar based on our previous discussion, or I'll treat it as string for now.
                    // If it is truly varbinary(MAX) for a file/image, change this parameter to byte[].
                    if (reader["EmergencyContact"] != DBNull.Value)
                    {
                         // Warning: Direct cast only works if DB type matches. 
                         // If DB is varbinary, use: (byte[])reader["EmergencyContact"]
                         // I will assume it is string (nvarchar) for phone number context.
                         EmergencyContact = reader["EmergencyContact"].ToString(); 
                    }
                    else
                    {
                        EmergencyContact = "";
                    }
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

        public static bool GetPatientInfoByPersonID(int PersonID, ref int PatientID, ref string MedicalHistory,
            ref string BloodType, ref string EmergencyContact)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Patients WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PatientID = (int)reader["PatientID"]; // Or PetientID
                    
                    MedicalHistory = (reader["MedicalHistory"] != DBNull.Value) ? (string)reader["MedicalHistory"] : "";
                    BloodType = (reader["BloodType"] != DBNull.Value) ? (string)reader["BloodType"] : "";
                    
                    if (reader["EmergencyContact"] != DBNull.Value)
                        EmergencyContact = reader["EmergencyContact"].ToString();
                    else
                        EmergencyContact = "";
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

        public static int AddNewPatient(int PersonID, string MedicalHistory, string BloodType, string EmergencyContact)
        {
            int PatientID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Patients (PersonID, MedicalHistory, BloodType, EmergencyContact)
                             VALUES (@PersonID, @MedicalHistory, @BloodType, @EmergencyContact);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            
            if (!string.IsNullOrEmpty(MedicalHistory))
                command.Parameters.AddWithValue("@MedicalHistory", MedicalHistory);
            else
                command.Parameters.AddWithValue("@MedicalHistory", DBNull.Value);

            if (!string.IsNullOrEmpty(BloodType))
                command.Parameters.AddWithValue("@BloodType", BloodType);
            else
                command.Parameters.AddWithValue("@BloodType", DBNull.Value);

            if (!string.IsNullOrEmpty(EmergencyContact))
                command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact); // Ensure DB column type matches
            else
                command.Parameters.AddWithValue("@EmergencyContact", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PatientID = insertedID;
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

            return PatientID;
        }

        public static bool UpdatePatient(int PatientID, int PersonID, string MedicalHistory, string BloodType, string EmergencyContact)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Patients  
                             set PersonID = @PersonID,
                                 MedicalHistory = @MedicalHistory,
                                 BloodType = @BloodType,
                                 EmergencyContact = @EmergencyContact
                             where PatientID = @PatientID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@PatientID", PatientID);

            if (!string.IsNullOrEmpty(MedicalHistory))
                command.Parameters.AddWithValue("@MedicalHistory", MedicalHistory);
            else
                command.Parameters.AddWithValue("@MedicalHistory", DBNull.Value);

            if (!string.IsNullOrEmpty(BloodType))
                command.Parameters.AddWithValue("@BloodType", BloodType);
            else
                command.Parameters.AddWithValue("@BloodType", DBNull.Value);

            if (!string.IsNullOrEmpty(EmergencyContact))
                command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact);
            else
                command.Parameters.AddWithValue("@EmergencyContact", DBNull.Value);

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

        public static DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Added new columns to select query
            string query = @"SELECT Patients.PatientID, Patients.PersonID,
                             People.FullName,
                             Patients.MedicalHistory,
                             Patients.BloodType,
                             Patients.EmergencyContact
                             FROM Patients INNER JOIN
                                  People ON Patients.PersonID = People.PersonID";

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

        public static bool DeletePatient(int PatientID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete Patients where PatientID = @PatientID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientID", PatientID);

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

        public static bool IsPatientExist(int PatientID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Patients WHERE PatientID = @PatientID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PatientID", PatientID);

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

        public static bool IsPatientExistByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Patients WHERE PersonID = @PersonID";
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
    }
}