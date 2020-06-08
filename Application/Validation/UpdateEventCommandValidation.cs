using Application.Commands.Events;
using FluentValidation;

namespace Application.Validation
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        // can inject what ever you want in here
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(2000);
            RuleFor(x => x.Name).MinimumLength(5);
        }
    }
}
