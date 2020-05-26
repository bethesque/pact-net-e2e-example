using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PactNet;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace Sample.Consumer.Pacts
{
    public class BarClientFacts : IClassFixture<SampleApiPact>
    {
        private readonly SampleApiPact _pact;

        public BarClientFacts(SampleApiPact pact)
        {
            _pact = pact;
            _pact.MockProviderService.ClearInteractions();
        }

        [Fact]
        public async Task Can_Retrieve_A_ThingAsync()
        {
            var barService = _pact.MockProviderService;
            barService
                .UponReceiving("a retrieve thing request")
                .With(new ProviderServiceRequest {
                    Method = HttpVerb.Get,
                    Path = "/thing",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json" }
                    },
                    Body = new
                    {
                        company = Match.Type("My big company")
                    }
                });

            // This request would normally be performed some BarClient class,
            // but just use simple request for the purposes of this test
            var barClient = new HttpClient();
            barClient.BaseAddress = _pact.MockProviderServiceUri;
            barClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var barResponse = await barClient.GetAsync("/thing");

            // This would normally be checking the results of some deserialisation process,
            // (eg. check for an array of Factory classes), but just check the response code
            // for the purposes of this test.
            Assert.Equal(HttpStatusCode.OK, barResponse.StatusCode);
            _pact.MockProviderService.VerifyInteractions();

            // Publish pact to broker
            var pactPublisher = new PactPublisher(
                "https://test.pact.dius.com.au",
                new PactUriOptions("dXfltyFMgNOFZAxr8io9wJ37iUpY42M", "O5AIZWxelWbLvqMd8PkAVycBJh2Psyg1"));
            pactPublisher.PublishToBroker(
                @"../../../spec/pacts/foo-bar.json",
                "1.0.0", new [] { "master" });
        }
    }
}
