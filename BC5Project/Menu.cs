using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace BC5Project
{
    class Menu
    {
        // MAIN MENU that redirects each User according to their Role
        public static void MainMenu(User user)
        {
            Console.WriteLine("------MAIN MENU------");

            switch (user.RoleName)
            {
                case "Admin":
                    {
                        AdminMenu(user);
                        break;
                    }
                case "A":
                    {
                        ViewerEditorDeletorMenu(user);
                        break;
                    }
                case "B":
                    {
                        ViewerEditorMenu(user);
                        break;
                    }
                case "C":
                    {
                        ViewerMenu(user);
                        break;
                    }
                case "SimpleUser":
                    {
                        SimpleUserMenu(user);
                        break;
                    }
            }
        }


        // SIMPLE USER
        public static StringBuilder SimpleUserMenu(User user)
        {
            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("1. View your Inbox")
                .AppendLine("2. View your Outbox")
                .AppendLine("3. Update your messages")
                .AppendLine("4. Delete your messages")
                .AppendLine("5. Send a message");

            if(user.RoleName == "SimpleUser")
            {
                builder.AppendLine("6.Exit");
                Console.WriteLine(builder);
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Message.ViewInbox(user);
                            break;
                        }
                    case 2:
                        {
                            Message.ViewOutbox(user);
                            break;
                        }
                    case 3:
                        {
                            Message.UpdatePrivate(user);
                            break;
                        }
                    case 4:
                        {
                            Message.DeletePrivate(user);
                            break;
                        }
                    case 5:
                        {
                            Message.Send(user);
                            break;
                        }
                    case 6:
                        {
                            Environment.Exit(2);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid option");
                            SimpleUserMenu(user);
                            break;
                        }
                }
            }
            

            return builder;
        }

        // VIEW ONLY USER
        public static StringBuilder ViewerMenu(User user)
        {
            var builder = SimpleUserMenu(user);
            builder
                  .AppendLine("--------------------")
                  .AppendLine("6. View other messages");

            if(user.RoleName == "C")
            {

                builder.AppendLine("7.Exit");
                Console.WriteLine(builder);
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Message.ViewInbox(user);
                            break;
                        }
                    case 2:
                        {
                            Message.ViewOutbox(user);
                            break;
                        }
                    case 3:
                        {
                            Message.UpdatePrivate(user);
                            break;
                        }
                    case 4:
                        {
                            Message.DeletePrivate(user);
                            break;
                        }
                    case 5:
                        {
                            Message.Send(user);
                            break;
                        }
                    case 6:
                        {
                            Message.ViewAll(user);
                            break;
                        }
                    case 7:
                        {
                            Environment.Exit(2);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid option");
                            ViewerMenu(user);
                            break;
                        }
                }
            }
            
            return builder;
        }

        // VIEW AND EDIT USER
        public static StringBuilder ViewerEditorMenu(User user)
        {
            var builder = ViewerMenu(user);
            builder.AppendLine("7. Update other messages");

            if(user.RoleName == "B")
            {
                builder.AppendLine("8.Exit");
                Console.WriteLine(builder);
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Message.ViewInbox(user);
                            break;
                        }
                    case 2:
                        {
                            Message.ViewOutbox(user);
                            break;
                        }
                    case 3:
                        {
                            Message.UpdatePrivate(user);
                            break;
                        }
                    case 4:
                        {
                            Message.DeletePrivate(user);
                            break;
                        }
                    case 5:
                        {
                            Message.Send(user);
                            break;
                        }
                    case 6:
                        {
                            Message.ViewAll(user);
                            break;
                        }
                    case 7:
                        {
                            Message.UpdateAll(user);
                            break;
                        }
                    case 8:
                        {
                            Environment.Exit(2);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid option");
                            ViewerEditorMenu(user);
                            break;
                        }
                }

            }
            
            return builder;
        }

        // VIEW EDIT AND DELETE USER
        public static StringBuilder ViewerEditorDeletorMenu(User user)
        {
            var builder = ViewerEditorMenu(user);
            builder.AppendLine("8. Delete other messages");

            if(user.RoleName == "A")
            {
                builder.AppendLine("9.Exit");
                Console.WriteLine(builder);
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            Message.ViewInbox(user);
                            break;
                        }
                    case 2:
                        {
                            Message.ViewOutbox(user);
                            break;
                        }
                    case 3:
                        {
                            Message.UpdatePrivate(user);
                            break;
                        }
                    case 4:
                        {
                            Message.DeletePrivate(user);
                            break;
                        }
                    case 5:
                        {
                            Message.Send(user);
                            break;
                        }
                    case 6:
                        {
                            Message.ViewAll(user);
                            break;
                        }
                    case 7:
                        {
                            Message.UpdateAll(user);
                            break;
                        }
                    case 8:
                        {
                            Message.DeleteAll(user);
                            break;
                        }
                    case 9:
                        {
                            Environment.Exit(2);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid option");
                            ViewerEditorDeletorMenu(user);
                            break;
                        }
                }

            }
            
            return builder;
        }

        // ADMIN USER
        public static StringBuilder AdminMenu(User admin)
        {
            var builder = ViewerEditorDeletorMenu(admin);
            builder
                .AppendLine("--------------------")
                .AppendLine("9. Create new User")
                .AppendLine("10. View all Users")
                .AppendLine("11. Update a User")
                .AppendLine("12. Delete a User")
                .AppendLine("13.Exit");

            Console.WriteLine(builder);
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        Message.ViewInbox(admin);
                        break;
                    }
                case 2:
                    {
                        Message.ViewOutbox(admin);
                        break;
                    }
                case 3:
                    {
                        Message.UpdatePrivate(admin);
                        break;
                    }
                case 4:
                    {
                        Message.DeletePrivate(admin);
                        break;
                    }
                case 5:
                    {
                        Message.Send(admin);
                        break;
                    }
                case 6:
                    {
                        Message.ViewAll(admin);
                        break;
                    }
                case 7:
                    {
                        Message.UpdateAll(admin);
                        break;
                    }
                case 8:
                    {
                        Message.DeleteAll(admin);
                        break;
                    }
                case 9:
                    {
                        User.AdminCreatesUser(admin);
                        break;
                    }
                case 10:
                    {
                        User.AdminViewsUsers(admin);
                        break;
                    }
                case 11:
                    {
                        User.AdminUpdatesUser(admin);
                        break;
                    }
                case 12:
                    {
                        User.AdminDeletesUser(admin);
                        break;
                    }
                case 13:
                    {
                        Environment.Exit(2);
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid option");
                        AdminMenu(admin);
                        break;
                    }
            }
            return builder;
        }
        
    }
}
