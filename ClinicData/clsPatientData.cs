using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsPatientData
    {
        public static bool GetPatientInfoByID(int PatientID, ref int PersonID, ref string MedicalHistory,
            ref byte BloodType, ref string EmergencyContact)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
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

                    // التعامل مع الحقول التي تقبل Null
                    MedicalHistory = (reader["MedicalHistory"] != DBNull.Value) ? (string)reader["MedicalHistory"] : "";

                    // تحويل tinyint إلى byte، وإذا كان Null نعطيه 0 (Unknown)
                    BloodType = (reader["BloodType"] != DBNull.Value) ? Convert.ToByte(reader["BloodType"]) : (byte)0;

                    EmergencyContact = (string)reader["EmergencyContact"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPatientInfoByPersonID(int PersonID, ref int PatientID, ref string MedicalHistory,
            ref byte BloodType, ref string EmergencyContact)
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
                    PatientID = (int)reader["PatientID"];

                    MedicalHistory = (reader["MedicalHistory"] != DBNull.Value) ? (string)reader["MedicalHistory"] : "";
                    BloodType = (reader["BloodType"] != DBNull.Value) ? Convert.ToByte(reader["BloodType"]) : (byte)0;
                    EmergencyContact = (string)reader["EmergencyContact"];
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

        public static int AddNewPatient(int PersonID, string MedicalHistory, byte BloodType, string EmergencyContact)
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

            // نرسل قيمة الفصيلة (إذا كانت 0 ستسجل كـ Unknown في الداتا بيز)
            command.Parameters.AddWithValue("@BloodType", BloodType);

            command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact);

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return PatientID;
        }

        public static bool UpdatePatient(int PatientID, int PersonID, string MedicalHistory, byte BloodType, string EmergencyContact)
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

            command.Parameters.AddWithValue("@BloodType", BloodType);

            command.Parameters.AddWithValue("@EmergencyContact", EmergencyContact);

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

            string query = @"SELECT Patients.PatientID, Patients.PersonID,
                  People.FullName,
                  Patients.MedicalHistory,
                  CASE Patients.BloodType
                      WHEN 1 THEN 'A+'
                      WHEN 2 THEN 'A-'
                      WHEN 3 THEN 'B+'
                      WHEN 4 THEN 'B-'
                      WHEN 5 THEN 'AB+'
                      WHEN 6 THEN 'AB-'
                      WHEN 7 THEN 'O+'
                      WHEN 8 THEN 'O-'
                      ELSE 'Unknown'
                  END AS BloodTypeName,
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