using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC5Project
{
    class Login
    {
        // LOGIN with Username and Password
        public static void Page()
        {
            Console.WriteLine("------LOGIN PAGE------");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            while (!Validation.CheckUser(username,password))
            {
                Console.WriteLine("Username or Password is Incorrect. Please try again");
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();
                Validation.CheckUser(username,password);
            }

            // CheckRole returns a User, which is saved in my variable
            User user = Database.GetUser(username);
            Console.Clear();
            // Send this user to the MainMenu
            Menu.MainMenu(user);
        }
    }
}
