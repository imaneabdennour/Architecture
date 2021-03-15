using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace Cds.BusinessCustomer.Tests.ProviderPact.PactSetup
{
    // This class will ensure the output from Pact is displayed in the console. 
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        public XUnitOutput(ITestOutputHelper output)
        {
            _output = output;
        }

        public void WriteLine(string line)
        {
            _output.WriteLine(line);
        }
    }
}
