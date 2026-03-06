using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicData
{
    public class clsAppointmentTypeData
    {
        public static bool GetAppointmentTypeInfoByID(int AppointmentTypeID,
        ref string AppointmentTypeName, ref decimal AppointmentTypeFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            // تم استخدام أسماء الأعمدة من جدولك: AppointmentType
            string query = "SELECT * FROM AppointmentType WHERE AppointmentTypeID = @AppointmentTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentTypeID", AppointmentTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    AppointmentTypeName = (string)reader["AppointmentTypeName"];
                    // تحويل القيمة لـ decimal لضمان الدقة المالية
                    AppointmentTypeFees = Convert.ToDecimal(reader["AppointmentTypeFees"]);
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

        // 2. دالة جلب جميع أنواع المواعيد (لتعبئة الـ ComboBox)
        public static DataTable GetAllAppointmentTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM AppointmentType";

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
                // التعامل مع الخطأ
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        // 3. دالة تحديث السعر أو الاسم (اختياري لو أردت تعديل الأسعار من السيستم)
        public static bool UpdateAppointmentType(int AppointmentTypeID,
            string AppointmentTypeName, decimal AppointmentTypeFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE AppointmentType  
                         SET AppointmentTypeName = @AppointmentTypeName, 
                             AppointmentTypeFees = @AppointmentTypeFees
                         WHERE AppointmentTypeID = @AppointmentTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentTypeID", AppointmentTypeID);
            command.Parameters.AddWithValue("@AppointmentTypeName", AppointmentTypeName);
            command.Parameters.AddWithValue("@AppointmentTypeFees", AppointmentTypeFees);

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
    }
}
