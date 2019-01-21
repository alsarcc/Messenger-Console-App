using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BC5Project
{
    class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string RoleName { get; set; }

        public User()
        {

        }

        // ADMIN - CREATE USER
        public static void AdminCreatesUser(User admin)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    // CREATE NEW USER
                    User user = new User();

                    Console.WriteLine("Enter a Username for new User:");
                    user.Username = Console.ReadLine();

                    Console.WriteLine("Enter a Password for new User:");
                    user.Password = Console.ReadLine();

                    while (Validation.CheckUser(user.Username))
                    {
                        Console.WriteLine("Please enter a different User.");
                        Console.WriteLine("Enter a Username for new User:");
                        user.Username = Console.ReadLine();
                        Console.WriteLine("Enter a Password for new User:");
                        user.Password = Console.ReadLine();
                        Validation.CheckUser(user.Username);
                    }

                    user.Role = AdminAssignsRole();

                    SqlCommand cmdInsert = new SqlCommand
                    ($"INSERT INTO [User](Username, Password,RoleID) VALUES ('{user.Username}','{user.Password}','{user.Role}')", sqlConnection);
                    int rowsInserted = cmdInsert.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Insertion of User Successful");
                        Console.WriteLine($"{rowsInserted} rows updated Successfully");
                    }

                    // What to do next?
                    Console.WriteLine("---------------------");
                    Console.WriteLine("1. Create Another User");
                    Console.WriteLine("2. Return To Main Menu");
                    Console.WriteLine("3. Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                AdminCreatesUser(admin);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(admin);
                                break;
                            }
                        case 3:
                            {
                                Environment.Exit(2);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid Option");
                                Menu.MainMenu(admin);
                                break;
                            }
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
        }

        // ADMIN - ASSIGN ROLE
        public static int AdminAssignsRole()
        {
            // ASSIGN ROLE
            Console.WriteLine("Select a Role for User:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. View Edit And Delete");
            Console.WriteLine("3. View And Edit");
            Console.WriteLine("4. View");
            Console.WriteLine("5. Simple User");

            int role = int.Parse(Console.ReadLine());

            // CHECK WHETHER THE RIGHT NUMBER IS PRESSED
            while (role < 0 || role > 5)
            {
                Console.WriteLine("Invalid Option. Please Choose another number:");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. View Edit And Delete");
                Console.WriteLine("3. View And Edit");
                Console.WriteLine("4. View");
                Console.WriteLine("5. Simple User");

                role= int.Parse(Console.ReadLine());
            }

            return role;
        }

        // ADMIN - VIEW USERS
        public static void AdminViewsUsers(User admin)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    // VIEW USERS
                    SqlCommand cmdUsers = new SqlCommand
                        ($"SELECT [User].ID, Username, Password, Role.Name FROM [User], Role WHERE [User].RoleID = Role.ID ", sqlConnection);
                    SqlDataReader readerUsers = cmdUsers.ExecuteReader();

                    List<User> Users = new List<User>();
                    Console.WriteLine("Users: ");
                    while (readerUsers.Read())
                    {
                        User ViewedUser = new User()
                        {
                            ID = readerUsers.GetInt32(0),
                            Username = readerUsers.GetString(1),
                            Password = readerUsers.GetString(2),
                            RoleName = readerUsers.GetString(3)
                        };
                        Users.Add(ViewedUser);
                    }

                    Console.WriteLine("------ALL USERS------");
                    foreach (User ViewedUser in Users)
                    {
                        Console.WriteLine(ViewedUser);
                    }
                    readerUsers.Close();

                    // What to do next?
                    Console.WriteLine("---------------------");
                    Console.WriteLine("1. Return To Main Menu");
                    Console.WriteLine("2. Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Menu.MainMenu(admin);
                                break;
                            }
                        case 2:
                            {
                                Environment.Exit(2);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid Option");
                                Menu.MainMenu(admin);
                                break;
                            }
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
        }

        // ADMIN - UPDATE USER
        public static void AdminUpdatesUser(User admin)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdUsers = new SqlCommand
                        ($"SELECT [User].ID, Username, Password, Role.Name FROM [User], Role WHERE [User].RoleID = Role.ID ", sqlConnection);
                    SqlDataReader readerUsers = cmdUsers.ExecuteReader();

                    List<User> Users = new List<User>();
                    Console.WriteLine("Users: ");
                    while (readerUsers.Read())
                    {
                        User ViewedUser = new User()
                        {
                            ID = readerUsers.GetInt32(0),
                            Username = readerUsers.GetString(1),
                            Password = readerUsers.GetString(2),
                            RoleName = readerUsers.GetString(3)
                        };
                        Users.Add(ViewedUser);
                    }

                    Console.WriteLine("------ALL USERS------");
                    foreach (User ViewedUser in Users)
                    {
                        Console.WriteLine(ViewedUser);
                    }
                    readerUsers.Close();

                    // UPDATE USER
                    Console.WriteLine("Enter a User for Update:");
                    string usernameToUpdate = Console.ReadLine();

                    while (!Validation.CheckUser(usernameToUpdate))
                    {
                        Console.WriteLine("User Does Not Exist. Please enter a different User:");
                        usernameToUpdate = Console.ReadLine();
                        Validation.CheckUser(usernameToUpdate);
                    }

                    Console.WriteLine("Enter a new Password for User:");
                    string newPassword = Console.ReadLine();

                    SqlCommand cmdUpdate = new SqlCommand
                    ($"UPDATE [User] SET Password = '{newPassword}' WHERE Username = '{usernameToUpdate}'", sqlConnection);
                    int rowsUpdated = cmdUpdate.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine("Update of User Successful");
                        Console.WriteLine($"{rowsUpdated} rows updated Successfully");
                    }

                    // What to do next?
                    Console.WriteLine("---------------------");
                    Console.WriteLine("1. Update Another User");
                    Console.WriteLine("2. Return To Main Menu");
                    Console.WriteLine("3. Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                AdminUpdatesUser(admin);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(admin);
                                break;
                            }
                        case 3:
                            {
                                Environment.Exit(2);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid Option");
                                Menu.MainMenu(admin);
                                break;
                            }
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
        }
        
        // ADMIN - DELETE USER
        public static void AdminDeletesUser(User admin)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdUsers = new SqlCommand
                        ($"SELECT [User].ID, Username, Password, Role.Name FROM [User], Role WHERE [User].RoleID = Role.ID ", sqlConnection);
                    SqlDataReader readerUsers = cmdUsers.ExecuteReader();

                    List<User> Users = new List<User>();
                    Console.WriteLine("Users: ");
                    while (readerUsers.Read())
                    {
                        User ViewedUser = new User()
                        {
                            ID = readerUsers.GetInt32(0),
                            Username = readerUsers.GetString(1),
                            Password = readerUsers.GetString(2),
                            RoleName = readerUsers.GetString(3)
                        };
                        Users.Add(ViewedUser);
                    }

                    Console.WriteLine("------ALL USERS------");
                    foreach (User ViewedUser in Users)
                    {
                        Console.WriteLine(ViewedUser);
                    }
                    readerUsers.Close();

                    // DELETE USER
                    Console.WriteLine("Enter a User to Delete:");
                    string nameToDelete = Console.ReadLine();

                    while (!Validation.CheckUser(nameToDelete))
                    {
                        Console.WriteLine("User Does Not Exist. Please enter a different User:");
                        nameToDelete = Console.ReadLine();
                        Validation.CheckUser(nameToDelete);
                    }

                    SqlCommand cmdDelete = new SqlCommand
                    ($"DELETE FROM [User] WHERE Username = '{nameToDelete}'", sqlConnection);
                    int rowsDeleted = cmdDelete.ExecuteNonQuery();
                    if (rowsDeleted > 0)
                    {
                        Console.WriteLine("Delete User Successful");
                        Console.WriteLine($"{rowsDeleted} rows deleted Successfully");
                    }

                    // What to do next?
                    Console.WriteLine("---------------------");
                    Console.WriteLine("1. Delete Another User");
                    Console.WriteLine("2. Return To Main Menu");
                    Console.WriteLine("3. Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                AdminDeletesUser(admin);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(admin);
                                break;
                            }
                        case 3:
                            {
                                Environment.Exit(2);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid Option");
                                Menu.MainMenu(admin);
                                break;
                            }
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

        }

        // PRINT USER DATA
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine($"User ID: {ID}")
                .AppendLine($"Username: {Username}")
                .AppendLine($"Password: {Password}")
                .AppendLine($"Role Name: {RoleName}");

            return builder.ToString();
        }
    }
}
