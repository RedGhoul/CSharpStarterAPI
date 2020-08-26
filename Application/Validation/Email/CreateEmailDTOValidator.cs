using Application.DTO.Email;
using FluentValidation;

namespace Application.Validation.Email
{
    public class CreateEmailDTOValidator : AbstractValidator<SendEmailDTO>
    {

        public CreateEmailDTOValidator()
        {
            RuleFor(x => x.Recipient).EmailAddress();
            RuleFor(x => x.BodyHTML).MaximumLength(1000);
            RuleFor(x => x.BodyPlainText).MaximumLength(500);
            RuleFor(x => x.Subject).MaximumLength(60);
        }

    }
}
