using System;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Hosting.Internal;

namespace SmartfaceSolution.Message
{
    /// <summary>
    /// Class <c>Message</c> have two method that sends the detection notification message to the member
    /// </summary>
    public class Message
    {
        private static string messageText;

        public Message(int cameraPosition, string name, string timestamp)
        {
            messageText = "Dear " + name + ",\nYou have checked-" + (cameraPosition == 1 ? "in" : "out") +
                          " today at " + timestamp + "\nThanks."; // message that will be sent 
        }

        /// <summary>
        /// Method <c>sendEmail</c> will send the message using the email
        /// We are using the MailKit library to create the SMTP client
        /// </summary>
        /// <param name="memberEmail">the member email</param>
        public void sendEmail(string memberEmail)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("HAWK", "ersthethreemusketeers@gmail.com"));
            message.To.Add(
                MailboxAddress.Parse(memberEmail));
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
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
        /// <summary>
        /// Method <c>sendSMS</c> will send a message using the REST API of BulkSMS 
        /// </summary>
        /// <param name="phoneNumber"></param>

        public void sendSMS(string phoneNumber)
        {
            string url = "https://api.bulksms.com/v1/messages";// the URL thar use for the SMS service 
            // user account information
            string username = "rahaf_id";
            string password = "R6620551r";

            // the details of the message we want to send as JSON format 
            string data = "{to: \"" + phoneNumber + "\", body:\"" + messageText + "\"}";
            var request = WebRequest.Create(url);// create the web requests
            request.Credentials = new NetworkCredential(username, password);
            request.PreAuthenticate = true;
            request.Method = "POST"; // HTTP post method 
            request.ContentType = "application/json";// determine the JSON type
            var encoding = new UnicodeEncoding();// encode the message using the Unicode encoding
            var encodedData = encoding.GetBytes(data);
            var stream = request.GetRequestStream();
            try
            {
                stream.Write(encodedData, 0, encodedData.Length);// execute the request and write the streams
            }
            finally
            {
                stream.Flush();
                stream.Close();
            }
            var response = request.GetResponse();
            // read the response of the request 
            var reader = new StreamReader(response.GetResponseStream());
            Debug.WriteLine(reader);
        }
    }
}
