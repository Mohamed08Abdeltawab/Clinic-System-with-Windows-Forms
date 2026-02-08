using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsBillData
    {

        public static bool GetBillInfoByID(int BillID, ref int VisitID, ref decimal TotalAmount,
            ref byte PaymentStatus, ref DateTime PaymentDate, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Bills WHERE BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BillID", BillID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    VisitID = (int)reader["VisitID"];

                    // smallmoney maps to decimal
                    TotalAmount = (decimal)reader["TotalAmount"];

                    // tinyint maps to byte
                    PaymentStatus = (byte)reader["PaymentStatus"];

                    PaymentDate = (DateTime)reader["PaymentDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static int AddNewBill(int VisitID, decimal TotalAmount,
            byte PaymentStatus, DateTime PaymentDate, int CreatedByUserID)
        {
            //this function will return the new bill id if succeeded and -1 if not.
            int BillID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Bills (VisitID, TotalAmount, PaymentStatus, PaymentDate, CreatedByUserID)
                             VALUES (@VisitID, @TotalAmount, @PaymentStatus, @PaymentDate, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    BillID = insertedID;
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

            return BillID;
        }

        public static bool UpdateBill(int BillID, int VisitID, decimal TotalAmount,
            byte PaymentStatus, DateTime PaymentDate, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Bills  
                             set VisitID = @VisitID,
                                 TotalAmount = @TotalAmount,
                                 PaymentStatus = @PaymentStatus,
                                 PaymentDate = @PaymentDate,
                                 CreatedByUserID = @CreatedByUserID
                             where BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@BillID", BillID);

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

        public static DataTable GetAllBills()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // قمت بعمل Join مع الزيارات والمواعيد والمرضى لجلب اسم المريض ليكون الجدول مفهوماً
            string query = @"SELECT Bills.BillID, Bills.VisitID, 
                             PatientName = People.FullName,
                             Bills.TotalAmount, 
                             Bills.PaymentStatus,
                             CASE 
                                WHEN Bills.PaymentStatus = 1 THEN 'Paid'
                                WHEN Bills.PaymentStatus = 2 THEN 'Unpaid'
                                ELSE 'Unknown'
                             END as StatusCaption,
                             Bills.PaymentDate
                             FROM Bills 
                             INNER JOIN Visits ON Bills.VisitID = Visits.VisitID
                             INNER JOIN Appointments ON Visits.AppointmentID = Appointments.AppointmentID
                             INNER JOIN Patients ON Appointments.PatientID = Patients.PetientID
                             INNER JOIN People ON Patients.PersonID = People.PersonID
                             ORDER BY Bills.BillID DESC";

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

        public static bool DeleteBill(int BillID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Bills 
                             where BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BillID", BillID);

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

        public static bool IsBillExist(int BillID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Bills WHERE BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BillID", BillID);

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

        // دالة إضافية مفيدة: هل توجد فاتورة لزيارة معينة؟
        public static bool IsBillExistByVisitID(int VisitID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Bills WHERE VisitID = @VisitID";

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
    }
}