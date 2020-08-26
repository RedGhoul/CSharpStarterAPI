using System;

namespace Domain.Entities
{
    public class SentEmailRecord
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BodyHTML { get; set; }
        public string BodyPlainText { get; set; }
        public string Subject { get; set; }
    }
}
