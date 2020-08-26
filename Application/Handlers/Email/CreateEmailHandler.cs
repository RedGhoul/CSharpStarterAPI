using Application.Commands;
using Application.Response.Email;
using AutoMapper;
using Domain.Entities;
using ExternalServices.Email;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Email
{
    public class CreateEmailHandler : IRequestHandler<SendEmailCommand, SendEmailResponse>
    {
        private readonly IMapper _Mapper;
        private readonly ILogger<CreateEmailHandler> _Logger;
        private readonly ApplicationDbContext _Context;
        private readonly ISendEmailService _SendEmailService;
        private readonly IBackgroundJobClient _BackgroundJobClient;
        public CreateEmailHandler(ApplicationDbContext context,
            IMapper mapper, ILogger<CreateEmailHandler> logger,
            ISendEmailService sendEmailService, IBackgroundJobClient backgroundJobClient)
        {
            _Mapper = mapper;
            _Logger = logger;
            _Context = context;
            _SendEmailService = sendEmailService;
            _BackgroundJobClient = backgroundJobClient;
        }

        public async Task<SendEmailResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    return await Task.FromResult(new SendEmailResponse() { Success = false });
                }

                _BackgroundJobClient.Enqueue(() => SendEmail(request));
                return await Task.FromResult(new SendEmailResponse() { Success = true });
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Failed to Enqueue Email Job. Cause By ${ex.InnerException}");

                return await Task.FromResult(new SendEmailResponse() { Success = false });
            }

        }

        public async Task SendEmail(SendEmailCommand request)
        {
            if (await _SendEmailService.SendSimpleSingleEmail(request.Recipient,
                request.Subject, request.BodyHTML, request.BodyPlainText))
            {
                await _Context.SentEmailRecords.AddAsync(_Mapper.Map<SentEmailRecord>(request));
                await _Context.SaveChangesAsync();
            }

        }

    }
}
