using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;


namespace BC5Project
{
    class Message
    {
        private const int TextLength = 250;

        public int ID { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

        private string messageData;
        public string MessageData
        {
            get
            {
                return messageData;
            }
            set
            {
                if (value.Length > TextLength)
                {
                    throw new ArgumentException($"Message should be less than {TextLength} characters");
                }
                messageData = value;
            }
        }

        public Message()
        {
            DateOfSubmission = DateTime.Now.Date;
        }


        // INBOX
        public static void ViewInbox(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand selectInbox = new SqlCommand
                        ($"SELECT * FROM Message WHERE Receiver = '{user.Username}'",sqlConnection);
                    SqlDataReader inboxReader = selectInbox.ExecuteReader();

                    var inboxMessages = new List<Message>();

                    if (inboxReader.HasRows)
                    {
                        while (inboxReader.Read())
                        {
                            Message message = new Message();
                            message.ID = inboxReader.GetInt32(0);
                            message.DateOfSubmission = inboxReader.GetDateTime(1);
                            message.Sender = inboxReader.GetString(2);
                            message.Receiver = inboxReader.GetString(3);
                            message.messageData = inboxReader.GetString(4);

                            inboxMessages.Add(message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have any Inbox at the moment.");
                    }
                    
                    inboxReader.Close();

                    foreach(var message in inboxMessages)
                    {
                        Console.WriteLine(message);
                    }


                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1.Return to Main Menu");
                    Console.WriteLine("2.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
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

        // OUTBOX
        public static void ViewOutbox(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand selectOutbox = new SqlCommand
                        ($"SELECT * FROM Message WHERE Sender = '{user.Username}'", sqlConnection);
                    SqlDataReader outboxReader = selectOutbox.ExecuteReader();

                    var outboxMessages = new List<Message>();

                    if (outboxReader.HasRows)
                    {
                        while (outboxReader.Read())
                        {
                            Message message = new Message();
                            message.ID = outboxReader.GetInt32(0);
                            message.DateOfSubmission = outboxReader.GetDateTime(1);
                            message.Sender = outboxReader.GetString(2);
                            message.Receiver = outboxReader.GetString(3);
                            message.messageData = outboxReader.GetString(4);

                            outboxMessages.Add(message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have any Outbox at the moment");
                    }

                    outboxReader.Close();

                    foreach (var msg in outboxMessages)
                    {
                        Console.WriteLine(msg);
                    }

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1.Return to Main Menu");
                    Console.WriteLine("2.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
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

        // VIEW All Messages
        public static void ViewAll(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand selectMessages = new SqlCommand
                        ($"SELECT * FROM Message", sqlConnection);
                    SqlDataReader messageReader = selectMessages.ExecuteReader();

                    var messages = new List<Message>();

                    if (messageReader.HasRows)
                    {
                        while (messageReader.Read())
                        {
                            Message message = new Message();
                            message.ID = messageReader.GetInt32(0);
                            message.DateOfSubmission = messageReader.GetDateTime(1);
                            message.Sender = messageReader.GetString(2);
                            message.Receiver = messageReader.GetString(3);
                            message.messageData = messageReader.GetString(4);

                            messages.Add(message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no messages to view.");
                    }

                    messageReader.Close();

                    foreach (var msg in messages)
                    {
                        Console.WriteLine(msg);
                    }

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1.Return to Main Menu");
                    Console.WriteLine("2.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
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

        // UPDATE My Message
        public static void UpdatePrivate(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdMessage = new SqlCommand
                        ($"SELECT * FROM Message WHERE Receiver = '{user.Username}' OR Sender = '{user.Username}'", sqlConnection);
                    SqlDataReader reader = cmdMessage.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        Message msg = new Message();
                        msg.ID = reader.GetInt32(0);
                        msg.DateOfSubmission = reader.GetDateTime(1);
                        msg.Sender = reader.GetString(2);
                        msg.Receiver = reader.GetString(3);
                        msg.messageData = reader.GetString(4);

                        messages.Add(msg);
                    }
                    reader.Close();

                    foreach (var message in messages)
                    {
                        Console.WriteLine(message);
                    }

                    Message newMessage = new Message();

                    Console.WriteLine("Select the ID of the message you want to update : ");
                    newMessage.ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Write the new message");

                    newMessage.MessageData = Console.ReadLine();
                    
                    SqlCommand cmdEditMessage = new SqlCommand
                        ($"UPDATE Message SET MessageData = '{newMessage.MessageData}',DateOfSubmission = '{DateTime.Now.Date}' WHERE ID = '{newMessage.ID}' ", sqlConnection);
                    int rowsUpdated = cmdEditMessage.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine($"Message Updated Successfull");
                        Console.WriteLine($"{rowsUpdated} rows updated successfully");
                    }

                    // append to the existing file when an update occurs
                    File.AppendMessage(newMessage, user.Username);

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1. Update another Message");
                    Console.WriteLine("2.Return to Main Menu");
                    Console.WriteLine("3.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                UpdatePrivate(user);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    sqlConnection.Close();

                }
            }
        }

        // UPDATE Other Messages
        public static void UpdateAll(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdMessage = new SqlCommand
                        ($"SELECT * FROM Message", sqlConnection);
                    SqlDataReader reader = cmdMessage.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        Message msg = new Message();
                        msg.ID = reader.GetInt32(0);
                        msg.DateOfSubmission = reader.GetDateTime(1);
                        msg.Sender = reader.GetString(2);
                        msg.Receiver = reader.GetString(3);
                        msg.messageData = reader.GetString(4);

                        messages.Add(msg);
                    }
                    reader.Close();

                    foreach (var message in messages)
                    {
                        Console.WriteLine(message);
                    }

                    Message newMessage = new Message();

                    Console.WriteLine("Select the ID of the message you want to update : ");
                    newMessage.ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Write the new message");

                    newMessage.MessageData = Console.ReadLine();

                    SqlCommand cmdEditMessage = new SqlCommand
                        ($"UPDATE Message SET MessageData = '{newMessage.MessageData}',DateOfSubmission = '{DateTime.Now.Date}' WHERE ID = '{newMessage.ID}' ", sqlConnection);
                    int rowsUpdated = cmdEditMessage.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine($"Message Updated Successfull");
                        Console.WriteLine($"{rowsUpdated} rows updated successfully");
                    }

                    // append to the existing file when an update occurs
                    File.AppendMessage(newMessage, newMessage.Sender);

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1. Update another Message");
                    Console.WriteLine("2.Return to Main Menu");
                    Console.WriteLine("3.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();
                                UpdatePrivate(user);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    sqlConnection.Close();

                }
            }

        }

        // DELETE My Messages
        public static void DeletePrivate(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdMessage = new SqlCommand
                        ($"SELECT * FROM Message WHERE Receiver = '{user.Username}' OR Sender = '{user.Username}'", sqlConnection);
                    SqlDataReader reader = cmdMessage.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        Message message = new Message();
                        message.ID = reader.GetInt32(0);
                        message.DateOfSubmission = reader.GetDateTime(1);
                        message.Sender = reader.GetString(2);
                        message.Receiver = reader.GetString(3);
                        message.messageData = reader.GetString(4);

                        messages.Add(message);
                    }
                    reader.Close();

                    foreach (var message in messages)
                    {
                        Console.WriteLine(message);
                    }

                    Console.WriteLine("Select the ID of the message you want to delete : ");
                    int messageToDelete = int.Parse(Console.ReadLine());


                    SqlCommand cmdDeleteMessage = new SqlCommand
                        ($"DELETE FROM UserMessages WHERE MsgID = '{messageToDelete}' AND UserID = '{user.ID}'", sqlConnection);

                    int rowsUpdated = cmdDeleteMessage.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine($"Message Delete Successfull");
                        Console.WriteLine($"{rowsUpdated} rows updated successfully");
                    }

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1. Delete another Message");
                    Console.WriteLine("2.Return to Main Menu");
                    Console.WriteLine("3.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                DeletePrivate(user);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    sqlConnection.Close();

                }
            }
        }

        // DELETE Other Messages
        public static void DeleteAll(User user)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdMessage = new SqlCommand
                        ($"SELECT * FROM Message", sqlConnection);
                    SqlDataReader reader = cmdMessage.ExecuteReader();

                    var messages = new List<Message>();

                    while (reader.Read())
                    {
                        Message message = new Message();
                        message.ID = reader.GetInt32(0);
                        message.DateOfSubmission = reader.GetDateTime(1);
                        message.Sender = reader.GetString(2);
                        message.Receiver = reader.GetString(3);
                        message.messageData = reader.GetString(4);

                        messages.Add(message);
                    }
                    reader.Close();

                    foreach (var message in messages)
                    {
                        Console.WriteLine(message);
                    }

                    Console.WriteLine("Select the ID of the message you want to delete : ");
                    int messageToDelete = int.Parse(Console.ReadLine());


                    SqlCommand cmdDeleteMessage = new SqlCommand
                        ($"DELETE FROM UserMessages WHERE MsgID = '{messageToDelete}'", sqlConnection);

                    int rowsUpdated = cmdDeleteMessage.ExecuteNonQuery();
                    if (rowsUpdated > 0)
                    {
                        Console.WriteLine($"Message Delete Successfull");
                        Console.WriteLine($"{rowsUpdated} rows updated successfully");
                    }

                    // What to do next?
                    Console.WriteLine("------------------");
                    Console.WriteLine("1. Delete another Message");
                    Console.WriteLine("2.Return to Main Menu");
                    Console.WriteLine("3.Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                DeletePrivate(user);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Menu.MainMenu(user);
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
                                Menu.MainMenu(user);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    sqlConnection.Close();

                }
            }
        }

        // SEND Message
        public static void Send(User sender)
        {
            SqlConnection sqlConnection = Database.Access();

            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();

                    Message msg = new Message();

                    Console.WriteLine("Type the Username of the User you would like to send a message to: ");
                    msg.Receiver = Console.ReadLine();

                    while (!Validation.CheckUser(msg.Receiver))
                    {
                        Console.WriteLine("User Does Not Exist");
                        Console.WriteLine("Type another User: ");
                        msg.Receiver = Console.ReadLine();
                        Validation.CheckUser(msg.Receiver);
                    }

                    //Message Form
                    Console.WriteLine("Type the Message you want to send:");
                    msg.MessageData = Console.ReadLine();

                    // Insert message into message table
                    SqlCommand sendMessage = new SqlCommand
                    ($"INSERT INTO Message (DateOfSubmission, Sender, Receiver, MessageData) VALUES('{msg.DateOfSubmission}',(SELECT Username FROM [User] WHERE Username = '{sender.Username}'), (SELECT Username FROM [User] WHERE Username = '{msg.Receiver}'), '{msg.MessageData}')", sqlConnection);

                    int rowsInserted = sendMessage.ExecuteNonQuery();
                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Message Sent.");
                        Console.WriteLine($"{rowsInserted} rows inserted successfully");
                    }

                    // Select Message ID to use for other insertions
                    SqlCommand selectMsgId = new SqlCommand
                        ($"SELECT ID FROM Message WHERE DateOfSubmission = '{msg.DateOfSubmission}' AND Sender = '{sender.Username}' AND Receiver = '{msg.Receiver}' AND MessageData = '{msg.MessageData}'", sqlConnection);
                    SqlDataReader idReader = selectMsgId.ExecuteReader();

                    while (idReader.Read())
                    {
                        msg.ID = idReader.GetInt32(0);
                    }

                    idReader.Close();

                    // Insert message once into UserMessages
                    SqlCommand insertSender = new SqlCommand
                        ($"INSERT INTO UserMessages (MsgID,UserID) VALUES ((SELECT Message.ID FROM Message WHERE MessageData = '{msg.messageData}'),(SELECT ID FROM [User] WHERE Username = '{sender.Username}'))", sqlConnection);
                    int rowsUpdated = insertSender.ExecuteNonQuery();


                    // Insert second time message into UserMessages
                    SqlCommand insertReceiver = new SqlCommand
                        ($"INSERT INTO UserMessages (MsgID,UserID) VALUES ((SELECT Message.ID FROM Message WHERE MessageData = '{msg.messageData}'),(SELECT ID FROM [User] WHERE Username = '{msg.Receiver}'))", sqlConnection);
                    int updatedRows = insertReceiver.ExecuteNonQuery();

                    File.SaveMessage(msg,sender.Username);

                    // What to do next?
                    Console.WriteLine("Would you like to send another message? Y/N");
                    string answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "Y":
                            {
                                Console.Clear();
                                Send(sender);
                                break;
                            }
                        case "N":
                            {
                                Console.Clear();
                                Menu.MainMenu(sender);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid option");
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


        // PRINT Message Data
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("------MESSAGE INFO------")
                .AppendLine($"ID: {ID}")
                .AppendLine($"Date Sent: {DateOfSubmission}")
                .AppendLine($"Sender: {Sender}")
                .AppendLine($"Receiver: {Receiver}")
                .AppendLine($"Message: {MessageData}");

            return builder.ToString();
        }
    }
}
