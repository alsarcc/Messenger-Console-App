using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BC5Project
{
    class Validation
    {
        // CHECK EXISTANCE OF USER through Username & Password
        public static bool CheckUser(string username,string password)
        {
            SqlConnection sqlConnection = Database.Access();

            bool userExist = true;

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM [USER] WHERE Username = '{username}' AND Password = '{password}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();

                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("User Exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User Does Not Exist");
                        userExist = false;
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return userExist;
        }

        // CHECK EXISTANCE OF USER through Username
        public static bool CheckUser(string username)
        {
            SqlConnection sqlConnection = Database.Access();

            bool userExist = true;

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM [USER] WHERE Username = '{username}'", sqlConnection);
                    SqlDataReader reader = cmdSelect.ExecuteReader();

                    if (reader.Read())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("User Exists");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User Does Not Exist");
                        userExist = false;
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return userExist;
        }

    }
}
