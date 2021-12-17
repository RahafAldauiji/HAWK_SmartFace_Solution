using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Net;
using System.Text;

namespace SmartfaceSolution.Message
{
    public class Message
    {
        private static string messageText;   
        public Message(int cameraPosition,string name, string timestamp)
        {
            messageText = "Dear " + name + ",\nYou have checked-" + (cameraPosition == 1 ? "in" : "out") + " today at " + timestamp + "\nThanks.";
        }
        public  void sendEmail(string memberEmail)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("HAWK", "ersthethreemusketeers@gmail.com"));
            message.To.Add(
                MailboxAddress.Parse(memberEmail)); // Change the email to the watchlistMember email
            message.Subject = "Attendance";
            // plain is simple text message 
            message.Body = new TextPart("plain")
            {
                Text = messageText
            };

            // Email
            string email = "ersthethreemusketeers@gmail.com";
            string password = "ers181817";


            //Create SMTP client
            SmtpClient client = new SmtpClient();

            //Create connection 
            try
            {
                // connect to the Gmail smtp server
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(email, password);
                client.Send(message);
                Console.WriteLine("Email Sent ;)");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public void sendSMS(string phoneNumber)
        {
            string myURI = "https://api.bulksms.com/v1/messages";
            string myUsername = "rahaf_id";
            string myPassword = "R6620551r";

            // the details of the message we want to send
            string myData = "{to: \""+phoneNumber+"\", body:\""+messageText+"\"}";

            var request = WebRequest.Create(myURI);
            request.Credentials = new NetworkCredential(myUsername, myPassword);
            request.PreAuthenticate = true;
            request.Method = "POST";
            request.ContentType = "application/json";
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);
            var stream = request.GetRequestStream();
            try
            {
                stream.Write(encodedData, 0, encodedData.Length);
            }
            finally
            {
                stream.Flush();
                stream.Close();
            }

            var response = request.GetResponse();
            // read the response and print it to the console
            var reader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(reader.ReadToEnd());
        }
    }
}