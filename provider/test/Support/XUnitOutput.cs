using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Sample.Provider.Pacts.Support
{
    internal class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        internal XUnitOutput(ITestOutputHelper output)
        {
            _output = output;
        }

        public void WriteLine(string line)
        {
            _output.WriteLine(line);
        }
    }
}