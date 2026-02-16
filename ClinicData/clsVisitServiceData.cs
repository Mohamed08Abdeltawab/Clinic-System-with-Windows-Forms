using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsVisitServiceData
    {
        public static bool GetVisitServiceInfoByID(int VisitServiceID, ref int VisitID, ref int ServiceID, ref decimal ServiceFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM VisitServices WHERE VisitServiceID = @VisitServiceID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitServiceID", VisitServiceID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    VisitID = (int)reader["VisitID"];
                    ServiceID = (int)reader["ServiceID"];
                    // smallmoney maps to decimal
                    ServiceFees = (decimal)reader["ServiceFees"];
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

        public static int AddNewVisitService(int VisitID, int ServiceID, decimal ServiceFees)
        {
            int VisitServiceID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO VisitServices (VisitID, ServiceID, ServiceFees)
                             VALUES (@VisitID, @ServiceID, @ServiceFees);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);
            command.Parameters.AddWithValue("@ServiceFees", ServiceFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    VisitServiceID = insertedID;
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

            return VisitServiceID;
        }

        public static bool UpdateVisitService(int VisitServiceID, int VisitID, int ServiceID, decimal ServiceFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update VisitServices  
                             set VisitID = @VisitID,
                                 ServiceID = @ServiceID,
                                 ServiceFees = @ServiceFees
                             where VisitServiceID = @VisitServiceID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);
            command.Parameters.AddWithValue("@ServiceFees", ServiceFees);
            command.Parameters.AddWithValue("@VisitServiceID", VisitServiceID);

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

        public static DataTable GetAllVisitServices()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Join with Services table to get Service Name
            string query = @"SELECT VisitServices.VisitServiceID, VisitServices.VisitID,
                             Services.ServiceName,
                             VisitServices.ServiceFees
                             FROM VisitServices INNER JOIN
                                  Services ON VisitServices.ServiceID = Services.ServiceID
                             ORDER BY VisitServices.VisitID DESC";

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

        // دالة مهمة لجلب كل الخدمات الخاصة بزيارة معينة (عشان الفاتورة)
        public static DataTable GetVisitServicesByVisitID(int VisitID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT VisitServices.VisitServiceID, 
                             Services.ServiceName,
                             VisitServices.ServiceFees
                             FROM VisitServices INNER JOIN
                                  Services ON VisitServices.ServiceID = Services.ServiceID
                             WHERE VisitServices.VisitID = @VisitID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VisitID", VisitID);

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

        public static bool DeleteVisitService(int VisitServiceID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete VisitServices where VisitServiceID = @VisitServiceID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VisitServiceID", VisitServiceID);

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

        public static bool IsVisitServiceExist(int VisitServiceID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM VisitServices WHERE VisitServiceID = @VisitServiceID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VisitServiceID", VisitServiceID);

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