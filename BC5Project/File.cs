using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BC5Project
{
    class File
    {
        // Save messages using Streamwriter - Catalog them according to Sender Folders and into Message ID subfolders
        public static void SaveMessage(Message message, string username)
        {
            var directoryPath = @"C:\Users\alexa\source\BC5\Classroom Projects\BC5Project\Messages";
            var usernamePath = $"Sender_{username}";

            //Combine the directory and the username as a new Directory Path
            var path = Path.Combine(directoryPath, usernamePath);

            //Create the new Directory
            Directory.CreateDirectory(path);

            // Catalog the messages according to their ID(inside of each Sender folder)
            string messagePath = $"MessageID_{message.ID}.txt";

            //add the message path to the whole path
            var filePath = Path.Combine(path, messagePath);

            if (!System.IO.File.Exists(filePath))
            {
                using (StreamWriter stream = System.IO.File.CreateText(filePath))
                {
                    stream.WriteLine($"Date Of Submission: {message.DateOfSubmission.ToShortDateString()}");
                    stream.WriteLine($"Sender: {message.Sender}");
                    stream.WriteLine($"Receiver: {message.Receiver}");
                    stream.WriteLine($"Message Data: {message.MessageData}");
                }
                Console.WriteLine("Message saved");
            }
            else
                Console.WriteLine("File Already Exists");
        }

        // Append message to the same file it was saved
        public static void AppendMessage(Message message, string username)
        {
            var directoryPath = @"C:\Users\alexa\source\BC5\Classroom Projects\BC5Project\Messages";
            var usernamePath = $"Sender_{username}";
            string messagePath = $"MessageID_{message.ID}.txt";

            var path = Path.Combine(directoryPath, usernamePath, messagePath);

            if (System.IO.File.Exists(path))
            {
                // if the file is found, append into the same txt file
                using (StreamWriter stream = new StreamWriter(path, true))
                {
                    stream.WriteLine();
                    stream.WriteLine($"Date Of Submission: {message.DateOfSubmission.ToShortDateString()}");
                    stream.WriteLine($"Sender: {username}");
                    stream.WriteLine($"Receiver: {message.Receiver}");
                    stream.WriteLine($"Message Data: {message.MessageData}");
                }
                Console.WriteLine("Message Updated");
            }
            else
                Console.WriteLine("File Does Not Exist");
        }
    }
}
