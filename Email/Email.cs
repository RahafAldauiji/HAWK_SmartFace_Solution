using System;
using MailKit.Net.Smtp;
using MimeKit;
namespace SmartfaceSolution.Email
{
    public class Email
    {
        public static void sendEmail()
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Rahaf","ersthethreemusketeers@gmail.com"));
            message.To.Add(MailboxAddress.Parse("rahafaldauiji12@gmail.com"));// Change the email to the watchlistMember email
            message.Subject = "TestMessage";
            // plain is simple text message 
            message.Body = new TextPart("plain")
            {
                Text = @"Hi., This message just for testing  the code if it working Successfully>> from Rahaf ;)"
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
                client.Connect("smtp.gmail.com",465,true);
                client.Authenticate(email,password);
                client.Send(message);
                Console.WriteLine("Email Sent ;)");

            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            } 
        }
    }
}