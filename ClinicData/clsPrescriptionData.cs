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

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    VisitID = (int)reader["VisitID"];
                    MedicineID = (int)reader["MedicineID"];
                    Quantity = (int)reader["Quantity"];
                    Instructions = (string)reader["Instructions"];
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

        public static int AddNewPrescription(int VisitID, int MedicineID,
            int Quantity, string Instructions)
        {
            //this function will return the new prescription id if succeeded and -1 if not.
            int PrescriptionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Prescriptions (VisitID, MedicineID, Quantity, Instructions)
                             VALUES (@VisitID, @MedicineID, @Quantity, @Instructions);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@MedicineID", MedicineID);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Instructions", Instructions);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PrescriptionID = insertedID;
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

            return PrescriptionID;
        }

        public static bool UpdatePrescription(int PrescriptionID, int VisitID, int MedicineID,
            int Quantity, string Instructions)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Prescriptions  
                             set VisitID = @VisitID,
                                 MedicineID = @MedicineID,
                                 Quantity = @Quantity,
                                 Instructions = @Instructions
                             where PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@VisitID", VisitID);
            command.Parameters.AddWithValue("@MedicineID", MedicineID);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Instructions", Instructions);
            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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

        public static DataTable GetAllPrescriptions()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // ملاحظة: قمت بجلب البيانات كما هي (Raw Data) لأنني لا أملك جدول Medicines لعمل Join معه لجلب اسم الدواء.
            // يمكنك تعديل الكويري لاحقاً لعمل Inner Join مع جدول الأدوية لعرض الاسم بدلاً من الرقم.
            string query = @"SELECT * FROM Prescriptions ORDER BY PrescriptionID DESC";

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

        public static bool DeletePrescription(int PrescriptionID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Prescriptions 
                             where PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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

        public static bool IsPrescriptionExist(int PrescriptionID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);

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

        // دالة إضافية مفيدة للتحقق من الوصفات الخاصة بزيارة معينة
        public static bool IsPrescriptionExistByVisitID(int VisitID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Prescriptions WHERE VisitID = @VisitID";

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