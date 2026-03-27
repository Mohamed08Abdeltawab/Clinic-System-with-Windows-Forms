using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ClinicData
{
    public class clsPrescriptionData
    {
        // 1. جلب معلومات الروشتة الأساسية (Header)
        public static bool GetPrescriptionInfoByID(int PrescriptionID, ref int VisitID,
            ref DateTime PrescriptionDate, ref string Notes)
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
                                PrescriptionDate = (DateTime)reader["PrescriptionDate"];
                                Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                            }
                        }
                    }
                }
            }
            catch { isFound = false; }
            return isFound;
        }

        // 2. إضافة روشتة جديدة (Header) وترجع الـ ID الجديد
        public static int AddNewPrescription(int VisitID, DateTime PrescriptionDate, string Notes)
        {
            int PrescriptionID = -1;
            string query = @"INSERT INTO Prescriptions (VisitID, PrescriptionDate, Notes)
                             VALUES (@VisitID, @PrescriptionDate, @Notes);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VisitID", VisitID);
                        command.Parameters.AddWithValue("@PrescriptionDate", PrescriptionDate);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PrescriptionID = insertedID;
                        }
                    }
                }
            }
            catch { }
            return PrescriptionID;
        }

        // 3. إضافة دواء واحد داخل الروشتة (Detail Item)
        public static int AddPrescriptionItem(int PrescriptionID, int MedicineID, int Quantity, string Instructions,string Dosage)
        {
            int ItemID = -1;
            string query = @"INSERT INTO PrescriptionItems (PrescriptionID, MedicineID, Quantity,Dosage, Instructions)
                             VALUES (@PrescriptionID, @MedicineID, @Quantity, @Dosage, @Instructions);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                        command.Parameters.AddWithValue("@MedicineID", MedicineID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@Dosage", Dosage);
                        command.Parameters.AddWithValue("@Instructions", Instructions);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ItemID = insertedID;
                        }
                    }
                }
            }
            catch { }
            return ItemID;
        }

        // 4. عرض كل الروشتات (Master List) ببيانات المرضى (Join)
        public static DataTable GetAllPrescriptions()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT P.PrescriptionID, 
                                    Pe.FullName AS PatientName,  
                                    P.PrescriptionDate, 
                                    P.VisitID
                             FROM Prescriptions P
                             JOIN Visits V ON P.VisitID = V.VisitID
                             JOIN Appointments App ON V.AppointmentID = App.AppointmentID
                             JOIN Patients Pat ON App.PatientID = Pat.PatientID
                             JOIN People Pe ON Pat.PersonID = Pe.PersonID
                             ORDER BY P.PrescriptionID DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        // 5. جلب كافة أدوية روشتة معينة (Details List)
        public static DataTable GetPrescriptionItems(int PrescriptionID)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT I.ItemID, I.MedicineID, M.MedicineName, I.Quantity, I.Dosage, I.Instructions
                 FROM PrescriptionItems I
                 JOIN Medicines M ON I.MedicineID = M.MedicineID
                 WHERE I.PrescriptionID = @PrescriptionID";

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
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        // 6. حذف الروشتة (سيقوم الـ Cascade في SQL بحذف الأدوية تلقائياً)
        public static bool DeletePrescription(int PrescriptionID)
        {
            int rowsAffected = 0;
            string query = "DELETE Prescriptions WHERE PrescriptionID = @PrescriptionID";

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
            catch { }
            return (rowsAffected > 0);
        }

        // 7. حذف كافة الأدوية من روشتة معينة (نحتاجها عند التعديل)
        public static bool DeleteAllItemsByPrescriptionID(int PrescriptionID)
        {
            int rowsAffected = 0;
            string query = "DELETE PrescriptionItems WHERE PrescriptionID = @PrescriptionID";

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
            catch { }
            return (rowsAffected >= 0);
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