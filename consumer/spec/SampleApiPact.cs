using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace Sample.Consumer.Pacts
{
    public class SampleApiPact : IDisposable
    {
        private readonly int _port = new Random().Next(10000, 50000);

        public SampleApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig
            {
                SpecificationVersion = "2",
                PactDir = "spec/pacts"
            })
            .ServiceConsumer("Foo")
            .HasPactWith("Bar");

            MockProviderService = PactBuilder.MockService(_port);
        }

        public IPactBuilder PactBuilder { get; private set; }

        public IMockProviderService MockProviderService { get; private set; }

        public Uri MockProviderServiceUri => new Uri($"http://localhost:{_port}");

        public void Dispose()
        {
            // Save the Pact once finished
            PactBuilder.Build();
        }
    }
}