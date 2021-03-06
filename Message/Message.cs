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
    /// Class <c>Message</c> Will sends the detection notification message to the member
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
    }
}
