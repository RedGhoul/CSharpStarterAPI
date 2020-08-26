using Application.DTO;
using Application.Validation.Helpers;
using ExternalServices;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Validation.Event
{
    public class CreateEventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        private readonly ILogger<CreateEventDTOValidator> _Logger;
        private readonly IBitCoinService _BitCoinService;

        // can inject what ever you want in here
        public CreateEventDTOValidator(EventValidationHelpers eventValidation, IBitCoinService BitCoinService, ILogger<CreateEventDTOValidator> logger)
        {
            _BitCoinService = BitCoinService;
            _Logger = logger;

            RuleFor(x => x.Cost).GreaterThanOrEqualTo(2000);

            RuleFor(x => x.Cost).MustAsync(PercentCostMustByHigherThenBitCoinUSD)
                .WithMessage(EventErrorMessages.AfterTaxCostMustByLowerThenBitCoinUSD);

            RuleFor(x => x.EventTypeId).MustAsync(eventValidation.BeValidEventTypeId)
                .WithMessage(EventErrorMessages.BeValidEventTypeId);

            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.Description).MaximumLength(500);

        }

        private async Task<bool> PercentCostMustByHigherThenBitCoinUSD(decimal cost, CancellationToken arg)
        {
            _Logger.LogInformation($"Cost that came in was ${cost}");
            ExternalServices.DTO.BitCoinInfo info = await _BitCoinService.GetInfo();
            if (info.Bpi == null)
            {
                _Logger.LogError($"BitCoinService GetInfo has new BPI information");
                return false;
            }
            float percentCost = (float)cost * 0.95f;
            return float.Parse(info.Bpi.USD.Rate) > percentCost;
        }


    }
}
