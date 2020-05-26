using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Sample.Provider.Pacts.Support;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Provider.Pacts
{
    public class FooProviderFacts
    {
        private readonly ITestOutputHelper _output;

        public FooProviderFacts(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task HonoursPactWithConsumer()
        {
            // Arrange
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                ProviderVersion = "1.2.3",
                PublishVerificationResults = true,

                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                },

                Verbose = true
            };

            await using (await WebApp.Start<BarApp>(serviceUri))
            {
                var pactVerifier = new PactVerifier(config);

                pactVerifier
                    .ServiceProvider("Bar", serviceUri)
                    .HonoursPactWith("Foo")
                    .PactUri(
                        "https://test.pact.dius.com.au/pacts/provider/Bar/consumer/Foo/latest",
                        new PactUriOptions("dXfltyFMgNOFZAxr8io9wJ37iUpY42M", "O5AIZWxelWbLvqMd8PkAVycBJh2Psyg1"))
                    .Verify();
            }
        }
    }
}
