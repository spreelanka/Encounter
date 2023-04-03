using System;
using Moq;
using Moq.Protected;
using static EncounterMobileUnitTests.BaseApiServiceTests;
using System.Text;

namespace EncounterMobileUnitTests
{
	public class MonsterServiceTests
	{
        private Task<HttpResponseMessage> GetMockResponse(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.LocalPath == "/expectedPath")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(GetJson(), Encoding.UTF8, "application/json");
                return Task.FromResult(response);
            }
            else
            {
                var response2 = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                response2.Content = new StringContent(GetJson(), Encoding.UTF8, "application/json");
                return Task.FromResult(response2);
            }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetMonster_OkParses()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object);

            var result = await subject.Get<Response>("/expectedPath");
            Assert.Pass();
        }
    }
}

