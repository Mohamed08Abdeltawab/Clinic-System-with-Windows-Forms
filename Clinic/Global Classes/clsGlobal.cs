using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinicbusiness;

namespace Clinic.Global_Classes
{
    public class clsGlobal
    {
        public clsUser CurrentUser;
        public static bool RememberUserAndPassword(string username, string password)
        {
            try
            {
                //get the current directory of the application
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                //create a file path for the text file to store the username and password
                string filePath = CurrentDirectory + "\\RememberMe.txt";

                //if username is empty and file is exist delete the file and return true
                if (username == ""  && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string dataToSave = username + "#//#" + password;
                //write the username and password to the text file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}");
                return false;
            }
        }


        public static bool GetStordCardintal(ref string username, ref string password)
        {
            try
            {
                //get the current directory of the application
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                //create a file path for the text file to store the username and password
                string filePath = CurrentDirectory + "\\RememberMe.txt";

                //check if the file exist if exist read the username and password from the file and return true else return false
                if (File.Exists(filePath))
                {
                    //read the username and password from the text file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        //read file until find the line that contain the username and password
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] data = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            username = data[0];
                            password = data[1];
                        }
                        //if username and password are not empty return true else return false
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
                return false;
            }
        }
        
        
    }
}
