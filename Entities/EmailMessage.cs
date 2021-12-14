using MailKit.Net.Smtp;
using MimeKit;

namespace SmartfaceSolution.Entities
{
    public class EmailMessage
    {
        private string toAddress;
        private string timestamp;
        private string message;

        public EmailMessage(string toAddress, string timestamp)
        {
            this.toAddress = toAddress;
            this.timestamp = timestamp;
        }

        public string ToAddress
        {
            get => toAddress;
            set => toAddress = value;
        }

        public string Timestamp
        {
            get => timestamp;
            set => timestamp = value;
        }

        public string Message
        {
            get => message;
            set => message = value;
        }

        public void Official_Send_Email_Example()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,
                    I just wanted to let you know that Monica and I were going to go play some paintball, you in?
                     -- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("localhost", 1025, false);

                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}