using Application.DTO;
using Application.Validation.Helpers;
using FluentValidation;

namespace Application.Validation
{
    public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDTO>
    {

        public UpdateEventDTOValidator(EventValidationHelpers eventValidation)
        {
            RuleFor(x => x.EventTypeId)
                .MustAsync(eventValidation.BeValidEventTypeId)
               .WithMessage(EventErrorMessages.BeValidEventTypeId);
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(2000);
            RuleFor(x => x.Name).MinimumLength(5).Unless(x => x.Cost > 50000);
        }

    }
}
