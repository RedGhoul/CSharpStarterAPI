using Application.DTO.Email;

namespace Common.Tests.Generators.DTO
{
    public static class EmailDTOGenerator
    {
        public static SendEmailDTO GetdEmailEventDTOWithInvalidBodyPlainTextLength()
        {
            string BodyPlainText = "";
            for (int i = 0; i < 600; i++)
            {
                BodyPlainText += i;
            }
            return new SendEmailDTO()
            {
                Recipient = "john@gmail.com",
                BodyHTML = "<h1>Hi</h1>",
                BodyPlainText = BodyPlainText,
                Subject = "Test Subject"
            };
        }

        public static SendEmailDTO GetdEmailEventDTOWithInvalidBodyHTMLLength()
        {
            string BodyHTML = "";
            for (int i = 0; i < 3000; i++)
            {
                BodyHTML += i;
            }
            return new SendEmailDTO()
            {
                Recipient = "john@gmail.com",
                BodyHTML = BodyHTML,
                BodyPlainText = "Hi",
                Subject = "Test Subject"
            };
        }

        public static SendEmailDTO GetdEmailEventDTOWithInvalidSubjectLength()
        {
            return new SendEmailDTO()
            {
                Recipient = "john@gmail.com",
                BodyHTML = "<h1>Hi</h1>",
                BodyPlainText = "Hi",
                Subject = "Test Subject 1212121212121212121212222222222222222222222222222222222222"
            };
        }

        public static SendEmailDTO GetValidSendEmailDTO()
        {
            return new SendEmailDTO()
            {
                Recipient = "john@gmail.com",
                BodyHTML = "<h1>Hi</h1>",
                BodyPlainText = "Hi",
                Subject = "Test Subject"
            };
        }

        public static SendEmailDTO GetInValidEmailEventDTO()
        {
            return new SendEmailDTO()
            {
                Recipient = "johngmail.com",
                BodyHTML = "<h1>Hi</h1>",
                BodyPlainText = "Hi",
                Subject = "Test Subject"
            };
        }
    }
}
