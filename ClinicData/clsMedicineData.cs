using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsMedicineData
    {

        public static bool GetMedicineInfoByID(int MedicineID, ref string MedicineName, ref decimal Price)
        {
            bool isFound = false;

            string query = "SELECT * FROM Medicines WHERE MedicineID = @MedicineID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                MedicineName = (string)reader["MedicineName"];
                                Price = (decimal)reader["Price"];
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

        public static bool GetMedicineInfoByName(string MedicineName, ref int MedicineID, ref decimal Price)
        {
            bool isFound = false;

            string query = "SELECT * FROM Medicines WHERE MedicineName = @MedicineName";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineName", MedicineName);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                MedicineID = (int)reader["MedicineID"];
                                Price = (decimal)reader["Price"];
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

        public static int AddNewMedicine(string MedicineName, decimal Price)
        {
            int MedicineID = -1;

            string query = @"INSERT INTO Medicines (MedicineName, Price)
                             VALUES (@MedicineName, @Price);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineName", MedicineName);
                        command.Parameters.AddWithValue("@Price", Price);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            MedicineID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return MedicineID;
        }

        public static bool UpdateMedicine(int MedicineID, string MedicineName, decimal Price)
        {
            int rowsAffected = 0;

            string query = @"Update Medicines  
                             set MedicineName = @MedicineName,
                                 Price = @Price
                             where MedicineID = @MedicineID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineName", MedicineName);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);

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

        public static DataTable GetAllMedicines()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Medicines ORDER BY MedicineName";

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

        public static bool DeleteMedicine(int MedicineID)
        {
            int rowsAffected = 0;

            string query = @"Delete Medicines where MedicineID = @MedicineID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);
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

        public static bool IsMedicineExist(int MedicineID)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Medicines WHERE MedicineID = @MedicineID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);
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

        public static bool IsMedicineExist(string MedicineName)
        {
            bool isFound = false;

            string query = "SELECT Found=1 FROM Medicines WHERE MedicineName = @MedicineName";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MedicineName", MedicineName);
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