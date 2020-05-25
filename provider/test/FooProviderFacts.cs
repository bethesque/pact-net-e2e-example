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
        private readonly string _brokerBaseUri;
        private readonly ITestOutputHelper _output;

        public FooProviderFacts(ITestOutputHelper output)
        {
            _output = output;

            _brokerBaseUri = EnvironmentVariableToBool("CI") ?
                "https://test.pact.dius.com.au" :
                "http://localhost:9292";
        }

        [Fact]
        public async Task HonoursPactWithConsumer()
        {
            // Arrange
            const string serviceUri = "http://localhost:9222";
            var config = new PactVerifierConfig
            {
                ProviderVersion = "1.2.3",
                PublishVerificationResults = EnvironmentVariableToBool("PUBLISH_VERIFICATION_RESULTS"),

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
                    .PactUri("foo-bar.json")
                    .Verify();
            }
        }

        private bool EnvironmentVariableToBool(string environmentVariableName, bool defaultValue = default)
        {
            var value = Environment.GetEnvironmentVariable(environmentVariableName);
            if (string.IsNullOrWhiteSpace(value))
            {
                _output.WriteLine($"Environment variable {environmentVariableName} is undefined.");
                return defaultValue;
            }

            if (bool.TryParse(value, out bool boolValue))
            {
                _output.WriteLine($"Environment variable {environmentVariableName} is a boolean with value '{boolValue}'.");
                return boolValue;
            }

            string valueLowercase = value.ToLowerInvariant();
            if (valueLowercase == "y" || valueLowercase == "n")
            {
                _output.WriteLine($"Environment variable {environmentVariableName} is a string with value '{valueLowercase}'.");
                return valueLowercase == "y";
            }

            if (int.TryParse(value, out int intValue))
            {
                if (intValue == 0 || intValue == 1)
                {
                    _output.WriteLine($"Environment variable {environmentVariableName} is an int with value '{intValue}'.");
                    return intValue == 1;
                }
            }

            _output.WriteLine($"Environment variable {environmentVariableName} is defined with unparseable value '{intValue}'.");
            return defaultValue;
        }
    }
}
