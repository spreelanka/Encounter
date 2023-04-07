using System;
using System.Net.Http;
using System.Text;
using EncounterMobile.NetworkPolicies;
using EncounterMobile.Services;
using Moq;
using Moq.Protected;
using Polly;
using Polly.Registry;

namespace EncounterMobileUnitTests
{
	public class BaseApiServiceTests
	{
        BaseApiService subject;
        
        public class ConcreteBaseApiService : BaseApiService
        {
            public ConcreteBaseApiService(HttpMessageHandler messageHandler, IReadOnlyPolicyRegistry<string> policyRegistry) : base(messageHandler, policyRegistry)
            {
            }

            protected override string BaseUri => "https://api.example.com";
        }

        private string GetJson()
        {
            return "{ \"isAuthenticated\": true }";
        }

        private class Response
        {
            public bool isAuthenticated { get; set; }
        }

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

        IReadOnlyPolicyRegistry<string> policyRegistry;
        [SetUp]
        public void Setup()
        {
            policyRegistry = new PolicyRegistry
            {
                { PolicyNames.DefaultPolicy, Policy.NoOpAsync() }
            };
        }

        [Test]
        public async Task Get_OkParses()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);

            var result = await subject.Get<Response>("/expectedPath");
            Assert.Pass();
        }

        [Test]
        public async Task Get_NotFoundDoesNotParse()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);

            var result = await subject.Get<Response>("/expectedPathPlus");
            Assert.IsNull(result);
        }

        private Task<HttpResponseMessage> PutMockResponse(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var expected = "{\"isAuthenticated\":false}";

            var actual = System.Text.Encoding.Default.GetString(request.Content.ReadAsByteArrayAsync().Result);
            Assert.AreEqual(expected, actual);

            if (request.RequestUri.LocalPath == "/expectedPath")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(actual, Encoding.UTF8, "application/json");
                return Task.FromResult(response);
            }
            else
            {
                var response2 = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response2.Content = new StringContent(actual, Encoding.UTF8, "application/json");
                return Task.FromResult(response2);
            }
        }

        //System.Text.Encoding.Default.GetString(request.Content.ReadAsByteArrayAsync().Result)
        [Test]
        public async Task Put_OkParses()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => PutMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);
            var content = new Response { isAuthenticated = false };
            var code = await subject.Put<Response>("/expectedPath",content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, code);
        }

        [Test]
        public async Task Put_BadRequestReturns()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => PutMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);
            var content = new Response { isAuthenticated = false };
            var code = await subject.Put<Response>("/expectedPathPlus", content);
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, code);
        }

        private Task<HttpResponseMessage> PostMockResponse(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var expectedRequest = "{\"Flag\":true}";

            var actualRequest = System.Text.Encoding.Default.GetString(request.Content.ReadAsByteArrayAsync().Result);
            Assert.AreEqual(expectedRequest, actualRequest);

            if (request.RequestUri.LocalPath == "/expectedPath")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new StringContent(GetJson(), Encoding.UTF8, "application/json");
                return Task.FromResult(response);
            }
            else
            {
                var response2 = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                //response2.Content = new StringContent(GetJson(), Encoding.UTF8, "application/json");
                return Task.FromResult(response2);
            }
        }

        private class Request
        {
            public bool Flag { get; set; }
        }
        //System.Text.Encoding.Default.GetString(request.Content.ReadAsByteArrayAsync().Result)
        [Test]
        public async Task Post_OkParses()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => PostMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);
            var content = new Request { Flag = true };
            var response = await subject.Post<Response,Request>("/expectedPath", content);

            Assert.AreEqual(true, response.isAuthenticated);
        }

        [Test]
        public async Task Post_BadRequestIsNull()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();
            httpMessageHandlerMoq.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => PostMockResponse(request, cancellationToken))
                .Verifiable();

            subject = new ConcreteBaseApiService(httpMessageHandlerMoq.Object, policyRegistry);
            var content = new Request { Flag = true };
            var response = await subject.Post<Response,Request>("/expectedPathPlus", content);
            Assert.IsNull(response);
        }

    }
}

