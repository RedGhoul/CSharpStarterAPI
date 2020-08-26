namespace Application.DTO.Email
{
    public class SendEmailDTO
    {
        public string Recipient { get; set; }
        public string BodyHTML { get; set; }
        public string BodyPlainText { get; set; }
        public string Subject { get; set; }
    }
}
