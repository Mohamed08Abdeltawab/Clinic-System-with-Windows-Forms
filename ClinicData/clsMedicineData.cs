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

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Medicines WHERE MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineID", MedicineID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    MedicineName = (string)reader["MedicineName"];
                    // smallmoney in SQL maps to decimal in C#
                    Price = (decimal)reader["Price"];
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

        public static bool GetMedicineInfoByName(string MedicineName, ref int MedicineID, ref decimal Price)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Medicines WHERE MedicineName = @MedicineName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineName", MedicineName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    MedicineID = (int)reader["MedicineID"];
                    Price = (decimal)reader["Price"];
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

        public static int AddNewMedicine(string MedicineName, decimal Price)
        {
            //this function will return the new medicine id if succeeded and -1 if not.
            int MedicineID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Medicines (MedicineName, Price)
                             VALUES (@MedicineName, @Price);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineName", MedicineName);
            command.Parameters.AddWithValue("@Price", Price);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    MedicineID = insertedID;
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

            return MedicineID;
        }

        public static bool UpdateMedicine(int MedicineID, string MedicineName, decimal Price)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Medicines  
                             set MedicineName = @MedicineName,
                                 Price = @Price
                             where MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineName", MedicineName);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@MedicineID", MedicineID);

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

        public static DataTable GetAllMedicines()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Medicines ORDER BY MedicineName";

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

        public static bool DeleteMedicine(int MedicineID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Medicines 
                             where MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineID", MedicineID);

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

        public static bool IsMedicineExist(int MedicineID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Medicines WHERE MedicineID = @MedicineID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineID", MedicineID);

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

        public static bool IsMedicineExist(string MedicineName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Medicines WHERE MedicineName = @MedicineName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedicineName", MedicineName);

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