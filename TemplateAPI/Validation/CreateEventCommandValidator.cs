using FluentValidation;
using TemplateAPI.DAL.CQRS.Commands.Events;

namespace TemplateAPI.Validation
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        // can inject what ever you want in here
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(2000);
            RuleFor(x => x.Name).MinimumLength(5);
        }
    }
}
