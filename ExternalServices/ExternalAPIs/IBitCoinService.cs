using ExternalServices.DTO;
using System.Threading.Tasks;

namespace ExternalServices
{
    public interface IBitCoinService
    {
        public Task<BitCoinInfo> GetInfo();
    }
}
