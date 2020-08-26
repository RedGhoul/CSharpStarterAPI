using Application.Response.Email;

namespace Common.Tests.Generators.Response
{
    public static class EmailResponseGenerator
    {
        public static SendEmailResponse CreateValidSendEmailResponse()
        {
            return new SendEmailResponse() { Success = true };
        }

        public static SendEmailResponse CreateInValidSendEmailResponse()
        {
            return new SendEmailResponse() { Success = false };
        }
    }
}
