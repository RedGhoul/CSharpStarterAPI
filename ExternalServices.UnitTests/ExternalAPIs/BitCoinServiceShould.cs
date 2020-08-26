using ExternalServices.DTO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExternalServices.UnitTests
{
    [Trait("Category", "ExternalServices")]
    [Trait("Category", "ExternalAPIs")]
    public class BitCoinServiceShould
    {
        private readonly BitCoinService _sut;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly Mock<ILogger<BitCoinService>> _logger;
        private readonly string NamedHttpClient = "BitCoin";
        private readonly string FakeReturnString =
            @"{""time"":{""updated"":""Sep 18, 2013 17:27:00 UTC"",
            ""updatedISO"":""2013-09-18T17:27:00+00:00""},""disclaimer"":
            ""This data was produced from the CoinDesk Bitcoin Price Index.Non-USD currency 
            data converted using hourly conversion rate from openexchangerates.org"",""bpi"":
            {""USD"":{""code"":""USD"",""symbol"":""$"",""rate"":""126.5235"",""description"":
            ""United States Dollar"",""rate_float"":126.5235},""GBP"":{""code"":""GBP"",""symbol"":
            ""£"",""rate"":""79.2495"",""description"":""British Pound Sterling"",""rate_float"":
            79.2495},""EUR"":{""code"":""EUR"",""symbol"":""€"",""rate"":""94.7398"",""description"":
            ""Euro"",""rate_float"":94.7398}}}";

        public BitCoinServiceShould()
        {
            _configuration = new Mock<IConfiguration>();
            _logger = new Mock<ILogger<BitCoinService>>();
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpClient httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            _configuration.Setup(s => s.GetSection("External")["MAX_API_RETRY"])
                .Returns("2");
            _configuration.Setup(s => s.GetSection("External")["API_END_POINT_BITCOIN"])
                .Returns("http://test.com/");


            _httpClientFactory.Setup(x => x.CreateClient(NamedHttpClient))
                                 .Returns(httpClient);



            _sut = new BitCoinService(_httpClientFactory.Object, _configuration.Object, _logger.Object);
        }

        [Fact]
        public async void Return_Empty_BitCoinInfo_On_Non_Success_StatusCode()
        {
            // Arrange
            _httpMessageHandler.Protected()
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.BadRequest,
                       Content = new StringContent(FakeReturnString),
                   })
                   .Verifiable();
            // Act
            BitCoinInfo result = await _sut.GetInfo();

            // Assert
            result.Should().BeOfType<BitCoinInfo>();
            result.Bpi.Should().BeNull();
        }

        [Fact]
        public async void Return_Empty_BitCoinInfo_On_Failed_Deserialization()
        {
            // Arrange
            _httpMessageHandler.Protected()
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.BadRequest,
                       Content = new StringContent("[{'id':1,'value':'1'}]"),
                   })
                   .Verifiable();
            // Act
            BitCoinInfo result = await _sut.GetInfo();

            // Assert
            result.Should().BeOfType<BitCoinInfo>();
            result.Bpi.Should().BeNull();
        }


        [Fact]
        public async void Return_BitCoinInfo_On_Success_StatusCode()
        {
            // Arrange
            _httpMessageHandler.Protected()
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(FakeReturnString),
                   })
                   .Verifiable();

            // Act
            BitCoinInfo result = await _sut.GetInfo();

            // Assert
            result.Should().BeOfType<BitCoinInfo>();
            result.Bpi.USD.Rate.Should().NotBeNullOrEmpty();
        }
    }
}
