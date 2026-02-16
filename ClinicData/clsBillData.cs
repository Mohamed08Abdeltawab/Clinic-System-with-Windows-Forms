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
            ref byte PaymentStatus, ref DateTime? PaymentDate, ref byte PaymentMethod,
            ref decimal TaxAmount, ref decimal Discount, ref int CreatedByUserID)
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
                    TotalAmount = (decimal)reader["TotalAmount"];
                    PaymentStatus = (byte)reader["PaymentStatus"];

                    // PaymentDate Allow Null
                    if (reader["PaymentDate"] != DBNull.Value)
                        PaymentDate = (DateTime)reader["PaymentDate"];
                    else
                        PaymentDate = null;

                    // PaymentMethod (tinyint -> byte)
                    // Assuming PaymentMethod is NOT NULL based on your schema, otherwise check for DBNull
                    PaymentMethod = (byte)reader["PaymentMethod"];

                    // TaxAmount Allow Null (Default to 0 if null)
                    if (reader["TaxAmount"] != DBNull.Value)
                        TaxAmount = (decimal)reader["TaxAmount"];
                    else
                        TaxAmount = 0;

                    // Discount Allow Null (Default to 0 if null)
                    if (reader["Discount"] != DBNull.Value)
                        Discount = (decimal)reader["Discount"];
                    else
                        Discount = 0;

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                {
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

        public static int AddNewBill(int VisitID, decimal TotalAmount, byte PaymentStatus,
             DateTime? PaymentDate, byte PaymentMethod, decimal TaxAmount, decimal Discount, int CreatedByUserID)
        {
            int BillID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Bills (VisitID, TotalAmount, PaymentStatus, PaymentDate, PaymentMethod, TaxAmount, Discount, CreatedByUserID)
                             VALUES (@VisitID, @TotalAmount, @PaymentStatus, @PaymentDate, @PaymentMethod, @TaxAmount, @Discount, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            // Handling Nullable Parameters
            if (PaymentDate != null)
                command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            else
                command.Parameters.AddWithValue("@PaymentDate", DBNull.Value);

            // TaxAmount can be 0 or value, if you want to store NULL for 0, change logic here
            command.Parameters.AddWithValue("@TaxAmount", TaxAmount); 
            command.Parameters.AddWithValue("@Discount", Discount);

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

        public static bool UpdateBill(int BillID, int VisitID, decimal TotalAmount, byte PaymentStatus,
            DateTime? PaymentDate, byte PaymentMethod, decimal TaxAmount, decimal Discount, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Bills  
                             set VisitID = @VisitID,
                                 TotalAmount = @TotalAmount,
                                 PaymentStatus = @PaymentStatus,
                                 PaymentDate = @PaymentDate,
                                 PaymentMethod = @PaymentMethod,
                                 TaxAmount = @TaxAmount,
                                 Discount = @Discount,
                                 CreatedByUserID = @CreatedByUserID
                             where BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            command.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
            command.Parameters.AddWithValue("@TaxAmount", TaxAmount);
            command.Parameters.AddWithValue("@Discount", Discount);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@BillID", BillID);

            if (PaymentDate != null)
                command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            else
                command.Parameters.AddWithValue("@PaymentDate", DBNull.Value);

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

        public static DataTable GetAllBills()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Bills.BillID, 
                             Bills.VisitID, 
                             PatientName = People.FullName,
                             Bills.TotalAmount, 
                             Bills.PaymentStatus,
                             CASE 
                                WHEN Bills.PaymentStatus = 1 THEN 'Paid'
                                WHEN Bills.PaymentStatus = 2 THEN 'Unpaid'
                                ELSE 'Unknown'
                             END as StatusCaption,
                             Bills.PaymentMethod,
                             CASE 
                                WHEN Bills.PaymentMethod = 1 THEN 'Cash'
                                WHEN Bills.PaymentMethod = 2 THEN 'Credit Card'
                                WHEN Bills.PaymentMethod = 3 THEN 'Insurance'
                                ELSE 'Unknown'
                             END as PaymentMethodCaption,
                             Bills.PaymentDate,
                             Bills.TaxAmount,
                             Bills.Discount
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

        public static bool GetBillInfoByVisitID(int VisitID, ref int BillID, ref decimal TotalAmount,
            ref byte PaymentStatus, ref DateTime? PaymentDate, ref byte PaymentMethod,
            ref decimal TaxAmount, ref decimal Discount, ref int CreatedByUserID)
        {
            bool isFound = false;

            // استعلام لجلب كافة الأعمدة الجديدة
            string query = "SELECT * FROM Bills WHERE VisitID = @VisitID";

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

                                BillID = (int)reader["BillID"];
                                TotalAmount = (decimal)reader["TotalAmount"];
                                PaymentStatus = (byte)reader["PaymentStatus"];

                                // التعامل مع تاريخ الدفع لأنه قد يكون Null في حالة الفاتورة غير المدفوعة
                                if (reader["PaymentDate"] != DBNull.Value)
                                    PaymentDate = (DateTime)reader["PaymentDate"];
                                else
                                    PaymentDate = null;

                                // الحقول الجديدة بناءً على المخطط النسخة الثانية
                                PaymentMethod = (byte)reader["PaymentMethod"];

                                // التحقق من الـ Null للضرائب والخصم
                                TaxAmount = (reader["TaxAmount"] != DBNull.Value) ? (decimal)reader["TaxAmount"] : 0;
                                Discount = (reader["Discount"] != DBNull.Value) ? (decimal)reader["Discount"] : 0;

                                CreatedByUserID = (int)reader["CreatedByUserID"];
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // تم الإبقاء على معالجة الخطأ كما هي
                isFound = false;
            }

            return isFound;
        }

        public static bool DeleteBill(int BillID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Bills where BillID = @BillID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BillID", BillID);

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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

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