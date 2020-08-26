using ExternalServices.DTO;

namespace Common.Tests.Generators.Services
{
    public static class BitCoinServiceDataGenerator
    {
        public static BitCoinInfo GetValidBitCoinInfo()
        {
            return new BitCoinInfo()
            {
                Bpi = new Bpi()
                {
                    USD = new USD()
                    {
                        Rate = "8999"
                    }
                }
            };
        }

        public static BitCoinInfo GetInValidBitCoinInfo()
        {
            return new BitCoinInfo()
            {
            };
        }
    }
}
