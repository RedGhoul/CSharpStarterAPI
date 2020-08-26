using Application.Commands;

namespace Common.Tests.Generators.CommandQuery
{
    public static class EmailCommandQueryGenerator
    {
        public static SendEmailCommand GetValidCreateEmailCommand()
        {
            return new SendEmailCommand()
            {
                Recipient = "john@gmail.com",
                BodyHTML = "<h1>Hello</h1>",
                BodyPlainText = "Hello There",
                Subject = "this is a subject"
            };
        }

    }
}
