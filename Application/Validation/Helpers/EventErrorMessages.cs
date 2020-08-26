namespace Application.Validation.Helpers
{
    public static class EventErrorMessages
    {
        public const string BeValidEventTypeId = "The Entity Type Id give does not exist";
        public const string AfterTaxCostMustByLowerThenBitCoinUSD = @"The cost given after taxes is higher then the current price of BitCoin. Lower Cost";
    }
}
