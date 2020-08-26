using Application.Response.Email;
using MediatR;
using System;

namespace Application.Commands
{
    public class SendEmailCommand : IRequest<SendEmailResponse>
    {
        public SendEmailCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public string Recipient { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BodyHTML { get; set; }
        public string BodyPlainText { get; set; }
        public string Subject { get; set; }
    }
}
