using System;
using System.Data;
using System.Data.SqlClient;

namespace ClinicData
{
    public class clsServiceData
    {
        public static bool GetServiceInfoByID(int ServiceID, ref string ServiceName,
            ref string ServiceDescription, ref decimal ServicePrice)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Services WHERE ServiceID = @ServiceID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ServiceName = (string)reader["ServiceName"];

                    if (reader["ServiceDescription"] != DBNull.Value)
                        ServiceDescription = (string)reader["ServiceDescription"];
                    else
                        ServiceDescription = "";

                    // smallmoney maps to decimal
                    ServicePrice = (decimal)reader["ServicePrice"];
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

        public static bool GetServiceInfoByName(string ServiceName, ref int ServiceID,
            ref string ServiceDescription, ref decimal ServicePrice)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Services WHERE ServiceName = @ServiceName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceName", ServiceName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ServiceID = (int)reader["ServiceID"];

                    if (reader["ServiceDescription"] != DBNull.Value)
                        ServiceDescription = (string)reader["ServiceDescription"];
                    else
                        ServiceDescription = "";

                    ServicePrice = (decimal)reader["ServicePrice"];
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

        public static int AddNewService(string ServiceName, string ServiceDescription, decimal ServicePrice)
        {
            int ServiceID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // لاحظ أن ServiceID ليس Identity في كود الـ SQL القديم الذي كتبناه
            // لكن لو عدلته ليكون Identity استخدم هذا الكود. 
            // إذا كنت ستدخله يدوياً، ستحتاج لإضافة ServiceID كباراميتر.
            // سأفترض هنا أنه Identity كما هو المعتاد في الجداول الجديدة.
            string query = @"INSERT INTO Services (ServiceName, ServiceDescription, ServicePrice)
                             VALUES (@ServiceName, @ServiceDescription, @ServicePrice);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceName", ServiceName);

            if (!string.IsNullOrEmpty(ServiceDescription))
                command.Parameters.AddWithValue("@ServiceDescription", ServiceDescription);
            else
                command.Parameters.AddWithValue("@ServiceDescription", DBNull.Value);

            command.Parameters.AddWithValue("@ServicePrice", ServicePrice);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ServiceID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return ServiceID;
        }

        public static bool UpdateService(int ServiceID, string ServiceName, string ServiceDescription, decimal ServicePrice)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Services  
                             set ServiceName = @ServiceName,
                                 ServiceDescription = @ServiceDescription,
                                 ServicePrice = @ServicePrice
                             where ServiceID = @ServiceID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceName", ServiceName);

            if (!string.IsNullOrEmpty(ServiceDescription))
                command.Parameters.AddWithValue("@ServiceDescription", ServiceDescription);
            else
                command.Parameters.AddWithValue("@ServiceDescription", DBNull.Value);

            command.Parameters.AddWithValue("@ServicePrice", ServicePrice);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);

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

        public static DataTable GetAllServices()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Services ORDER BY ServiceName";
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

        public static bool DeleteService(int ServiceID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete Services where ServiceID = @ServiceID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceID", ServiceID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static bool IsServiceExist(string ServiceName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Services WHERE ServiceName = @ServiceName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ServiceName", ServiceName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { isFound = false; }
            finally { connection.Close(); }

            return isFound;
        }
    }
}