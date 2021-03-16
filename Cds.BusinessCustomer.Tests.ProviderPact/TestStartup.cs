using Cds.BusinessCustomer.Api.Bootstrap;
using Cds.BusinessCustomer.Tests.ProviderPact.PactSetup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cds.BusinessCustomer.Tests.ProviderPact
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment webHost)
        {
            app.UseMiddleware<ProviderStateMiddleware>();
            base.Configure(app, webHost);
        }
    }
}

