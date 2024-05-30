using System.Net;
using System.Net.Mail;

namespace TicketProcessor
{
    public class EmailSender
    {
        public void SendEmail(Ticket ticket)
        {
            var fromAddress = new MailAddress("sendermail@gmail.com", "Muthu");
            var toAddress = new MailAddress("receivermail@gmail.com", "Admin");
            var fromPassword = "SamplePassword";
            const string subject = "High Priority Ticket Alert";
            string body = $"Ticket ID: {ticket.Id}\nUser: {ticket.UserId}\nPriority: {ticket.Priority}\nModule: {ticket.Module}\nTitle: {ticket.Title}\nOrder ID: {ticket.OrderId}\nDescription: {ticket.Description}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
