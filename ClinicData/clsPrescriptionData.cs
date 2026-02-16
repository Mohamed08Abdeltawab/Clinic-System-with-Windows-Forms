using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsPrescriptionData
    {

        public static bool GetPrescriptionInfoByID(int PrescriptionID, ref int VisitID, ref int MedicineID,
            ref int Quantity, ref string Instructions)
        {
            bool isFound = false;

            string query = "SELECT * FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                VisitID = (int)reader["VisitID"];
                                MedicineID = (int)reader["MedicineID"];
                                Quantity = (int)reader["Quantity"];
                                Instructions = (string)reader["Instructions"];
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

        public static int AddNewPrescription(int VisitID, int MedicineID,
            int Quantity, string Instructions)
        {
            int PrescriptionID = -1;

            string query = @"INSERT INTO Prescriptions (VisitID, MedicineID, Quantity, Instructions)
                             VALUES (@VisitID, @MedicineID, @Quantity, @Instructions);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@Instructions", Instructions);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PrescriptionID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return PrescriptionID;
        }

        public static bool UpdatePrescription(int PrescriptionID, int VisitID, int MedicineID,
            int Quantity, string Instructions)
        {
            int rowsAffected = 0;

            string query = @"Update Prescriptions  
                             set VisitID = @VisitID,
                                 MedicineID = @MedicineID,
                                 Quantity = @Quantity,
                                 Instructions = @Instructions
                             where PrescriptionID = @PrescriptionID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@Instructions", Instructions);
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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

        public static DataTable GetAllPrescriptions()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Prescriptions ORDER BY PrescriptionID DESC";

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
            }

            return dt;
        }


        public static DataTable GetPrescriptionsByVisitID(int VisitID)
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Prescriptions WHERE VisitID = @VisitID";

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


        public static bool DeletePrescription(int PrescriptionID)
        {
            int rowsAffected = 0;

            string query = @"Delete Prescriptions 
                             where PrescriptionID = @PrescriptionID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return (rowsAffected > 0);
        }

        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
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

        public static bool IsPrescriptionExistByVisitID(int VisitID)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Prescriptions WHERE VisitID = @VisitID";

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
    }
}