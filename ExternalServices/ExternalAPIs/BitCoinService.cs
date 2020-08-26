using ExternalServices.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalServices
{
    public class BitCoinService : IBitCoinService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AsyncRetryPolicy<BitCoinInfo> _retryPolicy;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BitCoinService> _logger;
        public BitCoinService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<BitCoinService> logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
            _retryPolicy = Policy<BitCoinInfo>
                .Handle<HttpRequestException>().RetryAsync(
                int.Parse(_configuration.GetSection("External")["MAX_API_RETRY"]));
        }

        public async Task<BitCoinInfo> GetInfo()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                _configuration.GetSection("External")["API_END_POINT_BITCOIN"]);

            HttpClient client = _clientFactory.CreateClient("BitCoin");

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<BitCoinInfo>(await response.Content.ReadAsStringAsync());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to DeserializeObject: {ex.InnerException}");
                        return new BitCoinInfo();
                    }
                }
                else
                {
                    return new BitCoinInfo();
                }
            });

        }
    }
}
