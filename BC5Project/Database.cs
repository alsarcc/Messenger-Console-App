using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BC5Project
{
    class Database
    {
        public static SqlConnection Access()
        {
            string connectionString =
            @"Server = DESKTOP-D7EFOJ3\SQLEXPRESS; Database = MessengerDB; Trusted_Connection = True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            return sqlConnection;
        }


        // CHECK ROLE OF USER through Username
        public static User GetUser(string username)
        {
            SqlConnection sqlConnection = Database.Access();

            User user = new User();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdSelectRole = new SqlCommand($"SELECT [User].ID, Username, Password, Role.Name FROM [User],Role WHERE Role.ID = [User].RoleID AND Username = '{username}'", sqlConnection);
                    var reader = cmdSelectRole.ExecuteReader();
                    while (reader.Read())
                    {
                        user.ID = reader.GetInt32(0);
                        user.Username = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.RoleName = reader.GetString(3);
                        Console.WriteLine("Your role is: " + user.RoleName);
                    }
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
            return user;
        }
    }
}
