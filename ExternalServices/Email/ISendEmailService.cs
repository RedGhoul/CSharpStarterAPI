using System.Threading.Tasks;

namespace ExternalServices.Email
{
    public interface ISendEmailService
    {
        public Task<bool> SendSimpleSingleEmail(string Recipent,
                    string Subject,
                    string HTMLContent,
                    string PlainTextContent);
    }
}
