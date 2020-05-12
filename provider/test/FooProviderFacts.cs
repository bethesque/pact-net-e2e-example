using System;
using System.Collections.Generic;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Sample.Provider.Pacts.Support;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Provider.Pacts
{
    public class FooProviderFacts
    {
        private readonly string _brokerBaseUri;
        private readonly ITestOutputHelper _output;

        public FooProviderFacts(ITestOutputHelper output)
        {
            _brokerBaseUri = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CI")) ?
                "http://localhost:9292" :
                "https://test.pact.dius.com.au";

            _output = output;
        }

        [Fact]
        public void HonoursPactWithConsumer()
        {
            // Arrange
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }
            };

            using (WebApp.Start<TestStartup>(serviceUri))
            {
                var pactVerifier = new PactVerifier(config);

                pactVerifier
                    .ProviderState($@"{serviceUri}/provider-states")
                    .ServiceProvider("Bar", serviceUri)
                    .HonoursPactWith("Foo")
                    .PactBroker(_brokerBaseUri)
                    .Verify();
            }
        }
    }
}
