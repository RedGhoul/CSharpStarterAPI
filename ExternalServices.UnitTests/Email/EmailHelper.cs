using SendGrid.Helpers.Mail;

namespace ExternalServices.UnitTests.Email
{
    public static class EmailHelper
    {
        public static SendGridMessage Get_Valid_SendGridMsg()
        {
            EmailAddress from = new EmailAddress("avaneesab5@gmail.com", "CAbot@ca.com");
            SendGridMessage msg = MailHelper.CreateSingleEmail(from,
                new EmailAddress("joe@gmail.com", "CAUser"), "Test Email", "Hello", "<h1>Hello</h1>");
            return msg;
        }
    }
}
